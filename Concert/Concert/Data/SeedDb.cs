using Concert.Data.Entities;

namespace Concert.Data
{
    public class SeedDb
    {
        private readonly DataContext _context;


        public SeedDb(DataContext context)
        {
            _context = context;
        }

        public async Task SeedAsync()
        {
            await _context.Database.EnsureCreatedAsync();
            await CheckTicketsAsync();
            await CheckEntranceAsync();
        }



        private async Task CheckTicketsAsync()
        {

            if (!_context.Tickets.Any())
            {
                for (int i = 0; i < 5000; i++)
                {
                    _context.Tickets.Add(new Ticket
                    {

                        WasUsed = false,
                        Document = null,
                        Name = null,
                        Entrance = null,
                        Date = null,

                    });
                }
                await _context.SaveChangesAsync();
            }

            _context.Tickets.Add(new Ticket
            {

                WasUsed = true,
                Document = "1234",
                Name = "Julio",
                Entrance = null,
                Date = DateTime.Now,

            });

            await _context.SaveChangesAsync();


        }


        private async Task CheckEntranceAsync()
        {
            if (!_context.Entrances.Any())
            {
                _context.Entrances.Add(new Entrance { Description = "Norte" });
                _context.Entrances.Add(new Entrance { Description = "Sur" });
                _context.Entrances.Add(new Entrance { Description = "Occidental" });
                _context.Entrances.Add(new Entrance { Description = "Oriental" });



                await _context.SaveChangesAsync();
            }
        }

    }
}