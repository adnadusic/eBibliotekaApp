using Market.Domain.Common;
using System.Collections.Generic;

namespace Market.Domain.Entities.Catalog
{
    public sealed class Jezik : BaseEntity
    {
        public string Naziv { get; set; }

        // VEZA sa knjigama — opcionalno
        //public ICollection<Knjiga> Knjige { get; set; }
    }
}
