using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float _moveSpeed;
    [SerializeField] private float _runSpeed;
    private enum State
    {
        Move,
        Run
    }
    private Rigidbody2D _rigidbody2D;
    private Vector3 _movementDirection;
    private Vector3 _runDirection;
    private Vector3 _lastMovementDirection;
    private State _state;
    private void Awake()
    {
        _rigidbody2D = GetComponent<Rigidbody2D>();
        _state = State.Move;
    }
    private void Update()
    {
        if (_state == State.Move)
        {
            float moveX = 0f;
            float moveY = 0f;

            if (Input.GetKey(KeyCode.W)) moveY = +1f;
            if (Input.GetKey(KeyCode.S)) moveY = -1f;
            if (Input.GetKey(KeyCode.A)) moveX = -1f;
            if (Input.GetKey(KeyCode.D)) moveX = +1f;

            _movementDirection = new Vector3(moveX, moveY).normalized;
            if (moveX != 0 || moveY != 0) _lastMovementDirection = _movementDirection;

            if (Input.GetKeyDown(KeyCode.Space)) // dash
            {
                _runDirection = _lastMovementDirection;
                _runSpeed = 150f;
                _state = State.Run;
            }

            if (Input.GetKeyDown(KeyCode.Mouse0)) // rocket
            {
                _runDirection = _lastMovementDirection;
                _runSpeed = 450f;
                _state = State.Run;
            }
        }
        if (_state == State.Run)
        {
            if (Input.GetKeyDown(KeyCode.Space)) // dash
            {
                _runDirection = _lastMovementDirection;
                _runSpeed = 150f;
            }

            if (Input.GetKeyDown(KeyCode.Mouse0)) // rocket
            {
                _runDirection = _lastMovementDirection;
                _runSpeed = 450f;
            }
            float runSpeedDropMultiplier = 5f;
            _runSpeed -= _runSpeed * runSpeedDropMultiplier * Time.deltaTime;

            float runSpeedMinimum = 50f;
            if (_runSpeed < runSpeedMinimum) _state = State.Move;
        }
    }
    private void FixedUpdate()
    {
        if (_state == State.Move) _rigidbody2D.velocity = _movementDirection * _moveSpeed;
        if (_state == State.Run) _rigidbody2D.velocity = _runDirection * _runSpeed;
    }
}
