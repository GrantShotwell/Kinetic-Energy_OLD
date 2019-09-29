using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using KineticEnergy.Ships.Blocks;

namespace KineticEnergy.Intangibles.Global {

    /// <summary>
    /// Stores and manages the one and only <see cref="BlockPalette"/>.
    /// </summary>
    /// <remarks>
    /// Comtains implicit cast operator from <see cref="GlobalPaletteManager"/> and <see cref="BlockPalette"/>.
    /// </remarks>
    /// <seealso cref="BlockPalette"/>
    /// <seealso cref="ColorPalette"/>
    public class GlobalPaletteManager : GlobalBehaviour {

        public BlockPalette blocks = new BlockPalette();
        public ColorPalette colors = new ColorPalette(2);

        public void OnEnable() {
            UpdateBlockPalette(ref blocks);
        }

        static void UpdateBlockPalette(ref BlockPalette blockPalette) {

            //Get all prefabs in Resources/Blocks
            GameObject[] prefabs = Resources.LoadAll<GameObject>("Blocks");
            //Iterate through the prefabs. Since the previews have the refrence to their "real" counterpart, we only need to find the previews.
            foreach(GameObject prefab in prefabs) {
                BlockPreview preview = prefab.GetComponent<BlockPreview>();
                if(preview != null) blockPalette.Add(new BlockPalette.Sample(preview.realBlockPrefab, preview.gameObject));
            }

        }

    }

    #region Block Palette

    /// <summary>An object that contains a list of <see cref="Sample"/>s.</summary>
    /// <remarks>Extends <see cref="IEnumerable"/>.</remarks>
    /// <seealso cref="GlobalPaletteManager"/>
    /// <seealso cref="Sample"/>
    [Serializable]
    public class BlockPalette : IEnumerable<BlockPalette.Sample> {

        /// <summary>A <see cref="List{T}"/> of <see cref="Sample"/>s.</summary>
        [SerializeField]
        private List<Sample> samples = new List<Sample>();

        /// <summary>A simple "get" function for <see cref="samples"/>.
        /// <para/><c>return <see cref="samples"/>[index];</c></summary>
        /// <param name="index">The index to look at.</param>
        /// <returns>Returns the <see cref="Sample"/> at the given index.</returns>
        public Sample this[int index] { get { return samples[index]; } }

        /// <summary>Adds a <see cref="Sample"/> to <see cref="samples"/>.</summary>
        /// <param name="blockSample">The <see cref="Sample"/> to add to <see cref="samples"/>.</param>
        public void Add(Sample blockSample) {
            samples.Add(blockSample);
        }

        /// <summary>The <see cref="List{T}.Count"/> for <see cref="samples"/>.</summary>
        public int Count => samples.Count;

        public IEnumerator<Sample> GetEnumerator() { return ((IEnumerable<Sample>)samples).GetEnumerator(); }
        IEnumerator IEnumerable.GetEnumerator() { return ((IEnumerable<Sample>)samples).GetEnumerator(); }

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
            /// <param name="b\lockPrefab_preview">The prefab that will be cloned with <see cref="Object.Instantiate()"/></param>
            /// <see cref="Block"/>
            /// <see cref="BlockPreview"/>
            public Sample(GameObject prefabBlock, GameObject prefabBlock_preview) {
                this.prefabBlock = prefabBlock;
                this.prefabBlock_preview = prefabBlock_preview;
            }

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
        [Serializable] public class Sample {
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
        public Sample this[int index] { get { return samples[index]; } }

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
