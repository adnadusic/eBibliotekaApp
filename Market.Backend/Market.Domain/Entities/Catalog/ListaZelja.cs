using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Market.Domain.Entities.Catalog
{
    public class ListaZelja
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public int BookId { get; set; }
        public DateTime DatumDodavanja { get; set; }
        public int? Prioritet { get; set; }
        public string Napomena { get; set; }

        public Korisnik User { get; set; }
        public Knjiga Book { get; set; }
    }
}
