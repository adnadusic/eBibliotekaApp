using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Market.Domain.Entities.Catalog
{
    public class Recenzija
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public int BookId { get; set; }
        public int Ocjena { get; set; }
        public string Naslov { get; set; }
        public string Komentar { get; set; }
        public DateTime Datum { get; set; }
        public int? BrojHelpful { get; set; }
        public int? BrojUnhelpful { get; set; }
        public bool? Izmijenjeno { get; set; }
        public DateTime? DatumIzmjene { get; set; }
        public DateTime? CreatedAt { get; set; }

        public Korisnik User { get; set; }
        public Knjiga Book { get; set; }

        public ICollection<OcjenaRecenzije> Ocjene { get; set; }
    }
}
