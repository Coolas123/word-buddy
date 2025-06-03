namespace Application.SignalR.Models
{
    public class UserConnection
    {
        public string UserName { get; set; } = null!;

        public List<Connection> _connections { get; }

        public IEnumerable<Connection> Connections => _connections;

        public UserConnection(string userName) {
            UserName = userName;
            _connections =new List<Connection>();
        }

        public void AppendConnection(string connectionId) {
            if (Connections == null) {
                return;
            }

            var newConnection = new Connection
            {
                ConnectionId = connectionId
            };

            _connections.Add(newConnection);
        }

        public void RemoveConnection(string connectionId) {
            if (connectionId == null) {
                return;
            }

            var connectionExist = _connections.SingleOrDefault(x=>x.ConnectionId == connectionId);

            if (connectionExist == null) {
                return;
            }

            _connections.Remove(connectionExist);
        }
    }
}
