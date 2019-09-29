using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace KineticEnergy.Intangibles.Global {

    public class GlobalBehavioursManager : MonoBehaviour {

        /// <summary>An array of <see cref="GameObject"/>s that, combined, have all <see cref="GlobalBehaviour"/>s.</summary>
        public GameObject[] parents;
        /// <summary>Finds all <see cref="GlobalBehaviour"/>s from <see cref="parents"/>.</summary>
        public GlobalBehaviour[] GlobalBehaviours {
            get {
                List<GlobalBehaviour> behaviours = new List<GlobalBehaviour>();
                foreach(GameObject parent in parents)
                    foreach(GlobalBehaviour behaviour in parent.GetComponents<GlobalBehaviour>())
                        behaviours.Add(behaviour);
                return behaviours.ToArray();
            }
        }

        public void Awake() {
            var prefabs = Resources.LoadAll<GameObject>("Behaviours/Global");
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
