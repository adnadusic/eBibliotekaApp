using Market.Domain.Common;

namespace Market.Domain.Entities.Catalog
{
    public class Izdavac : BaseEntity
    {
        public string Naziv { get; set; }
        public string Adresa { get; set; }
        public string Email { get; set; }
        public string Telefon { get; set; }
        public string Website { get; set; }

        public ICollection<Knjiga> Knjige { get; set; }
    }
}