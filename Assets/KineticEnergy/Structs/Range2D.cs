using System;
using UnityEngine;

namespace KineticEnergy.Structs {

    /// <summary>Class for an inclusive Vector2 range for x and y components.</summary>
    [Serializable]
    public struct Range2D {
        /// <summary>Unlimited range.</summary>
        public static Range2D infinite = new Range2D(Range.infinite, Range.infinite);

        /// <summary>Area of 1.</summary>
        public static Range2D half = new Range2D(Range.half, Range.half);

        /// <summary>The range of this vector component.</summary>
        public Range x, y;

        /// <summary>
        /// Creates a new Range2D.
        /// </summary>
        /// <param name="x">Range for the x component.</param>
        /// <param name="y">Range for the y component.</param>
        public Range2D(Range x, Range y) {
            this.x = x;
            this.y = y;
        }

        /// <summary>
        /// Creates a new Range2D.
        /// </summary>
        public Range2D(float x_min, float x_max, float y_min, float y_max) {
            x = new Range(x_min, x_max);
            y = new Range(y_min, y_max);
        }

        /// <summary>
        /// Checks if the given value is within the range.
        /// </summary>
        /// <returns>Returns 'true' if the given value is on the interval [min, max].</returns>
        public bool Contains(Vector2 value) {
            return x.Contains(value.x) && y.Contains(value.y);
        }

        /// <summary>
        /// Applies 'Range.Place(float)' to both components of the given vector to their respective range.
        /// </summary>
        /// <returns>Returns a new Vector2(x.Place(value.x), y.Place(value.y))</returns>
        public Vector2 Place(Vector2 value) {
            return new Vector2(
                x.Place(value.x),
                y.Place(value.y)
            );
        }

        public Vector2 PlaceOutside(Vector2 value) {
            return new Vector2(
                x.PlaceOutside(value.x),
                y.PlaceOutside(value.y)
            );
        }

        public Vector2 Random() {
            return new Vector2(x.Random, y.Random);
        }

        public Vector2 RandomSpread() {
            return new Vector2(x.RandomSpread, y.RandomSpread);
        }

        public override string ToString() {
            return "[" + x + ", " + y + "]";
        }
    }

}
