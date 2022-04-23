using Concert.Data;
using Concert.Data.Entities;
using Concert.Helpers;
using Concert.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Concert.Controllers
{
    public class AccessController : Controller
    {
        private readonly DataContext _context;
        private readonly ICombosHelper _combosHelper;

        public AccessController(DataContext context, ICombosHelper combosHelper)
        {
            _context = context;
            _combosHelper = combosHelper;
        }
        public async Task<IActionResult> Index()
        {
            return View(await _context.Tickets.ToListAsync());
        }

        public IActionResult CheckTicket()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CheckTicket(int? id)
        {
            if (id == null)
            {
                return NotFound();

            }

            Ticket ticket = await _context.Tickets
                .FirstOrDefaultAsync(t => t.Id == id);


            if (id < 0 || id > 5003)
            {
                TempData["Message"] = "Error de boleta, no existe";

                return RedirectToAction(nameof(CheckTicket));

            }

            if (ticket.WasUsed != false)
            {
                TempData["Message"] = "Ticket ya es usado.";
                TempData["Name"] = ticket.Name;
                TempData["Document"] = ticket.Document;
                TempData["Date"] = ticket.Date;
                return RedirectToAction(nameof(CheckTicket), new { Id = ticket.Id });
            }
            else
            {
                TempData["Message"] = "Ticket no ha sido usada.";
                return RedirectToAction(nameof(Register), new { Id = ticket.Id });
            }


        }


        public async Task<IActionResult> Register(int? id)
        {
            Ticket ticket = await _context.Tickets.FindAsync(id);
            TicketViewModel model = new()
            {
                Document = ticket.Document,
                Id = ticket.Id,
                Name = ticket.Name,
                Date = DateTime.Now,
                WasUsed = true,
                Entrances = await _combosHelper.GetComboEntrancesAsync(),
            };

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(int id, TicketViewModel model)
        {
            if (id != model.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    Ticket ticket = await _context.Tickets.FindAsync(model.Id);
                    ticket.Document = model.Document;
                    ticket.Name = model.Name;
                    ticket.Date = DateTime.Now;
                    ticket.WasUsed = true;
                    ticket.Entrance = await _context.Entrances.FindAsync(model.EntranceId);
                    _context.Update(ticket);
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
                }
                catch (Exception exception)
                {
                    ModelState.AddModelError(string.Empty, exception.Message);
                }
            }

            model.Entrances = await _combosHelper.GetComboEntrancesAsync();
            return View(model);
        }

    }

}
