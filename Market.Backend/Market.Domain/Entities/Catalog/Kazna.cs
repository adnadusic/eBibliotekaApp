using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Market.Domain.Entities.Catalog
{
    public class Kazna
    {
        public int Id { get; set; }
        public int? BorrowId { get; set; }
        public int UserId { get; set; }
        public string TipKazne { get; set; }
        public decimal Iznos { get; set; }
        public DateTime DatumNastanka { get; set; }
        public DateTime? DatumPlacanja { get; set; }
        public string Status { get; set; }
        public string Opis { get; set; }
        public int? BrojDanaKasnjenja { get; set; }
        public DateTime? CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }

        public Posudba Borrow { get; set; }
        public Korisnik User { get; set; }
    }
}
