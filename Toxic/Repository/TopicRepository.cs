using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Toxic.DTOs;
using Toxic.Interfaces;
using Toxic.Models;

namespace Toxic.Repository
{
    public class TopicRepository : ITopicRepository
    {
        public readonly ToxicDbContext _context;
        
        public TopicRepository(ToxicDbContext context) 
        {
            _context = context; 
        }

        public async Task<Topic> CreateTopicAsync(IMapper mapper, CreateTopicDTO createDTO)
        {
            var topic = mapper.Map<Topic>(createDTO);

            try
            {
                _context.Topics.Add(topic);
                await _context.SaveChangesAsync();
                return topic;
            }
            catch (DbUpdateException)
            {
                return null;
            }
        }

        public async Task<Topic> DeleteTopicAsync(int id)
        {
            var deletedTopic = await _context.Topics.FindAsync(id);

            _context.Topics.Remove(deletedTopic);
            await _context.SaveChangesAsync();
            return deletedTopic;
        }

        public async Task<List<Topic>> GetAllCategoryTopicsAsync(int categoryid)
        {
            return await _context.Topics
                .Where(t => t.CategoryId == categoryid)
                .ToListAsync();
        }

        public async Task<Topic> GetTopicByIdAsync(int id)
        {
            var topic = await _context.Topics
                .Include(t => t.Comments)
                .SingleOrDefaultAsync(t => t.Id == id);
                

            return topic;
        }

        public async Task<Topic> UpdateTopicAsync(int id, IMapper mapper, UpdateTopicDTO updateDTO)
        {
            var topicToUpdate = await _context.Topics.FindAsync(id);

            mapper.Map(updateDTO, topicToUpdate);
            try
            {
                await _context.SaveChangesAsync();
                return topicToUpdate;
            }
            catch (DbUpdateException)
            {
                return null;
            }
        }
    }
}
