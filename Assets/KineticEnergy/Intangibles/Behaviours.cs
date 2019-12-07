using System;
using System.Collections.Generic;

using KineticEnergy.Content;
using KineticEnergy.Interfaces.Generic;

using UnityEngine;

namespace KineticEnergy.Intangibles.Behaviours {

    public abstract class BehavioursManager<ManagedBehaviour> : MonoBehaviour,
        IManaged<Master>, IManager<ManagedBehaviour>
        where ManagedBehaviour : DetachedBehaviour, IManaged<BehavioursManager<ManagedBehaviour>> {

        public Master Manager { get; set; }
        protected ManagedBehaviour[] Managed { get; private set; }
        IEnumerable<ManagedBehaviour> IManager<ManagedBehaviour>.Managed => Managed;
        public bool AllSetup { get; private set; }

        public virtual void OnAllSetup() {
            foreach(ManagedBehaviour managed in Managed)
                managed.OnAllSetup();
        }

        public virtual void OnSetup() {

            //Get loaded behaviours.
            var success = Manager.Loader.FinalGroup.GetList(out ContentList<ManagedBehaviour> contents);

            //Create every behaviour.
            var behaviours = new List<ManagedBehaviour>(contents.List.Count);
            foreach(Content<ManagedBehaviour> content in contents.List) {
                var behaviour = (ManagedBehaviour)gameObject.AddComponent(content.Type);
                behaviours.Add(behaviour);
                behaviour.Manager = this;
                behaviour.OnSetup();
            }
            AllSetup = true;

            //Save the list.
            Managed = behaviours.ToArray();

        }

        /// <summary>Shorthand for "<c>this as ClientBehavioursManager</c>".</summary>
        public ClientBehavioursManager AsClient => this as ClientBehavioursManager;
        /// <summary>Shorthand for "<c>this as ServerBehavioursManager</c>".</summary>
        public ServerBehavioursManager AsServer => this as ServerBehavioursManager;
        /// <summary>Shorthand for "<c>this as GlobalBehavioursManager</c>".</summary>
        public GlobalBehavioursManager AsGlobal => this as GlobalBehavioursManager;

    }

    /// <summary>Class for scripts which need to be run detatched from any object.</summary>
    /// <seealso cref="GlobalBehaviour"/>
    /// <seealso cref="ClientBehaviour"/>
    /// <seealso cref="ServerBehaviour"/>
    public abstract class DetachedBehaviour : MonoBehaviour { }

    public abstract class ClientBehaviour : DetachedBehaviour, IManaged<BehavioursManager<ClientBehaviour>> {
        public BehavioursManager<ClientBehaviour> Manager { get; set; }
        /// <summary>Shorthand for "<c>Manager.Manager</c>".</summary>
        public Master Master => Manager.Manager;
        public virtual void OnSetup() { }
        public virtual void OnAllSetup() { }
    }

    public abstract class ServerBehaviour : DetachedBehaviour, IManaged<BehavioursManager<ServerBehaviour>> {
        public BehavioursManager<ServerBehaviour> Manager { get; set; }
        /// <summary>Shorthand for "<c>Manager.Manager</c>".</summary>
        public Master Master => Manager.Manager;
        public virtual void OnSetup() { }
        public virtual void OnAllSetup() { }
    }

    public abstract class GlobalBehaviour : DetachedBehaviour, IManaged<BehavioursManager<GlobalBehaviour>> {
        public BehavioursManager<GlobalBehaviour> Manager { get; set; }
        /// <summary>Shorthand for "<c>Manager.Manager</c>".</summary>
        public Master Master => Manager.Manager;
        public virtual void OnSetup() { }
        public virtual void OnAllSetup() { }
    }

    [AttributeUsage(AttributeTargets.Class, AllowMultiple = false, Inherited = true)]
    public class Attachable : Attribute {
        public readonly Type[] types;
        public readonly bool allowSubs, allowSups;
        public Attachable(Type[] to, bool allowSubclasses = false, bool allowSuperclasses = true) {
            types = to;
            allowSubs = allowSubclasses;
            allowSups = allowSuperclasses;
        }

        public bool AttachesTo(Type t) {
            foreach(Type type in types) {
                if(type == t) return true;
                if(allowSubs && t.IsSubclassOf(type)) return true;
                if(allowSups && type.IsSubclassOf(t)) return true;
            }
            return false;
        }

    }

    public abstract class AttachedBehaviour : MonoBehaviour { }

    public struct AttachableRef {
        public readonly Type type;
        public readonly Attachable attribute;
        public AttachableRef(Type type, Attachable attribute) {
            this.type = type;
            this.attribute = attribute;
        }
    }

}
