using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Market.Domain.Entities.Catalog
{
    public class Obavijest
    {
        public int NotificationId { get; set; }
        public int UserId { get; set; }
        public string Tip { get; set; }
        public string Naslov { get; set; }
        public string Poruka { get; set; }
        public DateTime DatumSlanja { get; set; }
        public bool? Procitano { get; set; }
        public DateTime? DatumCitanja { get; set; }
        public int? VezanoZaId { get; set; }
        public string VezanoZaTip { get; set; }
        public DateTime? CreatedAt { get; set; }

        public Korisnik User { get; set; }
    }
}
