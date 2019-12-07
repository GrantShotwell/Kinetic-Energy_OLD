using KineticEnergy.Intangibles.Behaviours;
using KineticEnergy.Intangibles.Client;
using System;
using System.Collections.Generic;
using UnityEngine;
using static KineticEnergy.Intangibles.Master.LevelsOfDetail;

namespace KineticEnergy.Intangibles.Server {

    /// <summary>Manages connection between the simulation (global) and connected users (client).</summary>
    public class ServerManager : ServerBehaviour {

        internal List<ClientData> clients = new List<ClientData>();
        internal List<ClientData> oldClients = new List<ClientData>();
        internal int count;
        internal GlobalBehavioursManager global;

        public override void OnAllSetup() {
            global = Manager.Manager.Global;
        }

        public void FixedUpdate() {
            //Smooth the inputs.
            for(int i = 0; i < count; i++) {

                var newClient = clients[i];
                var oldClient = oldClients[i];

                newClient.inputs.Smooth(oldClient.inputs);

                oldClient = clients[i];
                clients[i] = newClient;
                oldClients[i] = oldClient;

            }
            //Send the inputs.
            global.clients = clients.ToArray();
        }

        internal void Connect(ClientData client) {

            clients.Add(client);
            oldClients.Add(client);
            var comparison = new Comparison<ClientData>((a, b) => a.CompareTo(b));
            clients.Sort(comparison);
            oldClients.Sort(comparison);
            count++;

            if(CanShowLog(LevelOfDetail.Basic, Master.LogSettings.clients))
                Debug.LogFormat("Connected client {0}.", client);

        }

        internal void Disconnect(ClientData client, string reason) {

            clients.Remove(client);
            oldClients.Remove(client);
            count--;

            if(CanShowLog(LevelOfDetail.Basic, Master.LogSettings.clients))
                Debug.LogFormat("Disconnected client {0}: {1}", client, reason);

        }

        internal void SendClient(ClientData client) {

            var n = clients.Count;
            for(int i = 0; i < n; i++) {
                var c = clients[i];
                if(client.id == c.id) {
                    clients[i] = client;
                    return;
                }
            }

            Disconnect(client, "Connection Error.");

        }

    }

}
