using Market.Domain.Common;

namespace Market.Domain.Entities.Catalog;

public class Language : BaseEntity
{
    public string Name { get; set; }
    public string Code { get; set; }
}