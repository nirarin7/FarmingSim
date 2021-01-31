using System.Numerics;

namespace GameServer
{
    public class Player
    {
        public int Id { get; set; }
        public string Username { get; set; }

        public Vector2 position;

        // private float moveSpeed = 5f / Constants.TIC_PER_SEC;

        public Player(int id, string username, Vector2 position)
        {
            this.position = position;
            Id = id;
            Username = username;
        }

        public void Update()
        {
            ServerSend.PlayerPosition(this);
        }

        public void SetPosition(Vector2 position)
        {
            this.position = position;
        }
    }
}