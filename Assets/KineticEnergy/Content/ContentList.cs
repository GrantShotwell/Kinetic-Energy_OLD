using System;
using System.Collections;
using System.Collections.Generic;

namespace KineticEnergy.Content {

    /// <summary>Represents a collection of <see cref="Content{T}"/> with an unknown "T".</summary>
    public abstract class ContentList {

        /// <summary>The base class for all items.</summary>
        public Type Type { get; }

        /// <summary>The <see cref="ContentList{T}.List"/> but of unknown type "T".</summary>
        public abstract IList<Content> GenericList { get; }

        /// <summary>Creates a new <see cref="ContentList"/> with the given <see cref="Type"/>.</summary>
        /// <param name="type"></param>
        public ContentList(Type type) => Type = type;

        /// <summary>Adds the given item to this collection.</summary>
        public abstract void Add(Content content);

    }

    /// <summary>Represents a collection of <see cref="Content{T}"/> with a known "T".</summary>
    /// <typeparam name="T">The generic type of <see cref="Content{T}"/>.</typeparam>
    public class ContentList<T> : ContentList where T : class {

        /// <summary>The collection of <see cref="Content{T}"/> where "T" is known.</summary>
        public List<Content<T>> List { get; } = new List<Content<T>>();
        /// <summary>The <see cref="ContentList{T}.List"/> but of unknown type "T".</summary>
        public override IList<Content> GenericList { get; }

        /// <summary>Creates a new <see cref="ContentList{T}"/>.</summary>
        public ContentList() : base(typeof(T)) => GenericList = new GenericListWrap(List);

        /// <summary>Adds the given item to this collection.</summary>
        public override void Add(Content content) => List.Add(content.MakeExplicit<T>());
        /// <summary>Adds the given item to this collection.</summary>
        public void Add(Content<T> content) => List.Add(content);
        /// <summary>Adds the given item to this collection.</summary>
        public void Add<U>(string origin) where U : T => List.Add(new Content<T>(typeof(U), origin));

        public class GenericListWrap : IList<Content> {

            List<Content<T>> List { get; }

            public GenericListWrap(List<Content<T>> list) => List = list;

            public Content this[int index] { get => List[index]; set => List[index] = value.MakeExplicit<T>(); }

            public int Count => List.Count;

            public bool IsReadOnly => false;

            public bool Contains(Content item) => List.Contains(item.MakeExplicit<T>());
            public int IndexOf(Content item) => List.IndexOf(item.MakeExplicit<T>());
            public void Clear() => List.Clear();

            public void Add(Content item) => List.Add(item.MakeExplicit<T>());
            public void Insert(int index, Content item) => List.Insert(index, item.MakeExplicit<T>());

            public bool Remove(Content item) => List.Remove(item.MakeExplicit<T>());
            public void RemoveAt(int index) => List.RemoveAt(index);

            public void CopyTo(Content[] array, int index) {
                for(int i = 0; i < Count; i++)
                    array[index++] = List[i];
            }

            public IEnumerator<Content> GetEnumerator() => new Enumerator(this);
            IEnumerator IEnumerable.GetEnumerator() => List.GetEnumerator();

            public class Enumerator : IEnumerator<Content> {

                private int index;
                private readonly GenericListWrap wrapper;

                public Enumerator(GenericListWrap wrapper) {
                    this.wrapper = wrapper;
                    Reset();
                }

                public Content Current => wrapper[index];
                object IEnumerator.Current => Current;

                public bool MoveNext() => ++index < wrapper.Count;
                public void Reset() => index = -1;

                public void Dispose() { /* Nothing to dispose of. */ }

            }

        }

    }

}
