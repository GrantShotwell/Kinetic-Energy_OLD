using System.Collections.Generic;
using UnityEngine;
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
    [BlockAttributes.ParticleSystem(mAttribute_EXAUST)]
    public class Thruster : TransparentBlock, IBlockTerminal, IPrefabEditor<ParticleSystem> {
        const int mAttribute_EXAUST = 1;

        void IPrefabEditor<ParticleSystem>.OnPrefab(Master master, ParticleSystem particles, bool asPreview, BlockAttributes.BlockAttribute sender) {
            //Only intended for the ParticleSystem attribute.
            if(sender is BlockAttributes.ParticleSystem attribute) {

                //Only intended for the exaust attribute.
                if(attribute.identifier == mAttribute_EXAUST) {

                    var main = particles.main;
                    main.startSpeed = 15f;
                    main.simulationSpace = ParticleSystemSimulationSpace.World;

                    var shape = particles.shape;
                    shape.shapeType = ParticleSystemShapeType.Cone;
                    shape.radius = 0.1f;
                    shape.angle = 5.0f;
                    shape.rotation = new Vector3(-90f, 0f, 0f);

                    var renderer = particles.gameObject.GetComponent<ParticleSystemRenderer>();
                    renderer.material = master.materials.chemicalThrust;

                }

            }
        }

        public const float FORCE = 1000000;

        TerminalMenu mMenu_thrusting;
        public float Power { get; set; } = 1.00f;
        public bool Thrusting => Power > 0.00f;

        private GridDriver driver;
        public void SetDriver(GridDriver value) {
            Debug.Log("Set driver.");
            driver = value;
        }

        FaceMask Directions { get; set; }

        public IEnumerable<TerminalMenu> Menus => new PropertyEnumerable<TerminalMenu>(mMenu_thrusting);
        public UIManager Manager { get; set; }

        public ParticleSystem particles;

        public void Awake() {

            //mMenu_thrusting = new TerminalMenu("Enabled",
            //    new ButtonTerminal((value) => Thrusting = value));

            Directions = (FaceMask)Geometry.RoundToMultiple(
                transform.localRotation * Vector3.up, 1);

        }

        public void Start() {
            particles = GetComponent<ParticleSystem>();
            var driver = GetComponentInParent<GridDriver>();
            if(driver) SetDriver(driver);
        }

        //Apply thruster force.
        public void FixedUpdate() {

            if(driver) {

                float power = 1.00f;
                FaceMask directions = Directions;
                Vector3 move = driver.Move;

                if(directions.AnyX) power *= move.x;
                if(directions.AnyY) power *= move.y;
                if(directions.AnyZ) power *= move.z;
                Power = power;

            } else {

                Power = 0.00f;

            }

            bool thrusting = Thrusting;
            if(Grid != null && thrusting) {
                var force = FORCE * Power * Time.deltaTime;
                Grid.Rigidbody.AddForceAtPosition(transform.up * -force, transform.position);
            }

            var emission = particles.emission;
            emission.enabled = thrusting;

        }

        public void OnSetup() { }
        public void OnAllSetup() { }

        public class Preview : BlockPreview { }

    }

}
