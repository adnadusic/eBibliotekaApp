using Market.Domain.Common;

namespace Market.Domain.Entities.Catalog
{
    public class SystemSetting : BaseEntity
    {
        public string Name { get; set; }
        public string Value { get; set; }
        public string Type { get; set; }
        public string Description { get; set; }
        public DateTime? ModifiedAt { get; set; }
    }
}