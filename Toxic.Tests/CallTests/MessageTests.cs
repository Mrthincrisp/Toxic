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
    public class MessageTests
    {
        private readonly Mock<IMessageRepository> _mockMessageRepo;
        private readonly IMessageService _messageService;
        private readonly IMapper _mapper;
        public MessageTests() 
        {
            var mappingConfig = new MapperConfiguration(mc =>
            {
                mc.AddProfile(new MappingProfile());
            });
            _mapper = mappingConfig.CreateMapper();

            _mockMessageRepo = new Mock<IMessageRepository>();
            _messageService = new MessageService(_mockMessageRepo.Object);

        }

        [Fact] //Create Message
        public async Task CreateAMessage_WhenCalled_ReturnsNewMessage()
        {
            var messageDTO = new CreateMessageDTO
            {
                Content = "test"
            };

            var message = _mapper.Map<Message>(messageDTO);

            _mockMessageRepo.Setup(x => x.CreateAMessage(_mapper, messageDTO)).ReturnsAsync(message);

            var result = await _messageService.CreateAMessage(_mapper, messageDTO);

            Assert.NotNull(result);
            Assert.Equal(messageDTO.Content, result.Content);

        }

        [Fact] //Delete message
        public async Task DelteAMessage_WhenCalled_ReturnsDeletedMessage()
        {
            var message = new Message
            {
                Id = 1,
            };

            _mockMessageRepo.Setup(x => x.DeleteMessageAsync(message.Id)).ReturnsAsync(message);

            await _messageService.DeleteMessageAsync(message.Id);

            _mockMessageRepo.Verify( x => x.DeleteMessageAsync(message.Id), Times.Once());
            _mockMessageRepo.Setup( x => x.GetSingleMessageAsync(message.Id)).ReturnsAsync((Message)null);

        }

        [Fact] //Get single Message
        public async Task GetASingleMessage_whenCAlled_ReturnsSingleMessage()
        {
            var message = new Message
            {
                Id = 1
            };

            _mockMessageRepo.Setup(x => x.GetSingleMessageAsync(message.Id)).ReturnsAsync(message);

            var results = await _messageService.GetSingleMessageAsync(message.Id);

            Assert.NotNull(results);
            Assert.Equal(message.Id, results.Id);
        }

        [Fact]//Get messages in a chat
        public async Task GetMessagesInAChat_WhenCalledReturnChatMessages()
        {
            int chatId = 1;

            var messages = new List<Message>
            {
                new Message { Id = 1, ChatId = 1 },
                new Message { Id = 2, ChatId = 1 },
                new Message { Id = 3, ChatId = 2 },
                new Message { Id = 4, ChatId = 11 },
            };

            var filterMessages = messages.Where(m => m.ChatId == chatId).ToList();

            _mockMessageRepo.Setup(x => x.GetMessagesInAChatAsync(chatId)).ReturnsAsync(filterMessages);

            var results = await _messageService.GetMessagesInAChatAsync(chatId);

            Assert.NotNull(results);
            Assert.Equal(2, results.Count());
            Assert.Contains(results, t => t.Id == 1);
            Assert.Contains(results, t => t.Id == 2);
        }

        [Fact] //Update message
        public async Task UpdateMessage_whenCalled_ReturnUpdatedMessage()
        {
            int id = 1;

            var messageDTO = new UpdateMessageDTO
            {
                Content = "test"
            };

            var message = _mapper.Map<Message>(messageDTO);

            _mockMessageRepo.Setup(x => x.GetSingleMessageAsync(id)).ReturnsAsync(message);
            _mockMessageRepo.Setup(x => x.UpdateMessageAsync(id, _mapper, messageDTO)).ReturnsAsync(message);

            var result = await _messageService.UpdateMessageAsync(id, _mapper, messageDTO);

            Assert.NotNull(result);
            Assert.Equal(message.Content, result.Content);
        }
    }
}
