using AutoMapper;
using MaximTask.Business.ViewModel;
using MaximTask.Core.Entities;

namespace MaximTask.Business.MapperProfiles
{
    public class DefaultMapperProfile : Profile
    {
        public DefaultMapperProfile() 
        {
            CreateMap<CreateServiceVm, Service>();

            CreateMap<UpdateServiceVm, Service>();
            CreateMap<UpdateServiceVm, Service>().ReverseMap();
        }
    }
}
