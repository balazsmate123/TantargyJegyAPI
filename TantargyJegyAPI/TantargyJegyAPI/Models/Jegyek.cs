namespace TantargyJegyAPI.Models
{
    public class Jegyek
    {
        public Guid Id { get; set; }
        public int Jegy { get; set; }
        public string Leiras { get; set; }
        public DateTimeOffset Created { get; set; }

    }
}
