using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using KineticEnergy.Intangibles.Global;

namespace KineticEnergy.Ships.Blocks {

    /// <summary>
    /// Class for selecting then placing blocks on a <see cref="BlockGrid"/>.
    /// <para>
    /// <list type="number">
    ///     <listheader>
    ///     <para/>How to create a <see cref="BlockPreview"/> of your <see cref="Block"/>.
    ///     </listheader>
    ///     <item>
    ///         <term><para/>1.</term>
    ///         <description>
    ///         Create a "Prefab Variant" of the original <see cref="Block"/>.
    ///         This variant will have every component on the parent removed except for the the collider.
    ///         Add the <see cref="BlockPreview"/> component to your new <see cref="GameObject"/> prefab.
    ///         </description>
    ///         </item>
    ///     <item>
    ///         <term><para/>2.</term>
    ///         <description>
    ///         In the inspector, set up the <see cref="Block"/> prefab as the <see cref="realBlockPrefab"/>.
    ///         The <see cref="blockGridPrefab"/> should have been set when the <see cref="BlockPreview"/> component was added.
    ///         </description>
    ///     </item>
    ///     <item>
    ///         <term><para/>3.</term>
    ///         <description>
    ///         Place everything directly related to the block into a new folder located in "Assets/Resources/Blocks/[FOLDER: BlockName]/{stuff}".
    ///         This includes both prefabs, any associated materials, and the behavior scripts for the block.
    ///         </description>
    ///     </item>
    ///     </list>
    /// </para>
    /// </summary>
    /// <seealso cref="KineticEnergy.Intangibles.GlobalPaletteManager"/>
    public class BlockPreview : MonoBehaviour {
        /// <summary>
        /// The "real" <see cref="Block"/> counterpart to this <see cref="BlockPreview"/>.
        /// </summary>
        [Tooltip("The \"real\" Block counterpart to this BlockPreview.")]
        public GameObject realBlockPrefab;

        /// <summary>
        /// A refrence to a blank <see cref="BlockGrid"/> <see cref="GameObject"/> to instantiate when <see cref="PlaceNewGrid()"/> is called.
        /// </summary>
        [Tooltip("A refrence to a blank BlockGrid GameObject to instantiate when PlaceNewGrid() is called.")]
        public GameObject blockGridPrefab;

        /// <summary>
        /// Places the "real" <see cref="Block"/> of this <see cref="BlockPreview"/> into the specified <see cref="BlockGrid"/> at the specified location.
        /// </summary>
        /// <param name="grid">The <see cref="BlockGrid"/> to place the <see cref="realBlockPrefab"/> at.</param>
        /// <param name="position">The position in grid to place the <see cref="realBlockPrefab"/> at.</param>
        public void Place(BlockGrid grid, Vector3Int position) {
            grid.PlaceNewBlock(realBlockPrefab, position, transform.rotation);
        }

        /// <summary>
        /// Places the "real" <see cref="Block"/> of this <see cref="BlockPreview"/> into a new <see cref="BlockGrid"/> at it's current position.
        /// </summary>
        /// <returns>Returns the <see cref="BlockGrid"/> that was just created and contains the newly instantiated <see cref="realBlockPrefab"/>.</returns>
        public BlockGrid PlaceNewGrid() {

            // Setup the new GameObject.
            GameObject gridGameObject = Instantiate(blockGridPrefab);
            gridGameObject.name = "Unnamed Grid";
            gridGameObject.transform.rotation = transform.rotation;
            gridGameObject.transform.position += transform.position;

            // Setup the new grid.
            BlockGrid grid = gridGameObject.GetComponent<BlockGrid>();
            grid.PlaceNewBlock(realBlockPrefab, grid.WorldPointToGrid(transform.position), transform.rotation);

            //Return the new grid.
            return grid;

        }

        public Material material;
        public float scale = 0.95f;

        GlobalPaletteManager globalPalettes;
        public void Start() {
            globalPalettes = FindObjectOfType<GlobalPaletteManager>();
            transform.localScale = new Vector3(scale, scale, scale);
        }

        public bool placeable { get; private set; }
        public void Update() {
            placeable = intersectedColliders.Count == 0;
            globalPalettes.colors.Get("Preview Valid", out Color valid);
            globalPalettes.colors.Get("Preview Invalid", out Color invalid);
            if(placeable) material.SetColor("Hologram_Color", valid);
            else material.SetColor("Hologram_Color", invalid);
        }

        public List<Collider> intersectedColliders = new List<Collider>();
        public void OnTriggerEnter(Collider collider) { intersectedColliders.Add(collider); }
        public void OnTriggerExit(Collider collider) { intersectedColliders.Remove(collider); }
        public void OnDisable() { intersectedColliders.Clear(); }

    }

}
