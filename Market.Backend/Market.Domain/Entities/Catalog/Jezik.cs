using Market.Domain.Common;

namespace Market.Domain.Entities.Catalog;

public class Jezik : BaseEntity
{
    public string Naziv { get; set; }
    public string Kod { get; set; }
}