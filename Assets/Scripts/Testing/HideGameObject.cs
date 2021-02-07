using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// TODO: Remove this, this is a testing script
public class HideGameObject : MonoBehaviour {
    public GameObject _gameObject;



    public void ToggleHidden() {
        if(_gameObject)
            _gameObject.SetActive(!_gameObject.activeSelf);
    }
    
}
