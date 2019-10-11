using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using TMPro;
using KineticEnergy;
using KineticEnergy.Content;
using KineticEnergy.Interfaces.Manager;
using KineticEnergy.Intangibles.Terminal;
using KineticEnergy.Intangibles.Behaviours;
using KineticEnergy.Ships;
using KineticEnergy.Ships.Blocks;
using System;

namespace KineticEnergy.Intangibles.UI {

    public class UIManager : ClientBehaviour, IDynamicManager<TerminalWindow> {

        public bool AllSetup { get; set; } = true;

        //TerminalWindows
        private readonly List<TerminalWindow> terminalWindows = new List<TerminalWindow>();
        IEnumerator<TerminalWindow> IManager<TerminalWindow>.Managed => terminalWindows.GetEnumerator();
        public void RemoveMe(TerminalWindow me) => terminalWindows.Remove(me);

        /// <summary>Shorthand for "<c>this.Manager.asClient.camera</c>".</summary>
        new public Camera camera => Manager.asClient.camera;

        public struct CanvasStruct {
            public readonly GameObject gameObject;
            public readonly Canvas component;
            public readonly RectTransform transform;
            public CanvasStruct(GameObject canvasGameObject) {
                gameObject = canvasGameObject;
                component = gameObject.GetComponent<Canvas>();
                transform = (RectTransform)component.transform;
            }
            public static implicit operator GameObject(CanvasStruct canvas) { return canvas.gameObject; }
            public static implicit operator Canvas(CanvasStruct canvas) { return canvas.component; }
        }
        public CanvasStruct canvas;

        public struct EventsStruct {
            public readonly GameObject gameObject;
            public readonly EventSystem component;
            public readonly Transform transform;
            public EventsStruct(GameObject canvasGameObject) {
                gameObject = canvasGameObject;
                component = gameObject.GetComponent<EventSystem>();
                transform = component.transform;
            }
            public static implicit operator GameObject(EventsStruct events) { return events.gameObject; }
            public static implicit operator EventSystem(EventsStruct events) { return events.component; }
        }
        public EventsStruct events;

        public Prefabs prefabs;
        public struct Prefabs {
            public Prefab<TerminalWindow> terminal;
            public Prefab<Image> imageUI;
            public Prefab<TextMeshProUGUI> textUI;
            public Prefab<Button> buttonUI;
        }

        public override void OnSetup() {

            //Create Canvas and EventSystem.
            canvas = new CanvasStruct(Instantiate(ContentLoader.GetResource<GameObject>("Prefabs\\Canvas")));
            canvas.transform.SetParent(transform, false);
            events = new EventsStruct(Instantiate(ContentLoader.GetResource<GameObject>("Prefabs\\EventSystem")));
            events.transform.parent = transform;

            //Create prefabs.
            prefabs = new Prefabs() {
                terminal = ContentLoader.GetResource<GameObject>("Prefabs\\TerminalWindow"),
                imageUI = ContentLoader.GetResource<GameObject>("Prefabs\\ImageUI"),
                textUI = ContentLoader.GetResource<GameObject>("Prefabs\\TextUI"),
                buttonUI = ContentLoader.GetResource<GameObject>("Prefabs\\ButtonUI")
            };

        }

        public void Update() {

            canvas.component.worldCamera = camera;
            Client.Inputs inputs = Manager.asClient.inputs;

            if(inputs.terminal.Down) {
                foreach(BlockGrid grid in FindObjectsOfType<BlockGrid>())
                    ShowTerminal(grid);
            }
            if(inputs.terminal.Free) {
                HideAllTerminals();
            }

        }

        public void ShowTerminal(BlockGrid grid) {

            //Setup
            AllSetup = false;
            foreach(Block block in grid) {
                if(block is IBlockTerminal blockTrm) {

                    foreach(TerminalMenu menu in blockTrm.Menus) {
                        menu.block = block;
                        var window = prefabs.terminal.Instantiate(canvas.transform);
                        menu.Manager = window;
                        window.component.Manager = this;
                        window.component.menu = menu;
                        terminalWindows.Add(window);
                        window.component.OnSetup();
                    }

                    blockTrm.OnSetup();

                }
            }

            //AllSetup
            AllSetup = true;
            foreach(TerminalWindow window in terminalWindows) {
                if(window.menu.block.grid == grid) {
                    window.OnAllSetup();
                }
            }

        }

        public void HideTerminal(BlockGrid grid) {

            throw new NotImplementedException("UIManager.HideTerminal()");

        }

        public void HideAllTerminals() {

            for(int i = 0; i < terminalWindows.Count; i++)
                Destroy(terminalWindows[i].gameObject);
            terminalWindows.Clear();

        }

        public TerminalWindow AddTerminalMenu(TerminalMenu menu) {
            var window = prefabs.terminal.Instantiate();
            window.gameObject.transform.parent = transform;
            window.component.menu = menu;
            window.component.OnSetup();
            return window.component;
        }

    }

}
