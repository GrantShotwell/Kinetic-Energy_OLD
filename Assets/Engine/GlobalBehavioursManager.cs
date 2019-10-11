using System;
using System.Collections.Generic;
using UnityEngine;
using KineticEnergy.Content;
using KineticEnergy.Interfaces.Input;
using KineticEnergy.Interfaces.Manager;
using KineticEnergy.Intangibles.Client;
using KineticEnergy.Intangibles.Terminal;

namespace KineticEnergy.Intangibles.Behaviours {
    public class GlobalBehavioursManager : BehavioursManager<GlobalBehaviour>, IDynamicManager<AttachedBehaviour> {

        private readonly  List<ISingleInputReciever>  snglInputs =  new List<ISingleInputReciever>();
        private readonly List<IMultipleInputReciever> multInputs = new List<IMultipleInputReciever>();

        //AttachedBehaviours
        private readonly List<AttachableRef> attachablesRefs = new List<AttachableRef>();
        private readonly List<AttachedBehaviour> attachables = new List<AttachedBehaviour>();
        IEnumerator<AttachedBehaviour> IManager<AttachedBehaviour>.Managed => attachables.GetEnumerator();
        public void RemoveMe(AttachedBehaviour me) => attachables.Remove(me);

        public GlobalPaletteManager palettes { get; private set; }


        /// <summary><see cref="ClientData"/> array sorted by <see cref="ClientData.id"/>.</summary>
        internal ClientData[] clients = new ClientData[0];

        public override void OnSetup() {
            base.OnSetup();

            foreach(Content<AttachedBehaviour> content in Manager.loader.m_attachables)
                foreach(Attachable attribute in content.type.GetCustomAttributes(typeof(Attachable), true))
                    attachablesRefs.Add(new AttachableRef(content.type, attribute));

        }

        public void Update() {

            #region Send Inputs

            //Single
            foreach(ISingleInputReciever reciever in snglInputs) {

                int index = Array.BinarySearch(clients, new ClientData(reciever.Client));
                if(index < 0) reciever.Inputs = default;
                else reciever.Inputs = clients[index].inputs;

            }

            //Multiple
            foreach(IMultipleInputReciever reciever in multInputs) {

                var ids = reciever.Clients;
                var count = ids.Length;
                var inputs = new Inputs[count];
                foreach(int id in ids) {
                    int index = Array.BinarySearch(clients, new ClientData(id));
                    if(index < 0) inputs[count] = default;
                    else inputs[count] = clients[index].inputs;
                    count--;
                }

            }

            #endregion

        }

        /// <summary>Gives this prefab loaded components that want to attach to it, such as <see cref="AttachedBehaviour"/>s.</summary>
        public void PolishPrefab(GameObject prefab) {

            //Polishing a prefab with GlobalBehavioursManager means that it is a global object.
            prefab.layer = 10;

            //Add attachables.
            foreach(MonoBehaviour behaviour in prefab.GetComponents<MonoBehaviour>()) {
                
                //Attachables attach to MonoBehaviours.
                Type monoType = behaviour.GetType();
                foreach(AttachableRef attachableRef in attachablesRefs) {

                    //Which MonoBehaviours can this attachable attach to?
                    Type attachable = attachableRef.type;
                    foreach(Type allowedMonos in attachableRef.attribute.types) {

                        //Make sure there are no duplicates.
                        if(attachableRef.attribute.AttachesTo(monoType)) {
                            bool hasAttachable = false;
                            foreach(MonoBehaviour m in prefab.GetComponents<MonoBehaviour>())
                                if(m.GetType() == attachableRef.type) hasAttachable = true;

                            //Finally, attatch the attachable!
                            if(!hasAttachable) {
                                var addedBehaviour = prefab.AddComponent(attachableRef.type);
                                PolishBehaviour((MonoBehaviour)addedBehaviour);
                                break; //Since duplicates must be implemented via other behaviours if desired (ie. not us), we know that we can stop here.
                            }
                        }

                    }
                }
            }

        }

        /// <summary>Adds the given <see cref="MonoBehaviour"/> to lists such as for input receiving.</summary>
        public void PolishBehaviour(MonoBehaviour behaviour) {

            // Input Recievers //
            if(behaviour is ISingleInputReciever snglInput)
                snglInputs.Add(snglInput);
            if(behaviour is IMultipleInputReciever multInput)
                multInputs.Add(multInput);

        }

    }
}
