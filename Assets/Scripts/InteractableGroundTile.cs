using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractableGroundTile : MonoBehaviour
{
    public Plant plant;

    private SpriteRenderer _spriteRenderer;
    private Color _highlightedColor = Color.red;
    private Color _defaultColor;
    
    // Start is called before the first frame update
    void Start()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
        if(_spriteRenderer)
            _defaultColor = _spriteRenderer.color;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnMouseEnter()
    {
        Debug.Log("Mouse Enter");
        _spriteRenderer.color = _highlightedColor;
    }
    
    void OnMouseExit()
    {
        Debug.Log("Mouse Exit");
        _spriteRenderer.color = _defaultColor;
    }
}
