using AutoMapper;
using Toxic.Models;
using Toxic.DTOs;
namespace Toxic.Mapper
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Category, UpsertCategoryDTO>().ReverseMap();
        }
    }
}
