using Market.Domain.Common;
using Market.Domain.Enums;

namespace Market.Domain.Entities.Catalog
{
    public class Primjerak : BaseEntity
    {
        public int BookId { get; set; }
        public string InventarniBroj { get; set; }
        public string Stanje { get; set; }
        public string LokacijaPolice { get; set; }
        public BookCopyStatus Status { get; set; }
        public DateTime? DatumNabavke { get; set; }
        public string Napomena { get; set; }

        public Knjiga Book { get; set; }
        public ICollection<Posudba> Posudbe { get; set; }
    }
}