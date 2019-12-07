using System;
using System.Collections.Generic;
using KineticEnergy.Content;
using KineticEnergy.Entities;
using KineticEnergy.Enumeration;
using KineticEnergy.Grids;
using KineticEnergy.Intangibles.Behaviours;
using KineticEnergy.Intangibles.Client;
using KineticEnergy.Intangibles.Server;
using KineticEnergy.Interfaces.Generic;
using KineticEnergy.Structs;
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

        public ContentLoader Loader { get; } = new ContentLoader();

        public GameObject clientPrefab;
        public ClientBehavioursManager Client { get; private set; }

        public GameObject globalPrefab;
        public GlobalBehavioursManager Global { get; private set; }

        public GameObject serverPrefab;
        public ServerBehavioursManager Server { get; private set; }


        #region Prefabs
#pragma warning disable IDE0044 // Add readonly modifier
#pragma warning disable CS0649 // Private member is never assigned
        [SerializeField]
        private GameObject
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
#pragma warning restore
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
#pragma warning disable IDE0044 // Add readonly modifier
#pragma warning disable CS0649 // Private member is never assigned
        [SerializeField]
        private GameObject
            mMesh_plane;
#pragma warning restore
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
            => new Properties<BehavioursManager<ClientBehaviour>>((BehavioursManager<ClientBehaviour>)Client);
        IEnumerable<BehavioursManager<ServerBehaviour>> IManager<BehavioursManager<ServerBehaviour>>.Managed
            => new Properties<BehavioursManager<ServerBehaviour>>((BehavioursManager<ServerBehaviour>)Server);
        IEnumerable<BehavioursManager<GlobalBehaviour>> IManager<BehavioursManager<GlobalBehaviour>>.Managed
            => new Properties<BehavioursManager<GlobalBehaviour>>((BehavioursManager<GlobalBehaviour>)Global);
        #endregion

        public void Start() {

            #region Testing


            //Nothing for now.


            #endregion

            #region Load Content

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
            Loader.FindAndProcessAll(LogSettings, out _, out _);

            //Things that need to be loaded but aren't within the Content folder because it's part of the engine.
            Loader.FinalGroup.Clients.Add(
                new Content<ClientBehaviour>(typeof(ClientManager), "base"));
            Loader.FinalGroup.Clients.Add(
                new Content<ClientBehaviour>(typeof(UI.UIManager), "base"));
            Loader.FinalGroup.Servers.Add(
                new Content<ServerBehaviour>(typeof(ServerManager), "base"));
            Loader.FinalGroup.Globals.Add(
                new Content<GlobalBehaviour>(typeof(GlobalPaletteManager), "base"));
            Loader.FinalGroup.Entities.Add(
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

            #endregion

        }

        public void OnDestroy() {

            Loader.UnloadAll();

        }


    }

}
