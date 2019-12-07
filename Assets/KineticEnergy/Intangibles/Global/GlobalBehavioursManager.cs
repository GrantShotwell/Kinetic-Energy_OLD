using System;
using System.Collections.Generic;

using KineticEnergy.Content;
using KineticEnergy.Entities;
using KineticEnergy.Grids;
using KineticEnergy.Grids.Blocks;
using KineticEnergy.Intangibles.Client;
using KineticEnergy.Interfaces.Managed;
using KineticEnergy.Interfaces.Generic;

using UnityEngine;

namespace KineticEnergy.Intangibles.Behaviours {
    public class GlobalBehavioursManager : BehavioursManager<GlobalBehaviour>, IDynamicManager<AttachedBehaviour> {

        private ICollection<IInputReciever> InputRecievers { get; } = new LinkedList<IInputReciever>();

        private ICollection<AttachableRef> AttachablesRefs { get; }  = new List<AttachableRef>();
        private ICollection<AttachedBehaviour> Attachables { get; } = new List<AttachedBehaviour>();
        IEnumerable<AttachedBehaviour> IManager<AttachedBehaviour>.Managed => Attachables;
        public void RemoveMe(AttachedBehaviour me) => Attachables.Remove(me);

        public GlobalPaletteManager PaletteManager { get; internal set; }

        // TODO: replace array with hash thingy
        /// <summary><see cref="ClientData"/> array sorted by <see cref="ClientData.id"/>.</summary>
        internal ClientData[] clients = new ClientData[0];

        public override void OnSetup() {
            base.OnSetup();

            foreach(Content<AttachedBehaviour> content in Manager.Loader.FinalGroup.Attachables.List)
                foreach(Attachable attribute in content.Type.GetCustomAttributes(typeof(Attachable), true))
                    AttachablesRefs.Add(new AttachableRef(content.Type, attribute));

        }

        public void Update() {

            //Send inputs to input recievers.
            foreach(IInputReciever reciever in InputRecievers) {
                foreach(int id in reciever.Clients) {
                    int index = Array.BinarySearch(clients, new ClientData(id));
                    reciever.SendInputs(id, clients[index].inputs);
                }
            }

        }

        /// <summary>Gives this prefab loaded components that want to attach to it, such as <see cref="AttachedBehaviour"/>s.</summary>
        public void PolishPrefab(GameObject prefab) {

            //Polishing a prefab with GlobalBehavioursManager means that it is a global object.
            prefab.layer = 10;

            //Add attachables.
            foreach(MonoBehaviour behaviour in prefab.GetComponents<MonoBehaviour>()) {

                //Attachables attach to MonoBehaviours.
                Type behaviourType = behaviour.GetType();
                foreach(AttachableRef attachable in AttachablesRefs) {

                    //Which MonoBehaviours can this attachable attach to?
                    IEnumerable<MonoBehaviour> monos = prefab.GetComponents<MonoBehaviour>();
                    foreach(Type allowed in attachable.attribute.types) {
                        if(attachable.attribute.AttachesTo(behaviourType)) {

                            //Make sure there are no duplicates.
                            foreach(MonoBehaviour mono in monos)
                                if(mono.GetType() == allowed) goto NextAllowed;

                            //Finally, attatch the attachable!
                            PolishBehaviour((MonoBehaviour)prefab.AddComponent(attachable.type));

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
                case Block block: block.Master = Manager; block.Dimensions = block.Dimensions; break;
                case BlockPreview preview: preview.Master = Manager; break;
                case BlockGrid grid: grid.Master = Manager; break;
                case Entity entity: entity.Master = Manager; break;
            }

            // Input Recievers //
            if(behaviour is IInputReciever inputReciever)
                InputRecievers.Add(inputReciever);

        }

    }
}
