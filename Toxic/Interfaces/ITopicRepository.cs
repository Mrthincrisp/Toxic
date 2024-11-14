using AutoMapper;
using Toxic.DTOs;
using Toxic.Models;

namespace Toxic.Interfaces
{
    public interface ITopicRepository
    {

        Task<List<Topic>> GetAllCategoryTopicsAsync(int categoryid);
        Task<Topic> GetTopicByIdAsync(int Id);
        Task<Topic> CreateTopicAsync(IMapper mapper, CreateTopicDTO createDTO);
        Task<Topic> UpdateTopicAsync(int id, IMapper mapper, UpdateTopicDTO updateDTO);
        Task<Topic> DeleteTopicAsync(int id);

    }
}
