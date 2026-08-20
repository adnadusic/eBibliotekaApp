using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Market.Domain.Entities.Catalog
{
    public class SistemskePostavke
    {
        public int Id { get; set; }
        public string Naziv { get; set; }
        public string Vrijednost { get; set; }
        public string Tip { get; set; }
        public string Opis { get; set; }
        public DateTime? Izmijenjeno { get; set; }
        public DateTime? CreatedAt { get; set; }
    }
}
