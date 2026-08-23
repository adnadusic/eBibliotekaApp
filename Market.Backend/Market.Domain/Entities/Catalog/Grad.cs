using Market.Domain.Common;

namespace Market.Domain.Entities.Catalog;

public class Grad : BaseEntity
{
    public string Naziv { get; set; }
    public string PostanskiBroj { get; set; }
}