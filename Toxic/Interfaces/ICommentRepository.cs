using Toxic.Models;
using AutoMapper;
using Toxic.DTOs;

namespace Toxic.Interfaces
{
    public interface ICommentRepository
    {

        Task <Comment> DeleteACommentAsync(int id);
        Task <Comment> GetACommentAsync(int id);
        Task <List<Comment>> GetCommentsInTopicAsync(int topicId);
        Task<Comment> UpdateCommentAsync(int id, IMapper mapper, UpdateCommentDTO updateDTO);
        Task<Comment> CreateCommentAsync(IMapper mapper, CreateCommentDTO createDTO);


    }
}
