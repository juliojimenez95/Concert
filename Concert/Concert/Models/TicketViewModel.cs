using Microsoft.AspNetCore.Mvc.Rendering;

namespace Concert.Models
{
    public class TicketViewModel: EditTicketViewModel
    {
        public int EntranceId { get; set; }

        public IEnumerable<SelectListItem> Entrances { get; set; }
    }
}
