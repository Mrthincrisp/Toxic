using Microsoft.EntityFrameworkCore;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Toxic.Interfaces;
using Toxic.Models;
using Toxic.Services;

namespace Toxic.Tests.CallTests
{
    public class UserChatTests
    {
        private readonly Mock<IUserChatRepository> _mockUserChatRepo;
        private readonly IUserChatService _userChatService;

        public UserChatTests()
        {
            _mockUserChatRepo = new Mock<IUserChatRepository>();
            _userChatService = new UserChatService(_mockUserChatRepo.Object);
        }

        [Fact]
        public async Task DeleteUserChatAsync_whenCalled_ReturnsDeletedUserChatAsync()
        {
            // Arrange
            int userId = 1;
            int chatId = 1;

            var userChat = new UserChat { UserId = userId, ChatId = chatId };

            _mockUserChatRepo.Setup(repo => repo.DeleteChatAsync(userId, chatId))
                .ReturnsAsync(userChat); // Simulate deleting the UserChat

            // Act
            var result = await _userChatService.DeleteChatAsync(userId, chatId);

            // Assert
            Assert.NotNull(result); // Ensure a UserChat was returned
            Assert.Equal(userChat.UserId, result.UserId);
            Assert.Equal(userChat.ChatId, result.ChatId);

            _mockUserChatRepo.Verify(repo => repo.DeleteChatAsync(userId, chatId), Times.Once);
        }
    }
}
