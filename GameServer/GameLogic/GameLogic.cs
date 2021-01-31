using GameServer.Server;

namespace GameServer.GameLogic
{
    public static class GameLogic
    {
        public static void Update()
        {
            foreach (var clientsValue in Server.GameServer.Clients.Values)
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