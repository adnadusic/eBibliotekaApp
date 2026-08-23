using Market.Domain.Common;

namespace Market.Domain.Entities.Catalog
{
    public class Zanr : BaseEntity
    {
        public string Naziv { get; set; }
        public string Opis { get; set; }

        public ICollection<KnjigaZanr> Knjige { get; set; }
    }
}