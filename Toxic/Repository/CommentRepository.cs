using AutoMapper;
using Microsoft.EntityFrameworkCore;
using System.Data.Common;
using Toxic.DTOs;
using Toxic.Interfaces;
using Toxic.Models;

namespace Toxic.Repository
{
    public class CommentRepository : ICommentRepository
    {
        private readonly ToxicDbContext _context;

        public CommentRepository(ToxicDbContext context)
        { 
            _context = context;
        }

        //Create comment
        public async Task<Comment> CreateCommentAsync(IMapper mapper, CreateCommentDTO createDTO)
        {
           var createdComment = mapper.Map<Comment>(createDTO);

            try
            {
                _context.Add(createdComment);
                await _context.SaveChangesAsync();
                return createdComment;
            }
            catch (DbUpdateException)
            {
                return null;
            }
        }

        //Delete comment
        public async Task<Comment> DeleteACommentAsync(int id)
        {
            var deletedComment = await _context.Comments.FindAsync(id);

            _context.Comments.Remove(deletedComment);
            await _context.SaveChangesAsync();
            return deletedComment;
        }

        //Get a comment
        public async Task<Comment> GetACommentAsync(int id)
        {
            return await _context.Comments.FindAsync(id);

        }

        //Get comments of a topic
        public async Task<List<Comment>> GetCommentsInTopicAsync(int topicId)
        {
            return await _context.Comments
                 .Where(c => c.TopicId == topicId)
                 .ToListAsync();
        }

        //Update comment
        public async Task<Comment> UpdateCommentAsync(int id, IMapper mapper, UpdateCommentDTO updateDTO)
        {
            var updatedComment = await _context.Comments.FindAsync(id);

            mapper.Map(updateDTO, updatedComment);

            try
            {                
                await _context.SaveChangesAsync();
                return updatedComment;
            }
            catch (DbUpdateException)
            {
                return null;
            }
        }
    }
}
