using Market.Domain.Common;

namespace Market.Domain.Entities.Catalog
{
    public class SistemskePostavke : BaseEntity
    {
        public string Naziv { get; set; }
        public string Vrijednost { get; set; }
        public string Tip { get; set; }
        public string Opis { get; set; }
        public DateTime? Izmijenjeno { get; set; }
    }
}