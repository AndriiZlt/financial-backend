
using Microsoft.AspNetCore.SignalR;
using Serilog;




namespace aspnetcore.ntier.BLL.Utilities
{

    public class NotificationHub : Hub
    {
        private readonly IConnectionService _connectionService;

        public NotificationHub( IConnectionService connectionService)
        {
            _connectionService = connectionService;
        }

        public override async Task OnConnectedAsync()
        {
            var currentConnectionId = Context.ConnectionId;
            Log.Information("New connection started: {id}", currentConnectionId);
            await Clients.Client(currentConnectionId.ToString()).SendAsync("status", "connected");
            await base.OnConnectedAsync();
        }

        public override async Task OnDisconnectedAsync(Exception? exception)
        {
            try 
            {
                var currentConnectionId = Context.ConnectionId;
                Log.Information("Disconnected: {id}", currentConnectionId);
                string userId = _connectionService.ClearConnections(currentConnectionId);
            }
            catch (Exception ex)
            {
                Log.Error("Error occurred in SignalRHub OnDisconnectedAsync function", ex.Message);
            }

        }

/*        public async Task Register(string userId)
        {
            Log.Information("STRING Registered userId: {response}", userId);
            var currentConnectionId = Context.ConnectionId;
            var key = $"UserId_{userId}";
            try
            {
                List<string> updatedUserConnections = _connectionService.AddToCashe(key, currentConnectionId);
                Log.Information("Registered User {userId} with connections: {@response}", userId, updatedUserConnections);
            }
            catch (Exception ex)
            {
                Log.Error("Error occurred in SignalRHub Register function. {@ex}", ex.Message);
            }

        }*/

        public async Task Register(int userId)
        {
            var currentConnectionId = Context.ConnectionId;
            var key = $"UserId_{userId}";
            try
            {
                List<string> updatedUserConnections = _connectionService.AddToCashe(key, currentConnectionId);
                Log.Information("User {userId} connections: {@response}", key, updatedUserConnections);
            }
            catch (Exception ex)
            {
                Log.Error("An unexpected error occurred in SignalRHub Register function. {@ex}", ex.Message);
                Log.Information($"Error in Register user {userId} with connectionId {currentConnectionId}");
            }

        }

        public async Task SendNewNotification(string connectionId )
        {
            Log.Information("Send notification to: {connection}", connectionId);
            await Clients.Client(connectionId).SendAsync("New_Notification", "Update notifications");
        }

    }
}
