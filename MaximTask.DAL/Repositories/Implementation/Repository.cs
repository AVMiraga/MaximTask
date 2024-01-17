using MaximTask.Core.Entities.Common;
using MaximTask.DAL.Context;
using MaximTask.DAL.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace MaximTask.DAL.Repositories.Implementation
{
    public class Repository<T> : IRepository<T> where T : BaseEntity, new()
    {
        private readonly AppDbContext _context;

        public Repository(AppDbContext context)
        {
            _context = context;
        }

        public DbSet<T> Table => _context.Set<T>();

        public async Task CreateAsync(T entity)
        {
            await Table.AddAsync(entity);
        }

        public void Delete(T entity)
        {
            entity.IsDeleted = true;
            
            Table.Update(entity);
        }

        public async Task<IEnumerable<T>> GetAllAsync()
        {
            IQueryable<T> entites = Table.Where(e => !e.IsDeleted);

            return await entites.ToListAsync();
        }

        public async Task<T> GetByIdAsync(int id)
        {
            return await Table.AsNoTracking().Where(e => !e.IsDeleted && e.Id == id).FirstOrDefaultAsync();
        }

        public async Task<int> SaveChangesAsync()
        {
            return await _context.SaveChangesAsync();
        }

        public void Update(T entity)
        {
            Table.Update(entity);
        }
    }
}
