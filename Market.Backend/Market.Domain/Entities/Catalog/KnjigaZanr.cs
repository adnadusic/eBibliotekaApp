using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Market.Domain.Entities.Catalog
{
    public class KnjigaZanr
    {
        public int Id { get; set; }

        public int BookId { get; set; }
        public int GenreId { get; set; }

        public Knjiga Knjiga { get; set; }
        public Zanr Zanr { get; set; }
    }
}
