using AutoMapper;
using Toxic.DTOs;
using Toxic.Interfaces;
using Toxic.Models;

namespace Toxic.Services
{
    public class TopicService : ITopicService
    {
        private readonly ITopicRepository _repository;

        public TopicService(ITopicRepository topicRepo)
        {
            _repository = topicRepo;
        }

        public async Task<Topic> CreateTopicAsync(IMapper mapper, CreateTopicDTO createDTO)
        {
            return await _repository.CreateTopicAsync(mapper, createDTO);
        }

        public async Task<Topic> DeleteTopicAsync(int id)
        {
            return await _repository.DeleteTopicAsync(id);
        }

        public async Task<List<Topic>> GetAllCategoryTopicsAsync(int categoryid)
        {
            return await _repository.GetAllCategoryTopicsAsync(categoryid);
        }

        public async Task<Topic> GetTopicByIdAsync(int Id)
        {
            return await _repository.GetTopicByIdAsync(Id); 
        }

        public async Task<Topic> UpdateTopicAsync(int id, IMapper mapper, UpdateTopicDTO updateDTO)
        {
            return await _repository.UpdateTopicAsync(id, mapper, updateDTO);
        }
    }
}
