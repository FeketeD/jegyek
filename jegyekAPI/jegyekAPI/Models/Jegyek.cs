namespace jegyekAPI.Models
{
    public class Jegyek
    {
        public Guid Id { get; set; }
        public string Jegy { get; set; }
        public string Desc { get; set; }
        public DateTimeOffset Created { get; set; }
    }
}
