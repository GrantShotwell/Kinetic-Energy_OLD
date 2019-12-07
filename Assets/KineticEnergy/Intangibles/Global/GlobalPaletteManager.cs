using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using KineticEnergy.Structs;
using KineticEnergy.Grids.Blocks;
using KineticEnergy.Content;
using KineticEnergy.Entities;
using static KineticEnergy.Intangibles.Master.LevelsOfDetail;
using static KineticEnergy.Grids.Blocks.BlockAttributes;

namespace KineticEnergy.Intangibles.Behaviours {

    /// <summary>Stores and manages the one and only <see cref="BlockPalette"/>.</summary>
    /// <seealso cref="BlockPalette"/>
    /// <seealso cref="ColorPalette"/>
    public class GlobalPaletteManager : GlobalBehaviour {

        public BlockPalette blocks = new BlockPalette();
        public EntityPalette entities = new EntityPalette();
        public ColorPalette colors = new ColorPalette(2);

        public override void OnSetup() {
            //Maybe make these async?? Seems like a good idea???
            // (maybe research async a bit more first)
            UpdateColorPalette();
            UpdateBlockPalette();
            UpdateEntityPalette();
            Manager.AsGlobal.PaletteManager = this;
        }

        // Delayeds //
        private readonly List<Action> delayeds = new List<Action>();
        public void Update() {
            foreach(Action delayed in delayeds)
                delayed.Invoke();
        }

        void UpdateBlockPalette() {

            Prefab<Grids.BlockGrid> gridPrefab = Master.prefabs.grid;
            Manager.AsGlobal.PolishPrefab(gridPrefab);

            blocks.Clear();
            foreach(Content<BlockPreview> content in Master.Loader.FinalGroup.Blocks.List) {
                Type typePreview = content.Type;
                Type typeOriginal = content.Type.DeclaringType;

                //Create the original prefab.
                var original = new Prefab<Block>((Block)new GameObject().AddComponent(typeOriginal));
                original.gameObject.SetActive(false);
                //Create the preview prefab.
                var preview = new Prefab<BlockPreview>((BlockPreview)new GameObject().AddComponent(typePreview));
                preview.gameObject.SetActive(false);

                //Find attributes and sort by priority.
                var attributes = new List<BlockAttribute>();
                foreach(BlockAttribute attribute in typeOriginal.GetCustomAttributes(typeof(BlockAttribute), true))
                    attributes.Add(attribute);
                attributes.Sort(new Comparison<BlockAttribute>((a, b) => (int)b.Order - (int)a.Order));

                //Apply the attributes.
                #region Log Attribute (Basic)
                if(CanShowLog(LevelOfDetail.Basic, Master.LogSettings.attributes))
                    Debug.LogFormat("/* Applying BlockAttributes to types '{0}':'{1}' :: original:preview.", typeOriginal, typePreview);
                #endregion
                foreach(BlockAttribute attribute in attributes) {
                    try {
                        #region Log Attribute (High)
                        if(CanShowLog(LevelOfDetail.High, Master.LogSettings.attributes))
                            Debug.LogFormat("Applying '{0}' to preview...", attribute, typePreview);
                        #endregion
                        attribute.ApplyToPreview(preview, Master);
                        #region Log Attribute (High)
                        if(CanShowLog(LevelOfDetail.High, Master.LogSettings.attributes))
                            Debug.LogFormat("Applying '{0}' to original...", attribute, typeOriginal);
                        #endregion
                        attribute.ApplyToOriginal(original, Master);
                    } catch(Exception e) {
                        Debug.LogErrorFormat("Exception thrown while applying {0} '{1}' to '{2}'/'{3}'. \"{4}\".",
                            nameof(BlockAttribute), attribute, original, preview, e);
                        Destroy(preview.gameObject);
                        Destroy(original.gameObject);
                        goto NextContent;
                    }
                }
                #region Log Attribute (Basic)
                if(CanShowLog(LevelOfDetail.Basic, Master.LogSettings.attributes))
                    Debug.LogFormat("Finished applying attributes. */");
                #endregion

                //Finish the prefab setup.
                Manager.AsGlobal.PolishPrefab(preview);
                Manager.AsGlobal.PolishPrefab(original);
                preview.component.blockPrefab = original;
                preview.component.gridPrefab = gridPrefab;

                //Notify the block components to do whatever else they want to do for a prefab.
                preview.component.PrefabSetup();
                original.component.PrefabSetup();

                //Save the prefabs.
                blocks.Add(new BlockPalette.Sample(original, preview));

                NextContent: continue;

            }

        }

        /// <summary>Uses <see cref="Master.Loader"/> to add all values into <see cref="entities"/>.</summary>
        void UpdateEntityPalette() {
            GlobalBehavioursManager global = Manager.AsGlobal;

            entities.Clear();
            foreach(Content<Entity> content in Manager.Manager.Loader.FinalGroup.Entities.List) {

                //We need an instance of the preview to make a prefab since abstract methods can't be static,
                // but we can't create a monobehaviour without a prefab...
                // so we create a temp entity monobehaivour on our GameObject!
                var temp = (Entity)gameObject.AddComponent(content.Type);
                temp.enabled = false; delayeds.Add(() => Destroy(temp));

                //New we make the prefab using *.BuildEntityPrefab(),
                // then allow the GlobalBehavioursManager to 'polish' them.
                // After that, we tie the type and the prefab in a EntityPalette.Sample(type, prefab).
                global.PolishBehaviour(temp);
                GameObject prefab = temp.BuildEntityPrefab();
                global.PolishPrefab(prefab);
                global.PolishBehaviour(prefab.GetComponent<Entity>());
                entities.Add(new EntityPalette.Sample(content.Type, prefab));

            }

        }

        void UpdateColorPalette() {

            colors[0] = new ColorPalette.Sample("Preview Valid", new Color(0.10f, 0.90f, 0.00f, 0.30f));
            colors[1] = new ColorPalette.Sample("Preview Invalid", new Color(0.90f, 0.10f, 0.00f, 0.30f));

        }

    }

    #region Block Palette

    /// <summary>An object that contains a list of <see cref="Sample"/>s.</summary>
    /// <remarks>Extends <see cref="IEnumerable"/> for type <see cref="Sample"/>.</remarks>
    /// <seealso cref="GlobalPaletteManager"/>
    /// <seealso cref="Sample"/>
    [Serializable]
    public class BlockPalette : IEnumerable<BlockPalette.Sample> {

        /// <summary>A <see cref="List{T}"/> of <see cref="Sample"/>s.</summary>
        [SerializeField]
        private List<Sample> samples = new List<Sample>();

        /// <summary>A class for containing a <see cref="Block"/> prefab and it's respective <see cref="BlockPreview"/> prefab.</summary>
        /// <seealso cref="GlobalPaletteManager"/>
        /// <seealso cref="BlockPalette"/>
        [Serializable]
        public class Sample {

            /// <summary>The prefab that will be cloned with <see cref="Object.Instantiate()"/>.</summary>
            /// <see cref="Block"/>
            public GameObject prefabBlock;

            /// <summary>The prefab variant of the <see cref="prefabBlock"/>.</summary>
            /// <see cref="BlockPreview"/>
            public GameObject prefabBlock_preview;

            /// <summary>Creates a new <see cref="Sample"/> from the given arguments.</summary>
            /// <param name="prefabBlock">The prefab variant of the <see cref="prefabBlock"/>.</param>
            /// <param name="prefabBlock_preview">The prefab that will be cloned with <see cref="Object.Instantiate()"/></param>
            /// <see cref="Block"/>
            /// <see cref="BlockPreview"/>
            public Sample(GameObject prefabBlock, GameObject prefabBlock_preview) {
                this.prefabBlock = prefabBlock;
                this.prefabBlock_preview = prefabBlock_preview;
            }

        }

        public IEnumerator<Sample> GetEnumerator() { return ((IEnumerable<Sample>)samples).GetEnumerator(); }
        IEnumerator IEnumerable.GetEnumerator() { return ((IEnumerable<Sample>)samples).GetEnumerator(); }

        /// <summary>A simple "get" function for <see cref="samples"/>.
        /// <para/><c>return <see cref="samples"/>[index];</c></summary>
        /// <param name="index">The index to look at.</param>
        /// <returns>Returns the <see cref="Sample"/> at the given index.</returns>
        public Sample this[int index] { get { return samples[index]; } }

        /// <summary>The <see cref="List{T}.Count"/> for <see cref="samples"/>.</summary>
        public int Count => samples.Count;

        /// <summary>Adds a <see cref="Sample"/> to <see cref="samples"/>.</summary>
        /// <param name="blockSample">The <see cref="Sample"/> to add to <see cref="samples"/>.</param>
        public void Add(Sample blockSample) {
            samples.Add(blockSample);
        }

        /// <summary>Clears <see cref="samples"/>.</summary>
        public void Clear() {
            samples.Clear();
        }

    }
    #endregion

    #region Entity Palette

    /// <summary>An object that contains a list of <see cref="Sample"/>s.</summary>
    /// <remarks>Extends <see cref="IEnumerable"/> for type <see cref="Sample"/>.</remarks>
    /// <seealso cref="GlobalPaletteManager"/>
    /// <seealso cref="Sample"/>
    [Serializable]
    public class EntityPalette : IEnumerable<EntityPalette.Sample> {

        /// <summary>A <see cref="List{T}"/> of <see cref="Sample"/>s.</summary>
        [SerializeField]
        private List<Sample> samples = new List<Sample>();
        /// <summary>Contains a subclass (<see cref="Type"/>) of <see cref="Entity"/> and its prefab.</summary>
        [Serializable]
        public class Sample {
            /// <summary>The subclass <see cref="Type"/> of <see cref="Entity"/> that this represents.</summary>
            public readonly Type type;
            /// <summary>The prefab created from <see cref="type"/>'s <see cref="Entity.BuildEntityPrefab"/> method.</summary>
            public readonly GameObject prefab;
            /// <summary>Creates a new <see cref="Sample"/> with the given properties.</summary>
            /// <param name="type">The <see cref="type"/>.</param>
            /// <param name="prefab">The <see cref="prefab"/>.</param>
            public Sample(Type type, GameObject prefab) {
                this.type = type;
                this.prefab = prefab;
            }
        }

        public IEnumerator<Sample> GetEnumerator() { return ((IEnumerable<Sample>)samples).GetEnumerator(); }
        IEnumerator IEnumerable.GetEnumerator() { return ((IEnumerable<Sample>)samples).GetEnumerator(); }

        /// <summary>A simple "get" function for <see cref="samples"/>.
        /// <para/><c>return <see cref="samples"/>[index];</c></summary>
        /// <param name="index">The index to look at.</param>
        /// <returns>Returns the <see cref="Sample"/> at the given index.</returns>
        public Sample this[int index] { get { return samples[index]; } }

        /// <summary>The <see cref="List{T}.Count"/> for <see cref="samples"/>.</summary>
        public int Count => samples.Count;

        /// <summary>Adds a <see cref="Sample"/> to <see cref="samples"/>.</summary>
        /// <param name="entitySample">The <see cref="Sample"/> to add to <see cref="samples"/>.</param>
        public void Add(Sample entitySample) {
            samples.Add(entitySample);
        }

        /// <summary>Clears <see cref="samples"/>.</summary>
        public void Clear() {
            samples.Clear();
        }

    }

    #endregion

    #region Color Palette

    /// <summary>An object that contains a list of <see cref="Sample"/>s.</summary>
    [Serializable]
    public class ColorPalette : IEnumerable<ColorPalette.Sample> {

        /// <summary>An array of <see cref="Sample"/>s.</summary>
        [SerializeField]
        private Sample[] samples;
        /// <summary>A named color.</summary>
        /// <seealso cref="GlobalPaletteManager"/>
        /// <seealso cref="ColorPalette"/>
        [Serializable]
        public class Sample {
            public string name;
            public Color color;
            public Sample(string name, Color color) {
                this.name = name;
                this.color = color;
            }
        }

        /// <summary>A simple "get" function for <see cref="samples"/>.
        /// <para/><c>return <see cref="samples"/>[index];</c></summary>
        /// <param name="index">The index to look at.</param>
        /// <returns>Returns the <see cref="Sample"/> at the given index.</returns>
        public Sample this[int index] { get => samples[index]; internal set => samples[index] = value; }

        /// <summary>Creates a <see cref="ColorPalette"/> with the given capacity.</summary>
        /// <param name="capacity">Capacity for <see cref="samples"/>.</param>
        public ColorPalette(int capacity) {
            samples = new Sample[capacity];
        }

        /// <summary>Retreives a <see cref="Color"/> by name.</summary>
        /// <param name="name">Name of the <see cref="Sample"/> to get.</param>
        /// <param name="color">The color that was found. Defaults to <see cref="Color.clear"/>.</param>
        /// <returns>Returns if any color was found.</returns>
        public bool Get(string name, out Color color) {
            foreach(Sample sample in samples) {
                if(sample.name == name) {
                    color = sample.color;
                    return true;
                }
            }
            color = Color.clear;
            return false;
        }

        public IEnumerator<Sample> GetEnumerator() { return ((IEnumerable<Sample>)samples).GetEnumerator(); }
        IEnumerator IEnumerable.GetEnumerator() { return ((IEnumerable<Sample>)samples).GetEnumerator(); }

    }
    #endregion

}
