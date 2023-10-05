using TantargyJegyAPI.Models;
using static TantargyJegyAPI.DTOs;
namespace TantargyJegyAPI

{
    public static class Extensions
    {
        public static JegyekDTO AsDto(this Jegyek jegyek)
        {
            return new JegyekDTO(jegyek.Id, jegyek.Jegy, jegyek.Leiras, jegyek.Created);
        }
    }
}
