using System;
using KineticEnergy.Structs;
using UnityEngine;

namespace KineticEnergy.Maths {

    /// <summary>Stores data and methods for a box with a position.</summary>
    [Serializable]
    public struct Box {
        public Range x, y;

        public Vector2 Center => new Vector2(x.Center, y.Center);

        /// <summary>Clockwise, starting from bottom-left.</summary>
        public Vector2[] Points => new Vector2[] {
                        new Vector2(x.min, y.min),
                        new Vector2(x.min, y.max),
                        new Vector2(x.max, y.max),
                        new Vector2(x.max, y.min)
                    };

        /// <summary>
        /// Creates a new box given two corners (any order).
        /// </summary>
        /// <param name="corner1">A corner of the box.</param>
        /// <param name="corner2">A corner of the box.</param>
        public Box(Vector2 corner1, Vector2 corner2) {
            x = new Range(
                corner1.x < corner2.x ? corner1.x : corner2.x,
                corner1.x > corner2.x ? corner1.x : corner2.x
            );
            y = new Range(
                corner1.y < corner2.y ? corner1.y : corner2.y,
                corner1.y > corner2.y ? corner1.y : corner2.y
            );
        }

        public Box(Range x, Range y) {
            this.x = x;
            this.y = y;
        }

        /// <summary>
        /// Checks if the given point lies within the box.
        /// </summary>
        /// <param name="point">Point to test.</param>
        /// <returns>Returns true if the point lies within the box.</returns>
        public bool Contains(Vector2 point) {
            return x.Contains(point.x) && y.Contains(point.y);
        }

        public bool Contains(Box box) {
            return x.Contains(box.x) && y.Contains(box.y);
        }

        public bool Overlaps(Box box) {
            return x.Overlaps(box.x) && y.Overlaps(box.y);
        }

        /// <summary>
        /// If nessesary, shrinks the given box to fit within this box. The center is kept the same.
        /// </summary>
        /// <param name="box">Box to place inside.</param>
        /// <returns>Returns a new box that fits inside the original.</returns>
        public Box Place(Box box) {
            float deltaLt = box.x.min - x.min;
            float deltaRt = x.max - box.x.max;
            float deltaDn = box.y.min - y.min;
            float deltaUp = y.max - box.y.max;

            float deltaHz = deltaLt > deltaRt ? deltaLt : deltaRt;
            float deltaVt = deltaDn > deltaUp ? deltaUp : deltaDn;

            box.x = box.x.ChangedByAmount(deltaHz);
            box.y = box.y.ChangedByAmount(deltaVt);

            return box;
        }

        /// <summary>
        /// Gives two boxes that are the original box split at the given y value.
        /// </summary>
        /// <param name="value">Y value to split the box at.</param>
        /// <returns>Returns an array of two boxes.</returns>
        public Box[] SplitAtY(float value) {
            return new Box[] {
                new Box( //Top
                    new Vector2(x.min, value),
                    new Vector2(x.max, y.max)
                ),
                new Box( //Bottom
                    new Vector2(x.min, y.min),
                    new Vector2(x.max, value)
                )
            };
        }

        /// <summary>
        /// Gives two boxes that are the original box split at the given x value.
        /// </summary>
        /// <param name="value">X value to split the box at.</param>
        /// <returns>Returns an array of two boxes.</returns>
        public Box[] SplitAtX(float value) {
            return new Box[] {
                new Box( //Left
                    new Vector2(x.min, y.min),
                    new Vector2(value, y.max)
                ),
                new Box( //Right
                    new Vector2(value, y.min),
                    new Vector2(x.max, y.max)
                )
            };
        }

        #region Operators
        public static bool operator ==(Box left, Box right) { return left.x == right.x && left.y == right.y; }
        public static bool operator !=(Box left, Box right) { return left.x != right.x && left.y != right.y; }

        #region misc.
        public override bool Equals(object obj) {
            return base.Equals(obj);
        }

        public override int GetHashCode() {
            return base.GetHashCode();
        }

        public override string ToString() {
            return base.ToString();
        }
        #endregion
        #endregion
    }

}
