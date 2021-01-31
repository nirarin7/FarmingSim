using System.Numerics;
using GameServer.Server;

namespace GameServer.GameModels
{
    public class Player
    {
        public int Id { get; private set; }
        public string Username { get; private set; }

        public Vector2 Position { get; private set; }

        // private float moveSpeed = 5f / Constants.TIC_PER_SEC;

        public Player(int id, string username, Vector2 position)
        {
            Position = position;
            Id = id;
            Username = username;
        }

        public void Update()
        {
            ServerSend.PlayerPosition(this);
        }

        public void SetPosition(Vector2 position)
        {
            Position = position;
        }
    }
}