using MaximTask.Core.Entities;
using MaximTask.DAL.Context;
using MaximTask.DAL.Repositories.Interfaces;

namespace MaximTask.DAL.Repositories.Implementation
{
    public class ServiceRepository : Repository<Service>, IServiceRepository
    {
        public ServiceRepository(AppDbContext context) : base(context)
        {
        }
    }
}
