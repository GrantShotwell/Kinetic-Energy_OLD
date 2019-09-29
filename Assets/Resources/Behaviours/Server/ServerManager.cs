using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using KineticEnergy.Intangibles.Client;

namespace KineticEnergy.Intangibles.Server {

    /// <summary>Manages connection between the simulation and the <see cref="ClientData"/>s.</summary>
    public class ServerManager : ServerBehaviour {

        public List<ClientData> clients = new List<ClientData>();

        public void AddClient(ClientData client) {

            clients.Add(client);

        }

        public void SendClient(ClientData client) {

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

        private void Disconnect(ClientData client, string reason) {
            clients.Remove(client);
        }

    }

}
