using UnityEngine;

namespace Better.Commons.Runtime.Utilities
{
	public struct MatrixUtility
	{
		public static Vector3 InverseTransformPoint(Vector3 point, Vector3 position, Quaternion rotation, Vector3 lossyScale)
		{
			var source = new Vector3(1f / lossyScale.x, 1f / lossyScale.y, 1f / lossyScale.z);
			var scaleFactor = Quaternion.Inverse(rotation) * (point - position);
			var inversedPoint = Vector3.Scale(source, scaleFactor);

			return inversedPoint;
		}

		public static Vector3 InverseTransformVector(Vector3 position, Quaternion rotation, Vector3 localScale, Vector3 vector3)
		{
			var initialMatrix = Matrix4x4.TRS(position, rotation, localScale);
			initialMatrix = initialMatrix.inverse;
			var scaleMatrix = Matrix4x4.Scale(vector3);
			var transformed = initialMatrix * scaleMatrix;
			var inversedVector = transformed.lossyScale;

			return inversedVector;
		}

		public static Vector3 TransformPoint(Vector3 point, Vector3 position, Quaternion rotation, Vector3 lossyScale)
		{
			var pointedScale = Vector3.Scale(lossyScale, point);
			var transformedPoint = position + rotation * pointedScale;

			return transformedPoint;
		}
	}
}