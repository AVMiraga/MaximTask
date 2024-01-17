using MaximTask.Core.Entities.Common;
using Microsoft.EntityFrameworkCore;

namespace MaximTask.DAL.Repositories.Interfaces
{
    public interface IRepository<T> where T : BaseEntity, new()
    {
        public DbSet<T> Table { get; }
        Task<T> GetByIdAsync(int id);
        Task<IEnumerable<T>> GetAllAsync();
        void Delete(T entity);
        void Update(T entity);
        Task CreateAsync(T entity);

        Task<int> SaveChangesAsync();
    }
}
