using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VFX;
using KineticEnergy.Ships.Blocks;
using KineticEnergy.CodeTools.Enumerators;
using KineticEnergy.Interfaces;
using KineticEnergy.Intangibles;
using KineticEnergy.Intangibles.UI;
using KineticEnergy.Intangibles.Terminal;
using KineticEnergy.CodeTools.Math;

namespace KineticEnergy.Mods.Vanilla.Blocks {

    [BlockAttributes.BasicInfo(
        "Thruster", 1, 1, 1,
        8000, 0.0f, -0.4f, 0.0f
    )]
    [BlockAttributes.BoxCollider(
        0.0f, 0.0f, 0.0f,
        1.0f, 1.0f, 1.0f
    )]
    [BlockAttributes.Mesh(
        "Content\\Vanilla\\Blocks\\TestThruster\\ObjTest.obj")]
    [BlockAttributes.Material(
        "Content\\Vanilla\\Blocks\\ArmorBlock\\diffuse.png", true, 0)]
    [BlockAttributes.ThrusterVFX(mAttribute_EXAUST)]
    public class Thruster : TransparentBlock, IBlockTerminal, IPrefabEditor<VisualEffect> {
        const int mAttribute_EXAUST = 1;

        void IPrefabEditor<VisualEffect>.OnPrefab(Master master, VisualEffect effect, bool asPreview, BlockAttributes.BlockAttribute sender) {
            //Only intended for the ParticleSystem attribute.
            if(sender is BlockAttributes.ParticleSystem attribute) {

                //Only intended for the exaust attribute.
                if(attribute.identifier == mAttribute_EXAUST) {

                    Effect = effect;

                }

            }
        }

        public const float FORCE = 1000000;

        TerminalMenu mMenu_thrusting;
        public float ThrustPower { get; set; } = 1.00f;
        public bool IsThrusting => thrustForced || ThrustPower > 0.00f;

        /// <summary>Override driver controls to be full thrust?</summary>
        /// <returns>Returns the input value.</returns>
        public bool ForceThrust(bool input) => thrustForced = input;
        private bool thrustForced = false;

        private GridDriver driver;
        public void SetDriver(GridDriver value) {
            Debug.Log("Set driver.");
            driver = value;
        }

        public IEnumerable<TerminalMenu> Menus => new PropertyEnumerable<TerminalMenu>(mMenu_thrusting);
        public UIManager Manager { get; set; }

        public VisualEffect Effect { get; set; }

        public void Awake() {

            mMenu_thrusting = new TerminalMenu("Enabled",
                new ButtonTerminal(ForceThrust));

        }

        public void Start() {
            var driver = GetComponentInParent<GridDriver>();
            if(driver) SetDriver(driver);
        }

        //Apply thruster force.
        public void FixedUpdate() {

            //If the thrust is forced, power = 1, otherwise check driver inputs if there is one.
            ThrustPower = driver ? Vector3.Dot(driver.Move, transform.up) : thrustForced ? 1.00f : 0.00f;

            bool thrusting = IsThrusting;
            if(Grid != null && thrusting) {
                var force = FORCE * ThrustPower * Time.deltaTime;
                Grid.Rigidbody.AddForceAtPosition(transform.up * -force, transform.position);
            }

            VisualEffect effect = Effect;
            if(effect) effect.SetBool("Thrusting", IsThrusting);

        }

        public void OnSetup() { }
        public void OnAllSetup() { }

        public class Preview : BlockPreview { }

    }

}
