using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UiManager : MonoBehaviour
{
    public static UiManager instance;

    public GameObject startMenu;
    public InputField username;
    
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else if(instance != null)
        {
            Debug.Log("Instance already exist, destroying object");
            Destroy(this);
        }
    }

    public void ConnectedToServer()
    {
       startMenu.SetActive(false);
       username.interactable = false;
       Client.Instance.ConnectToServer();
    }
}
