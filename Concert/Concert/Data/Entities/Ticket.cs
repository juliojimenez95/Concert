namespace Concert.Data.Entities
{
    public class Ticket
    {
        public int Id { get; set; }


        public bool WasUsed { get; set; }

        public string? Document { get; set; }

        public string? Name { get; set; }
        public Entrance? Entrance { get; set; }

        public DateTime? Date { get; set; }

    }
}
