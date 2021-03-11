using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Composites;

namespace AF.Platformer
{
    class PlayerInput2D : IPlayerInput
    {
        private InputActionReference MoveAction { get; set; }
        private InputActionReference JumpAction { get; set; }
        public event Action Jumped;
        public Vector2 MoveInputVector { get; private set; }
        public PlayerInput2D(
            InputActionReference moveAction,
            InputActionReference jumpAction)
        {
            MoveAction = moveAction;
            JumpAction = jumpAction;

            MoveAction.action.performed += UpdateMoveInputVector;
            MoveAction.action.canceled += UpdateMoveInputVector;

            JumpAction.action.performed += cbt => Jumped?.Invoke();
        }

        public void EnableAll()
        {
            MoveAction.action.Enable();
            JumpAction.action.Enable();
        }

        public void DisableAll()
        {
            MoveAction.action.Disable();
            JumpAction.action.Disable();
        }

        private void UpdateMoveInputVector(InputAction.CallbackContext cbt)
        {
            MoveInputVector = new Vector2(cbt.ReadValue<float>(), 0f);
        }
    }
}
