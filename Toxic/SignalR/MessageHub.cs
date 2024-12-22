using AutoMapper;
using Microsoft.AspNetCore.SignalR;
using System.Text.Json;
using Toxic.DTOs;
using Toxic.Interfaces;
namespace Toxic.SignalR
{
    public class MessageHub : Hub
    {
        private readonly IMessageService _messageService;
        private readonly IMapper _mapper;

        public MessageHub(IMessageService messageService, IMapper mapper)
        {
            _mapper = mapper;
            _messageService = messageService;
        }

        public override async Task OnConnectedAsync()
        {
            await Clients.All.SendAsync("reciveMessage", $"{Context.ConnectionId} has joined");
        }

        public async Task SendMessage(CreateMessageDTO createDTO)
        {


            var message = await _messageService.CreateAMessage(_mapper, createDTO);

            if (message != null)
            {
                await Clients.Group(createDTO.ChatId.ToString())
                    .SendAsync("RecieveMessage", message);
            }
            else
            {
                await Clients.Caller.SendAsync("ErrorMessage", "Failed to send message.");
            }
        }

        public async Task JoinChat(int chatId)
        {
            // Ensure chatId is valid
            if (chatId <= 0)
            {
                throw new ArgumentException("Invalid chatId", nameof(chatId));
            }

            // Convert chatId to string for the group name if needed
            var chatGroupId = chatId.ToString();

            // Add the connection to the group
            await Groups.AddToGroupAsync(Context.ConnectionId, chatGroupId);

            // Send a message to the group
            await Clients.Group(chatGroupId).SendAsync("RecieveMessage", $"{Context.ConnectionId} has joined the chat.");
        }
    }
}
