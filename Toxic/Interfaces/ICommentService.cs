using AutoMapper;
using Toxic.DTOs;
using Toxic.Models;

namespace Toxic.Interfaces
{
    public interface ICommentService
    {
        Task<Comment> DeleteACommentAsync(int id);
        Task<Comment> GetACommentAsync(int id);
        Task<List<Comment>> GetCommentsInTopicAsync(int topicId);
        Task<Comment> UpdateCommentAsync(int id, IMapper mapper, UpdateCommentDTO updateDTO);
        Task<Comment> CreateCommentAsync(IMapper mapper, CreateCommentDTO createDTO);

    }
}
