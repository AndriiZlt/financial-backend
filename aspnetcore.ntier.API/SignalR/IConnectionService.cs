
namespace aspnetcore.ntier.API
{
    public interface IConnectionService
    {
        List<string> AddToCashe(string userId, string connectionId);
        string ClearConnections(string connectionId);
        List<string> GetUserConnections(string userId);
        string GetValue(string key);
        void setValue(string key, object value);
    }
}