using KineticEnergy.Interfaces.Generic;
using KineticEnergy.Grids.Blocks;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace KineticEnergy.Intangibles.Terminal {

    /// <summary>Interface <see cref="Block"/>s implement to show inputs on the terminal.</summary>
    public interface IBlockTerminal : IManaged<UI.UIManager> {

        /// <summary>Returns a list of all <see cref="TerminalMenu"/>s for this block.</summary>
        IEnumerable<TerminalMenu> Menus { get; }

    }

    /// <summary>Represents a group of <see cref="TerminalItem"/>s to be displayed.</summary>
    public class TerminalMenu : IManaged<TerminalWindow>, IManager<TerminalItem> {

        //Constants
        public const float ITEM_SPACING = 5f;
        public const float WINDOW_SPACING = 5f;

        //IManaged overrides
        public TerminalWindow Manager { get; set; }
        //IManager overrides
        public bool AllSetup { get; private set; }
        public IEnumerable<TerminalItem> Managed => items;

        /// <summary>Shorthand for "<c>this.Manager.Manager</c>".</summary>
        public UI.UIManager uiManager => Manager.Manager;
        /// <summary>Shorthand for "<c>this.Manager</c>".</summary>
        public TerminalWindow window => Manager;

        /// <summary>The title of this <see cref="TerminalMenu"/>.</summary>
        public readonly string title;
        /// <summary>Where this <see cref="TerminalMenu"/> will visually attach to.</summary>
        public Vector3 relativePosition;
        /// <summary>The <see cref="Block"/> this <see cref="TerminalMenu"/> is for.</summary>
        public Block block;
        /// <summary>A list of <see cref="TerminalItem"/>s, in order, to display.</summary>
        public readonly TerminalItem[] items;
        /// <summary>Length of <see cref="items"/> saved during constructor.</summary>
        public readonly int count;
        /// <summary>Size needed for this <see cref="TerminalMenu"/> found during constructor.</summary>
        public readonly Vector2 size = Vector2.zero;

        /// <summary>Creates a new <see cref="TerminalMenu"/> from the given <see cref="TerminalItem"/>s.</summary>
        /// <param name="items">All <see cref="TerminalItem"/>s, in order, for this <see cref="TerminalWindow"/> to display.</param>
        public TerminalMenu(string title, params TerminalItem[] items) {

            //Apply basic properties.
            this.title = title;
            relativePosition = Vector3.zero;
            this.items = items;

            //Process basic properties.
            count = items.Length;
            foreach(TerminalItem item in items) {
                //Get width and height.
                var height = item.Height + ITEM_SPACING;
                var width = item.MinWidth + (WINDOW_SPACING * 2);
                //Apply width and height.
                size.y += height;
                if(width > size.x)
                    size.x = height;
            }
            size.x += ITEM_SPACING;

        }

        /// <summary>Creates a new <see cref="TerminalMenu"/> from the given <see cref="TerminalItem"/>s.</summary>
        /// <param name="items">All <see cref="TerminalItem"/>s, in order, for this <see cref="TerminalWindow"/> to display.</param>
        public TerminalMenu(string title, Vector3 relativePosition, params TerminalItem[] items) {

            //Apply basic properties.
            this.title = title;
            this.relativePosition = relativePosition;
            this.items = items;

            //Process basic properties.
            count = items.Length;
            foreach(TerminalItem item in items) {
                //Get width and height.
                var height = item.Height + ITEM_SPACING;
                var width = item.MinWidth + (WINDOW_SPACING * 2);
                //Apply width and height.
                size.y += height;
                if(width > size.x)
                    size.x = height;
            }
            size.x += ITEM_SPACING;

        }

        public void OnWindow(Rect window) {
            var current = Vector2.zero;
            foreach(TerminalItem item in items) {
                var height = item.Height;
                item.OnWindow(new Rect(current, new Vector2(window.x, height)));
                current.y -= height + ITEM_SPACING;
            }
        }

        public void OnSetup() {
            foreach(TerminalItem item in items) {
                item.Manager = this;
                item.OnSetup();
            }
        }

        public void OnAllSetup() { }

    }

    public abstract class TerminalItem : IManaged<TerminalMenu> {

        /// <summary>Returns the space this <see cref="TerminalItem"/> needs on the screen.</summary>
        public abstract float Height { get; }
        /// <summary>Returns the minimum width this <see cref="TerminalItem"/> needs on the screen.</summary>
        public abstract float MinWidth { get; }

        public TerminalMenu Manager { get; set; }
        public virtual void OnAllSetup() { }
        public virtual void OnSetup() { }

        /// <summary>Shorthand for "<c>this.Manager.Manager.Manager</c>". Yeah.</summary>
        public UI.UIManager uiManager => Manager.Manager.Manager;
        /// <summary>Shortand for "<c>this.Manager.Manager</c>".</summary>
        public TerminalWindow window => Manager.Manager;
        /// <summary>Shortand for "<c>this.Manager</c>".</summary>
        public TerminalMenu menu => Manager;

        /// <summary>Creates this <see cref="TerminalItem"/>'s window inside of the given <see cref="Rect"/>.</summary>
        /// <param name="window"></param>
        public abstract void OnWindow(Rect window);

    }

    public class ButtonTerminal : TerminalItem {

        public override float Height => 50f;
        public override float MinWidth => 200f;

        public bool value;
        public readonly Func<bool, bool> send;
        public ButtonTerminal(Func<bool, bool> send) => this.send = send;

        public Button buttonUI;

        public override void OnSetup() {
            base.OnSetup();
            buttonUI = uiManager.Master.prefabs.buttonUI.Instantiate(window.transform);
            buttonUI.onClick.AddListener(OnClick);
        }

        bool m_down = false;
        public void OnClick() {
            if(m_down) value = !value;
            m_down = !m_down;
        }

        public override void OnWindow(Rect window) {
            value = send(value);
        }

    }

    public class SliderTerminal : TerminalItem {

        public override float Height => 50f;
        public override float MinWidth => 200f;

        public override void OnWindow(Rect window) {
            throw new NotImplementedException("SliderTerminal.OnWindow()");
        }

    }

}
