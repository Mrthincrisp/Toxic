using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Moq;
using AutoMapper;
using Toxic.Mapper;
using Toxic.DTOs;
using Toxic.Interfaces;
using Toxic.Models;
using Toxic.Services;

namespace Toxic.Tests.CallTests
{
    public class CommentTests
    {
        private readonly Mock<ICommentRepository> _mockCommentRepo;
        private readonly ICommentService _commentService;
        private readonly IMapper _mapper;

        public CommentTests()
        {
            var mappingConfig = new MapperConfiguration(mc =>
            {
                mc.AddProfile(new MappingProfile());
            });
            _mapper = mappingConfig.CreateMapper();

            _mockCommentRepo = new Mock<ICommentRepository>();
            _commentService = new CommentService(_mockCommentRepo.Object);
        }

        [Fact] //Create a comment           
        public async Task CreateAComment_WhenCalled_ReturnsNewComment()
        {
            var commentDTO = new CreateCommentDTO
            {
                Content = "test"
            };

            var comment = _mapper.Map<Comment>(commentDTO);

            _mockCommentRepo.Setup(x => x.CreateCommentAsync(_mapper, commentDTO)).ReturnsAsync(comment);

            var result = await _commentService.CreateCommentAsync(_mapper, commentDTO);

            Assert.NotNull(result);
            Assert.Equal(commentDTO.Content, result.Content);

        }

        [Fact] //Delete a comment
        public async Task DeleteAComment_WhenCalled_ReturnsDeletedComment()
        {
            var comment = new Comment
            {
                Id = 1
            };

            _mockCommentRepo.Setup(x => x.DeleteACommentAsync(comment.Id)).ReturnsAsync(comment);

            await _commentService.DeleteACommentAsync(comment.Id);
            
            _mockCommentRepo.Verify(x => x.DeleteACommentAsync(comment.Id), Times.Once());

            _mockCommentRepo.Setup(x => x.GetACommentAsync(comment.Id)).ReturnsAsync((Comment)null);
        }

        [Fact] //Get a single comment
        public async Task GetASingleComment_WhenCalled_ReturnsSingleComment()
        {
            var comment = new Comment
            {
                Id = 1,
            };

            _mockCommentRepo.Setup(x => x.GetACommentAsync(comment.Id)).ReturnsAsync(comment);

            var results = await _commentService.GetACommentAsync(comment.Id);

            Assert.NotNull(results);
            Assert.Equal(comment.Id, results.Id);
        }

        [Fact] //Get comments in a Topic
        public async Task GetCommentsInATopic_WhenCalled_ReturnSpecificComments()
        {
            int topicId = 1;

            var comments = new List<Comment>
            {
                new Comment { Id = 1, TopicId = 1 },
                new Comment { Id = 2, TopicId = 1 },
                new Comment { Id = 3, TopicId = 2 }
            };

            var filterComments = comments.Where(c => c.TopicId == topicId).ToList();

            _mockCommentRepo.Setup(x => x.GetCommentsInTopicAsync(topicId)).ReturnsAsync(filterComments);

            var results = await _commentService.GetCommentsInTopicAsync(topicId);

            Assert.NotNull(results);
            Assert.Equal(2, results.Count());
            Assert.Contains(results, t => t.Id == 1);
            Assert.Contains(results, t => t.Id == 2);
        }

        [Fact] //Update a comment
        public async Task UpdateComment_WhenCalled_ReturnUpdatedComment()
            {
                int id = 1;

                var commentDTO = new UpdateCommentDTO
                {
                    Content = "test"
                };

                var comment = _mapper.Map<Comment>(commentDTO);

                _mockCommentRepo.Setup(x => x.GetACommentAsync(id)).ReturnsAsync(comment);
                _mockCommentRepo.Setup(x => x.UpdateCommentAsync(id, _mapper, commentDTO)).ReturnsAsync(comment);

                var result = await _commentService.UpdateCommentAsync(id, _mapper, commentDTO);

                Assert.NotNull(result);
                Assert.Equal(comment.Content, result.Content);
            }
        
    }
}
