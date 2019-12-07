using KineticEnergy.Grids.Blocks;
using KineticEnergy.Structs;
using UnityEngine;

namespace KineticEnergy.Grids {

    public class BlockGridEditor : MonoBehaviour {

        /// <summary>The maximum distance from the camera the should be placed at.</summary>
        public float distance = 5.0f;

        /// <summary>Expected error of raycasting into the grid.</summary>
        public float hitError = 0.01f;

        /// <summary>Current <see cref="BlockPreview"/> that is being moved around.</summary>
        public BlockPreview Preview { get; set; }

        /// <summary>The <see cref="BlockGrid"/> in focus.</summary>
        public BlockGrid Grid { get; private set; }

        /// <summary>The rotation of the <see cref="Preview"/> relative to the grid.</summary>
        public Quaternion Rotation { get; set; }

        private RaycastHit look;

        public void Start() {
            Rotation = Quaternion.Euler(0, 0, 0);
        }

        public void Update() {

            BlockPreview preview = Preview;
            if(preview != null) {

                //Find which GameObject is being targeted.
                Transform camera = Camera.main.transform;
                Vector3 direction = camera.forward;
                if(Physics.Raycast(camera.position, direction, out look)) {
                    // <<something was hit>>

                    //Apply 'hitError' to the raycast hit.
                    look.point -= direction * hitError;

                    BlockGrid grid = Grid = look.transform.gameObject.GetComponent<BlockGrid>();
                    if(grid) {
                        //There is a grid, so align the block to the grid.
                        preview.transform.position = grid.WorldPoint_to_WorldGrid(look.point);
                        preview.transform.rotation = grid.transform.rotation * Rotation;
                    } else {
                        //There isn't any grid, so just put the preview at the raycast hit.
                        preview.transform.position = look.point;
                    }

                } else {
                    //Nothing is being targeted so just put the preview at the max distance.
                    Grid = null;
                    look.point = camera.position + (direction * distance);
                    preview.transform.position = look.point;
                }
            }

        }
        
        public void TryPlaceBlock() {
            if(Preview != null) {
                if(Grid != null) Preview.TryPlace(Grid, Grid.WorldPoint_to_LocalWorld(Preview.transform.position), Rotation);
                else Preview.TryPlaceNewGrid();
            }
        }

        public void RemoveBlock() {
            GameObject possibleBlock = look.collider ? look.collider.gameObject : null;
            if(possibleBlock != null) {
                var block = possibleBlock.GetComponent<Block>();
                if(block != null) {
                    GridLocation location = block.Location;
                    location.Grid.RemoveBlock(location.GridVector);
                    Destroy(block.gameObject);
                }
            }
        }

    }

}
