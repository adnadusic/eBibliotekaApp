using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Market.Domain.Entities.Catalog
{
    public class Korisnik
    {
        public int Id { get; set; }
        public string Ime { get; set; }
        public string Prezime { get; set; }
        public string Email { get; set; }
        public string Lozinka { get; set; }
        public string Telefon { get; set; }
        public string Adresa { get; set; }
        public DateTime DatumRegistracije { get; set; }
        public DateTime? DatumRodjenja { get; set; }
        public string Status { get; set; }
        public int? BrojUpozorenja { get; set; }
        public decimal? UkupnoKazni { get; set; }
        public DateTime? CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }

        public ICollection<Kazna> Kazne { get; set; }
        public ICollection<Obavijest> Obavijesti { get; set; }
        public ICollection<ListaZelja> ListaZelja { get; set; }
        public ICollection<OcjenaRecenzije> OcjeneRecenzija { get; set; }
        public ICollection<Posudba> Posudbe { get; set; }
        public ICollection<Recenzija> Recenzije { get; set; }
        public ICollection<Rezervacija> Rezervacije { get; set; }

        public Administrator Administrator { get; set; }
    }
}
