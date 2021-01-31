using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public float speed = .5f;

    private Rigidbody2D _rigidbody;
    private Animator _animator;

    // Start is called before the first frame update
    void Start()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
        _animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
    }

    private void FixedUpdate()
    {
        var moveHorizontal = Input.GetAxis("Horizontal");
        var moveVertical = Input.GetAxis("Vertical");

        _animator.SetFloat("HorizontalDirection", moveHorizontal);
        _animator.SetFloat("VerticalDirection", moveVertical);
        
        var movement = new Vector2(moveHorizontal, moveVertical) * speed;
        var position = transform.position;
        transform.position = new Vector3(position.x + movement.x, position.y + movement.y);
    }
}