using Market.Domain.Common;

namespace Market.Domain.Entities.Catalog
{
    public class Autor : BaseEntity
    {
        public string Ime { get; set; }
        public string Prezime { get; set; }
        public string Biografija { get; set; }
        public DateTime? DatumRodjenja { get; set; }
        public string Zemlja { get; set; }
        public string Slika { get; set; }

        public ICollection<KnjigaAutor> Knjige { get; set; }
    }
}