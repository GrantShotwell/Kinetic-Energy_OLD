using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace KineticEnergy.Intangibles {

    public class Master : MonoBehaviour {

        public GameObject clientPrefab;
        [HideInInspector] public GameObject client;

        public GameObject globalPrefab;
        [HideInInspector] public GameObject global;

        public GameObject serverPrefab;
        [HideInInspector] public GameObject server;

        public void Awake() {

            client = Instantiate(clientPrefab);
            client.transform.parent = transform;
            client.name = "Client GameObject";

            global = Instantiate(globalPrefab);
            global.transform.parent = transform;
            global.name = "Global GameObject";

            server = Instantiate(serverPrefab);
            server.transform.parent = transform;
            server.name = "Server GameObject";

        }

    }

}
