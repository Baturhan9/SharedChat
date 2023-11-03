using SharedChatWebSite.Models;

namespace SharedChatWebSite.Repository;

public interface IMessageRepository
{
    void Create(string text, string userId);
    List<Message> GetAll();
    List<Message> GetUsersAll(string userId);
}