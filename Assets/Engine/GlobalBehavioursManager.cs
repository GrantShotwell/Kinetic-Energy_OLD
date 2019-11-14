using KineticEnergy.Content;
using KineticEnergy.Entities;
using KineticEnergy.Intangibles.Client;
using KineticEnergy.Interfaces.Input;
using KineticEnergy.Interfaces.Manager;
using KineticEnergy.Ships;
using KineticEnergy.Ships.Blocks;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace KineticEnergy.Intangibles.Behaviours {
    public class GlobalBehavioursManager : BehavioursManager<GlobalBehaviour>, IDynamicManager<AttachedBehaviour> {

        private readonly List<ISingleInputReciever> snglInputs = new List<ISingleInputReciever>();
        private readonly List<IMultipleInputReciever> multInputs = new List<IMultipleInputReciever>();

        //AttachedBehaviours
        private readonly List<AttachableRef> attachablesRefs = new List<AttachableRef>();
        private readonly List<AttachedBehaviour> attachables = new List<AttachedBehaviour>();
        IEnumerable<AttachedBehaviour> IManager<AttachedBehaviour>.Managed => attachables;
        public void RemoveMe(AttachedBehaviour me) => attachables.Remove(me);

        public GlobalPaletteManager Palettes { get; internal set; }


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
                reciever.Inputs = index < 0 ? default : clients[index].inputs;

            }

            //Multiple
            foreach(IMultipleInputReciever reciever in multInputs) {

                var ids = reciever.Clients;
                var count = ids.Length;
                var inputs = new Inputs[count];
                foreach(int id in ids) {
                    int index = Array.BinarySearch(clients, new ClientData(id));
                    inputs[count--] = index < 0 ? default : clients[index].inputs;
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
                Type behaviourType = behaviour.GetType();
                foreach(AttachableRef attachable in attachablesRefs) {

                    //Which MonoBehaviours can this attachable attach to?
                    IEnumerable<MonoBehaviour> monos = prefab.GetComponents<MonoBehaviour>();
                    foreach(Type allowed in attachable.attribute.types) {
                        if(attachable.attribute.AttachesTo(behaviourType)) {

                            //Make sure there are no duplicates.
                            foreach(MonoBehaviour mono in monos)
                                if(mono.GetType() == allowed) goto NextAllowed;

                            //Finally, attatch the attachable!
                            PolishBehaviour((MonoBehaviour)
                                prefab.AddComponent(attachable.type));

                            break; //Since duplicates must be implemented via other behaviours if desired (ie. not us), we know that we can stop here.
                            NextAllowed: continue;

                        }

                    }

                    //NextAttachable: continue;

                }

            }

        }

        /// <summary>Notifies this <see cref="GlobalBehavioursManager"/> that the given element is a new <see cref="MonoBehaviour"/> that now exists.</summary>
        public void PolishBehaviour(MonoBehaviour behaviour) {

            // The four horsemen of the apocolypse. //
            switch(behaviour) {
                case Block block: block.Master = Manager; break;
                case Entity entity: entity.Master = Manager; break;
                case BlockGrid grid: grid.Master = Manager; break;
                case BlockPreview preview: preview.Master = Manager; break;
            }

            // Input Recievers //
            if(behaviour is ISingleInputReciever snglInput)
                snglInputs.Add(snglInput);
            if(behaviour is IMultipleInputReciever multInput)
                multInputs.Add(multInput);

        }

    }
}
