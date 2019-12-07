using System;
using UnityEngine;
using KineticEnergy.Structs;
using KineticEnergy.Intangibles;
using KineticEnergy.Intangibles.Behaviours;

namespace KineticEnergy.Entities {

    public abstract class Entity : MonoBehaviour {

        public Master Master {
            get => master ?? throw new NullReferenceException(string.Format("'{0}' is not set to an instance of an object. Was '{1}' called on this {2}?",
                nameof(Master), nameof(GlobalBehavioursManager.PolishBehaviour), nameof(Entity)));
            internal set => master = value;
        }
        private Master master;

        public GlobalBehavioursManager Global => Master.Global;

        public abstract Prefab BuildEntityPrefab();

    }

    public static class EntityAttributes {

        /// <summary>What is the priority of this attribute? (affects order in which attributes are applied)</summary>
        public enum Order {
            /// <summary>Signifies that this attribute must be amung the first to be applied.</summary>
            FIrst = 1,
            /// <summary>Signifies that this attribute has normal priority.</summary>
            Default = 0,
            /// <summary>Signifies that this attribute must be amung the last to be applied.</summary>
            Last = -1
        }

        /// <summary>Abstract base class for <see cref="Entity"/> attributes.</summary>
        public abstract class EntityAttribute : Attribute {

            /// <summary>The <see cref="EntityAttributes.Order"/> of this <see cref="BlockAttribute"/>.</summary>
            public abstract Order Priority { get; }

            /// <summary>Applies this <see cref="EntityAttribute"/> to the given <see cref="Entity"/> <see cref="GameObject"/>.</summary>
            /// <param name="entity">The <see cref="Entity"/> <see cref="GameObject"/> to apply this <see cref="EntityAttribute"/> to.</param>
            public abstract void ApplyTo(GameObject entity);

        }

    }

}
