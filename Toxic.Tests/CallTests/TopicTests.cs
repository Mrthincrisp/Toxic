using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Moq;
using Toxic.Interfaces;
using Toxic.Models;
using Toxic.Services;

namespace Toxic.Tests.CallTests
{
    public class TopicTests
    {
        private readonly Mock<ITopicRepository> _mockTopicRepo;
        private readonly ITopicService _topicService;

        public TopicTests()
        {
            _mockTopicRepo = new Mock<ITopicRepository>();
            _topicService = new TopicService(_mockTopicRepo.Object);
        }

        [Fact]

        public async Task GetTopicByIdAsync_WhenCalled_ReturnTopicAsync()
        {
            var topic = new Topic
            {
                Id = 1,
                Header = "test"
            };

            _mockTopicRepo.Setup(x => x.GetTopicByIdAsync(topic.Id)).ReturnsAsync(topic);

            var results = await _topicService.GetTopicByIdAsync(topic.Id);

            Assert.NotNull(results);
            Assert.Equal(topic.Id, results.Id);
            Assert.Equal(topic.Header, results.Header);

        }
    }
}
