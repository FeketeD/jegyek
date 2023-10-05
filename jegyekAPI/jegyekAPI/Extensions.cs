using jegyekAPI.Models;
using static jegyekAPI.Dtos;

namespace jegyekAPI
{
    public static class Extensions
    {
        public static JegyekDto AsDto(this Jegyek jegyek)
        {
            return new JegyekDto(jegyek.Id, jegyek.Jegy, jegyek.Desc, jegyek.Created);
        }
    }
}
