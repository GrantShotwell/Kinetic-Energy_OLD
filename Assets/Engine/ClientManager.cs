using System;
using UnityEngine;
using KineticEnergy.Intangibles.Server;
using KineticEnergy.Intangibles.Behaviours;

namespace KineticEnergy.Intangibles.Client {

    /// <summary>One of these exist on each user's computer to send messages to the server simulation.
    /// Should not exist on the server simulation.</summary>
    public class ClientManager : ClientBehaviour {

        public ClientData client;
        public ServerManager server;

        public override void OnAllSetup() {
            client = new ClientData(UnityEngine.Random.Range(int.MinValue, int.MaxValue));
            server = Manager.Manager.server.GetComponent<ServerManager>();
            server.Connect(client);
            client.inputs.hotbar = -1;
        }

        public void Update() {

            #region Input

            // Move //
            {
                client.inputs.move.x = Input.GetAxis("Move X");
                client.inputs.move.y = Input.GetAxis("Move Y");
                client.inputs.move.z = Input.GetAxis("Move Z");
            }

            // Look //
            {
                client.inputs.look.x = Input.GetAxis("Look X");
                client.inputs.look.y = Input.GetAxis("Look Y");
                client.inputs.look.z = Input.GetAxis("Rotate");
            }

            // Spin //
            {
                client.inputs.spin.x = Input.GetAxis("Rotate Block X");
                client.inputs.spin.y = Input.GetAxis("Rotate Block Y");
                client.inputs.spin.z = Input.GetAxis("Rotate Block Z");
            }

            // Hotbar //
            {
                bool hotbar1 = Input.GetButtonDown("Hotbar 1");
                bool hotbar2 = Input.GetButtonDown("Hotbar 2");
                bool hotbar3 = Input.GetButtonDown("Hotbar 3");
                bool hotbar4 = Input.GetButtonDown("Hotbar 4");
                bool hotbar5 = Input.GetButtonDown("Hotbar 5");

                var hotbar = 0;
                /**/ if(hotbar1) hotbar = 0;
                else if(hotbar2) hotbar = 1;
                else if(hotbar3) hotbar = 2;
                else if(hotbar4) hotbar = 3;
                else if(hotbar5) hotbar = 4;
                bool any = hotbar1 || hotbar2 || hotbar3 || hotbar4 || hotbar5;
                if(any) {
                    if(Input.GetButton("Hotbar ALT")) hotbar += 5;
                    client.inputs.hotbar = hotbar;
                }

            }

            // Buttons //
            {
                client.inputs.primary = new Button("Primary");
                client.inputs.scndary = new Button("Secondary");
                client.inputs.terminal = new Button("ToggleTerminal");
            }

            #endregion

            SendClient();

        }

        public void SendClient() {

            server.SendClient(client);

        }

    }

    #region Client Data
    /// <summary>Represents a user that is connected to the server.</summary>
    /// <seealso cref="id"/>
    /// <seealso cref="name"/>
    public struct ClientData : IComparable<ClientData> {

        /// <summary>The (hopefully) unique id of this <see cref="ClientData"/>.</summary>
        public readonly int id;
        /// <summary>The display name of this <see cref="ClientData"/>.</summary>
        /// <remarks>The display name is never used for equality checks. This includes
        /// <see cref="Equals(object)"/>, <see cref="operator ==(ClientData, ClientData)"/>,
        /// <see cref="operator !=(ClientData, ClientData)"/>, and <see cref="GetHashCode"/>.</remarks>
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

        #region Operators and Comparison
        /// <summary>Returns the <see cref="id"/>.</summary>
        public static implicit operator int(ClientData client) => client.id;
        /// <summary>Uses <see cref="int.CompareTo(int)"/> on the <see cref="id"/>s.</summary>
        public int CompareTo(ClientData other) => id.CompareTo(other.id);
        /// <summary>Shorthand for "<c>new Comparison((a, b) => a.CompareTo(b))</c>".</summary>
        public static Comparison<ClientData> comparison => new Comparison<ClientData>((a, b) => a.CompareTo(b));
        /// <summary>Returns if the <see cref="id"/>s are equal.</summary>
        public static bool operator ==(ClientData left, ClientData right) => left.id == right.id;
        /// <summary>Returns if the <see cref="id"/>s are unequal.</summary>
        public static bool operator !=(ClientData left, ClientData right) => left.id != right.id;
        /// <summary>Returns the <see cref="id"/>.</summary>
        public override int GetHashCode() => id;
        /// <summary>Returns if the given object is a <see cref="ClientData"/> and if both have the same <see cref="id"/>.</summary>
        public override bool Equals(object obj) => obj is ClientData other ? this.id == other.id : false;
        /// <summary>Shorthand for "<c>string.Format("{0}: \"{1}\"", id, name)</c>."</summary>
        public override string ToString() => string.Format("{0}: \"{1}\"", id, name);
        #endregion

    }
    #endregion

    #region Inputs
    /// <summary>Represents all recognized inputs that a user could be sending.</summary>
    public struct Inputs {
        /// <summary>Move character or ship.</summary>
        public Vector3 move;
        /// <summary>Look camera or ship.</summary>
        public Vector3 look;
        /// <summary>Spin item in hand, such as block.</summary>
        public Vector3 spin;
        /// <summary>Index of hotbar selection.</summary>
        public int hotbar;
        /// <summary>Primary button.</summary>
        public Button primary;
        /// <summary>Secondary button.</summary>
        public Button scndary;
        /// <summary>Toggle Terminal button.</summary>
        public Button terminal;

        public void Smooth(Inputs last) {
            primary.Smooth(last.primary);
            scndary.Smooth(last.scndary);
        }

        public override string ToString() {
            return string.Format("Move: {0}, Look: {1}, Spin: {2}, Hotbar: {3}, Primary: {4}, Secondary: {5}.",
                                        move,      look,      spin,       hotbar,       primary,       scndary);
        }

    }
    #endregion

    #region Button
    /// <summary>A struct for a <see cref="State"/>.</summary>
    public struct Button {

        #region State
        /// <summary>Represents the input state of a <see cref="Button"/>.</summary>
        public enum State {
            /// <summary>Button is being pushed down this frame.</summary>
            Down,
            /// <summary>Button is still being held down after <see cref="Down"/>.</summary>
            Held,
            /// <summary>Button was just released this frame.</summary>
            Free,
            /// <summary>Button is still released after <see cref="Free"/>.</summary>
            Idle
        }
        #endregion

        #region Struct
        /// <summary>The <see cref="State"/> of this button.</summary>
        public State state { get; private set; }
        /// <summary>Creates a new <see cref="Button"/> with the given values.</summary>
        /// <param name="state">The <see cref="State"/> of this button.</param>
        public Button(State state) { this.state = state; }
        /// <summary>Creates a new <see cref="Button"/> with values from <see cref="Input"/>.</summary>
        /// <param name="name">Name of the <see cref="Input"/> axis to get the button from.</param>
        public Button(string name) =>
            state = Input.GetButtonDown(name) ? State.Down
                  : Input.GetButtonUp(name) ? State.Free
                  : Input.GetButton(name) ? State.Held 
                  : State.Idle;
        #endregion

        #region Next Frame
        /// <summary>Modifies this <see cref="Button"/> to go to the next predicted frame.</summary>
        /// <seealso cref="NextFrame"/>
        public void ToNextFrame() {
            if(state == State.Down) state = State.Held;
            if(state == State.Held) state = State.Held;
            state = State.Idle;
        }
        /// <summary>Creates a new <see cref="Button"/> for the next predicted frame.</summary>
        /// <seealso cref="ToNextFrame"/>
        public Button NextFrame {
            get {
                if(state == State.Down) return new Button(State.Held);
                if(state == State.Held) return new Button(State.Held);
                return new Button(State.Idle);
            }
        }
        #endregion

        #region Smooth

        /// <summary>Attempts to 'smooth' incoming inputs by comparing the last known frame, its prediction for the current frame, and the current frame (this).</summary>
        /// <param name="last">The last frame of this <see cref="Button"/>.</param>
        public void Smooth(Button last) {
            Button predicted = last.NextFrame, actual = this;
            if(predicted.Held) {
                if(actual.IsDown) state = State.Held;
                if(actual.IsFree) state = State.Free;
            }
            if(predicted.Idle) {
                if(actual.IsFree) state = State.Idle;
                if(actual.IsDown) state = State.Down;
            }
        }

        #endregion

        #region Checks
        /// <summary>Shorthand for "<c>state == <see cref="State.Down"/> || state == <see cref="State.Held"/></c>".</summary>
        /// <remarks>Essentially the same use as <see cref="Input.GetButton"/>. Always the opposite of <see cref="IsFree"/>.
        /// <para/>Not to be confused with <see cref="Down"/>, which only checks for <see cref="State.Down"/>.</remarks>
        public bool IsDown => state == State.Down || state == State.Held;
        /// <summary>Shorthand for "<c>state == <see cref="State.Free"/> || state == <see cref="State.Idle"/></c>".</summary>
        /// <remarks>Essentially the same use as ~<see cref="Input.GetButton"/>. Always the opposite of <see cref="IsDown"/>.
        /// <para/>Not to be confused with <see cref="Free"/>, which only checks for <see cref="State.Free"/>.</remarks>
        public bool IsFree => state == State.Free || state == State.Idle;

        /// <summary>Shorthand for "<c>state == State.Down</c>".</summary>
        /// <remarks>Not to be confused with <see cref="IsDown"/>.</remarks>
        public bool Down => state == State.Down;
        /// <summary>Shorthand for "<c>state == State.Held</c>".</summary>
        public bool Held => state == State.Held;
        /// <summary>Shorthand for "<c>state == State.Free</c>".</summary>
        /// <remarks>Not to be confused with <see cref="IsFree"/>.</remarks>
        public bool Free => state == State.Free;
        /// <summary>Shorthand for "<c>state == State.Idle</c>".</summary>
        public bool Idle => state == State.Idle;
        #endregion

        #region Other

        public static implicit operator bool(Button button) => button.IsDown;
        public override string ToString() {
            if(state == State.Down) return "Down";
            if(state == State.Held) return "Held";
            if(state == State.Free) return "Free";
            else                    return "None";
        }

        #endregion

    }
    #endregion

}
