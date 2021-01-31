namespace GameServer
{
    public class GameLoop
    {
        public static void Update()
        {
            foreach (var clientsValue in Server.Clients.Values)
            {
                if (clientsValue.Player != null)
                {
                    clientsValue.Player.Update();
                }
            }
            ThreadManager.UpdateMain();
        }
    }
}