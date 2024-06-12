
using Microsoft.AspNetCore.SignalR;
using Serilog;




namespace aspnetcore.ntier.API
{

    public class SignalRHub : Hub
    {
        private readonly IConnectionService _connectionService;

        public SignalRHub( IConnectionService connectionService)
        {
            _connectionService = connectionService;
        }

        public override async Task OnConnectedAsync()
        {
            var currentConnectionId = Context.ConnectionId;
            Log.Information("Connections started for {s}", currentConnectionId);
            await Clients.Client(currentConnectionId.ToString()).SendAsync("status", "connected");
            await base.OnConnectedAsync();
        }

        public override async Task OnDisconnectedAsync(Exception? exception)
        {
            try 
            {
                var currentConnectionId = Context.ConnectionId;
                Log.Information("OnDisconnectedAsync. CurrentConnectionId: {id},  ", currentConnectionId);
                string userId = _connectionService.ClearConnections(currentConnectionId);
                Log.Information("User ID: {userId} ", userId);
            }
            catch (Exception ex)
            {
                Log.Error("An unexpected error occurred in SignalRHub OnDisconnectedAsync function");
            }

        }

        public async Task Register(int userId)
        {
            var currentConnectionId = Context.ConnectionId;
            try
            {
                Log.Information("Register with UserID: {userId}", userId);
                List<string> updatedUserConnections = _connectionService.AddToCashe(userId.ToString(), currentConnectionId);
                Log.Information("User {userId} connections: {@response}", userId, updatedUserConnections);
            }
            catch (Exception ex)
            {
                Log.Error("An unexpected error occurred in SignalRHub Register function", ex);
                Log.Information($"Error in Register user {userId} with connectionId {currentConnectionId}");
            }

        }

        public async Task SendMessage(string targetUserId, string message)
        {
            /*            Clients.Client(targetUserId).SendAsync("recieveMessage", $"Message:'{message}'.");*/
            List<string> targetUserConnections = _connectionService.GetUserConnections(targetUserId);
            var currentConnectionId = Context.ConnectionId;
            var senderId = _connectionService.GetValue(currentConnectionId);
            Log.Information("Target userId:{id}. Connections:{@connections}", targetUserId, targetUserConnections);

            if (targetUserConnections != null)
            {
                foreach (var connection in targetUserConnections)
                {
                    string connection_id = connection.Split('_')[1];
                    await Clients.Client(connection_id).SendAsync("recieveMessage", $"Sender:{senderId}. Message:'{message}'.");
                }
            }

        }
    }
}
