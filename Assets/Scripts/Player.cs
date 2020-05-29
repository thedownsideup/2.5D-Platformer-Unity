using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using UnityEngine;

public class Player : MonoBehaviour
{

    private CharacterController _controller;

    [SerializeField]
    private float _speed = 5.0f;
    [SerializeField]
    private float _gravity = 2.0f;
    [SerializeField]
    private float _jumpHeight = 17.0f;
    private float _cachedVelocity;
    private bool _canDoubleJump;
    [SerializeField]
    private int _coins = 0;
    private UI_Manager _uiManager;
    [SerializeField]
    private int _lives = 3;

    [SerializeField]
    private float _doubleJumpHeight = 15.0f;

    private Vector3 _pos;

    // Start is called before the first frame update
    void Start()
    {
        _pos = transform.position;
        
        _controller = GetComponent<CharacterController>();
        _uiManager = GameObject.Find("Canvas").GetComponent<UI_Manager>();

        if (_controller == null)
            Debug.LogError("Character Controller Is Empty");
        if (_uiManager == null)
            Debug.LogError("UI Manager Is Empty");
    }

    // Update is called once per frame
    void Update()
    {
        Move();
    }

    private void Move()
    {
        float horizontalInput = Input.GetAxis("Horizontal");
        Vector3 direction = new Vector3(horizontalInput, 0, 0);
        Vector3 velocity = direction * _speed;

        if (_controller.isGrounded)
        {
            if (Input.GetKeyDown("space"))
            {
                _cachedVelocity = _jumpHeight;
                _canDoubleJump = true;
            }
        }
        else
        {
            if (Input.GetKeyDown("space") && _canDoubleJump)
            {
                _cachedVelocity += _doubleJumpHeight;
                _canDoubleJump = false;
            }
            _cachedVelocity -= _gravity;
        }

        velocity.y = _cachedVelocity;

        _controller.Move(velocity * Time.deltaTime);

        if (transform.position.y <= -100) 
        {
            transform.position = _pos;
            _lives--;
            if (_lives == 0) {
                _lives = 3;
                _coins = 0;
                _uiManager.UpdateCoins(_coins);
            }
            _uiManager.UpdateLives(_lives);
        }
    }

    public void AddCoins()
    {
        _coins++;
        _uiManager.UpdateCoins(_coins);
    }
}
