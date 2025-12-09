using Market.Domain.Common;
using System.Collections.Generic;

namespace Market.Domain.Entities.Catalog
{
    public sealed class Zanr : BaseEntity
    {
        public string Naziv { get; set; }
        public string Opis { get; set; }

        public ICollection<KnjigaZanr> Knjige { get; set; }
    }
}
