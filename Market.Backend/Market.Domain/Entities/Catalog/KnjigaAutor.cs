using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Market.Domain.Entities.Catalog
{
    public class KnjigaAutor
    {
        public int Id { get; set; }

        public int BookId { get; set; }
        public int AuthorId { get; set; }

        public string TipDoprinosa { get; set; }

        public Knjiga Knjiga { get; set; }
        public Autor Autor { get; set; }
    }
}
