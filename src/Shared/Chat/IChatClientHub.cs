using System.Threading.Tasks;

namespace SukiG.Shared.Chat
{
    public interface IChatClientHub
    {
        string UserName { get; }

        Task Login(ChatHubConfig config);

        Task Logout();

        Task Send<T>(string method, T input);
    }
}
