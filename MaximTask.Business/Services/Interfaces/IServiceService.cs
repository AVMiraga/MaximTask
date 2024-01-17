using MaximTask.Business.ViewModel;
using MaximTask.Core.Entities;

namespace MaximTask.Business.Services.Interfaces
{
    public interface IServiceService
    {
        Task CreateAsync(CreateServiceVm vm);
        Task UpdateAsync(UpdateServiceVm vm);
        Task DeleteAsync(int id);
        Task<Service> GetById(int id);
        Task<IEnumerable<Service>> GetAllAsync();
    }
}
