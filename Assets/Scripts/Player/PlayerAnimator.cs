using System;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Player
{
    public class PlayerAnimator : MonoBehaviour
    {
        private enum Direction
        {
            Up,
            Down,
            Left,
            Right
        }
        private enum AnimationState
        {
            Idle,
            Walk,
            Run
        }

        [SerializeField] private SpriteRenderer _spriteRenderer;
        [SerializeField] private PlayerController _playerController;
        [SerializeField] private Rigidbody2D _rigidbody;
        [SerializeField] private Animator _animator;
        [SerializeField] private InputActionReference _moveAction;

        private Vector2Int _previousInput;
        private Direction _currentDirection = Direction.Down;
        private AnimationState _currentAnimState = AnimationState.Idle;
        
        private static readonly int IDLE_TRIGGER = Animator.StringToHash("Idle");
        private static readonly int WALK_TRIGGER = Animator.StringToHash("Walk");
        private static readonly int RUN_TRIGGER = Animator.StringToHash("Run");
        
        private static readonly int UP = Animator.StringToHash("Up");
        private static readonly int DOWN = Animator.StringToHash("Down");
        private static readonly int LEFT = Animator.StringToHash("Left");
        private static readonly int RIGHT = Animator.StringToHash("Right");

        private void Awake()
        {
            _moveAction.action.performed += OnMovePressed;
        }

        private void OnDestroy()
        {
            _moveAction.action.performed -= OnMovePressed;
        }
        
        private void Update()
        {
            UpdateAnimationState();
        }

        private void UpdateAnimationState()
        {
            AnimationState animState = AnimationState.Idle;
            if (_rigidbody.linearVelocity.magnitude > 0.01f && _playerController.MovedLastFrame)
            {
                animState = _playerController.IsSprinting ? AnimationState.Run : AnimationState.Walk;
            }

            if (animState != _currentAnimState)
            {
                _currentAnimState = animState;

                switch (animState)
                {
                    case AnimationState.Idle:
                        _animator.SetTrigger(IDLE_TRIGGER);
                        break;
                    case AnimationState.Walk:
                        _animator.SetTrigger(WALK_TRIGGER);
                        break;
                    case AnimationState.Run:
                        _animator.SetTrigger(RUN_TRIGGER);
                        break;
                }
            }
            
            _animator.SetBool(UP, _currentDirection == Direction.Up);
            _animator.SetBool(DOWN, _currentDirection == Direction.Down);
            _animator.SetBool(LEFT, _currentDirection == Direction.Left);
            _animator.SetBool(RIGHT, _currentDirection == Direction.Right);
        }

        private void UpdateDirection(Vector2Int newInput)
        {
            if ((newInput.x > 0 && newInput.y == 0) ||
                (newInput.x > 0 && _previousInput.x != newInput.x))
            {
                _currentDirection = Direction.Right;
                _spriteRenderer.flipX = false;
            }

            if ((newInput.x < 0 && newInput.y == 0) ||
                (newInput.x < 0 && _previousInput.x != newInput.x))
            {
                _currentDirection = Direction.Left;
                _spriteRenderer.flipX = true;
            }

            if ((newInput.y > 0 && newInput.x == 0) ||
                (newInput.y > 0 && _previousInput.y != newInput.y))
                _currentDirection = Direction.Up;
            
            if ((newInput.y < 0 && newInput.x == 0) ||
                (newInput.y < 0 && _previousInput.y != newInput.y))
                _currentDirection = Direction.Down;
        }

        private void OnMovePressed(InputAction.CallbackContext obj)
        {
            Vector2 currentInput = obj.ReadValue<Vector2>();
            Vector2Int roundedInput = new Vector2Int(Mathf.RoundToInt(currentInput.x), Mathf.RoundToInt(currentInput.y));
            if (roundedInput != _previousInput)
            {
                UpdateDirection(roundedInput);
                _previousInput = roundedInput;
            }
        }
    }
}
