using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Market.Domain.Entities.Catalog
{
    public class OcjenaRecenzije
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public int ReviewId { get; set; }
        public string TipOcjene { get; set; }
        public DateTime Datum { get; set; }

        public Korisnik User { get; set; }
        public Recenzija Review { get; set; }
    }
}
