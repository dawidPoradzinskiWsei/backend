
using ApplicationCore.Commons.Repository;

public class ChatUserService : IChatUserService
{
    private readonly IGenericRepository<ChatUser, int> chatUserRepository;

    public ChatUserService(IGenericRepository<ChatUser, int> chatUserRepository)
    {
        this.chatUserRepository = chatUserRepository;
    }
    
    public void Add(string connectionId, string username)
    {
        chatUserRepository.Add(new ChatUser
        {
            ConnectionId = connectionId,
            Username = username,
            Id = chatUserRepository.FindAll().Count() + 1
        });
    }

    public IEnumerable<(string ConnectionId, string Username)> GetAll()
    {
        return chatUserRepository.FindAll().Select(x => (x.ConnectionId, x.Username));
    }

    public string GetConnectionIdByName(string username)
    {
        return chatUserRepository.FindAll().FirstOrDefault(x => x.Username == username)?.ConnectionId;
    }

    public void RemoveByName(string username)
    {
        int id = chatUserRepository.FindAll().FirstOrDefault(x => x.Username == username).Id;
        
        chatUserRepository.RemoveById(id);
    }
}