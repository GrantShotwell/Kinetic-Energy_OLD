using UnityEngine;

namespace KineticEnergy.Structs {

    /// <summary>Stores any <see cref="GameObject"/>, as if it is a prefab.</summary>
    /// <seealso cref="Instantiated"/>
    /// <seealso cref="Prefab{TComponent1}"/>
    /// <seealso cref="Prefab{TComponent1, TComponent2}"/>
    public struct Prefab {

        public GameObject gameObject;

        /// <summary>Creates a new <see cref="Prefab"/>.</summary>
        public Prefab(GameObject gameObject) {
            this.gameObject = gameObject;
            this.gameObject.SetActive(false);
        }

        /// <summary>Equivalent function to <see cref="UnityEngine.Object.Instantiate"/>.</summary>
        /// <returns>Returns <see cref="Instantiated"/>.</returns>
        public Instantiated Instantiate(bool active = true) {
            var instantiated = UnityEngine.Object.Instantiate(gameObject);
            instantiated.name = gameObject.name;
            instantiated.SetActive(active);
            return instantiated;
        }

        /// <summary>Equivalent function to <see cref="UnityEngine.Object.Instantiate"/>.</summary>
        /// <param name="parent">Sets <see cref="Transform.parent"/> of the new <see cref="GameObject"/>.</param>
        /// <returns>Returns <see cref="Instantiated"/>.</returns>
        public Instantiated Instantiate(Transform parent, bool worldPositionStays = false, bool active = true) {
            var instantiated = UnityEngine.Object.Instantiate(gameObject);
            instantiated.name = gameObject.name;
            instantiated.transform.SetParent(parent, worldPositionStays);
            instantiated.SetActive(active);
            return instantiated;
        }

        public static implicit operator Prefab(GameObject gameObject) => new Prefab(gameObject);
        public static implicit operator GameObject(Prefab prefab) => prefab.gameObject;

    }

    /// <summary>Stores any <see cref="GameObject"/>, as if it is a prefab, that has at least one defined <see cref="Component"/>.</summary>
    /// <seealso cref="Instantiated{TComponent1}"/>
    /// <seealso cref="Prefab"/>
    /// <seealso cref="Prefab{TComponent1, TComponent2}"/>
    public struct Prefab<TComponent1> where TComponent1 : Component {

        public GameObject gameObject;
        public TComponent1 component;

        /// <summary>Creates a new <see cref="Prefab{TComponent1}"/> by using <see cref="GameObject.GetComponent"/>.</summary>
        public Prefab(GameObject gameObject) {
            this.gameObject = gameObject;
            gameObject.SetActive(false);
            component = gameObject.GetComponent<TComponent1>();
        }

        /// <summary>Creates a new <see cref="Prefab{TComponent1}"/> by using <see cref="Component.gameObject"/>.</summary>
        public Prefab(TComponent1 component) {
            this.component = component;
            gameObject = component.gameObject;
            gameObject.SetActive(false);
        }

        /// <summary>Equivalent function to <see cref="UnityEngine.Object.Instantiate"/>.</summary>
        /// <returns>Returns <see cref="Instantiated{TComponent1}"/>.</returns>
        public Instantiated<TComponent1> Instantiate(bool active = true) {
            var instantiated = UnityEngine.Object.Instantiate(gameObject);
            instantiated.name = gameObject.name;
            instantiated.SetActive(active);
            return instantiated;
        }

        /// <summary>Equivalent function to <see cref="UnityEngine.Object.Instantiate"/>.</summary>
        /// <param name="parent">Sets <see cref="Transform.parent"/> of the new <see cref="GameObject"/>.</param>
        /// <returns>Returns <see cref="Instantiated{TComponent1}"/>.</returns>
        public Instantiated<TComponent1> Instantiate(Transform parent, bool worldPositionStays = false, bool active = true) {
            var instantiated = UnityEngine.Object.Instantiate(gameObject);
            instantiated.name = gameObject.name;
            instantiated.transform.SetParent(parent, worldPositionStays);
            instantiated.SetActive(active);
            return instantiated;
        }

        public static implicit operator Prefab(Prefab<TComponent1> prefab) => new Prefab(prefab.gameObject);
        public static implicit operator Prefab<TComponent1>(GameObject gameObject) => new Prefab<TComponent1>(gameObject);
        public static implicit operator Prefab<TComponent1>(TComponent1 component) => new Prefab<TComponent1>(component);
        public static implicit operator GameObject(Prefab<TComponent1> prefab) => prefab.gameObject;
        public static implicit operator TComponent1(Prefab<TComponent1> prefab) => prefab.component;

    }

    /// <summary>Stores any <see cref="GameObject"/>, as if it is a prefab, with at least two defined <see cref="Component"/>s.</summary>
    /// <seealso cref="Instantiated{TComponent1, TComponent2}"/>
    /// <seealso cref="Prefab"/>
    /// <seealso cref="Prefab{TComponent1}"/>
    public struct Prefab<TComponent1, TComponent2> where TComponent1 : Component where TComponent2 : Component {

        public GameObject gameObject;
        public TComponent1 component1;
        public TComponent2 component2;

        /// <summary>Creates a new <see cref="Prefab{TComponent1, TComponent2}"/> by using <see cref="GameObject.GetComponent"/>.</summary>
        public Prefab(GameObject gameObject) {
            this.gameObject = gameObject;
            gameObject.SetActive(false);
            component1 = gameObject.GetComponent<TComponent1>();
            component2 = gameObject.GetComponent<TComponent2>();
        }

        /// <summary>Creates a new <see cref="Prefab{TComponent1, TComponent2}"/> by using <see cref="Component.gameObject"/>.</summary>
        public Prefab(TComponent1 component1, TComponent2 component2) {
            this.component1 = component1;
            this.component2 = component2;
            gameObject = component1.gameObject;
            gameObject.SetActive(false);
        }

        /// <summary>Equivalent function to <see cref="UnityEngine.Object.Instantiate"/>.</summary>
        /// <returns>Returns <see cref="Instantiated{TComponent1, TComponent2}"/>.</returns>
        public Instantiated<TComponent1, TComponent2> Instantiate(bool active = true) {
            var instantiated = UnityEngine.Object.Instantiate(gameObject);
            instantiated.name = gameObject.name;
            instantiated.SetActive(active);
            return instantiated;
        }

        /// <summary>Equivalent function to <see cref="UnityEngine.Object.Instantiate"/>.</summary>
        /// <param name="parent">Sets <see cref="Transform.parent"/> of the new <see cref="GameObject"/>.</param>
        /// <returns>Returns <see cref="Instantiated{TComponent1, TComponent2}"/>.</returns>
        public Instantiated<TComponent1, TComponent2> Instantiate(Transform parent, bool worldPositionStays = false, bool active = true) {
            var instantiated = UnityEngine.Object.Instantiate(gameObject);
            instantiated.name = gameObject.name;
            instantiated.transform.SetParent(parent, worldPositionStays);
            instantiated.SetActive(active);
            return instantiated;
        }

        public static implicit operator Prefab(Prefab<TComponent1, TComponent2> prefab) => new Prefab(prefab.gameObject);
        public static implicit operator Prefab<TComponent1>(Prefab<TComponent1, TComponent2> prefab) => new Prefab<TComponent1>(prefab.component1);
        public static implicit operator Prefab<TComponent2>(Prefab<TComponent1, TComponent2> prefab) => new Prefab<TComponent2>(prefab.component2);
        public static implicit operator Prefab<TComponent1, TComponent2>(GameObject gameObject) => new Prefab<TComponent1, TComponent2>(gameObject);
        public static implicit operator GameObject(Prefab<TComponent1, TComponent2> prefab) => prefab.gameObject;
        public static implicit operator TComponent1(Prefab<TComponent1, TComponent2> prefab) => prefab.component1;
        public static implicit operator TComponent2(Prefab<TComponent1, TComponent2> prefab) => prefab.component2;

    }

}
