using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Moq;
using AutoMapper;
using Toxic.Interfaces;
using Toxic.Models;
using Toxic.Services;
using Toxic.Mapper;
using Toxic.DTOs;

namespace Toxic.Tests.CallTests
{
    public class TopicTests
    {
        private readonly Mock<ITopicRepository> _mockTopicRepo;
        private readonly ITopicService _topicService;
        private readonly IMapper _mapper;
        public TopicTests()
        {
            var mappingConfig = new MapperConfiguration(mc =>
            {
                mc.AddProfile(new MappingProfile());
            });
            _mapper = mappingConfig.CreateMapper();

            _mockTopicRepo = new Mock<ITopicRepository>();
            _topicService = new TopicService(_mockTopicRepo.Object);
        }

        [Fact] //Get a single topic by Id
        public async Task GetTopicByIdAsync_WhenCalled_ReturnTopicAsync()
        {
            var topic = new Topic
            {
                Header = "test"
            };

            _mockTopicRepo.Setup(x => x.GetTopicByIdAsync(topic.Id)).ReturnsAsync(topic);

            var results = await _topicService.GetTopicByIdAsync(topic.Id);

            Assert.NotNull(results);
            Assert.Equal(topic.Id, results.Id);
            Assert.Equal(topic.Header, results.Header);

        }

        [Fact] //Create Topic
        public async Task CreateTopic_WhenCalled_ReturnsNewTopic()
        {
            var topicDTO = new CreateTopicDTO
            {
                Content = "test"
            };

            var topic = _mapper.Map<Topic>(topicDTO);

            _mockTopicRepo.Setup(x => x.CreateTopicAsync(_mapper, topicDTO)).ReturnsAsync(topic);

            var result = await _topicService.CreateTopicAsync(_mapper, topicDTO);

            Assert.NotNull(result);
            Assert.Equal(topicDTO.Header, result.Header);
        }

        [Fact] //DeleteTopic
        public async Task DeleteTopic_WhenCalled_ReturnsDeletedTopic()
        {
            var topic = new Topic
            {
                Id = 1,
                Header = "test"
            };

            _mockTopicRepo.Setup(x => x.DeleteTopicAsync(topic.Id)).ReturnsAsync(topic);

            await _topicService.DeleteTopicAsync(topic.Id);

            _mockTopicRepo.Verify(x => x.DeleteTopicAsync(topic.Id), Times.Once());

            _mockTopicRepo.Setup(x => x.GetTopicByIdAsync(topic.Id)).ReturnsAsync((Topic)null);
        }

        [Fact] //Get all Category Topics
        public async Task GetAllCategoryTopics_WhenCalled_ReturnAllCategoryTopics()
        {
            int categoryId = 1;

            var topics = new List<Topic>
            {
                new Topic { Id = 1, CategoryId = 1 },
                new Topic { Id = 2, CategoryId = 2 },
                new Topic { Id = 3, CategoryId = 1 }
            };

            var filteredTopics = topics.Where(t => t.CategoryId == categoryId).ToList();

            _mockTopicRepo.Setup(x => x.GetAllCategoryTopicsAsync(categoryId)).ReturnsAsync(filteredTopics);

            var results = await _topicService.GetAllCategoryTopicsAsync(categoryId);

            Assert.NotNull(results);
            Assert.Equal(2, results.Count);
            Assert.Contains(results, t => t.Id == 1);
            Assert.Contains(results, t => t.Id == 3);
        }

        [Fact] //Update Topic
        public async Task UpdateTopic_WhenCalled_ReturnUpdatedTopic()
        {
            int id = 1;

            var topicDTO = new UpdateTopicDTO
            {
                Header = "test"
            };

            var topic = _mapper.Map<Topic>(topicDTO);

            _mockTopicRepo.Setup(x => x.GetTopicByIdAsync(id)).ReturnsAsync(topic);
            _mockTopicRepo.Setup(x => x.UpdateTopicAsync(id, _mapper, topicDTO)).ReturnsAsync(topic);

            var result = await _topicService.UpdateTopicAsync(id, _mapper, topicDTO);

            Assert.NotNull(result);
            Assert.Equal(topic.Header, result.Header);
        }
    }
}
