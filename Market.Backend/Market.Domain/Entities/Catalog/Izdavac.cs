using Market.Domain.Common;
using System.Collections.Generic;

namespace Market.Domain.Entities.Catalog
{
    public sealed class Izdavac : BaseEntity
    {
        public string Naziv { get; set; }
        public string Adresa { get; set; }
        public string Grad { get; set; }
        public string Drzava { get; set; }
        public string WebStranica { get; set; }

        public ICollection<Knjiga> Knjige { get; set; }
    }
}
