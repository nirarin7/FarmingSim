using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private void FixedUpdate()
    {
        SendPositionToServer();
    }

    private void SendPositionToServer()
    {
        Vector2 position = gameObject.transform.position;
        ClientSend.PlayerPosition(position);
    }
}