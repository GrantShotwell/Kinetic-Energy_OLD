using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using KineticEnergy.Ships;
using KineticEnergy.Ships.Blocks;

namespace KineticEnergy.Ships {

    public class BlockGridEditor : MonoBehaviour {

        /// <summary>
        /// The maximum distance from the camera the should be placed at.
        /// </summary>
        public float distance = 5.0f;

        /// <summary>
        /// Expected error of raycasting into the grid.
        /// </summary>
        public float hitError = 0.01f;

        /// <summary>
        /// Current <see cref="BlockPreview"/> that is being moved around.
        /// </summary>
        public BlockPreview preview;

        public BlockGrid grid { get; private set; }
        private RaycastHit look;

        /// <summary>The rotation of this block relative to the grid.</summary>
        public Quaternion rotation;

        public void Start() {
            rotation = Quaternion.Euler(0, 0, 0);
        }

        public void Update() {

            if(preview != null) {
                //Find which GameObject is being targeted.
                Camera camera = Camera.main;
                var direction = camera.transform.forward;
                if(Physics.Raycast(camera.transform.position, camera.transform.forward, out look)) {

                    look.point -= direction * hitError;

                    //If it has a grid, align the block to the grid.
                    grid = look.transform.gameObject.GetComponent<BlockGrid>();
                    if(grid != null) {

                        Vector3 alignedPosition = grid.AlignWorldPoint(look.point);
                        preview.transform.position = alignedPosition;
                        preview.transform.rotation = grid.transform.rotation * rotation;

                    //If it doesn't have a grid, just put the preview at the look.point.
                    } else {

                        preview.transform.position = look.point;

                    }

                //If nothing is being targeted, just put the preview at the max distance.
                } else {

                    grid = null;
                    look.point = camera.transform.position + (direction * distance);
                    preview.transform.position = look.point;

                }
            }

        }

        public void TryPlaceBlock() {
            if(preview != null) {
                if(grid != null) preview.TryPlace(grid, grid.WorldPointToGrid(preview.transform.position));
                else preview.TryPlaceNewGrid();
            }
        }

        public void RemoveBlock() {
            var possibleBlock = look.transform?.gameObject;
            if(possibleBlock != null) {
                var block = possibleBlock.GetComponent<Block>();
                if(block != null) {
                    Destroy(block);
                }
            }
        }

    }

}
