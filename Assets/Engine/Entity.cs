using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using KineticEnergy.Content;
using KineticEnergy.Interfaces;
using KineticEnergy.Intangibles.Behaviours;

namespace KineticEnergy.Entities {

    public abstract class Entity : MonoBehaviour {

        public abstract GameObject BuildEntityPrefab();

    }

    public static class EntityAttributes {

        /// <summary>What is the priority of this attribute? (affects order in which attributes are applied)</summary>
        public enum Priority {
            /// <summary>Signifies that this attribute must be amung the first to be applied.</summary>
            High = 1,
            /// <summary>Signifies that this attribute has normal priority.</summary>
            Normal = 0,
            /// <summary>Signifies that this attribute must be amung the last to be applied.</summary>
            Low = -1
        }

        /// <summary>Abstract base class for <see cref="Entity"/> attributes.</summary>
        public abstract class EntityAttribute : Attribute {

            /// <summary>The <see cref="EntityAttributes.Priority"/> of this <see cref="BlockAttribute"/>.</summary>
            public abstract Priority Priority { get; }

            /// <summary>Applies this <see cref="EntityAttribute"/> to the given <see cref="Entity"/> <see cref="GameObject"/>.</summary>
            /// <param name="entity">The <see cref="Entity"/> <see cref="GameObject"/> to apply this <see cref="EntityAttribute"/> to.</param>
            public abstract void ApplyTo(GameObject entity);

        }

    }

}
