using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Market.Domain.Entities.Catalog
{
    public class Administrator
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public DateTime DatumZaposlenja { get; set; }
        public string NivoPristupa { get; set; }
        public DateTime? CreatedAt { get; set; }

        public Korisnik User { get; set; }

        public ICollection<Produzenje> Produzenja { get; set; }
        public ICollection<UpravljanjeKorisnicima> Upravljanja { get; set; }
    }
}
