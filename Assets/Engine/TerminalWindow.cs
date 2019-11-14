using KineticEnergy.CodeTools.Enumerators;
using KineticEnergy.Intangibles.UI;
using KineticEnergy.Interfaces.Manager;
using System.Collections.Generic;
using UnityEngine;

namespace KineticEnergy.Intangibles.Terminal {

    /// <summary>Displays a <see cref="TerminalMenu"/>.</summary>
    public class TerminalWindow : MonoBehaviour, IManaged<UIManager>, IManager<TerminalMenu> {

        //IManaged overrides
        public UIManager Manager { get; set; }
        //IManager overrides
        public bool AllSetup { get; private set; }
        public IEnumerable<TerminalMenu> Managed => new PropertyEnumerable<TerminalMenu>(menu);

        public RectTransform rectTransform { get; private set; }
        /// <summary>Shorthand for "<c>rectTransform.rect</c>"</summary>
        public Rect rect => rectTransform.rect;

        /// <summary>The <see cref="TerminalMenu"/> that this <see cref="TerminalWindow"/> is displaying.</summary>
        public TerminalMenu menu;

        public void OnAllSetup() => menu.OnAllSetup();
        public void OnSetup() {
            rectTransform = transform as RectTransform;
            rectTransform.sizeDelta = menu.size;
            menu.Manager = this;
            menu.OnSetup();
        }

        public void Update() {

            if(Manager != null) {

                //Update screen position every frame.
                var screenPosition = menu.block.transform.TransformPoint(menu.relativePosition);
                rectTransform.position = screenPosition;

                //Update TerminalMenu
                menu.OnWindow(rect);

            }

        }

    }

}
