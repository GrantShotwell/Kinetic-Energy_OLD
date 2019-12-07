using KineticEnergy.Maths;
using UnityEngine;

namespace KineticEnergy.Maths {

    /// <summary>Ties together a value and a unit for an angle.</summary>
    public struct Angle {

        /// <summary>The value of the angle with unknown units.</summary>
        public float value;

        /// <summary>The units of the angle.</summary>
        public AngleType type;

        /// <summary>
        /// Creates a new angle
        /// </summary>
        /// <param name="value">The value of the angle in the units given.</param>
        /// <param name="type">The units of the angle.</param>
        public Angle(float value, AngleType type = AngleType.Radians) {
            this.value = value;
            this.type = type;
        }

        public Angle(Angle original) {
            value = original.value;
            type = original.type;
        }

        /// <summary>Changes the units and applies the conversion.</summary>
        /// <param name="type">The new units of the angle.</param>
        public Angle ConvertType(AngleType type) {
            if(this.type != type) {
                if(type == AngleType.Degrees)
                    value *= Mathf.Rad2Deg;
                else if(type == AngleType.Radians)
                    value *= Mathf.Deg2Rad;
                this.type = type;
            }
            return this;
        }

        /// <summary>Use this only if you don't know the units for sure. If you do, then use 'Angle.value'.</summary>
        public float Radians =>
            type == AngleType.Radians ? value
            : value * Mathf.Deg2Rad;

        /// <summary>Use this only if you don't know the units for sure. If you do, then use 'Angle.value'.</summary>
        public float Degrees =>
            type == AngleType.Degrees ? value
            : value * Mathf.Rad2Deg;

        public Angle Normalize() {
            float circle = type == AngleType.Degrees ? degRight : radRight;
            value %= circle;
            return this;
        }

        public Angle Center() {
            float circle = type == AngleType.Degrees ? degRight : radRight;
            value %= circle;
            float halfCircle = circle / 2f;
            if(value > halfCircle) value -= circle;
            if(value < halfCircle) value += circle;
            return this;
        }

        public Angle Mirror() {
            value += type == AngleType.Degrees ? degUp : radUp;
            return this;
        }

        public Angle Abs() => value < 0 ? new Angle(-value, type) : new Angle(this);

        /// <summary>Casts a <see cref="float"/> into a new <see cref="Angle"/> with units of radians.</summary>
        public static implicit operator Angle(float value) { return new Angle(value, AngleType.Radians); }
        /// <summary>Casts an <see cref="Angle"/> into a <see cref="float"/> by using <see cref="Radians"/>.</summary>
        public static implicit operator float(Angle angle) { return angle.Radians; }
        /// <summary>Casts an <see cref="int"/> into a new <see cref="Angle"/> with units of degrees.</summary>
        public static implicit operator Angle(int value) { return new Angle(value, AngleType.Degrees); }
        /// <summary>Casts an <see cref="Angle"/> into an <see cref="int"/> by using <see cref="Degrees"/>.</summary>
        public static explicit operator int(Angle angle) { return (int)angle.Degrees; }

        public static Angle operator +(Angle left, Angle right) { return new Angle(left.Radians + right.Radians, AngleType.Radians).ConvertType(left.type); }
        public static Angle operator -(Angle left, Angle right) { return new Angle(left.Radians - right.Radians, AngleType.Radians).ConvertType(left.type); }
        public static Angle operator *(Angle left, Angle right) { return new Angle(left.Radians * right.Radians, AngleType.Radians).ConvertType(left.type); }
        public static Angle operator /(Angle left, Angle right) { return new Angle(left.Radians / right.Radians, AngleType.Radians).ConvertType(left.type); }
        public static Angle operator %(Angle left, Angle right) { return new Angle(left.Radians % right.Radians, AngleType.Radians).ConvertType(left.type); }
        public static bool operator >(Angle left, Angle right) { return left.Radians > right.Radians; }
        public static bool operator <(Angle left, Angle right) { return left.Radians < right.Radians; }


        public override string ToString() {
            return value + (
                type == AngleType.Degrees ?
                " degrees" : " radians"
            );
        }


        /// <summary>1/2 PI</summary>
        public static float radUp = Mathf.PI / 2f;
        /// <summary>1 PI</summary>
        public static float radLeft = Mathf.PI;
        /// <summary>2/3 PI</summary>
        public static float radDown = 2f * Mathf.PI / 3f;
        /// <summary>2 PI</summary>
        public static float radRight = 2f * Mathf.PI;

        public static float degUp = 90.0f;
        public static float degLeft = 180.0f;
        public static float degDown = 270.0f;
        public static float degRight = 360.0f;

        public static float RadDirection(Direction direction) {
            switch(direction) {
                case Direction.Up: return radUp;
                case Direction.Left: return radLeft;
                case Direction.Down: return radDown;
                case Direction.Right: return radRight;
                default: return 0f;
            }
        }

        public static float DegDirection(Direction direction) {
            switch(direction) {
                case Direction.Up: return degUp;
                case Direction.Left: return degLeft;
                case Direction.Down: return degDown;
                case Direction.Right: return degRight;
                default: return 0f;
            }
        }
    }

}
