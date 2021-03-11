using UnityEngine;
using UnityEngine.InputSystem;

namespace AF.Platformer
{
    interface IPlayerInput
    {
        Vector2 MoveInputVector { get; }

        void DisableAll();
        void EnableAll();
    }
}