using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace AF.Platformer
{
    static class PlayerMovement
    {
        public static Vector3 TickVertical(
            Vector3 velocity,
            Vector3 gravityDir,
            float gravityScalar,
            float terminalVelocity,
            float deltaTime = 1f)
        {
            return Vector3.Max(velocity + (gravityDir * gravityScalar * deltaTime), gravityDir * terminalVelocity);
        }

        public static float CalculateJumpPower(
            float hMax,
            float th)
        {
            return (hMax * 2) / th;
        }

        public static float CalculateGravity(
            float hMax,
            float th)
        {
            return (2 * hMax) / (th * th);
        }
    }
}
