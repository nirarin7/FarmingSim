﻿namespace GameServer.Enums {
    /// <summary>Sent from server to client.</summary>
    public enum ServerPackets {
        Welcome = 1,
        SpawnPlayer,
        PlayerPosition,
    }
}