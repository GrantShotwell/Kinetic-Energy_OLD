using System;
using System.Collections;
using System.Collections.Generic;

namespace KineticEnergy.Enumeration {

    /// <summary>Enumerator enumerator!</summary>
    public class EnumerationQueue<T> : IEnumerable<T>, IEnumerator<T> {

        IEnumerable<T>[] Enumerables { get; }
        Queue<IEnumerator<T>> Enumerators { get; }
        IEnumerator<T> CurrentEnumerator { get; set; }

        void MoveEnumerator() => CurrentEnumerator = Enumerators.Dequeue();

        /// <summary>Creates a new <see cref="EnumerationQueue{T}"/>
        /// which will enumerate through each of the given <see cref="IEnumerable{T}"/>'s items in order.</summary>
        public EnumerationQueue(params IEnumerable<T>[] enumerables) {
            Enumerators = new Queue<IEnumerator<T>>(enumerables.Length);
            Enumerables = enumerables;
        }

        /// <summary>Implementation of <see cref="IEnumerable{T}.GetEnumerator()"/>.</summary>
        public IEnumerator<T> GetEnumerator() { Reset(); return this; }
        /// <summary>Implementation of <see cref="IEnumerable.GetEnumerator()"/>.</summary>
        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

        /// <summary>Implementation of <see cref="IEnumerator{T}.Current"/>.</summary>
        public T Current => CurrentEnumerator.Current;
        /// <summary>Implementation of <see cref="IEnumerator{T}.Current"/>.</summary>
        object IEnumerator.Current => Current;

        /// <summary>Implementation of <see cref="IEnumerator.MoveNext()"/>.</summary>
        public bool MoveNext() {
            if(CurrentEnumerator == null) {
                if(Enumerators.Count > 0) MoveEnumerator();
                else return false;
            }

            if(CurrentEnumerator.MoveNext()) {
                return true;
            } else {
                if(Enumerators.Count > 0) {
                    MoveEnumerator();
                    return MoveNext();
                } else return false;
            }

        }

        /// <summary>Implementation of <see cref="IEnumerator.Reset()"/>.</summary>
        public void Reset() {
            foreach(IEnumerable<T> enumerable in Enumerables)
                Enumerators.Enqueue(enumerable.GetEnumerator());
            CurrentEnumerator = Enumerators.Dequeue();
        }

        /// <summary>Implementation of <see cref="IDisposable.Dispose()"/>.</summary>
        public void Dispose() {
            foreach(IEnumerator<T> enumerator in Enumerators)
                enumerator.Dispose();
        }

    }

}
