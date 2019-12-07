using System;
using System.Collections;
using System.Collections.Generic;

namespace KineticEnergy.Structs {

    /// <summary>Represents a series of 32 <see cref="bool"/>s held within an <see cref="int"/>, but accessed like an array.</summary>
    public struct Mask32 : IEnumerable<bool> {

        /// <summary>The only value held within <see cref="Mask32"/>.</summary>
        public int value;

        /// <summary>The (n+1)th digit of the binary number <see cref="value"/>.</summary>
        /// <param name="index">The index of the digit from the right.</param>
        public bool this[int index] {
            get => (value >> index & 1) == 1;
            set {
                int mask = 1 << index;
                bool bit = (this.value & mask) == 1;
                if(bit != value) {
                    if(bit) this.value -= mask;
                    else this.value += mask;
                }
            }
        }

        /// <summary>Gets a <see cref="Mask32"/> with every value false, except for at the given index which is
        /// instead copied from this <see cref="Mask32"/>.</summary>
        /// <param name="index">Index of this <see cref="Mask32"/> to get.</param>
        public Mask32 GetCulled(int index) => value & 1 << index;

        /// <summary>Shorthand for "<c>new <see cref="Mask32"/> { value = value };</c>"</summary>
        /// <param name="value">The <see cref="value"/> of the new <see cref="Mask32"/>.</param>
        public static implicit operator Mask32(int value) => new Mask32 { value = value };
        /// <summary>Shorthand for "<c>mask.value</c>".</summary>
        /// <param name="mask">The <see cref="Mask32"/> to get the <see cref="value"/> from.</param>
        public static implicit operator int(Mask32 mask) => mask.value;

        /// <summary>Converts this <see cref="Mask32"/> into a useful string form.</summary>
        /// <returns>Returns <see cref="value"/> in binary form with a few extra zeros beforehand.</returns>
        public override string ToString() {

            //Get value in binary.
            string num = Convert.ToString(value, 2);

            //Add zeros beforehand so there are always 32 digits.
            var count = 32 - num.Length;
            char[] zeros = new char[count];
            for(var i = 0; i < count; i++) zeros[i] = '0';
            num = new string(zeros) + num;

            //Return the result.
            return num;

        }

        /// <summary>Implementation of <see cref="IEnumerable{T}.GetEnumerator()"/>.</summary>
        IEnumerator<bool> IEnumerable<bool>.GetEnumerator() => new BoolSeries(this);
        /// <summary>Implementation of <see cref="IEnumerable.GetEnumerator()"/>.</summary>
        IEnumerator IEnumerable.GetEnumerator() => new BoolSeries(this);

        /// <summary>An <see cref="IEnumerator{T}"/> for all values of a <see cref="Mask32"/>.</summary>
        private class BoolSeries : IEnumerator<bool> {

            /// <summary>The series of <see cref="bool"/>s being enumerated through.</summary>
            public Mask32 Mask { get; }

            /// <summary>The index of the current <see cref="bool"/> in the <see cref="Mask"/>.</summary>
            private int index = -1;

            /// <summary>Creates a new <see cref="BoolSeries"/> with the given <see cref="Mask32"/>.</summary>
            public BoolSeries(Mask32 mask) => Mask = mask;

            /// <summary>Implementation of <see cref="IEnumerator{T}.Current"/>.</summary>
            public bool Current => Mask[index];
            /// <summary>Implementation of <see cref="IEnumerator.Current"/>.</summary>
            object IEnumerator.Current => Mask[index];

            /// <summary>Implementation of <see cref="IEnumerator.MoveNext()"/>.</summary>
            public bool MoveNext() => ++index < 32;

            /// <summary>Implementation of <see cref="IEnumerator.Reset()"/>.</summary>
            public void Reset() => index = -1;

            /// <summary>Implementation of <see cref="IDisposable.Dispose()"/>.</summary>
            public void Dispose() { /* Nothing to dispose of. */}
        }

    }

}
