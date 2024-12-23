﻿using AutoMapper;
using Toxic.Models;
using Toxic.DTOs;
namespace Toxic.Mapper
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Category, UpsertCategoryDTO>().ReverseMap();

            CreateMap<Chat, CreateChatDTO>().ReverseMap();

            CreateMap<Topic, CreateTopicDTO>().ReverseMap();
            CreateMap<Topic,  UpdateTopicDTO>().ReverseMap();

            CreateMap<Message, CreateMessageDTO>().ReverseMap();
            CreateMap<Message,  UpdateMessageDTO>().ReverseMap();

            CreateMap<Comment, UpdateCommentDTO>().ReverseMap();
            CreateMap<Comment, CreateCommentDTO>().ReverseMap();

            CreateMap<User, CreateUserDTO>().ReverseMap();
            CreateMap<User, UpdateUserDTO>().ReverseMap();
        }
    }
}
