namespace jegyekAPI
{
    public class Dtos
    {
        public record JegyekDto(Guid Id, int Jegy, string Desc, DateTimeOffset Created);
        public record CreateJegyekDto(int Jegy, string Desc, DateTimeOffset Created);
        public record UpdateJegyekDto(int Jegy, string Desc);
    }
}
