using System.Collections.Generic;
using UnityEngine;
using KineticEnergy.Ships.Blocks;
using KineticEnergy.CodeTools.Enumerators;
using KineticEnergy.Intangibles.Terminal;
using KineticEnergy.Intangibles.UI;
using KineticEnergy.Interfaces;
using KineticEnergy.Content;

namespace KineticEnergy.Mods.Vanilla.Blocks {

    [BlockAttributes.BasicInfo(
        "Test Thruster", 1, 1, 1,
        8000, 0.0f, -0.4f, 0.0f
    )]
    [BlockAttributes.BoxCollider(0, 0, 0, 1, 1, 1)]
    [BlockAttributes.Mesh("Content\\Vanilla\\Blocks\\TestThruster\\ObjTest.obj")]
    [BlockAttributes.Material("Content\\Vanilla\\Blocks\\ArmorBlock\\diffuse.png", 0)]
    [BlockAttributes.ParticleSystem()]
    public class TestThruster : TransparentBlock, IBlockTerminal, IPrefabComponentEditor<ParticleSystem> {

        void IPrefabComponentEditor<ParticleSystem>.OnPrefab(ParticleSystem particles) {
            var main = particles.main;
            var shape = particles.shape;

            main.startSpeed = 15f;
            main.simulationSpace = ParticleSystemSimulationSpace.World;

            shape.shapeType = ParticleSystemShapeType.Cone;
            shape.radius = 0.1f;
            shape.angle = 5.0f;
            shape.rotation = new Vector3(-90f, 0f, 0f);

            var renderer = particles.gameObject.GetComponent<ParticleSystemRenderer>();
            renderer.material = ContentLoader.GetResource<Material>("Materials\\ChemicalThrustParticle");

        }

        public float force = 75000;

        TerminalMenu mMenu_thrusting;
        public bool thrusting = false;

        public float power = 1.00f;

        public IEnumerable<TerminalMenu> Menus => new PropertyEnumerable<TerminalMenu>(mMenu_thrusting);

        public UIManager Manager { get; set; }

        public ParticleSystem particles;

        public void Awake() {
            mMenu_thrusting = new TerminalMenu("Enabled",
                new ButtonTerminal((value) => thrusting = value));
        }

        public void Start() {
            particles = GetComponent<ParticleSystem>();
        }

        //Apply thruster force.
        public void FixedUpdate() {
            if(grid != null && thrusting) {
                var f = force * power;
                grid.Rigidbody.AddForceAtPosition(transform.up * -f, transform.position);
            }

            var emission = particles.emission;
            emission.enabled = thrusting;

        }

        public void OnSetup() { }
        public void OnAllSetup() { }

        public class Preview : BlockPreview { }

    }

}
