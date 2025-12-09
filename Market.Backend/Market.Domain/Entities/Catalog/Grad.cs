using Market.Domain.Common;
using Market.Domain.Entities.Identity;
using System.Collections.Generic;

namespace Market.Domain.Entities.Catalog
{
    public sealed class Grad : BaseEntity
    {
        public string Naziv { get; set; }

        // preporučena veza — KO ŽIVI U OVOM GRADU
        // public ICollection<MarketUserEntity> Users { get; set; }
    }
}
