// Merle Roji
// 10/6/21

using UnityEngine;

namespace MonkeyKick.CustomPhysics
{
    public static class PhysicsQoL
    {
        #region ANGLES

		/// <summary>
		/// Keep the angle from going below -180 and above 180.
		/// </summary>
		/// <param name="angle"></param>
        public static void ClampAngle180(ref Vector3 angle)
		{
			if (angle.x < -180) angle.x += 360;
			else if (angle.x > 180) angle.x -= 360;

			if (angle.y < -180) angle.y += 360;
			else if (angle.y > 180) angle.y -= 360;

			if (angle.z < -180) angle.z += 360;
			else if (angle.z > 180) angle.z -= 360;
		}

		// AngleTo will return degrees (0-360) from one point ex. (x = 0, z = 0) to another point ex. (-x = 3, -z = 5)
		public static float AngleTo(in Vector2 from, in Vector2 to)
		{
			Vector2 direction = to - from;
			float angle = Mathf.Atan2(direction.x, direction.y) * Mathf.Rad2Deg;
			if (angle < 0f) angle += 360f;
			if (angle > 360f) angle -= 360f;
			return angle;
		}

        #endregion

        #region LINEAR

        public static Vector3 LinearMove(in Vector3 startPos, in Vector3 endPos, in float time)
        {
			Vector3 returnPos = new Vector3(endPos.x, startPos.y, endPos.z);
			return (returnPos - startPos) / time;
        }

		public static Vector3 LinearMove(in Vector3 startPos, in Vector3 endPos, in float time, in float xOffset)
		{
			Vector3 returnPos = new Vector3(endPos.x + xOffset, startPos.y, endPos.z);
			return (returnPos - startPos) / time;
		}

		#endregion

		#region PARABOLAS

		public static ParabolaData CalculateParabolaData(in Vector3 startPos, in Vector3 endPos, in float jumpHeight,
			in float targetHeight, in float gravity)
        {
			Vector3 targetPos = new Vector3(endPos.x, endPos.y + targetHeight, endPos.z); // adjust the target position by the height of the target.

			float displacementY = targetPos.y - startPos.y;
			Vector3 displacementXZ = new Vector3(targetPos.x - startPos.x, 0, targetPos.z - startPos.z);

			// calculate time it takes to perform parabola movement
			float time = Mathf.Sqrt((-2 * jumpHeight) / gravity) + Mathf.Sqrt(2 * (displacementY - jumpHeight) / gravity);
			Vector3 velocityY = Vector3.up * Mathf.Sqrt(-2 * gravity * jumpHeight);
			Vector3 velocityXZ = displacementXZ / time;

			return new ParabolaData(velocityXZ + velocityY * -Mathf.Sign(gravity), time); // return new ParabolaData using data
		}

		public static void ParabolaMove(in ParabolaData parabola, Rigidbody jumperRb)
        {
			jumperRb.velocity = parabola.InitialVelocity;
        }

        #endregion
    }
}
