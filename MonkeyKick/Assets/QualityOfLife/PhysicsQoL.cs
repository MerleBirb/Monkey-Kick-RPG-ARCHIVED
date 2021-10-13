// Merle Roji
// 10/6/21

using UnityEngine;

namespace MonkeyKick.QualityOfLife
{
    public static class PhysicsQoL
    {
		public static void ClampAngle(ref Vector3 angle)
		{
			if (angle.x < -180) angle.x += 360;
			else if (angle.x > 180) angle.x -= 360;

			if (angle.y < -180) angle.y += 360;
			else if (angle.y > 180) angle.y -= 360;

			if (angle.z < -180) angle.z += 360;
			else if (angle.z > 180) angle.z -= 360;
		}

		// AngleTo will return degrees (0-360) from one point ex. (x = 0, z = 0) to another point ex. (-x = 3, -z = 5)
		public static float AngleTo(Vector2 from, Vector2 to)
		{
			Vector2 direction = to - from;
			float angle = Mathf.Atan2(direction.x, direction.y) * Mathf.Rad2Deg;
			if (angle < 0f) angle += 360f;
			if (angle > 360f) angle -= 360f;
			return angle;
		}

        #region

		public static Vector3 LinearMove(Vector3 startPos, Vector3 endPos, float time)
        {
			Vector3 returnPos = new Vector3(endPos.x, startPos.y, endPos.z);
			return (returnPos - startPos) / time;
        }

		public static Vector3 LinearMove(Vector3 startPos, Vector3 endPos, float time, float xOffset)
		{
			Vector3 returnPos = new Vector3(endPos.x + xOffset, startPos.y, endPos.z);
			return (returnPos - startPos) / time;
		}

		#endregion

		#region PARABOLAS

		public static ParabolaData CalculateParabolaData(Vector3 startPos, Vector3 endPos, float jumpHeight, float targetHeight, float gravity)
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

		public static void ParabolaJump(ParabolaData parabola, Rigidbody jumperRb, Rigidbody targetRb)
        {
			jumperRb.velocity = parabola.InitialVelocity;
			targetRb.isKinematic = true;
        }

        #endregion

    }
}
