using System;
using System.Collections;
using System.Collections.Generic;

using UnityEngine;

namespace KineticEnergy.Enumeration {

    /// <summary>Represents a 'box' of <see cref="Vector3Int"/>s from a given minimum to a given maximum.</summary>
    public class BoxOfPoints : IEnumerable<Vector3Int>, IEnumerator<Vector3Int> {

        /// <summary>The inclusive minimum point of the box.</summary>
        Vector3Int Min { get; }

        /// <summary>The inclusive maximum point of the box.</summary>
        Vector3Int Max { get; }

        /// <summary>The starting value of <see cref="Current"/> for <see cref="IEnumerator.Reset()"/>.</summary>
        Vector3Int StartPoint => Min - new Vector3Int(0, 0, 1);

        /// <summary>Creates a new <see cref="BoxOfPoints"/> with the given size being the maximum and the minimum being the origin.</summary>
        /// <param name="size">The size of the box.</param>
        public BoxOfPoints(Vector3Int size) {
            Min = Vector3Int.zero;
            Max = size - Vector3Int.one;
            Current = StartPoint;
        }

        /// <summary>Creates a new <see cref="BoxOfPoints"/> with the given </summary>
        /// <param name="min">The value of <see cref="Min"/>.</param>
        /// <param name="max">The value of <see cref="Max"/>.</param>
        public BoxOfPoints(Vector3Int min, Vector3Int max) {
            Min = min;
            Max = max;
            Current = StartPoint;
        }

        /// <summary>Implementation of <see cref="IEnumerator{T}.Current"/>.</summary>
        public Vector3Int Current { get; private set; }
        /// <summary>Implementation of <see cref="IEnumerator.Current"/>.</summary>
        object IEnumerator.Current => Current;

        /// <summary>Implementation of <see cref="IEnumerable{T}.GetEnumerator()"/>.</summary>
        public IEnumerator<Vector3Int> GetEnumerator() {
            Reset();
            return this;
        }
        /// <summary>Implementation of <see cref="IEnumerable.GetEnumerator()"/>.</summary>
        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

        /// <summary>Implementation of <see cref="IEnumerator.MoveNext()"/>.</summary>
        public bool MoveNext() {

            // (same functionality as:)  // // // // //
            // for(int x = min.x; x <= max.x; x++)   //
            //  for(int y = min.y; y <= max.y; y++)  //
            //   for(int z = min.z; z <= max.z; z++) //

            Vector3Int point = Current;
            Vector3Int min = Min, max = Max;
            bool moved = true;

            if(point.z < max.z) {
                point.z++;
            } else {
                point.z = min.z;
                if(point.y < max.y) {
                    point.y++;
                } else {
                    point.y = min.y;
                    if(point.x < max.x) {
                        point.x++;
                    } else {
                        moved = false;
                    }
                }
            }

            Current = point;
            return moved;

        }

        /// <summary>Implementation of <see cref="IEnumerator.Reset()"/>.</summary>
        public void Reset() => Current = StartPoint;

        /// <summary>Implementation of <see cref="IDisposable.Dispose()"/>.</summary>
        public void Dispose() { /* Nothing to dispose of. */ }

    }

}
