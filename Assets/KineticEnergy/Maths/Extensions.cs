using UnityEngine;

namespace KineticEnergy.Maths {

    public static class Extentions {
        #region Vector2
        /// <summary>
        /// The heading of the Vector. Default units of the angle are radians.
        /// </summary>
        /// <returns>Returns the degrees of this vector from Vector2.right.</returns>
        public static Angle Heading(this Vector2 v) { return Mathf.Atan2(v.y, v.x); }

        /// <summary>
        /// Sets the heading of the vector.
        /// </summary>
        /// <param name="v">Original vector.</param>
        /// <param name="angle">Amount of rotation in degrees.</param>
        /// <returns>Returns a new vector is a heading of v.Heading + degrees and magnitude of the original.</returns>
        public static Vector2 SetHeading(this Vector2 v, Angle angle) { return new Vector2(v.magnitude, 0).Rotate(angle); }

        /// <summary>
        /// Sets the magnitude of the vector.
        /// </summary>
        /// <param name="v">Original vector.</param>
        /// <param name="magnitude">New magnitude of the vector.</param>
        /// <returns>Returns a new vector with the heading of the original but with the new magnitude.</returns>
        public static Vector2 SetMagnitude(this Vector2 v, float magnitude) { return new Vector2(magnitude, 0).Rotate(v.Heading()); }

        /// <summary>
        /// Rotates a vector by an angle.
        /// </summary>
        /// <param name="vector">Vector to rotate.</param>
        /// <param name="angle">The rotation angle.</param>
        /// <returns>Returns a vector of the same magnitude, but rotated.</returns>
        public static Vector2 Rotate(this Vector2 vector, Angle angle) {
            float theta = angle.Radians;
            float sin = Mathf.Sin(theta), cos = Mathf.Cos(theta);
            float x = vector.x, y = vector.y;
            vector.x = cos * x - sin * y;
            vector.y = sin * x + cos * y;
            return vector;
        }

        /// <summary>
        /// Rotates v1 towards v2.
        /// </summary>
        /// <param name="v1">Original vector.</param>
        /// <param name="v2">Vector to.</param>
        /// <param name="angle">Amount of rotation.</param>
        /// <returns>Returns v1.Rotate([+/-]degrees).</returns>
        public static Vector2 RotateTo(this Vector2 v1, Vector2 v2, Angle angle) {
            float theta = angle.Radians;
            if(Vector2.SignedAngle(v1, v2) > 0) return v1.Rotate(theta);
            else return v1.Rotate(-theta);
        }

        /// <summary>
        /// Rotates v1 away from v2.
        /// </summary>
        /// <param name="v1">Original vector.</param>
        /// <param name="v2">Vector from.</param>
        /// <param name="theta">Amount of rotation.</param>
        /// <returns>Returns v1.Rotate([-/+]degrees).</returns>
        public static Vector2 RotateFrom(this Vector2 v1, Vector2 v2, Angle angle) {
            float theta = angle.Radians;
            if(Vector2.SignedAngle(v1, v2) < 0) return v1.Rotate(theta);
            else return v1.Rotate(-theta);
        }

        /// <summary>
        /// Sets the heading of v1 equal to the heading of v2, then rotates it.
        /// </summary>
        /// <param name="v1">Original vector.</param>
        /// <param name="v2">Vector from.</param>
        /// <param name="angle">Change in angle from v2.</param>
        /// <returns>Returns a new vector with the heading of (v2.heading + degrees) and the magnitude of v1.</returns>
        public static Vector2 FromRotation(this Vector2 v1, Vector2 v2, Angle angle) {
            float theta = angle.Radians;
            float rotation = Vector2.SignedAngle(Vector2.right, v2);
            return v1.SetHeading(theta + rotation);
        }
        #endregion
        #region Vector3
        public static Vector3Int Rounded(this Vector3 v)
            => new Vector3Int(Mathf.RoundToInt(v.x), Mathf.RoundToInt(v.y), Mathf.RoundToInt(v.z));
        #endregion
    }

}
