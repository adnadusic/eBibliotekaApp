using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Market.Domain.Entities.Catalog
{
    public class Primjerak
    {
        public int Id { get; set; }
        public int BookId { get; set; }
        public string InventarniBroj { get; set; }
        public string Stanje { get; set; }
        public string LokacijaPolice { get; set; }
        public string Status { get; set; }
        public DateTime? DatumNabavke { get; set; }
        public string Napomena { get; set; }
        public DateTime? CreatedAt { get; set; }

        public Knjiga Book { get; set; }
        public ICollection<Posudba> Posudbe { get; set; }
    }
}
