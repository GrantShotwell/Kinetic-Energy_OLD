using UnityEngine;
using static KineticEnergy.Maths.Methods;

namespace KineticEnergy.Maths {

    /// <summary>Stores data and methods for a line.</summary>
    public class Line {

        /// <summary>ax + by = c;</summary>
        public float A, B, C;

        /// <summary>If the line is horizontal, the equation of the line is X = [a constant].</summary>
        public float x = 0;

        /// <summary>Is the line horizontal?</summary>
        public bool isVertical;

        /// <summary>
        /// Creates a vertical line given A, B, C, and it's x-coordinate.
        /// </summary>
        /// <param name="A">Ax + by = c</param>
        /// <param name="B">ax + By = c</param>
        /// <param name="C">ax + by = C</param>
        /// <param name="x">X = this (horizontal line)</param>
        public Line(float A, float B, float C, float x) {
            isVertical = true;
            this.A = A;
            this.B = B;
            this.C = C;
            this.x = x;
        }

        /// <summary>
        /// Creates a line given A, B, and C in Ax + By = C
        /// </summary>
        /// <param name="A">Ax + by = c</param>
        /// <param name="B">ax + By = c</param>
        /// <param name="C">ax + by = C</param>
        public Line(float A, float B, float C) {
            isVertical = false;
            this.A = A;
            this.B = B;
            this.C = C;
        }

        /// <summary>The slope of the line.</summary>
        public float Slope => -A;

        /// <summary>Angle of the slope.</summary>
        public Angle Angle(AngleType units = AngleType.Degrees) {
            return units == AngleType.Radians ? new Angle(Mathf.Atan(Slope), units) : new Angle(Mathf.Atan(Slope) * Mathf.Rad2Deg, units);
        }

        public float YFromX(float x) => (C - A * x) / B;
        public float XFromY(float y) => isVertical ? x : (C - B * y) / A;

        /// <summary>
        /// Shifts the line left/right by a distance of 'd'.
        /// </summary>
        /// <param name="d">distance to shift the line</param>
        public void ShiftX(float d) {
            if(isVertical) x += d;
            else C += d * A;
        }

        /// <summary>
        /// Shifts the line upwards/downwards by a distance of 'd'.
        /// </summary>
        /// <param name="d">distance to shift the line.</param>
        public void ShiftY(float d) {
            if(!isVertical) C += d * B;
        }

        /// <summary>Returns (x, y) given x.</summary>
        /// <param name="x">the x-coordinate of the point</param>
        public Vector2 PointFromX(float x) {
            return new Vector2(x, YFromX(x));
        }

        /// <summary>Returns (x, y) given y.</summary>
        /// <param name="y">the y-coordinate of the point</param>
        public Vector2 PointFromY(float y) {
            return new Vector2(XFromY(y), y);
        }

        /// <summary>
        /// Finds a point on this line from a start point, distance, and direction.
        /// </summary>
        /// <param name="point">distance from this point</param>
        /// <param name="distance">distance from point 'point'</param>
        /// <param name="direction">direction on the line (does not need to align with the lign)</param>
        /// <returns></returns>
        public Vector2 PointFromDistance(Vector2 point, float distance, Vector2 direction) {
            return PointFromDistance(point, distance, LinearDirection(direction.x - point.x));
        }

        /// <summary>
        /// Finds a point on this line from a start point, distance, and direction.
        /// </summary>
        /// <param name="point">Distance from this point.</param>
        /// <param name="distance">Distance from point 'point'.</param>
        /// <param name="direction">Left = -1, Right = +1.</param>
        /// <returns></returns>
        public Vector2 PointFromDistance(Vector2 point, float distance, int direction) {
            float theta = Angle(AngleType.Radians) * direction;
            var point2 = new Vector2(Mathf.Cos(theta) * distance, Mathf.Sin(theta) * distance);
            point2 = new Vector2(point2.x * direction, point2.y);
            point2 += point;
            return point2;
        }

        /// <summary>
        /// Finds the intersection of this and l2.
        /// </summary>
        /// <returns>Returns the point of intersection.</returns>
        public Vector2 Intersection(Line l2) {
            Line l1 = this;
            if(l1.isVertical != l2.isVertical) {
                if(l1.isVertical) return new Vector2(l1.x, l2.YFromX(l1.x));
                if(l2.isVertical) return new Vector2(l2.x, l1.YFromX(l2.x));
            }
            float delta = l1.A * l2.B - l2.A * l1.B;
            float x = (l2.B * l1.C - l1.B * l2.C) / delta;
            float y = (l1.A * l2.C - l2.A * l1.C) / delta;
            return new Vector2(x, y);
        }
    }

}
