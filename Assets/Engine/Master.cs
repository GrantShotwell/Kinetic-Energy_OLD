using KineticEnergy.Structs;
using KineticEnergy.Content;
using KineticEnergy.Entities;
using KineticEnergy.CodeTools.Enumerators;
using KineticEnergy.Intangibles.Behaviours;
using KineticEnergy.Intangibles.Client;
using KineticEnergy.Intangibles.Server;
using KineticEnergy.Interfaces.Manager;
using KineticEnergy.Ships;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;
using UnityEngine.VFX;

namespace KineticEnergy.Intangibles {

    public class Master : MonoBehaviour,
        IManager<BehavioursManager<ClientBehaviour>>,
        IManager<BehavioursManager<ServerBehaviour>>,
        IManager<BehavioursManager<GlobalBehaviour>> {

        [Serializable]
        public class LevelsOfDetail {
            public enum LevelOfDetail { All = 3, High = 2, Basic = 1, None = 0 }
            public static bool CanShowLog(LevelOfDetail required, LevelOfDetail current)
                => (int)current >= (int)required;
            public LevelOfDetail loading = LevelOfDetail.Basic;
            public LevelOfDetail directories = LevelOfDetail.Basic;
            public LevelOfDetail attributes = LevelOfDetail.Basic;
            public LevelOfDetail clients = LevelOfDetail.Basic;
            public LevelOfDetail traversal = LevelOfDetail.Basic;
        }
        public LevelsOfDetail LogSettings { get => logSettings; private set => logSettings = value; }
        [SerializeField] private LevelsOfDetail logSettings = new LevelsOfDetail();

        public readonly ContentLoader loader = new ContentLoader();

        public GameObject clientPrefab;
        public ClientBehavioursManager Client { get; private set; }

        public GameObject globalPrefab;
        public GlobalBehavioursManager Global { get; private set; }

        public GameObject serverPrefab;
        public ServerBehavioursManager Server { get; private set; }


        #region Prefabs
        [EditorBrowsable(EditorBrowsableState.Never)]
        [Obsolete("'Master.mPrefab_xxx' properties are not allowed to be accessed. Use 'Master.prefabs.xxx' instead.", false)]
        public GameObject
            mPrefab_grid,
            mPrefab_player,
            mPrefab_sun,
            mPrefab_canvas,
            mPrefab_events,
            mPrefab_textUI,
            mPrefab_imageUI,
            mPrefab_buttonUI,
            mPrefab_terminal,
            mPrefab_selectableBox,
            mPrefab_thrusterVFX;
        public struct Prefabs {

            public Prefab<BlockGrid> grid;
            public Prefab<Player, BlockGridEditor> player;
            public Prefab sun;

            public Prefab<Canvas> canvas;
            public Prefab<UnityEngine.EventSystems.EventSystem> events;
            public Prefab<TMPro.TextMeshProUGUI> textUI;
            public Prefab<UnityEngine.UI.Image> imageUI;
            public Prefab<UnityEngine.UI.Button, UnityEngine.UI.Image> buttonUI;

            public Prefab<Terminal.TerminalWindow> terminal;

            public Prefab<BoxCollider, MeshRenderer> selectableBox;

            public Prefab<VisualEffect> thrustVFX;

        }
        public Prefabs prefabs;
        #endregion

        #region Meshes
        [EditorBrowsable(EditorBrowsableState.Never)]
        [Obsolete("'Master.mMesh_xxx' properties are not allowed to be accessed. Use 'Master.meshes.xxx' instead.", false)]
        public GameObject
            mMesh_plane;
        public struct Meshes {

            public Prefab plane;

        }
        public Meshes meshes;
        #endregion

        #region Materials

        [Serializable]
        public struct Materials {

            public Material block;
            public Material preview;
            public Material selectable;
            public Material shield;
            public Material terminal;
            public Material chemicalThrust;

        }
        public Materials materials;

        #endregion

        public bool AllSetup { get; private set; } = false;

        #region unimportant
        IEnumerable<BehavioursManager<ClientBehaviour>> IManager<BehavioursManager<ClientBehaviour>>.Managed
            => new PropertyEnumerable<BehavioursManager<ClientBehaviour>>((BehavioursManager<ClientBehaviour>)Client);
        IEnumerable<BehavioursManager<ServerBehaviour>> IManager<BehavioursManager<ServerBehaviour>>.Managed
            => new PropertyEnumerable<BehavioursManager<ServerBehaviour>>((BehavioursManager<ServerBehaviour>)Server);
        IEnumerable<BehavioursManager<GlobalBehaviour>> IManager<BehavioursManager<GlobalBehaviour>>.Managed
            => new PropertyEnumerable<BehavioursManager<GlobalBehaviour>>((BehavioursManager<GlobalBehaviour>)Global);
        #endregion

        public void Start() {

            #region Testing


            //Nothing for now.


            #endregion

            if(LevelsOfDetail.CanShowLog(LevelsOfDetail.LevelOfDetail.Basic, LogSettings.loading))
                Debug.Log("*** Starting content loading process.");

            #region Prefabs

#pragma warning disable CS0618 // Type or member is obsolete
            prefabs = new Prefabs() {
                grid = mPrefab_grid,
                player = mPrefab_player,
                sun = mPrefab_sun,
                canvas = mPrefab_canvas,
                events = mPrefab_events,
                textUI = mPrefab_textUI,
                imageUI = mPrefab_imageUI,
                buttonUI = mPrefab_buttonUI,
                terminal = mPrefab_terminal,
                selectableBox = mPrefab_selectableBox,
                thrustVFX = mPrefab_thrusterVFX
            };
#pragma warning restore CS0618 // Type or member is obsolete

            #endregion

            #region Meshes

#pragma warning disable CS0618 // Type or member is obsolete
            meshes = new Meshes() {
                plane = mMesh_plane
            };
#pragma warning restore CS0618 // Type or member is obsolete

            #endregion

            #region Content Loader

            //Find and process all mod directories.
            loader.FindAndProcessAll(LogSettings, out _);

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
            Client = Instantiate(clientPrefab).GetComponent<ClientBehavioursManager>();
            Client.gameObject.transform.parent = transform;
            Client.gameObject.name = "Client GameObject";
            Client.Manager = this;
            Client.OnSetup();

            //Create ServerBehavioursManager
            Server = Instantiate(serverPrefab).GetComponent<ServerBehavioursManager>();
            Server.gameObject.transform.parent = transform;
            Server.gameObject.name = "Server GameObject";
            Server.Manager = this;
            Server.OnSetup();

            //Create GlobalBehavioursManager
            Global = Instantiate(globalPrefab).GetComponent<GlobalBehavioursManager>();
            Global.gameObject.transform.parent = transform;
            Global.gameObject.name = "Global GameObject";
            Global.Manager = this;
            Global.OnSetup();

            #endregion

            AllSetup = true;
            Client.OnAllSetup();
            Server.OnAllSetup();
            Global.OnAllSetup();

            if(LevelsOfDetail.CanShowLog(LevelsOfDetail.LevelOfDetail.Basic, LogSettings.loading))
                Debug.Log("Completed content loading process. ***");

        }

        public void OnDestroy() {

            loader.UnloadAll();

        }


    }

    namespace Behaviours {

        #region Detatched

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
                Managed = behaviours.ToArray();

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
