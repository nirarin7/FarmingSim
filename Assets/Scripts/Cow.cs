using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cow : MonoBehaviour, IMaturable {

    public string cowName;
    public int cowAge;
    public Sprite matureSprite;
    

    private SpriteRenderer _spriteRenderer;
    
    
    // Start is called before the first frame update
    void Start()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();  
    }

    // Update is called once per frame
    // void Update()
    // {
    //     Matures();
    // }

    
    public void Matures() {
        cowAge++;
        if (cowAge >= 5) {
            _spriteRenderer.sprite = matureSprite;
        }
        
        
        
    }

}
