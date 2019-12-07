using System.Collections.Generic;
using KineticEnergy.Enumeration;
using KineticEnergy.Grids;
using KineticEnergy.Grids.Blocks;
using KineticEnergy.Intangibles.Terminal;
using KineticEnergy.Intangibles.UI;
using KineticEnergy.Maths;
using UnityEditor;
using UnityEngine;
using UnityEngine.VFX;
using static KineticEnergy.Maths.Methods;

namespace KineticEnergy.Mods.Vanilla.Blocks {

    [BlockAttributes.BasicInfo(
        "Thruster", 1, 1, 1,
        8000, 0.0f, -0.4f, 0.0f,
        false
    )]
    [BlockAttributes.BoxCollider(
        0.0f, 0.0f, 0.0f,
        1.0f, 1.0f, 1.0f
    )]
    [BlockAttributes.Mesh(
        "Content\\Vanilla\\Models\\ObjTest.obj")]
    [BlockAttributes.Material(
        "Content\\Vanilla\\Textures\\armor-diffuse.png", true, 0)]
    [BlockAttributes.ThrusterVFX]
    public class Thruster : TransparentBlock, IBlockTerminal, IGridDrivable {

        #region Inspector
#if UNITY_EDITOR
        public override void OnInspectorGUI(Object[] targets, SerializedObject serializedObject) {
            
            base.OnInspectorGUI(targets, serializedObject);

            GUI.enabled = false;
            EditorGUILayout.Toggle("Thrusting", (targets[0] as Thruster).IsThrusting);

            GUI.enabled = false;
            EditorGUILayout.FloatField("Power", (targets[0] as Thruster).ThrustPower);

        }
#endif
        #endregion

        public const float FORCE = 1000000;

        private TerminalMenu mMenu_thrusting;
        public float ThrustPower { get; set; } = 1.00f;
        public bool ThrustForced { get; set; } = false;
        public bool IsThrusting => ThrustForced || ThrustPower > 0.10f;

        /// <summary>Override driver controls to be full thrust?</summary>
        /// <returns>Returns the input value.</returns>
        public bool ForceThrust(bool input) => ThrustForced = input;

        public IEnumerable<TerminalMenu> Menus => new Properties<TerminalMenu>(mMenu_thrusting);
        public UIManager Manager { get; set; }

        public VisualEffect Effect { get; set; }
        public GridDriver Driver { get; set; }

        public void Start() {
            Effect = GetComponentInChildren<VisualEffect>();
            mMenu_thrusting = new TerminalMenu("Enabled",
                new ButtonTerminal(ForceThrust));
            var driver = GetComponentInParent<GridDriver>();
            if(driver) Driver = driver;
        }

        //Apply thruster force.
        public void FixedUpdate() {

            //If the thrust is forced, power = 1, otherwise check driver inputs if there is one.
            if(ThrustForced) ThrustPower = 1.00f;
            else if(Driver) {

                Vector3 up = transform.up;
                Vector3 move = Driver.Move;
                Axis axis = LargestComponent(up);

                ThrustPower = (axis == Axis.X ? (move.x == 0 ? false : (up.x > 0 == move.x > 0))
                             : axis == Axis.Y ? (move.y == 0 ? false : (up.y > 0 == move.y > 0))
                             : axis == Axis.Z ? (move.z == 0 ? false : (up.z > 0 == move.z > 0))
                             : false) ? 1.00f : 0.00f;

            } else ThrustPower = 0.00f;

            BlockGrid grid = Location.Grid;
            if(grid && IsThrusting) {
                var force = FORCE * ThrustPower * Time.deltaTime;
                grid.Rigidbody.AddForceAtPosition(transform.up * -force, transform.position);
            }

            VisualEffect effect = Effect;
            if(effect) effect.SetBool("Thrusting", IsThrusting);

        }

        public void OnSetup() { }
        public void OnAllSetup() { }

        public class Preview : BlockPreview { }

    }

}
