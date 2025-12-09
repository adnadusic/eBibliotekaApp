using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Market.Domain.Entities.Catalog
{
    public class UpravljanjeKorisnicima
    {
        public int Id { get; set; }
        public int AdminId { get; set; }
        public int TargetUserId { get; set; }
        public string TipAkcije { get; set; }
        public string Razlog { get; set; }
        public DateTime Datum { get; set; }
        public int? TrajanjeDana { get; set; }
        public string DodatneNapomene { get; set; }
        public DateTime? CreatedAt { get; set; }

        public Administrator Admin { get; set; }
        public Korisnik TargetUser { get; set; }
    }
}
