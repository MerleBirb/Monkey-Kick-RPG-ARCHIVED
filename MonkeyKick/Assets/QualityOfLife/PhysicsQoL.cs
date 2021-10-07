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

	}
}
