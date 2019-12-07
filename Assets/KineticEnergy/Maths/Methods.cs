using System;
using UnityEngine;

namespace KineticEnergy.Maths {

    /// <summary>A collection of math functions and objects related to Geometry.</summary>
    public static class Methods {

        #region Tests
        /// <summary>On a 1-Dimentional line, the direction from X1 to X2 is equal to the sign of the difference: X2 - X1.</summary>
        /// <param name="input">X1 - X2</param>
        /// <returns>Returns the sign of the input (-1 or +1).</returns>
        public static int LinearDirection(float input) => input < 0 ? -1 : 1;

        /// <summary>On a 1-Dimentional line, the direction from X1 to X2 is equal to the sign of the input.</summary>
        /// <param name="input">X1 - X2</param>
        /// <returns>Returns the sign of the input (-1 or +1).</returns>
        public static Direction LinearDirection(float input, Axis axis) => input < 0
                ? axis == Axis.Horizontal ? Direction.Down : Direction.Left
                : axis == Axis.Horizontal ? Direction.Up : Direction.Right;

        /// <summary>Gives which direction the vector is more facing on the given axis.</summary>
        /// <param name="vector">The vector to compare with the given axis.</param>
        /// <param name="axis">The axis to compare the given vector to.</param>
        /// <returns>Returns the sign of the input (-1 or +1).</returns>
        public static Direction LinearDirection(Vector2 vector, Axis axis)
            => axis == Axis.Horizontal ? LinearDirection(vector.x, axis) : LinearDirection(vector.y, axis);

        /// <summary>Gives the Geometry.Direction of an angle relative to the given Geometry.Axis.</summary>
        /// <param name="angle">The input angle in degrees.</param>
        /// <param name="axis">The axis to make the output direction relative to.</param>
        /// <returns>Axis.Horizontal relates to Quadrant 1 and 2 for positive. Axis.Vertical relates to Quadrant 1 and 4 for positive.</returns>
        public static Direction AngleDirection(Angle angle, Axis axis = Axis.Horizontal) {
            float theta = angle.Degrees;
            return axis == Axis.Horizontal
                ? theta == -90 ? Direction.Right : theta == 90 ? Direction.Left
                    : -90 > angle && angle > 90 ? Direction.Right : Direction.Left
                : theta == 0 ? Direction.Right : theta == 180 ? Direction.Left
                    : 0 > angle && angle > 180 ? Direction.Up : Direction.Down;
        }

        /// <summary>Are these two lines parallel?</summary>
        /// <returns>Returns 'true' if (l1.a * l2.b) - (l2.a * l1.b) == 0.</returns>
        public static bool AreParallel(Line line1, Line line2) {
            float delta = line1.A * line2.B - line2.A * line1.B;
            if(delta == 0) return true;
            else return false;
        }

        /// <summary>Determines if the given value exists.</summary>
        /// <returns>Returns 'true' if the number is not Infinity and is not NaN.</returns>
        public static bool Exists(float number) { return !float.IsInfinity(number) && !float.IsNaN(number); }

        /// <summary>Determines if the given value exists.</summary>
        /// <returns>Returns 'true' if the components are not Infinity nor NaN.</returns>
        public static bool Exists(Vector2 vector) { return Exists(vector.x) && Exists(vector.y); }

        /// <summary>Checks if the input is on the interval [-range, +range].</summary>
        /// <param name="range">[-range, +range]</param>
        /// <returns>Returns true if input is on the interval [-range, +range]</returns>
        public static bool IsBetweenRange(float input, float range) {
            return -range <= input && input <= range;
        }

        /// <summary>Returns the axis with the largest magnitude.</summary>
        public static Axis LargestComponent(Vector3 vector) {
            float x = Math.Abs(vector.x), y = Math.Abs(vector.y), z = Math.Abs(vector.z);
            return x > y ? x > z ? Axis.X : Axis.Z : y > z ? Axis.Y : Axis.Z;
        }

        #endregion

        #region Conversions

        // TODO: replace the 'while()' with math
        /// <summary>Normalizes an angle on the interval [0, 360].</summary>
        /// <param name="degr">Input angle in degrees.</param>
        /// <returns>Returns an equivalent angle on the interval [0, 360].</returns>
        public static float NormalizeDegree1(float degree) {
            while(degree < 0) degree += 360;
            while(degree > 360) degree -= 360;
            return degree;
        }

        // TODO: replace the 'while()' with math
        /// <summary>
        /// Normalizes an angle on the interval [-360, +360].
        /// </summary>
        /// <param name="degr">Input angle in degrees.</param>
        /// <returns>Returns an equivalent angle on the interval [-360, +360].</returns>
        public static float NormalizeDegree2(float degree) {
            while(degree < -360) degree += 360;
            while(degree > +360) degree -= 360;
            return degree;
        }

        //todo: replace the 'while()' with math
        /// <summary>
        /// Normalizes an angle on the interval [-180, +180]
        /// </summary>
        /// <param name="degree">Input angle in degrees.</param>
        /// <returns>Returns an equivalent angle on the interval [-180, +180].</returns>
        public static float NormalizeDegree3(float degree) {
            while(degree < -180) degree += 360;
            while(degree > +180) degree -= 360;
            return degree;
        }

        /// <summary>Rounds the given number to the nearest multiple of another number.</summary>
        /// <param name="number">Input number.</param>
        /// <param name="multiple">Input number will be rounded to some (multiple * n).</param>
        /// <remarks>Returns some (multiple * n)</remarks>
        public static float RoundToMultiple(float number, float multiple)
            => Mathf.RoundToInt(number / multiple) * multiple;

        /// <summary>Rounds the given number to the nearest multiple of another number.</summary>
        /// <param name="number">Input number.</param>
        /// <param name="multiple">Input number will be rounded to some (multiple * n).</param>
        /// <remarks>Returns some (multiple * n)</remarks>
        public static float RoundToMultiple(ref float number, float multiple)
            => number = Mathf.RoundToInt(number / multiple) * multiple;

        /// <summary>Rounds the given <see cref="Vector3"/> components to the nearest multiple of another number.</summary>
        /// <param name="vector">Input <see cref="Vector3"/>.</param>
        /// <param name="multiple">Input <see cref="Vector3"/>'s components will be rounded to some (multiple * n).</param>
        /// <returns>Returns a new <see cref="Vector3"/> which components are some (multiple * n).</returns>
        public static Vector3 RoundToMultiple(Vector3 vector, float multiple)
            => new Vector3(RoundToMultiple(vector.x, multiple),
                           RoundToMultiple(vector.y, multiple),
                           RoundToMultiple(vector.z, multiple));

        /// <summary>Rounds the given <see cref="Vector3"/> components to the nearest multiple of another number.</summary>
        /// <param name="vector">Input <see cref="Vector3"/>.</param>
        /// <param name="multiple">Input <see cref="Vector3"/>'s components will be rounded to some (multiple * n).</param>
        public static void RoundToMultiple(ref Vector3 vector, float multiple) {
            vector.x = RoundToMultiple(vector.x, multiple);
            vector.y = RoundToMultiple(vector.y, multiple);
            vector.z = RoundToMultiple(vector.z, multiple);
        }

        /// <summary>Rounds the euler angles of the given <see cref="Quaternion"/> to the nearest multiple of another number.</summary>
        /// <param name="quaterion">Input <see cref="Quaternion"/>.</param>
        /// <param name="multiple">Input <see cref="Quaternion"/>'s euler angles will be rounded to some (multiple * n).</param>
        /// <returns>Returns a new <see cref="Quaternion"/> which euler angles are some (multiple * n).</returns>
        public static Quaternion RoundToMultiple(Quaternion quaterion, float multiple)
            => Quaternion.Euler(RoundToMultiple(quaterion.eulerAngles, multiple));
        /// <summary>Rounds the euler angles of the given <see cref="Quaternion"/> to the nearest multiple of another number.</summary>
        /// <param name="quaternion">Input <see cref="Quaternion"/>.</param>
        /// <param name="multiple">Input <see cref="Quaternion"/>'s euler angles will be rounded to some (multiple * n).</param>
        public static void RoundToMultiple(ref Quaternion quaternion, float multiple)
            => quaternion = Quaternion.Euler(RoundToMultiple(quaternion.eulerAngles, multiple));

        public static Vector3Int RoundToInt(Vector3 vector)
            => new Vector3Int(Mathf.RoundToInt(vector.x),
                              Mathf.RoundToInt(vector.y),
                              Mathf.RoundToInt(vector.z));

        #endregion

        #region Finders

        /// <summary>Returns the angle between v1 and v2 with respect to the X/Y axies.</summary>
        /// <param name="v1">Vector 1 (angle from)</param>
        /// <param name="v2">Vector 2 (angle to)</param>
        public static Angle GetAngle(Vector2 v1, Vector2 v2) {
            return Mathf.Atan2(v1.y - v2.y, v1.x - v2.x);
        }

        /// <summary>Generates a line given two points.</summary>
        /// <param name="p1">Point 1</param>
        /// <param name="p2">Point 2</param>
        /// <returns>Returns a line that intersects the two given points.</returns>
        public static Line LineFromTwoPoints(Vector2 point1, Vector2 point2) {
            float a = (point1.y - point2.y) / (point1.x - point2.x);
            float c = point1.y - a * point1.x;
            return float.IsInfinity(a) ? new Line(-a, 1, c, point1.x) : new Line(-a, 1, c);
        }

        /// <summary>Generates a line given one point and an angle.</summary>
        /// <returns></returns>
        public static Line LineFromAngle(Vector2 point, Angle angle) {
            float a = Mathf.Tan(angle.Radians);
            float c = point.y - a * point.x;
            return float.IsInfinity(a) ? new Line(-a, 1, c, point.x) : new Line(-a, 1, c);
        }

        /// <summary>Generates a line identical to the original, but shifted left/right by distance.</summary>
        /// <param name="distance"></param>
        /// <param name="line"></param>
        public static Line LineFromShift(Vector2 distance, Line line) {
            var p1 = new Vector2(1, line.YFromX(1));
            var p2 = new Vector2(2, line.YFromX(2));
            p1 += distance; p2 += distance;
            return LineFromTwoPoints(p1, p2);
        }

        /// <summary>Uses the Law of Cosines to find an angle of a triangle given only the side lenghs.</summary>
        /// <param name="a">Side length A.</param>
        /// <param name="b">Side length B.</param>
        /// <param name="c">Side length C.</param>
        /// <returns>Returns the angle opposite of side a.</returns>
        public static float LawOfCosForAngleA(float a, float b, float c) { return LawOfCosForAngleC(c, b, a); }

        /// <summary>Uses the Law of Cosines to find an angle of a triangle given only the side lenghs.</summary>
        /// <param name="a">Side length A.</param>
        /// <param name="b">Side length B.</param>
        /// <param name="c">Side length C.</param>
        /// <returns>Returns the angle opposite of side b.</returns>
        public static float LawOfCosForAngleB(float a, float b, float c) { return LawOfCosForAngleC(a, c, b); }

        /// <summary>Uses the Law of Cosines to find an angle of a triangle given only the side lenghs.</summary>
        /// <param name="a">Side length A.</param>
        /// <param name="b">Side length B.</param>
        /// <param name="c">Side length C.</param>
        /// <returns>Returns the angle opposite of side c.</returns>
        public static float LawOfCosForAngleC(float a, float b, float c) { return Mathf.Acos((Mathf.Pow(a, 2) + Mathf.Pow(b, 2) - Mathf.Pow(c, 2)) / (2 * a * b)) * Mathf.Rad2Deg; }

        /// <summary>Creates a new Vector2 with the given angle and magnitude.</summary>
        /// <param name="angle">The angle of the vector. Specify degrees or radians with 'units'.</param>
        /// <param name="magnitude">The magnitude of the vector.</param>
        /// <param name="units">Is the angle in degrees or radians?</param>
        /// <returns>Returns 'new Vector2(Cos(radians) * magnitude, Sin(radians) * magnitude)'</returns>
        public static Vector2 Vector2FromAngle(Angle angle, float magnitude) {
            float theta = angle.Radians;
            return new Vector2(
                Mathf.Cos(theta) * magnitude,
                Mathf.Sin(theta) * magnitude
            );
        }

        /// <summary>Creates a new Vector2 with the given angle and a magnitude of 1.</summary>
        /// <param name="angle">The angle of the vector. Specify degrees or radians with 'units'.</param>
        /// <param name="units">Is the angle in degrees or radians?</param>
        /// <returns>Returns 'new Vector2(Cos(radians) * magnitude, Sin(radians) * magnitude)'</returns>
        public static Vector2 Vector2FromAngle(Angle angle) {
            float theta = angle.Radians;
            return new Vector2(
                Mathf.Cos(theta),
                Mathf.Sin(theta)
            );
        }

        public static Vector2 VectorDirection(Direction direction)
            => direction == Direction.Right ? Vector2.right
             : direction == Direction.Left ? Vector2.left
             : direction == Direction.Up ? Vector2.up
             : direction == Direction.Down ? Vector2.down
             : Vector2.zero;

        /// <summary>
        /// Finds the angle between v1 and v2 if v2's tail is placed on the head of v1.
        /// This angle can also be described as finding the angle of a bend in a line.
        /// </summary>
        /// <param name="v1">The first vector in the angle.</param>
        /// <param name="v2">The second vector in the angle.</param>
        public static Angle HeadToTailAngle(Vector2 v1, Vector2 v2) {
            return Angle.radLeft - v1.Heading().value + v2.Heading().value;
        }

        /// <summary>
        /// Finds the shortest vector that goes from the given point to the given line.
        /// </summary>
        /// <param name="point">The point where to find the path from.</param>
        /// <param name="line">The line to find the path to.</param>
        public static Vector2 PathToLine(Vector2 point, Line line) {
            Line towards = LineFromAngle(point, line.Angle().Mirror());
            Vector2 intersection = Intersection(line, towards);
            Vector2 direction = intersection - point;
            return direction;
        }

        #endregion

        #region Setters
        /// <summary>
        /// Limits the input to [+/-] limit.
        /// </summary>
        /// <param name="input">The input value.</param>
        /// <param name="limit">The limiting value.</param>
        public static float LimitTo(float input, float limit) {
            if(Mathf.Abs(input) > Mathf.Abs(limit)) input = limit * LinearDirection(input);
            return input;
        }

        /// <summary>
        /// Changes the heading of v2 so that if v2's tail is placed on the head of v1, the angle between those two vectors is 'degrees'.
        /// This can also be described as setting the angle of a bend in a line.
        /// </summary>
        /// <param name="v1">Unchanged vector.</param>
        /// <param name="v2">Changed vector.</param>
        /// <param name="degrees">The angle in degrees.</param>
        /// <returns>Returns v2.SetHeading(180.0f - v1.Heading() - degrees).</returns>
        public static Vector2 HeadToTailAngle(Vector2 v1, Vector2 v2, Angle angle) {
            return v2.SetHeading(Angle.radLeft - v1.Heading().value - angle.Radians);
        }
        #endregion

        #region Intersections
        /// <summary>
        /// Finds the intersection between two or more Geometry objects.
        /// </summary>
        /// <param name="l1">Line 1</param>
        /// <param name="l2">Line 2</param>
        /// <returns>Returns the intersection between l1 and l2.</returns>
        public static Vector2 Intersection(Line l1, Line l2) {
            if(l1.isVertical != l2.isVertical) {
                if(l1.isVertical) return new Vector2(l1.x, l2.YFromX(l1.x));
                if(l2.isVertical) return new Vector2(l2.x, l1.YFromX(l2.x));
            }
            float delta = l1.A * l2.B - l2.A * l1.B;
            float x = (l2.B * l1.C - l1.B * l2.C) / delta;
            float y = (l1.A * l2.C - l2.A * l1.C) / delta;
            return new Vector2(x, y);
        }

        /// <summary>
        /// Finds the intersections between two or more Geometry objects.
        /// </summary>
        /// <param name="c1">Circle 1</param>
        /// <param name="c2">Circle 2</param>
        /// <returns>Returns [the closest] two intersections between the two circles.</returns>
        public static Vector2[] Intersection(Circle c1, Circle c2) {
            float r1 = c1.radius, r2 = c2.radius;
            float d = Vector2.Distance(c1.center, c2.center);
            if(d > c1.radius + c2.radius) {
                Vector2[] i = {   // only gets here if there is no real intersection
                Vector2.Lerp(c1.center, c2.center, c1.radius / d),
                Vector2.Lerp(c1.center, c2.center, c2.radius / d)
            };
                return i;
            }

            // squared versions of the variables, because we use them a lot.
            float d_2 = d * d, r1_2 = r1 * r1, r2_2 = r2 * r2;

            float b = d_2 - r1_2 + r2_2;
            float x = b / (2 * d);
            float a = 1 / d * Mathf.Sqrt(4 * d_2 * r2_2 - b * b);
            float y = a / 2;

            float angle = GetAngle(c1.center, c2.center);

            var intersections = new Vector2[2];
            intersections[0] = new Vector2(x, +y).Rotate(angle) + c1.center;
            intersections[1] = new Vector2(x, -y).Rotate(angle) + c1.center;

            return intersections;
        }

        /// <summary>
        /// Finds the intersections between two or more Geometry objects.
        /// </summary>
        /// <param name="c1">Circle 1.</param>
        /// <param name="c2">Circle 2.</param>
        /// <param name="c3">Circle 3.</param>
        /// <returns>Returns [the closest] intersection shared by all three circles.</returns>
        public static Vector2 Intersection(Circle c1, Circle c2, Circle c3) {
            var i1 = Intersection(c1, c2);
            var i2 = Intersection(c1, c3);

            int smallest = 0;
            float[] D = new float[4];
            D[0] = Vector2.Distance(i1[0], i2[0]);
            D[1] = Vector2.Distance(i1[0], i2[1]);
            D[2] = Vector2.Distance(i1[1], i2[0]);
            D[3] = Vector2.Distance(i1[1], i2[1]);

            for(int j = 1; j < 4; j++)
                if(D[smallest] > D[j]) smallest = j;

            return i2[smallest % 2]; //not 100% sure on this part, might be i1 instead?
        }
        #endregion

    }

}
