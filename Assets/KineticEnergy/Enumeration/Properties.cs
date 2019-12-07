using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace KineticEnergy.Enumeration {

    /// <summary>Represents a group of an <see cref="object"/>'s fields/properties for reading only.</summary>
    /// <typeparam name="T">The return type of the given fields/properties.</typeparam>
    public class Properties<T> : IEnumerable<T> {

        /// <summary><see cref="Func{T}"/>s that get the value of a field/property.</summary>
        private Func<T>[] Gets { get; }

        /// <summary>Gets the value of the field/property at the given index.</summary>
        /// <param name="index">Index of the field/property.</param>
        public T this[int index] => Gets[index].Invoke();

        /// <summary>The number of fields/properties.</summary>
        public int Count { get; }

        /// <summary>Creates a new <see cref="Properties{T}"/> for fields with the given values.</summary>
        /// <param name="values">The values of the fields to store.</param>
        /// <remarks>If you wish to have values that update, or just more control over the return,
        /// use <see cref="Properties{T}.Properties(Func{T}[])"/> instead.</remarks>
        public Properties(params T[] values) {
            var gets = Gets = new Func<T>[(Count = values.Length)];
            int current = 0; foreach(var value in values)
                gets[current++] = () => value;
        }

        /// <summary>Creates a new <see cref="Properties{T}"/> for properties with the given 'get' functions.</summary>
        /// <param name="properties">All of the <see cref="Func{T}"/>s that return the value of the property.</param>
        public Properties(params Func<T>[] properties) {
            Gets = properties;
            Count = properties.Length;
        }

        /// <summary>Implementation of <see cref="IEnumerable{T}.GetEnumerator()"/></summary>
        public IEnumerator<T> GetEnumerator() => new Enumerator(this);
        /// <summary>Implementation of <see cref="IEnumerable.GetEnumerator()"/>.</summary>
        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

        /// <summary>An <see cref="IEnumerator{T}"/> for <see cref="Properties{T}"/>.</summary>
        public class Enumerator : IEnumerator<T> {

            /// <summary>The index of the current item.</summary>
            private int index = -1;
            /// <summary>The <see cref="Properties{T}"/> reference.</summary>
            Properties<T> Properties { get; }

            /// <summary>Creates a new <see cref="Enumerator"/> with the given arguments.</summary>
            /// <param name="properties">The <see cref="Properties{T}"/> to enumerate through.</param>
            public Enumerator(Properties<T> properties) => Properties = properties
                ?? throw new ArgumentNullException(nameof(properties));

            /// <summary>Implementation of <see cref="IEnumerator{T}.Current"/>.</summary>
            public T Current => Properties[index];
            /// <summary>Implementation of <see cref="IEnumerator.Current"/>.</summary>
            object IEnumerator.Current => Current;

            /// <summary>Implementation of <see cref="IEnumerator.MoveNext()"/>.</summary>
            public bool MoveNext() => ++index < Properties.Count;

            /// <summary>Implementation of <see cref="IEnumerator.Reset()"/>.</summary>
            public void Reset() => index = -1;

            /// <summary>Implementation of <see cref="IDisposable.Dispose()"/>.</summary>
            public void Dispose() { /* Nothing to dispose of. */ }

        }

    }

}
