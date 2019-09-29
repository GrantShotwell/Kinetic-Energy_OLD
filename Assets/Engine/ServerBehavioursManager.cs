using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace KineticEnergy.Intangibles.Server {
    
    public class ServerBehavioursManager : MonoBehaviour {

        /// <summary>An array of <see cref="GameObject"/>s that, combined, have all <see cref="ServerBehaviour"/>s.</summary>
        public GameObject[] parents;
        /// <summary>Finds all <see cref="ServerBehaviour"/>s from <see cref="parents"/>.</summary>
        public ServerBehaviour[] GlobalBehaviours {
            get {
                List<ServerBehaviour> behaviours = new List<ServerBehaviour>();
                foreach(GameObject parent in parents)
                    foreach(ServerBehaviour behaviour in parent.GetComponents<ServerBehaviour>())
                        behaviours.Add(behaviour);
                return behaviours.ToArray();
            }
        }

        public void Awake() {
            var prefabs = Resources.LoadAll<GameObject>("Behaviours/Server");
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
