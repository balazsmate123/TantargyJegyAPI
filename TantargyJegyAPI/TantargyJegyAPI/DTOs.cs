namespace TantargyJegyAPI
{
    public class DTOs
    {
        public record JegyekDTO(Guid Id, int Jegy, string Leiras, DateTimeOffset Created);
        public record CreateJegyekDto(int Jegy, string Leiras);
        public record UpdateJegyekDto(int Jegy, string Leiras);
    }
}
