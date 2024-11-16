using AutoMapper;
using Toxic.DTOs;
using Toxic.Interfaces;
using Toxic.Models;

namespace Toxic.Services
{
    public class CommentService : ICommentService
    {
        private readonly ICommentRepository _commentRepo;

        public CommentService(ICommentRepository commentRepo)
        {
            _commentRepo = commentRepo;
        }

        public async Task<Comment> CreateCommentAsync(IMapper mapper, CreateCommentDTO createDTO)
        {
            return await _commentRepo.CreateCommentAsync(mapper, createDTO);   
        }

        public async Task<Comment> DeleteACommentAsync(int id)
        {
            return await _commentRepo.DeleteACommentAsync(id);
        }

        public async Task<Comment> GetACommentAsync(int id)
        {
            return await _commentRepo.GetACommentAsync(id);
        }

        public async Task<List<Comment>> GetCommentsInTopicAsync(int topicId)
        {
            return await _commentRepo.GetCommentsInTopicAsync(topicId);
        }

        public async Task<Comment> UpdateCommentAsync(int id, IMapper mapper, UpdateCommentDTO updateDTO)
        {
            return await _commentRepo.UpdateCommentAsync(id, mapper, updateDTO);
        }
    }
}
