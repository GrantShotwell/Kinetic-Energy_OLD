using System;
using UnityEngine;

namespace KineticEnergy.Structs {

    /// <summary>Class for an inclusive range.</summary>
    [Serializable]
    public struct Range {

        /// <summary>Unlimited range.</summary>
        public static Range infinite = new Range(float.NegativeInfinity, float.PositiveInfinity);
        /// <summary>Size of 1: [-0.5, +0.5]</summary>
        public static Range half = new Range(-0.5f, 0.5f);
        /// <summary>Size of 2: [-1, +1]</summary>
        public static Range full = new Range(-1.0f, 1.0f);

        public float Size => max - min;
        public float Center => min + Size / 2;
        public float Random => UnityEngine.Random.Range(min, max);
        public float RandomSpread => (UnityEngine.Random.value * UnityEngine.Random.Range(-1.0f, +1.0f) / 2 + 0.5f) * (max - min) + min;

        public float min, max;

        public Range(float minimum = float.NegativeInfinity, float maximum = float.PositiveInfinity) {
            min = minimum;
            max = maximum;
        }

        /// <summary>Adds twice the given value to the size.</summary>
        /// <param name="value">The value to add to the size.</param>
        /// <returns>Returns a new range with the new size and the same center.</returns>
        public Range ChangedByAmount(float value) {
            value /= 2;
            return new Range(min - value, max + value);
        }

        /// <summary>Multiplies the size by the given value.</summary>
        /// <param name="value">The value to multiply the size by.</param>
        /// <returns>Returns a new range with the new size and same center.</returns>
        public Range ChangeByFactor(float value) => ChangedByAmount(Size * value);

        /// <summary>Checks if the given value is within the range.</summary>
        /// <returns>Returns 'true' if the given value is on the interval [min, max].</returns>
        public bool Contains(float value) => min <= value && value <= max;

        /// <summary>Finds the value's distance to the closest edge of the range.</summary>
        /// <param name="value"></param>
        public float EdgeDistance(float value) {
            float dMin = Math.Abs(value - min);
            float dMax = Math.Abs(value - max);
            return dMin < dMax ? dMin : dMax;
        }

        /// <summary>Checks if the given range is completely encompassed by this range.</summary>
        /// <param name="range"></param>
        public bool Contains(Range range) {
            return EdgeDistance(range.Center) >= range.Size / 2;
        }

        public bool Overlaps(Range range) {
            return range.min <= max && range.max >= min;
        }

        /// <summary>Places the given value to the nearest value on the interval [min, max].</summary>
        /// <returns>If the given value is lesser/greater than min/max then it returns min/max. Otherwise, the value is unchanged.</returns>
        public float Place(float value) {
            if(min > value) value = min;
            if(max < value) value = max;
            return value;
        }

        /// <summary>Places the given value to either the minimum or maximum: whichever is closer.</summary>
        /// <returns>Returns the closest min/max to the given value.</returns>
        public float PlaceOutside(float value) => Contains(value) ? value : Math.Abs(min - value) > Math.Abs(max - value) ? min : max;

        public override string ToString() => "[" + min + ", " + max + "]";

        public static Range operator +(Range range, float value) { return new Range(range.min + value, range.max + value); }
        public static Range operator -(Range range, float value) { return new Range(range.min - value, range.max - value); }
        public static Range operator *(Range range, float value) { return new Range(range.min * value, range.max * value); }
        public static Range operator /(Range range, float value) { return new Range(range.min / value, range.max / value); }
        public static Range operator %(Range range, float value) { return new Range(range.min % value, range.max % value); }
        public static Range operator +(Range left, Range right) { return new Range(left.min + right.min, left.max + right.max); }
        public static Range operator -(Range left, Range right) { return new Range(left.min - right.min, left.max - right.max); }
        public static Range operator *(Range left, Range right) { return new Range(left.min * right.min, left.max * right.max); }
        public static Range operator /(Range left, Range right) { return new Range(left.min / right.min, left.max / right.max); }
        public static Range operator %(Range left, Range right) { return new Range(left.min % right.min, left.max % right.max); }
        public static bool operator ==(Range left, Range right) { return left.min == right.min && left.max == right.max; }
        public static bool operator !=(Range left, Range right) { return left.min != right.min && left.max != right.max; }

        public static explicit operator Vector2(Range range) { return new Vector2(range.min, range.max); }

        public override bool Equals(object obj) {
            if(!(obj is Range)) return false;

            var range = (Range)obj;
            return min == range.min &&
                   max == range.max;
        }

        public override int GetHashCode() {
            var hashCode = -897720056;
            hashCode = hashCode * -1521134295 + min.GetHashCode();
            hashCode = hashCode * -1521134295 + max.GetHashCode();
            return hashCode;
        }

    }

}
