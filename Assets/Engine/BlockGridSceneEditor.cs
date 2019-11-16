#if UNITY_EDITOR
using KineticEnergy.Ships.Blocks;
using UnityEditor;
using UnityEngine;

namespace KineticEnergy.Ships {

    /// <summary>
    /// A modified <see cref="BlockGridEditor"/> for use in Unity's scene view.
    /// </summary>
    /// <remarks>
    /// Currently not tested for multiple scene views.
    /// </remarks>
    [ExecuteInEditMode]
    public class BlockGridSceneEditor : MonoBehaviour {

        /// <summary>The maximum distance from the camera the should be placed at.</summary>
        public float distance = 10.0f;

        /// <summary>Expected error of raycasting into the grid.</summary>
        public float hitError = 0.01f;

        /// <summary>Current <see cref="BlockPreview"/> that is being moved around.</summary>
        public BlockPreview selectedBlock;

        private BlockGrid grid;
        private RaycastHit look;

        public void OnEnable() {
            SceneView.duringSceneGui += OnSceneGUI;
        }

        public void OnDisable() {
            SceneView.duringSceneGui -= OnSceneGUI;
        }

        public void OnSceneGUI(SceneView sceneView) {
            grid = null;

            //Get a ray from scene view.
            var mousePosition = Event.current.mousePosition;
            var sceneCamera = sceneView.camera;
            mousePosition = new Vector3(mousePosition.x, sceneCamera.pixelHeight - mousePosition.y);
            var sceneRay = sceneCamera.ScreenPointToRay(mousePosition);

            //Find which GameObject is being targeted.
            var direction = sceneRay.direction;
            if(Physics.Raycast(sceneRay, out look)) {

                look.point -= direction * hitError;

                //If it has a grid, align the block to the grid.
                grid = look.transform.gameObject.GetComponent<BlockGrid>();
                if(grid != null) {

                    Vector3 alignedPosition = grid.WorldPoint_to_WorldGrid(look.point);
                    selectedBlock.transform.position = alignedPosition;
                    selectedBlock.transform.rotation = grid.transform.rotation;


                    //If it doesn't have a grid, just put the preview at the look.point.
                } else {

                    selectedBlock.transform.position = look.point;

                }


                //If nothing is being targeted, disable the preview.
            } else {

                look.point = sceneCamera.transform.position + (direction * distance);
                selectedBlock.transform.position = look.point;

            }

            if(Event.current.type == EventType.MouseDown) {
                if(Event.current.button == 0) {
                    if(grid != null) {
                        selectedBlock.Place(grid, grid.WorldPoint_to_LocalWorld(selectedBlock.transform.position), Quaternion.Euler(0, 0, 0));
                    } else {
                        selectedBlock.PlaceNewGrid();
                    }
                } else if(Event.current.button == 1) {
                    if(look.transform != null) {
                        var possibleBlock = look.transform.gameObject;
                        if(possibleBlock != null) {
                            var block = possibleBlock.GetComponent<Block>();
                            if(block != null) Destroy(block.gameObject);
                        }
                    }
                }
            }

        }

    }

}

#endif