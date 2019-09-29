using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using KineticEnergy.Intangibles.Server;

namespace KineticEnergy.Intangibles.Client {

    /// <summary>One of these exist on each user's computer to send messages to the server simulation.
    /// Should not exist on the server simulation.</summary>
    public class ClientManager : ClientBehaviour {

        public ClientData client;
        public ServerManager server;

        public void Start() {
            client = new ClientData(UnityEngine.Random.Range(int.MinValue, int.MaxValue));
            server = FindObjectOfType<ServerManager>();
        }

        public void Update() {

            #region Input

            //Move
            client.inputs.move.x = Input.GetAxis("Move X");
            client.inputs.move.y = Input.GetAxis("Move Y");
            client.inputs.move.z = Input.GetAxis("Move Z");

            //Look
            client.inputs.look.x = Input.GetAxis("Look X");
            client.inputs.look.y = Input.GetAxis("Look Y");
            client.inputs.look.z = Input.GetAxis("Rotate");

            //Spin
            client.inputs.spin.x = Input.GetAxis("Rotate Block X");
            client.inputs.spin.y = Input.GetAxis("Rotate Block Y");
            client.inputs.spin.z = Input.GetAxis("Rotate Block Z");

            //Hotbar
            client.inputs.hotbar = -1;
            /**/ if(Input.GetButtonDown("Hotbar 1")) client.inputs.hotbar = 0;
            else if(Input.GetButtonDown("Hotbar 2")) client.inputs.hotbar = 1;
            else if(Input.GetButtonDown("Hotbar 3")) client.inputs.hotbar = 2;
            else if(Input.GetButtonDown("Hotbar 4")) client.inputs.hotbar = 3;
            else if(Input.GetButtonDown("Hotbar 5")) client.inputs.hotbar = 4;
            else client.inputs.hotbar = -6;
            if(Input.GetButton("Hotbar ALT")) client.inputs.hotbar += 5;
            if(client.inputs.hotbar < 0) client.inputs.hotbar = -1;

            //Primary & Secondary
            client.inputs.primary = Input.GetButton("Primary");
            client.inputs.secondary = Input.GetButton("Secondary");

            #endregion

            SendClient();

        }

        public void SendClient() {

            server.SendClient(client);

        }

    }

    /// <summary>Represents a user that is connected to the server.</summary>
    public struct ClientData {

        /// <summary>The id of this <see cref="ClientData"/>.</summary>
        public readonly int id;
        /// <summary>The display name of this <see cref="ClientData"/>.</summary>
        public readonly string name;

        /// <summary>The inputs this <see cref="ClientData"/> is sending,
        /// directed by its associated <see cref="ClientManager"/>.</summary>
        public Inputs inputs;

        /// <summary>Creates a new <see cref="ClientData"/> with the given properties.</summary>
        /// <param name="id">The <see cref="id"/> of the new <see cref="ClientData"/>.</param>
        /// <param name="name">The <see cref="name"/> of the new <see cref="ClientData"/>.</param>
        public ClientData(int id = 0, string name = "Unnamed Client") {
            this.id = id;
            this.name = name;
            inputs = new Inputs();
        }

        /// <summary>Contains the inputs that a <see cref="ClientData"/> is sending.</summary>
        public struct Inputs {
            /// <summary>Move character or ship.</summary>
            public Vector3 move;
            /// <summary>Look camera or ship.</summary>
            public Vector3 look;
            /// <summary>Spin item in hand, such as block.</summary>
            public Vector3 spin;
            /// <summary>Index of hotbar selection.</summary>
            public int hotbar;
            /// <summary>Primary button pressed.</summary>
            public bool primary;
            /// <summary>Secondary button pressed.</summary>
            public bool secondary;
        }

    }

}
