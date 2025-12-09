using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Market.Domain.Entities.Catalog
{
    public class Produzenje
    {
        public int Id { get; set; }
        public int BorrowId { get; set; }
        public DateTime DatumZahtjeva { get; set; }
        public DateTime NoviDatumVracanja { get; set; }
        public string Razlog { get; set; }
        public string Status { get; set; }
        public int? AdminId { get; set; }
        public DateTime? DatumObrade { get; set; }
        public string Napomena { get; set; }
        public DateTime? CreatedAt { get; set; }

        public Posudba Borrow { get; set; }
        public Administrator Admin { get; set; }
    }
}
