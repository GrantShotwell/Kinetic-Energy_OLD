using System;

namespace KineticEnergy.Content {

    /// <summary>A generic version of <see cref="Content{T}"/>.</summary>
    public struct Content {

        /// <summary>The class this content represents.</summary>
        public Type Type { get; }

        /// <summary>The given name of the <see cref="Assembly"/> this <see cref="System.Type"/> came from.</summary>
        public string Origin { get; }

        /// <summary>Creates a new <see cref="Content"/> with the given properties.</summary>
        public Content(Type type, string origin) {
            Type = type;
            Origin = origin;
        }

        /// <summary>Defines the base for a new <see cref="Content{T}"/>.</summary>
        /// <exception cref="ArgumentException">Thrown when <see cref="Type"/> is not assignable to given <typeparamref name="T"/>.</exception>
        public Content<T> MakeExplicit<T>() where T : class => new Content<T>(Type, Origin);

    }

    /// <summary>Stores a <see cref="System.Type"/> variable along with the name of the directory it came from.</summary>
    /// <typeparam name="T">The base type for this content. (ex. <see cref="ClientBehaviour"/>, <see cref="BlockPreview"/>, etc.)</typeparam>
    /// <remarks>Implicitly casts to <see cref="System.Type"/>.</remarks>
    public struct Content<T> where T : class {

        /// <summary>The class this content represents.</summary>
        public Type Type { get; }

        /// <summary>The given name of the <see cref="Assembly"/> this <see cref="System.Type"/> came from.</summary>
        public string Origin { get; }

        /// <summary>Creates a new <see cref="Content"/> with the given properties.</summary>
        /// <exception cref="ArgumentException">Thrown when given <paramref name="type"/> is not assignable to <typeparamref name="T"/>.</exception>
        public Content(Type type, string origin) {
            if(typeof(T).IsAssignableFrom(type)) {
                Type = type;
                Origin = origin;
            } else throw new ArgumentException(
                string.Format("Given type is not assignable to {0}.", nameof(T)),
                nameof(type));
        }

        /// <summary>Casts this <see cref="Content{Base}"/> into a <see cref="Content"/>.</summary>
        public static implicit operator Content(Content<T> content) => new Content(content.Type, content.Origin);

        /// <summary>Creates a new object of type <typeparamref name="T"/>.</summary>
        public T New() => Type.GetConstructor(Type.EmptyTypes).Invoke(null) as T;
        /// <summary>Creates a new object of type <typeparamref name="U"/>, subclass of type <typeparamref name="T"/>.</summary>
        public U New<U>() where U : class, T => Type.GetConstructor(Type.EmptyTypes).Invoke(null) as U;

    }

}
