using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

namespace AF.Platformer
{
    [RequireComponent(typeof(CharacterController))]
    public class PlayerBehaviour : MonoBehaviour
    {
        PlayerInput2D _playerInput;

        CharacterController _cc;

        [SerializeField] InputActionReference _moveAction;
        [SerializeField] InputActionReference _jumpAction;
        [Space]
        [SerializeField] float _speed;
        [SerializeField] Vector3 _gravityDir;
        [SerializeField] float _terminalVelocity;
        float _gravityScalar;
        [Space]
        [SerializeField] float _jumpApex;
        [SerializeField] float _jumpTimeApex;
        [SerializeField] Vector3 _jumpDir;
        [SerializeField] int _numOfJumps;
        [SerializeField] float _jumpGrace;
        [SerializeField] float _jumpInputGrace;
        float _currentJumpInputGrace;
        float _currentJumpGrace;
        public float _currentJumps;
        float _jumpScalar;

        Vector3 _velocityHorizontal;
        Vector3 _velocityVertical;

        // Start is called before the first frame update
        void Start()
        {
            _playerInput = new PlayerInput2D(_moveAction, _jumpAction);
            _playerInput.EnableAll();
            _playerInput.Jumped += () =>
            {
                _currentJumpInputGrace = _jumpInputGrace;
                Jump();
            };

            _cc = GetComponent<CharacterController>();

            _gravityScalar = PlayerMovement.CalculateGravity(_jumpApex, _jumpTimeApex);
            _jumpScalar = PlayerMovement.CalculateJumpPower(_jumpApex, _jumpTimeApex);

            _velocityHorizontal = Vector3.zero;
            _velocityVertical = Vector3.zero;

            _currentJumps = _numOfJumps;
            _currentJumpGrace = _jumpGrace;
        }

        private void Update()
        {
            _velocityHorizontal = _playerInput.MoveInputVector * _speed;
            _velocityVertical = PlayerMovement.TickVertical(
                _velocityVertical,
                _gravityDir,
                _gravityScalar,
                _terminalVelocity,
                Time.deltaTime);

            _cc.Move((_velocityHorizontal + _velocityVertical) * Time.deltaTime);

            if (_cc.isGrounded)
            {
                _currentJumps = _numOfJumps;
                _velocityVertical = Vector3.zero;
                _currentJumpGrace = _jumpGrace;

                if (_currentJumpInputGrace > 0)
                {
                    Jump();
                }

                _currentJumpInputGrace = 0;
            }
            else
            {
                _currentJumpGrace -= Time.deltaTime;
                _currentJumpInputGrace -= Time.deltaTime;
            }
        }

        private void Jump()
        {
            _currentJumps--;
            if (_currentJumpGrace > 0 || _currentJumps >= 0)
                _velocityVertical = _jumpDir * _jumpScalar;
        }
    }
}