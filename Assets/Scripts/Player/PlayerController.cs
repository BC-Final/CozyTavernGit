using System;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Player
{
    public class PlayerController : MonoBehaviour
    {
        [SerializeField] private InputActionReference _moveAction;
        [SerializeField] private InputActionReference _sprintAction;

        [SerializeField] private Rigidbody2D _rigidbody;
        [SerializeField] private float _maxWalkSpeed = 2;
        [SerializeField] private float _maxRunSpeed = 3;
        [SerializeField] private float _acceleration = 25;

        private bool _movedLastFrame = false;
        private bool _isSprinting = false;
        private Vector2 _currentInput;
        private Vector2 _currentVelocity;
        private Vector2 _lastFramePos;
        
        private float CurrentMaxSpeed => _isSprinting ? _maxRunSpeed : _maxWalkSpeed;
        public bool IsSprinting => _isSprinting;
        public bool MovedLastFrame => _movedLastFrame;

        private const float MIN_DISTANCE_TO_WALK = 0.001f;

        private void Awake()
        {
            _sprintAction.action.performed += OnPressedSprint;
            _sprintAction.action.canceled += OnReleasedSprint;
        }

        private void OnDestroy()
        {
            _sprintAction.action.performed -= OnPressedSprint;
            _sprintAction.action.canceled -= OnReleasedSprint;
        }

        private void Update()
        {
            UpdateVelocity();
            UpdatePosition();
        }
        
        private void FixedUpdate()
        {
            bool moved = Vector2.Distance(_lastFramePos, _rigidbody.position) > MIN_DISTANCE_TO_WALK;
            _movedLastFrame = moved;
            _lastFramePos = _rigidbody.position;
        }

        private void UpdateVelocity()
        {
            _currentInput = _moveAction.action.ReadValue<Vector2>();
            _currentVelocity = Vector2.Lerp(_currentVelocity, _currentInput * CurrentMaxSpeed,
                Time.deltaTime * _acceleration);
        }

        private void UpdatePosition()
        {
            _rigidbody.linearVelocity = _currentVelocity;
        }

        private void OnPressedSprint(InputAction.CallbackContext obj)
        {
            _isSprinting = true;
        }

        private void OnReleasedSprint(InputAction.CallbackContext obj)
        {
            _isSprinting = false;
        }
    }
}
