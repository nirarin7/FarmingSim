using GameServer.Server;
using GameServerLib.DataModels;

namespace GameServer.GameModels
{
    public class Player
    {
        public int Id { get; private set; }
        public string Username { get; private set; }

        public Vector2 Position { get; private set; }
        public Vector2 Direction { get; set; }

        // private float moveSpeed = 5f / Constants.TIC_PER_SEC;

        public Player(int id, string username, Vector2 position)
        {
            Position = position;
            Id = id;
            Username = username;
            Direction = Vector2.Zero;
        }

        public void Update()
        {
            ServerSend.PlayerPosition(this);
            // ServerSend.PlayerDirection(this);
        }

        public void SetDirection(Vector2 direction) {
            Direction = direction;
        }

        public void SetPosition(Vector2 position)
        {
            Position = position;
        }
    }
}