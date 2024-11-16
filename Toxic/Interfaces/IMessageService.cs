using AutoMapper;
using Toxic.DTOs;
using Toxic.Models;

namespace Toxic.Interfaces
{
    public interface IMessageService
    {
        //Get a message
        Task<Message> GetSingleMessageAsync(int id);

        //Create a Message
        Task<Message> CreateAMessage(IMapper mapper, CreateMessageDTO createDTO);

        //Update Message
        Task<Message> UpdateMessageAsync(int id, IMapper mapper, UpdateMessageDTO updatedTO);

        //Delete a Message
        Task<Message> DeleteMessageAsync(int id);

        //Get Messages in a chat
        Task<List<Message>> GetMessagesInAChatAsync(int id);

    }
}
