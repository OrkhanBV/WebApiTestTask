using AutoMapper;
using Task3.API.Resources;
using Task3.Core.Models;

namespace Task3.API.Mapping
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<MaterialVersion, MaterialVersionResources>();
            CreateMap<Material, MaterialResultDto>();

            CreateMap<MaterialVersionResources, MaterialVersion>();
            CreateMap<MaterialResultDto, Material>();
        }
    }
}