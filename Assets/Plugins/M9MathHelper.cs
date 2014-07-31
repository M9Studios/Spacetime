using UnityEngine;
using M9Debug;

namespace M9MathHelper {

	public class M9AngleHelper
	{
		private const float N = 000.0F, E = 090.0F, S = 180.0F, W = 270.0F;
		private const float conversionFactor = 57.2957795F;

		private static bool left, right, up, down;

		private static float xd, yd;
		private static float a;

		public static float Vector2Angle (Vector2 v1, Vector2 v2)
		{
			left = false; right = false;
			up = false; down = false;

			xd = v1.x - v2.x;
			yd = v1.y - v2.y;

			if (xd > 0)
				left = true;
			else
				right = true;

			if (yd > 0)
				down = true;
			else
				up = true;
			
			if ((right) && (up))
				a = Mathf.Atan((yd/xd))*conversionFactor + N;

			if ((right) && (down))
				a = Mathf.Atan((xd/yd))*conversionFactor + E;

			if ((left) && (down))
				a = Mathf.Atan((xd/yd))*conversionFactor + S;

			if ((left) && (up))
				a = Mathf.Atan((xd/yd))*conversionFactor + W;

			return a;
		}

	}

}