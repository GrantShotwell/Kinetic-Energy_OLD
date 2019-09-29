using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace KineticEnergy.Intangibles.Client {
    
    public class ClientBehavioursManager : MonoBehaviour {

        /// <summary>An array of <see cref="GameObject"/>s that, combined, have all <see cref="ClientBehaviour"/>s.</summary>
        public GameObject[] parents;
        /// <summary>Finds all <see cref="ClientBehaviour"/>s from <see cref="parents"/>.</summary>
        public ClientBehaviour[] GlobalBehaviours {
            get {
                List<ClientBehaviour> behaviours = new List<ClientBehaviour>();
                foreach(GameObject parent in parents)
                    foreach(ClientBehaviour behaviour in parent.GetComponents<ClientBehaviour>())
                        behaviours.Add(behaviour);
                return behaviours.ToArray();
            }
        }

        public void Awake() {
            var prefabs = Resources.LoadAll<GameObject>("Behaviours/Client");
            List<GameObject> instantiated = new List<GameObject>();
            foreach(GameObject prefab in prefabs) {
                var obj = Instantiate(prefab);
                obj.transform.parent = transform;
                obj.name = prefab.name;
                instantiated.Add(obj);
            }
            parents = instantiated.ToArray();
        }

    }

}
