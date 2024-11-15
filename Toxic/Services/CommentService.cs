using AutoMapper;
using System.Security.Cryptography.X509Certificates;
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

        public async Task<Comment> CreateComment(IMapper mapper, CreateCommentDTO createDTO)
        {
            return await _commentRepo.CreateComment(mapper, createDTO);   
        }

        public async Task<Comment> DeleteAComment(int id)
        {
            return await _commentRepo.DeleteAComment(id);
        }

        public async Task<Comment> GetAComment(int id)
        {
            return await _commentRepo.GetAComment(id);
        }

        public async Task<List<Comment>> GetCommentsInTopic(int topicId)
        {
            return await _commentRepo.GetCommentsInTopic(topicId);
        }

        public async Task<Comment> UpdateComment(int id, IMapper mapper, UpdateCommentDTO updateDTO)
        {
            return await _commentRepo.UpdateComment(id, mapper, updateDTO);
        }
    }
}
