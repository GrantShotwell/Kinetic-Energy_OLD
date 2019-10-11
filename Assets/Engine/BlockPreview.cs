using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using KineticEnergy.Content;
using KineticEnergy.Intangibles.Behaviours;

namespace KineticEnergy.Ships.Blocks {

    /// <summary>Class for selecting then placing <see cref="Block"/>s on a <see cref="BlockGrid"/>.
    /// <para/>Contains all of the information you could ever really need for a type of <see cref="Block"/>.</summary>
    /// <seealso cref="GlobalPaletteManager"/>
    public abstract class BlockPreview : MonoBehaviour {

        /// <summary>The "real" <see cref="Block"/> counterpart to this <see cref="BlockPreview"/>.</summary>
        [Tooltip("The \"real\" Block counterpart to this BlockPreview.")]
        public GameObject realBlockPrefab;

        /// <summary>A refrence to a blank <see cref="BlockGrid"/> <see cref="GameObject"/> to instantiate when <see cref="PlaceNewGrid()"/> is called.</summary>
        [Tooltip("A refrence to a blank BlockGrid GameObject to instantiate when PlaceNewGrid() is called.")]
        public GameObject blockGridPrefab;

        /// <summary>Tests <see cref="placeable"/> and looks at <see cref="BlockGrid.array"/> before running <see cref="Place"/>.</summary>
        /// <param name="grid">The <see cref="BlockGrid"/> to place the <see cref="realBlockPrefab"/> at.</param>
        /// <param name="position">The position in grid to place the <see cref="realBlockPrefab"/> at.</param>
        /// <returns>Returns <see cref="placeable"/>.</returns>
        public bool TryPlace(BlockGrid grid, Vector3Int position) {
            if(placeable && grid.CanPlaceBlock(realBlockPrefab.GetComponent<Block>(), position)) Place(grid, position);
            return placeable;
        }

        /// <summary>Places the "real" <see cref="Block"/> of this <see cref="BlockPreview"/> into the specified <see cref="BlockGrid"/> at the specified location.</summary>
        /// <param name="grid">The <see cref="BlockGrid"/> to place the <see cref="realBlockPrefab"/> at.</param>
        /// <param name="position">The position in grid to place the <see cref="realBlockPrefab"/> at.</param>
        public void Place(BlockGrid grid, Vector3Int position) {
            grid.PlaceNewBlock(realBlockPrefab, position, transform.rotation);
        }

        /// <summary>Tests <see cref="placeable"/> before running <see cref="PlaceNewGrid"/>.</summary>
        /// <param name="name">Name of the new <see cref="BlockGrid"/>.</param>
        /// <returns>Returns the <see cref="BlockGrid"/> that was just created and contains the newly instantiated <see cref="realBlockPrefab"/>.</returns>
        public BlockGrid TryPlaceNewGrid(string name = "Unnamed Grid") {
            if(placeable) return PlaceNewGrid(name);
            else return null;
        }

        /// <summary>Places the "real" <see cref="Block"/> of this <see cref="BlockPreview"/> into a new <see cref="BlockGrid"/> at it's current position.</summary>
        /// <param name="name">Name of the new <see cref="BlockGrid"/>.</param>
        /// <returns>Returns the <see cref="BlockGrid"/> that was just created and contains the newly instantiated <see cref="realBlockPrefab"/>.</returns>
        public BlockGrid PlaceNewGrid(string name = "Unnamed Grid") {

            // Setup the new GameObject.
            GameObject gridGameObject = Instantiate(blockGridPrefab);
            gridGameObject.name = name;
            gridGameObject.transform.rotation = transform.rotation;
            gridGameObject.transform.position += transform.position;
            gridGameObject.SetActive(true);

            // Setup the new grid.
            BlockGrid grid = gridGameObject.GetComponent<BlockGrid>();
            grid.PlaceNewBlock(realBlockPrefab, grid.WorldPointToGrid(transform.position), transform.rotation);

            //Return the new grid.
            return grid;

        }

        /// <summary>The <see cref="Material"/> that is rendering this <see cref="BlockPreview"/>.</summary>
        public Material[] materials;
        /// <summary>The scale of the <see cref="BlockPreview"/> compared to the <see cref="Block"/>.</summary>
        public float scale = 0.95f;

        GlobalPaletteManager globalPalettes;
        public void Awake() {

        }
        public void OnEnable() {

        }
        public void Start() {
            globalPalettes = FindObjectOfType<GlobalPaletteManager>();
            transform.localScale = new Vector3(scale, scale, scale);
            var renderer = GetComponent<Renderer>();
            if(renderer != null) materials = new Material[] { renderer.material };
            else {
                var mats = new List<Material>();
                foreach(Renderer r in GetComponentsInChildren<Renderer>())
                    mats.Add(r.material);
                materials = mats.ToArray();
            }
        }

        public virtual void OnSetup() { }
        
        //Determine if the block is placeable.
        /// <summary>The colliders that are intersecting this preview.</summary>
        public List<Collider> intersectedColliders = new List<Collider>();
        public void OnTriggerEnter(Collider collider) => intersectedColliders.Add(collider);
        public void OnTriggerExit(Collider collider) => intersectedColliders.Remove(collider);
        public void OnDisable() => intersectedColliders.Clear();

        //Show if it is placeable.
        /// <summary>Is this preview placeable?</summary>
        public bool placeable { get; private set; }
        public void FixedUpdate() {
            placeable = intersectedColliders.Count == 0;
            Color valid = globalPalettes.colors[0].color;
            Color invalid = globalPalettes.colors[1].color;
            foreach(Material material in materials) {
                if(placeable) material.SetColor("PreviewColor", valid);
                else material.SetColor("PreviewColor", invalid);
            }
        }

        /// <summary>Method called when prefab is loaded. Override this if you wanna do anything fancy with the prefab.</summary>
        public virtual void PrefabSetup() { }

    }

}
