using AutoMapper;
using MaximTask.Business.Services.Interfaces;
using MaximTask.Business.ViewModel;
using MaximTask.Core.Entities;
using MaximTask.DAL.Repositories.Interfaces;

namespace MaximTask.Business.Services.Implementation
{
    public class ServiceService : IServiceService
    {
        private readonly IServiceRepository _repo;
        private readonly IMapper _mapper;

        public ServiceService(IServiceRepository repo, IMapper mapper)
        {
            _repo = repo;
            _mapper = mapper;
        }

        public async Task CreateAsync(CreateServiceVm vm)
        {
            Service service = _mapper.Map<Service>(vm);

            await _repo.CreateAsync(service);
            await _repo.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var entity = await _repo.GetByIdAsync(id);

            _repo.Delete(entity);
            await _repo.SaveChangesAsync();
        }

        public async Task<IEnumerable<Service>> GetAllAsync()
        {
            return await _repo.GetAllAsync();
        }

        public async Task<Service> GetById(int id)
        {
            return await _repo.GetByIdAsync(id);
        }

        public async Task UpdateAsync(UpdateServiceVm vm)
        {
            Service service = await _repo.GetByIdAsync(vm.Id);
            
            service = _mapper.Map<UpdateServiceVm, Service>(vm);

            _repo.Update(service);
            await _repo.SaveChangesAsync();
        }
    }
}
