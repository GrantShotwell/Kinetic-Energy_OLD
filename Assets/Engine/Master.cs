using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using KineticEnergy.Content;
using KineticEnergy.Entities;
using KineticEnergy.CodeTools.Enumerators;
using KineticEnergy.Interfaces.Manager;
using KineticEnergy.Intangibles.Client;
using KineticEnergy.Intangibles.Server;
using KineticEnergy.Intangibles.Behaviours;

namespace KineticEnergy.Intangibles {

    public class Master : MonoBehaviour,
        IManager<BehavioursManager<ClientBehaviour>>,
        IManager<BehavioursManager<ServerBehaviour>>,
        IManager<BehavioursManager<GlobalBehaviour>> {

        public readonly ContentLoader loader = new ContentLoader();

        public GameObject clientPrefab;
        public ClientBehavioursManager client;

        public GameObject globalPrefab;
        public GlobalBehavioursManager global;

        public GameObject serverPrefab;
        public ServerBehavioursManager server;

        public bool AllSetup { get; private set; } = false;

        #region unimportant
        IEnumerator<BehavioursManager<ClientBehaviour>> IManager<BehavioursManager<ClientBehaviour>>.Managed
            => new PropertyEnumerable<BehavioursManager<ClientBehaviour>>((BehavioursManager<ClientBehaviour>)client).GetEnumerator();
        IEnumerator<BehavioursManager<ServerBehaviour>> IManager<BehavioursManager<ServerBehaviour>>.Managed
            => new PropertyEnumerable<BehavioursManager<ServerBehaviour>>((BehavioursManager<ServerBehaviour>)server).GetEnumerator();
        IEnumerator<BehavioursManager<GlobalBehaviour>> IManager<BehavioursManager<GlobalBehaviour>>.Managed
            => new PropertyEnumerable<BehavioursManager<GlobalBehaviour>>((BehavioursManager<GlobalBehaviour>)global).GetEnumerator();
        #endregion

        public void OnEnable() {

            #region Testing


            //Nothing for now.


            #endregion

            #region Content Loader

            //Find and process all mod directories.
            loader.FindAndProcessAll(out _);

            //Things that need to be loaded but aren't within the Content folder because it's part of the engine.
            loader.behaviours.client.Add(
                new Content<ClientBehaviour>(typeof(ClientManager), "base"));
            loader.behaviours.client.Add(
                new Content<ClientBehaviour>(typeof(UI.UIManager), "base"));
            loader.behaviours.server.Add(
                new Content<ServerBehaviour>(typeof(ServerManager), "base"));
            loader.behaviours.global.Add(
                new Content<GlobalBehaviour>(typeof(GlobalPaletteManager), "base"));
            loader.m_entities.Add(
                new Content<Entity>(typeof(Player), "base"));


            #endregion

            #region Create Managers


            //Create ClientBehavioursManager
            client = Instantiate(clientPrefab).GetComponent<ClientBehavioursManager>();
            client.gameObject.transform.parent = transform;
            client.gameObject.name = "Client GameObject";
            client.Manager = this;
            client.OnSetup();

            //Create ServerBehavioursManager
            server = Instantiate(serverPrefab).GetComponent<ServerBehavioursManager>();
            server.gameObject.transform.parent = transform;
            server.gameObject.name = "Server GameObject";
            server.Manager = this;
            server.OnSetup();

            //Create GlobalBehavioursManager
            global = Instantiate(globalPrefab).GetComponent<GlobalBehavioursManager>();
            global.gameObject.transform.parent = transform;
            global.gameObject.name = "Global GameObject";
            global.Manager = this;
            global.OnSetup();

            #endregion

            AllSetup = true;
            client.OnAllSetup();
            server.OnAllSetup();
            global.OnAllSetup();

        }

        public void OnDisable() {

            loader.UnloadAll();

        }


    }

    namespace Behaviours {

        #region Detatched

        public abstract class BehavioursManager<ManagedBehaviour> : MonoBehaviour,
            IManaged<Master>, IManager<ManagedBehaviour>
            where ManagedBehaviour: DetachedBehaviour, IManaged<BehavioursManager<ManagedBehaviour>> {

            public Master Manager { get; set; }
            protected ManagedBehaviour[] managed { get; private set; }
            IEnumerator<ManagedBehaviour> IManager<ManagedBehaviour>.Managed => (IEnumerator<ManagedBehaviour>)managed.GetEnumerator();
            public bool AllSetup { get; private set; }

            public virtual void OnAllSetup() {
                foreach(var managed in managed)
                    managed.OnAllSetup();
            }

            public virtual void OnSetup() {

                //Get loaded behaviours.
                var contents = Manager.loader.behaviours.Get<ManagedBehaviour>();

                //Create every behaviour.
                var behaviours = new List<ManagedBehaviour>(contents.Count);
                foreach(Content<ManagedBehaviour> content in contents) {
                    var behaviour = (ManagedBehaviour)gameObject.AddComponent(content);
                    behaviours.Add(behaviour);
                    behaviour.Manager = this;
                    behaviour.OnSetup();
                }
                AllSetup = true;

                //Save the list.
                managed = behaviours.ToArray();

            }

            /// <summary>Shorthand for "<c>this as ClientBehavioursManager</c>".</summary>
            public ClientBehavioursManager asClient => this as ClientBehavioursManager;
            /// <summary>Shorthand for "<c>this as ServerBehavioursManager</c>".</summary>
            public ServerBehavioursManager asServer => this as ServerBehavioursManager;
            /// <summary>Shorthand for "<c>this as GlobalBehavioursManager</c>".</summary>
            public GlobalBehavioursManager asGlobal => this as GlobalBehavioursManager;

        }

        /// <summary>Class for scripts which need to be run detatched from any object.</summary>
        /// <seealso cref="GlobalBehaviour"/>
        /// <seealso cref="ClientBehaviour"/>
        /// <seealso cref="ServerBehaviour"/>
        public abstract class DetachedBehaviour : MonoBehaviour { }

        public abstract class ClientBehaviour : DetachedBehaviour, IManaged<BehavioursManager<ClientBehaviour>> {
            public BehavioursManager<ClientBehaviour> Manager { get; set; }
            public virtual void OnSetup() { }
            public virtual void OnAllSetup() { }
        }

        public abstract class ServerBehaviour : DetachedBehaviour, IManaged<BehavioursManager<ServerBehaviour>> {
            public BehavioursManager<ServerBehaviour> Manager { get; set; }
            public virtual void OnSetup() { }
            public virtual void OnAllSetup() { }
        }

        public abstract class GlobalBehaviour : DetachedBehaviour, IManaged<BehavioursManager<GlobalBehaviour>> {
            public BehavioursManager<GlobalBehaviour> Manager { get; set; }
            public virtual void OnSetup() { }
            public virtual void OnAllSetup() { }
        }

        #endregion

        #region Attached

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

        #endregion

    }

}
