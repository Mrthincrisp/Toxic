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
            CreateMap<Topic, CreateTopicDTO>().ReverseMap();
            CreateMap<Topic,  UpdateTopicDTO>().ReverseMap();
            CreateMap<Comment, UpdateCommentDTO>().ReverseMap();
            CreateMap<Comment, CreateCommentDTO>().ReverseMap();
        }
    }
}
