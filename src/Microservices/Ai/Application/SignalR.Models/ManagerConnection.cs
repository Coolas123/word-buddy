namespace Application.SignalR.Models
{
    public class ManagerConnection
    {
        private readonly List<UserConnection> _users;

        public IEnumerable<UserConnection> Users =>_users;

        public ManagerConnection() {
            _users = new();
        }

        public void ConnectUser(string userName, string connectionId) {
            var userExist = _users.SingleOrDefault(x => x.UserName == userName);

            if (userExist != null) {
                userExist.AppendConnection(connectionId);
                return;
            }

            var user = new UserConnection(userName);
            user.AppendConnection(connectionId);
            _users.Add(user);
        }

        public bool DisconnectUser(string connectionId) {
            var userExist = _users.SingleOrDefault(x => x.Connections.Where(x=>x.ConnectionId == connectionId) !=null);
            if (userExist == null) {
                return false;
            }

            var connection = userExist.Connections.SingleOrDefault(x=>x.ConnectionId == connectionId);
            
            if(userExist.Connections.Count() == 1) {
                _users.Remove(userExist);
                return true;
            }

            userExist.RemoveConnection(connectionId);
            return false;
        }
    }
}
