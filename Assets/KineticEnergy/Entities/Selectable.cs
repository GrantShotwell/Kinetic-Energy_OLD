using KineticEnergy.Entities;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace KineticEnergy {

    public class Selectable : MonoBehaviour {
        public List<Action<Entity>> onDown = new List<Action<Entity>>();
        public bool show = false;
        public void Select(Entity sender) { foreach(var action in onDown) action.Invoke(sender); }

        new Renderer renderer;
        public void Start() {
            renderer = GetComponent<Renderer>();
        }

        public void FixedUpdate() {
            if(renderer.enabled != show)
                renderer.enabled = show;
            show = false;
        }

    }

}
