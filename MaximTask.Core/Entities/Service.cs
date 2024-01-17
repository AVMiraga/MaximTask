using MaximTask.Core.Entities.Common;

namespace MaximTask.Core.Entities
{
    public class Service : BaseEntity
    {
        public string IconCode { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
    }
}
