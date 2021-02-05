using AutoMapper;
using Task3.API.DtoRes;
using Task3.Core.Models;

namespace Task3.API.Mapping
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<MaterialVersion, MaterialVersionResultDto>();
            CreateMap<Material, MaterialResultDto>();

            CreateMap<MaterialVersionResultDto, MaterialVersion>();
            CreateMap<MaterialResultDto, Material>();
        }
    }
}