using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Market.Domain.Entities.Catalog
{
    public class Posudba
    {
        public int Id { get; set; }
        public int? ReservationId { get; set; }
        public int CopyId { get; set; }
        public int UserId { get; set; }
        public DateTime DatumPosudbe { get; set; }
        public DateTime PlaniraniDatumVracanja { get; set; }
        public DateTime? DatumVracanja { get; set; }
        public bool? Produzena { get; set; }
        public int? BrojProduzenja { get; set; }
        public string StanjePriPreuzimanju { get; set; }
        public string StanjePriVracanju { get; set; }
        public string Napomena { get; set; }
        public DateTime? CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }

        public Primjerak Copy { get; set; }
        public Korisnik User { get; set; }
        public Rezervacija Reservation { get; set; }

        public ICollection<Kazna> Kazne { get; set; }
        public ICollection<Produzenje> Produzenja { get; set; }

    }
}
