using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Market.Domain.Entities.Catalog
{
    public class Autor
    {
        public int Id { get; set; }
        public string Ime { get; set; }
        public string Prezime { get; set; }
        public string Biografija { get; set; }
        public DateTime? DatumRodjenja { get; set; }
        public string Zemlja { get; set; }
        public string Slika { get; set; }
        public DateTime? CreatedAt { get; set; }

        public ICollection<KnjigaAutor> Knjige { get; set; }
    }
}
