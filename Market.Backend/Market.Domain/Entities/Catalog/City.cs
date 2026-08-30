using Market.Domain.Common;

namespace Market.Domain.Entities.Catalog;

public class City : BaseEntity
{
    public string Name { get; set; }
    public string PostalCode { get; set; }
}