using System;
using UnityEngine;

public class LocalPlayer : Player {

    protected override void FixedUpdate() {
        base.FixedUpdate();
        SendDataToServer();
    }

    private void SendDataToServer() {
        SendPositionToServer();
    }

    void SendPositionToServer() {
        ClientSend.PlayerPosition(gameObject.transform.position, new Vector2(MoveHorizontal, MoveVertical));
    }
}