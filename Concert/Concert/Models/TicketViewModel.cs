namespace Concert.Models
{
    public class TicketViewModel
    {
        public int Id { get; set; }


        public bool WasUsed { get; set; }
        public string? Document { get; set; }
        public string? Name { get; set; }

        public int? EntranceId { get; set; }

        public DateTime? Date { get; set; }
    }
}
