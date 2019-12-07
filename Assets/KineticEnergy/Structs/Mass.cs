using System;
using System.Collections.Generic;
using UnityEngine;

namespace KineticEnergy.Structs {

    /// <summary>A magnitude and center of mass.</summary>
    [Serializable]
    public struct Mass {

        /// <summary>The magnitude of this mass in the arbitrary units of grams.</summary>
        public long magnitude;
        /// <summary>The local position of this mass.</summary>
        public Vector3 position;

        /// <summary>Creates a new <see cref="Mass"/> with the given properties.</summary>
        /// <param name="magnitude">(<see cref="magnitude"/>) The magnitude of this mass in the arbitrary units of grams.</param>
        /// <param name="position">(<see cref="position"/>) The local position of this mass.</param>
        public Mass(long magnitude, Vector3 position) {
            this.magnitude = magnitude;
            this.position = position;
        }

        #region Shorthands

        /// <summary>Shorthand for a new <see cref="Mass"/> with a <see cref="magnitude"/> and <see cref="position"/> of zero.</summary>
        /// <remarks>Reccomended usecase is for setting a default value in like a constructor or something.</remarks>
        public static Mass zero = new Mass(0, Vector3.zero);
        /// <summary>Shorthand for a new <see cref="Mass"/> with a <see cref="magnitude"/> of one and a <see cref="position"/> of zero.</summary>
        /// <remarks>Although not reccomended, this is useful in combination with <see cref="operator *(Mass, long)"/>.</remarks>
        public static Mass one = new Mass(1, Vector3.zero);

        #endregion

        /// <summary>If the given <see cref="Mass"/> is 'within' this, then what is the new mass if the given mass would be moved?</summary>
        /// <param name="mass">A <see cref="Mass"/> given to be within this.</param>
        /// <param name="shift">Delta vector to shift the given mass by.</param>
        public Mass PartShifted(Mass mass, Vector3 shift) => this - mass + (mass + shift);

        #region Operators

        #region Addition

        //TODO: The change from ulong to long made the overflow and underflow checks incorrect (ex. adding a negative).

        // - Add Mass - //
        /// <summary>An addition operator on the <see cref="magnitude"/> and <see cref="position"/> (weighted).</summary>
        public static Mass operator +(Mass left, Mass right) {
            var resultingMagnitude = left.magnitude + right.magnitude;
            var resultingPosition = Vector3.Lerp(left.position, right.position,
                resultingMagnitude == 0 ? .5f : (float)(right.magnitude / (double)resultingMagnitude));
            return new Mass(resultingMagnitude, resultingPosition);
        }

        // - Add Value - //
        /// <summary>An addition operator on the <see cref="magnitude"/>.</summary>
        public static Mass operator +(Mass mass, long value) {
            var result = mass.magnitude + value;
            return new Mass(result, mass.position);
        }

        // - Add Vector - //
        /// <summary>Shifts the center of mass by the given vector through addition.</summary>
        public static Mass operator +(Mass mass, Vector3 vector) => new Mass(mass.magnitude, mass.position + vector);

        #endregion

        #region Subtraction

        // - Subtract Mass - //
        /// <summary>A subtraction operator on the <see cref="magnitude"/> and the <see cref="position"/> (weighted).</summary>
        public static Mass operator -(Mass left, Mass right) {
            var resultingMagnitude = left.magnitude - right.magnitude;
            var resultingPosition = Vector3.Lerp(left.position, right.position,
                resultingMagnitude == 0 ? .5f : (float)(left.magnitude / (double)resultingMagnitude));
            return new Mass(resultingMagnitude, resultingPosition);
        }

        // - Subtract Value - //
        /// <summary>A subtraction operator on the <see cref="magnitude"/>.</summary>
        public static Mass operator -(Mass mass, long value) {
            var result = mass.magnitude - value;
            return new Mass(result, mass.position);
        }

        // - Subtract Vector - //
        /// <summary>Shifts the center of mass by the given vector through subtraction.</summary>
        public static Mass operator -(Mass mass, Vector3 vector) => new Mass(mass.magnitude, mass.position - vector);

        // - Negate Mass - //
        /// <summary>Negates the given <see cref="Mass"/> so that "<c>mass + (-mass) == <see cref="zero"/></c>" is true.</summary>
        public static Mass operator -(Mass mass) => new Mass { magnitude = -mass.magnitude, position = mass.position };
        /// <summary>Negates the given <see cref="Mass"/> so that "<c>mass + Negate(mass) == mass - mass.position</c>" is true.</summary>
        public static Mass Negate(Mass mass) => new Mass { magnitude = -mass.magnitude, position = -mass.position };

        #endregion

        #region Multiplication & Division

        // - Multiply by Value - //
        /// <summary>A multiplication operator on the <see cref="magnitude"/>.</summary>
        public static Mass operator *(Mass mass, long value) {
            var result = mass.magnitude * value;
            if(result < mass.magnitude || result < value)
                result = long.MaxValue;
            return new Mass(result, mass.position);
        }

        // - Divide by Value - //
        /// <summary>A division operator on the <see cref="magnitude"/>.</summary>
        public static Mass operator /(Mass mass, long value) {
            var result = mass.magnitude / value;
            if(result > mass.magnitude || result > value)
                result = long.MaxValue;
            return new Mass(result, mass.position);
        }

        #endregion

        #region Comparisions

        /// <summary>Compares the two <see cref="magnitude"/>s.</summary>
        public static bool operator <(Mass left, Mass right) => left.magnitude < right.magnitude;
        /// <summary>Compares the two <see cref="magnitude"/>s.</summary>
        public static bool operator >(Mass left, Mass right) => left.magnitude > right.magnitude;

        /// <summary>Checks if both given <see cref="Mass"/> objects have equal <see cref="magnitude"/>s and equal <see cref="position"/>s.</summary>
        public static bool operator ==(Mass mass1, Mass mass2) => mass1.magnitude == mass2.magnitude && mass1.position == mass2.position;
        /// <summary>Checks if both given <see cref="Mass"/> objects have unequal <see cref="magnitude"/>s or unequal <see cref="position"/>s.</summary>
        public static bool operator !=(Mass mass1, Mass mass2) => mass1.magnitude != mass2.magnitude || mass1.position != mass2.position;

        /// <summary>Auto-generated by Visual Studio.</summary>
        public override bool Equals(object obj) {
            return obj is Mass mass &&
                   magnitude == mass.magnitude &&
                   position.Equals(mass.position);
        }

        /// <summary>Auto-generated by Visual Studio.</summary>
        public override int GetHashCode() {
            var hashCode = 813208763;
            hashCode = hashCode * -1521134295 + magnitude.GetHashCode();
            hashCode = hashCode * -1521134295 + EqualityComparer<Vector3>.Default.GetHashCode(position);
            return hashCode;
        }

        #endregion

        /// <summary>"{<see cref="magnitude"/>}g at {<see cref="position"/>}"</summary>
        public override string ToString() {
            return string.Format("{0}g at {1}", magnitude, position);
        }

        #endregion

    }

}
