using UnityEngine;

namespace KineticEnergy.Structs {

    /// <summary>Stores the result of <see cref="Prefab.Instantiate"/>.</summary>
    public struct Instantiated {

        public GameObject gameObject;

        public Instantiated(GameObject gameObject) {
            this.gameObject = gameObject;
        }

        public static implicit operator Instantiated(GameObject gameObject) => new Instantiated(gameObject);

    }

    /// <summary>Stores the result of <see cref="Prefab{TComponent}.Instantiate"/>.</summary>
    public struct Instantiated<TComponent> where TComponent : Component {

        public GameObject gameObject;
        public TComponent component;

        public Instantiated(GameObject gameObject) {
            this.gameObject = gameObject;
            component = gameObject.GetComponent<TComponent>();
        }

        public Instantiated(TComponent component) {
            gameObject = component.gameObject;
            this.component = component;
        }

        public static implicit operator Instantiated(Instantiated<TComponent> instantiated) => new Instantiated(instantiated.gameObject);
        public static implicit operator Instantiated<TComponent>(GameObject gameObject) => new Instantiated<TComponent>(gameObject);
        public static implicit operator TComponent(Instantiated<TComponent> instantiated) => instantiated.component;

    }

    /// <summary>Stores the result of <see cref="Prefab{TComponent1, TComponent2}.Instantiate"/>.</summary>
    public struct Instantiated<TComponent1, TComponent2> where TComponent1 : Component where TComponent2 : Component {

        public GameObject gameObject;
        public TComponent1 component1;
        public TComponent2 component2;

        public Instantiated(GameObject gameObject) {
            this.gameObject = gameObject;
            component1 = gameObject.GetComponent<TComponent1>();
            component2 = gameObject.GetComponent<TComponent2>();
        }

        public Instantiated(TComponent1 component1, TComponent2 component2) {
            gameObject = component1.gameObject;
            this.component1 = component1;
            this.component2 = component2;
        }

        public static implicit operator Instantiated(Instantiated<TComponent1, TComponent2> instantiated) => new Instantiated(instantiated.gameObject);
        public static implicit operator Instantiated<TComponent1>(Instantiated<TComponent1, TComponent2> instantiated) => new Instantiated<TComponent1>(instantiated.component1);
        public static implicit operator Instantiated<TComponent1, TComponent2>(GameObject gameObject) => new Instantiated<TComponent1, TComponent2>(gameObject);
        public static implicit operator TComponent1(Instantiated<TComponent1, TComponent2> instantiated) => instantiated.component1;
        public static implicit operator TComponent2(Instantiated<TComponent1, TComponent2> instantiated) => instantiated.component2;

    }

}
