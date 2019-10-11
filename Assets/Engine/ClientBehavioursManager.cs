using System.Collections.Generic;
using UnityEngine;
using KineticEnergy.Ships;
using KineticEnergy.Ships.Blocks;
using KineticEnergy.Interfaces.Manager;
using KineticEnergy.Intangibles.Terminal;
using KineticEnergy.Intangibles.Client;

namespace KineticEnergy.Intangibles.Behaviours {
    public class ClientBehavioursManager : BehavioursManager<ClientBehaviour> {

        /// <summary>The camera currently being used by the client.</summary>
        new public Camera camera { get; private set; }

        /// <summary><see cref="ClientManager"/> found from <see cref="BehavioursManager{ManagedBehaviour}.OnSetup"/>.</summary>
        public ClientManager clientManager { get; private set; }
        /// <summary>Shorthand for "<c>this.clientManager.client</c>".</summary>
        public ClientData client => clientManager.client;
        /// <summary>Shorthand for "<c>this.clientManager.client.inputs</c>".</summary>
        public Inputs inputs => clientManager.client.inputs;

        public override void OnSetup() {
            base.OnSetup();

            //Get the ClientManager.
            foreach(ClientBehaviour behaviour in managed) {
                if(behaviour is ClientManager clientManager) {
                    this.clientManager = clientManager;
                    break;
                }
            }

        }

        public void Update() {
            camera = Camera.main;
        }
    }
}
