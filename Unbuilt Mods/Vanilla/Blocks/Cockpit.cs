using KineticEnergy.Entities;
using KineticEnergy.Grids;
using KineticEnergy.Grids.Blocks;
using KineticEnergy.Intangibles.Client;
using KineticEnergy.Structs;
using UnityEngine;

namespace KineticEnergy.Mods.Vanilla.Blocks {

    [BlockAttributes.BasicInfo(
        "Cockpit", 1, 2, 1,
        3000, 0.0f, 0.0f, 0.0f,
        true
    )]
    [BlockAttributes.BoxCollider( //base
        0.0f, -0.45f, 0.0f,
        1.0f, 0.1f, 1.0f)]
    [BlockAttributes.BoxCollider( //pole
        0.0f, -0.1302153f, 0.0f, 0.15f,
        0.5528123f, 0.15f)]
    [BlockAttributes.BoxCollider( //bottom
        0.0f, 0.184f, -0.03f, 0.550284f,
        0.1f, 0.6138796f)]
    [BlockAttributes.BoxCollider( //back
        0.0f, 0.6213292f, 0.4316063f,
        0.54f, 0.9018728f, 0.2546511f)]
    [BlockAttributes.BoxCollider( //left arm
        -0.3473111f, 0.5174136f, 0.1362655f,
        0.06344479f, 0.04286408f, 0.7031212f)]
    [BlockAttributes.BoxCollider( //right arm
        +0.3473111f, 0.5174136f, 0.1362655f,
        0.06344479f, 0.04286408f, 0.7031212f)]
    [BlockAttributes.Mesh(
        "Content\\Vanilla\\Models\\Cockpit.obj")]
    [BlockAttributes.Material(
        "Content\\Vanilla\\Textures\\armor-diffuse.png", true, 0)]
    [BlockAttributes.SelectableBox(
        0.00f, 0.25f, 0.00f,
        1.10f, 2.20f, 1.10f)]
    class Cockpit : TransparentBlock {

        public override Vector3 TransformOffset => base.TransformOffset + new Vector3(0.0f, -0.5f, 0.0f);

        public GridDriver Driver { get; set; }
        public Selectable Selectable { get; set; }
        public Player Player { get; set; }

        private Quaternion RotOffset { get; set; }
        public Quaternion LocalChairRotation => transform.localRotation * RotOffset;
        public Quaternion WorldChairRotation => transform.rotation * RotOffset;

        public void Start() {
            RotOffset = Quaternion.Euler(0f, 180f, 0f);
            Selectable = GetComponentInChildren<Selectable>();
            Selectable.onDown.Add(OnSelected);
        }
        
        protected override void VerifyLocation(GridLocation oldLocation, GridLocation newLocation) {

            if(oldLocation.Grid == newLocation.Grid) return; 

            BlockGrid grid = newLocation.Grid;
            GridDriver driver = grid.GetComponent<GridDriver>();
            if(!driver) {
                driver = grid.gameObject.AddComponent<GridDriver>();
                driver.Grid = grid;
            }
            Driver = driver;

        }

        public void OnSelected(Entity sender) {
            if(sender is Player player) {

                player.GetComponent<Collider>().enabled = false;
                player.holdInputs = true;
                player.gameObject.transform.SetParent(transform);

                SetPlayerPosRot(player);

                Player = player;

            }
        }

        public void OnExit() {

            Player player = Player;
            Player = null;

            player.GetComponent<Collider>().enabled = true;
            player.holdInputs = false;
            player.gameObject.transform.SetParent(null, true);
            player.transform.position += -player.transform.forward;

        }

        public void FixedUpdate() {

            Player player = Player;
            GridDriver driver = Driver;

            if(player) {

                Inputs inputs = player.Inputs;
                if(inputs.scndary) {

                    OnExit();

                } else {

                    Quaternion chair = LocalChairRotation;
                    driver.Move = Quaternion.Inverse(chair) * inputs.move;
                    driver.Look = Quaternion.Inverse(chair) * inputs.look;

                    SetPlayerPosRot(player);

                }

            }
        }

        private void SetPlayerPosRot(Player player) {

            Rigidbody rigidbody = player.Rigidbody;
            Rigidbody grid = Location.Grid.rigidbody;
            rigidbody.velocity = grid.velocity;
            rigidbody.angularVelocity = grid.angularVelocity;

            Transform transform = player.transform;
            transform.localPosition = Vector3.up / 2;
            transform.localRotation = Quaternion.Euler(0f, 180f, 0f);

        }

        public class Preview : BlockPreview { }

    }

}
