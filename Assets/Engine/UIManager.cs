using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using KineticEnergy.Ships;
using KineticEnergy.Ships.Blocks;
using KineticEnergy.Structs;
using KineticEnergy.Interfaces.Manager;
using KineticEnergy.Intangibles.Terminal;
using KineticEnergy.Intangibles.Behaviours;

namespace KineticEnergy.Intangibles.UI {

    public class UIManager : ClientBehaviour, IDynamicManager<TerminalWindow> {

        public bool AllSetup { get; set; } = true;

        //TerminalWindows
        private readonly List<TerminalWindow> terminalWindows = new List<TerminalWindow>();
        IEnumerable<TerminalWindow> IManager<TerminalWindow>.Managed => terminalWindows;
        public void RemoveMe(TerminalWindow me) => terminalWindows.Remove(me);

        /// <summary>Shorthand for "<c>this.Manager.asClient.camera</c>".</summary>
        new public Camera camera => Manager.asClient.camera;

        Instantiated<Canvas> canvas;
        Instantiated<EventSystem> events;

        public override void OnSetup() {

            //Create Canvas and EventSystem.
            canvas = Master.prefabs.canvas.Instantiate(transform);
            events = Master.prefabs.events.Instantiate(transform);

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
                        var window = Master.prefabs.terminal.Instantiate(canvas.gameObject.transform);
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
                if(window.menu.block.Grid == grid) {
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
            var window = Master.prefabs.terminal.Instantiate();
            window.gameObject.transform.parent = transform;
            window.component.menu = menu;
            window.component.OnSetup();
            return window.component;
        }

    }

}
