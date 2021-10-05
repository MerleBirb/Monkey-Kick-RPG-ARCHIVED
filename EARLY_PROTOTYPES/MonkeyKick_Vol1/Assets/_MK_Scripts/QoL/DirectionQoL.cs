//===== DIRECTION QUALITY OF LIFE =====//
/*
6/20/21
Description:
- Direction methods that are useful

Author: Merlebirb
*/

using UnityEngine;
using System;

namespace MonkeyKick.QoL
{
    public static class DirectionQoL
    {
        /// <summary>
        /// rounding from the conversion to int
        /// roundedAngle = 320f hits case 7, while roundedAngle = 340f hits case 0
        /// roundedAngle = 12.5f hits case 0 while roundedAngle = 25f hits case 1, etc
        /// <summary/>
        public static int DetermineDirectionFromDegToInt(float angle)
        {
            float roundedAngle = (float)Math.Round(angle, 1);
            return ((Convert.ToInt32((roundedAngle / 45f) % 7.5f)) + 1);
        }

        public static int DetermineDirectionFromVec2ToInt(Vector2 direction)
        {
            float xDir = (float)Math.Round(direction.x, 3);
            float yDir = (float)Math.Round(direction.y, 3);

            // LEFT OFF HERE

            return 0;
        }
    }
}