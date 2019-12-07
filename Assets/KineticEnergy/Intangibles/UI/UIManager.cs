using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using KineticEnergy.Grids;
using KineticEnergy.Grids.Blocks;
using KineticEnergy.Structs;
using KineticEnergy.Interfaces.Generic;
using KineticEnergy.Intangibles.Terminal;
using KineticEnergy.Intangibles.Behaviours;

namespace KineticEnergy.Intangibles.UI {

    public class UIManager : ClientBehaviour, IDynamicManager<TerminalWindow> {

        public bool AllSetup { get; set; } = true;

        //TerminalWindows
        private List<TerminalWindow> TerminalWindows { get; } = new List<TerminalWindow>();
        IEnumerable<TerminalWindow> IManager<TerminalWindow>.Managed => TerminalWindows;
        public void RemoveMe(TerminalWindow me) => TerminalWindows.Remove(me);

        /// <summary>Shorthand for "<c>this.Manager.asClient.camera</c>".</summary>
        public Camera Camera => Manager.AsClient.camera;

        public Instantiated<Canvas> Canvas { get; private set; }
        public Instantiated<EventSystem> Events { get; private set; }

        /// <summary>Implementation of <see cref="IManager{TManaged}.AllSetup()"/>.</summary>
        public override void OnSetup() {

            //Create Canvas and EventSystem.
            Canvas = Master.prefabs.canvas.Instantiate(transform);
            Events = Master.prefabs.events.Instantiate(transform);

        }

        /// <summary>Implementation of <see cref="MonoBehaviour"/> message.</summary>
        public void Update() {

            Canvas.component.worldCamera = Camera;
            Client.Inputs inputs = Manager.AsClient.inputs;

            if(inputs.terminal.Down) {
                foreach(BlockGrid grid in FindObjectsOfType<BlockGrid>())
                    ShowTerminal(grid);
            }
            if(inputs.terminal.Free) {
                HideAllTerminals();
            }

        }

        /// <summary>Shows the given <see cref="BlockGrid"/>'s terminal.</summary>
        public void ShowTerminal(BlockGrid grid) {

            //Setup
            AllSetup = false;
            foreach(Block block in grid) {
                if(block is IBlockTerminal terminal) {

                    foreach(TerminalMenu menu in terminal.Menus) {
                        menu.block = block;
                        var window = Master.prefabs.terminal.Instantiate(Canvas.gameObject.transform);
                        menu.Manager = window;
                        window.component.Manager = this;
                        window.component.menu = menu;
                        TerminalWindows.Add(window);
                        window.component.OnSetup();
                    }

                    terminal.OnSetup();

                }
            }

            //AllSetup
            AllSetup = true;
            foreach(TerminalWindow window in TerminalWindows)
                if(window.menu.block.Location.Grid == grid) window.OnAllSetup();

        }

        /// <summary>Hides the terminal of the given <see cref="BlockGrid"/>.</summary>
        public void HideTerminal(BlockGrid grid) {

            throw new NotImplementedException("UIManager.HideTerminal()");

        }

        /// <summary>Hides all terminals.</summary>
        public void HideAllTerminals() {

            for(int i = 0; i < TerminalWindows.Count; i++)
                Destroy(TerminalWindows[i].gameObject);
            TerminalWindows.Clear();

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
