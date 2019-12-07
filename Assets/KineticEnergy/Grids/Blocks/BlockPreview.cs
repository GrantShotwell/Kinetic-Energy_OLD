using System;
using System.Linq;
using System.Collections.Generic;
using UnityEngine;
using KineticEnergy.Intangibles;
using KineticEnergy.Intangibles.Behaviours;
using KineticEnergy.Structs;

namespace KineticEnergy.Grids.Blocks {

    /// <summary>Class for selecting then placing <see cref="Block"/>s on a <see cref="BlockGrid"/>.
    /// <para/>Contains all of the information you could ever really need for a type of <see cref="Block"/>.</summary>
    /// <seealso cref="GlobalPaletteManager"/>
    public abstract class BlockPreview : MonoBehaviour {

        public Master Master {
            get => master ?? throw new NullReferenceException(string.Format("'{0}' is null. Was '{1}' called on this {2}?",
                nameof(Master), nameof(GlobalBehavioursManager.PolishBehaviour), nameof(BlockPreview)));
            internal set {
                master = value;
                gridPrefab = master.prefabs.grid;
            }
        }
        private Master master;

        public GlobalBehavioursManager Global => Master.Global;

        /// <summary>The "real" <see cref="Block"/> counterpart to this <see cref="BlockPreview"/>.</summary>
        public Prefab<Block> blockPrefab;

        /// <summary><see cref="Prefab{TComponent1}"/> of <see cref="BlockGrid"/> to instantiate when <see cref="PlaceNewGrid()"/> is called.</summary>
        public Prefab<BlockGrid> gridPrefab;

        /// <summary>Tests <see cref="IsPlaceable"/> and looks at <see cref="BlockGrid.Array"/> before running <see cref="Place"/>.</summary>
        /// <param name="grid">The <see cref="BlockGrid"/> to place the <see cref="blockPrefab"/> at.</param>
        /// <param name="position">The position in grid to place the <see cref="blockPrefab"/> at.</param>
        /// <returns>Returns <see cref="IsPlaceable"/>.</returns>
        public bool TryPlace(BlockGrid grid, Vector3Int position, Quaternion rotation) {
            if(IsPlaceable && grid.CanPlaceBlock(blockPrefab.component, position)) {
                Place(grid, position, rotation);
                return true;
            } else return false;
        }

        /// <summary>Places the "real" <see cref="Block"/> of this <see cref="BlockPreview"/> into the specified <see cref="BlockGrid"/> at the specified location.</summary>
        /// <param name="grid">The <see cref="BlockGrid"/> to place the <see cref="blockPrefab"/> at.</param>
        /// <param name="position">The position in grid to place the <see cref="blockPrefab"/> at.</param>
        public void Place(BlockGrid grid, Vector3Int position, Quaternion rotation) {
            grid.PlaceNewBlock(blockPrefab, position, rotation);
        }

        /// <summary>Tests <see cref="IsPlaceable"/> before running <see cref="PlaceNewGrid"/>.</summary>
        /// <param name="name">Name of the new <see cref="BlockGrid"/>.</param>
        /// <returns>Returns the <see cref="BlockGrid"/> that was just created and contains the newly instantiated <see cref="blockPrefab"/>.</returns>
        public BlockGrid TryPlaceNewGrid(string name = "Unnamed Grid")
            => IsPlaceable ? PlaceNewGrid(name) : null;

        /// <summary>Places the "real" <see cref="Block"/> of this <see cref="BlockPreview"/> into a new <see cref="BlockGrid"/> at it's current position.</summary>
        /// <param name="name">Name of the new <see cref="BlockGrid"/>.</param>
        /// <returns>Returns the <see cref="BlockGrid"/> that was just created and contains the newly instantiated <see cref="blockPrefab"/>.</returns>
        public BlockGrid PlaceNewGrid(string name = "Unnamed Grid") {

            //Instantiate the prefab.
            var grid = gridPrefab.Instantiate();
            BlockGrid component = grid.component;
            GameObject gameObject = grid.gameObject;

            //Setup the GameObject.
            gameObject.name = name;
            gameObject.transform.rotation = transform.rotation;
            gameObject.transform.position += transform.position - blockPrefab.component.TransformOffset;
            Global.PolishBehaviour(component);

            //Setup and return the Component.
            component.PlaceNewBlock(blockPrefab, Vector3Int.zero, Quaternion.Euler(0, 0, 0));
            return grid;

        }

        /// <summary>The <see cref="Material"/>s that are rendering this <see cref="BlockPreview"/>.</summary>
        public IEnumerable<Material> materials;
        /// <summary>The scale of the <see cref="BlockPreview"/> compared to the <see cref="Block"/>.</summary>
        public float scale = 0.95f;

        GlobalPaletteManager globalPalettes;
        public void Awake() { /* do not use */ }
        public void OnEnable() { /* do not use */ }
        public void Start() {

            globalPalettes = Global.PaletteManager;

            transform.localScale = new Vector3(scale, scale, scale);

            var renderer = GetComponent<Renderer>();
            materials = renderer != null
                //Material of parent (this) renderer.
                ? (new Material[] { renderer.material })
                //All materials of all renderers (rendr) of children.
                : GetComponentsInChildren<Renderer>().Select(rendr => rendr.material);

        }

        public virtual void OnSetup() { }

        //Determine if the block is placeable.
        /// <summary>The colliders that are intersecting this preview.</summary>
        public List<Collider> intersectedColliders = new List<Collider>();

        public void OnTriggerEnter(Collider collider) => intersectedColliders.Add(collider);
        public void OnTriggerExit(Collider collider) => intersectedColliders.Remove(collider);
        public void OnDisable() => intersectedColliders.Clear();

        //Show if it is placeable through its color.
        /// <summary>Is this preview placeable?</summary>
        public bool IsPlaceable { get; private set; }
        public void FixedUpdate() {
            IsPlaceable = intersectedColliders.Count == 0;
            Color valid = globalPalettes.colors[0].color;
            Color invalid = globalPalettes.colors[1].color;
            foreach(Material material in materials)
                material.SetColor("PreviewColor", IsPlaceable ? valid : invalid);
        }

        /// <summary>Method called when prefab is loaded. Override this if you wanna do anything fancy with the prefab.</summary>
        public virtual void PrefabSetup() { }

    }

}
