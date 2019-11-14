using UnityEngine;
using KineticEnergy.Ships;
using KineticEnergy.Ships.Blocks;
using KineticEnergy.Entities;
using KineticEnergy.Interfaces.Input;
using KineticEnergy.Intangibles.Client;

namespace KineticEnergy.Mods.Vanilla.Blocks {

    [BlockAttributes.BasicInfo(
        "Cockpit", 1, 2, 1,
        3000, 0.0f, 0.0f, 0.0f
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
        "Content\\Vanilla\\Blocks\\Cockpit\\Cockpit.obj")]
    [BlockAttributes.Material(
        "Content\\Vanilla\\Blocks\\ArmorBlock\\diffuse.png", true, 0)]
    [BlockAttributes.SelectableBox(
        0.00f, 0.25f, 0.00f,
        1.10f, 2.20f, 1.10f)]
    class Cockpit : TransparentBlock {

        public GridDriver Driver { get; set; }
        public Selectable Selectable { get; set; }
        public Player Player { get; set; }

        private Quaternion RotOffset { get; set; }
        public Quaternion ChairRotation => transform.rotation * RotOffset;

        public void Start() {
            RotOffset = Quaternion.Euler(0f, 180f, 0f);
            Selectable = GetComponentInChildren<Selectable>();
            Selectable.onDown.Add(OnSelected);
        }

        public override void OnGridUpdated() {
            Driver = Grid.GetComponent<GridDriver>();
            if(!Driver) {
                Driver = Grid.gameObject.AddComponent<GridDriver>();
                Driver.Grid = Grid;
            }
        }

        public void OnSelected(Entity sender) {
            if(sender is Player player) {

                player.GetComponent<Collider>().enabled = false;
                player.holdInputs = true;
                player.gameObject.transform.SetParent(transform);
                player.transform.localPosition = Vector3.up / 2;
                Player = player;

            }
        }

        public void OnExit() {

            Player player = Player;
            Player = null;

            player.GetComponent<Collider>().enabled = true;
            player.holdInputs = false;
            player.gameObject.transform.SetParent(null, true);

        }

        public void FixedUpdate() {

            Player player = Player;
            GridDriver driver = Driver;

            if(player) {

                Inputs inputs = player.Inputs;
                if(inputs.scndary) {

                    OnExit();

                } else {

                    Quaternion chair = ChairRotation;

                    player.transform.localPosition = Vector3.up / 2;
                    player.transform.rotation = transform.rotation;
                    driver.Move = Quaternion.Inverse(chair) * inputs.move;
                    driver.Look = Quaternion.Inverse(chair) * inputs.look;

                }

            }
        }

        public class Preview : BlockPreview { }

    }

    public class GridDriver : MonoBehaviour {

        public Vector3 Move { get; set; }
        public Vector3 Look { get; set; }
        public BlockGrid Grid { get; set; }
        
        public void Start() {
            if(!Grid) Grid = GetComponent<BlockGrid>();
            foreach(Block block in Grid) {
                if(block is Thruster thruster)
                    thruster.SetDriver(this);
            }
        }

    }

}
