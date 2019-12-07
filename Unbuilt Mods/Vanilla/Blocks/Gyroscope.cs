using System;
using KineticEnergy.Grids.Blocks;
using KineticEnergy.Maths;
using KineticEnergy.PhysicsHelpers;
using UnityEditor;
using UnityEngine;
using Object = UnityEngine.Object;

namespace KineticEnergy.Mods.Vanilla.Blocks {

    [BlockAttributes.BasicInfo(
        "Gyroscope", 1, 1, 1,
        12000, 0, 0, 0, true)]
    [BlockAttributes.BoxCollider(0, 0, 0, 1, 1, 1)]
    [BlockAttributes.Face(Face.PosX, FACE_OBJ)]
    [BlockAttributes.Face(Face.NegX, FACE_OBJ)]
    [BlockAttributes.Face(Face.PosY, FACE_OBJ)]
    [BlockAttributes.Face(Face.NegY, FACE_OBJ)]
    [BlockAttributes.Face(Face.PosZ, FACE_OBJ)]
    [BlockAttributes.Face(Face.NegZ, FACE_OBJ)]
    class Gyroscope : OpaqueBlock, IGridDrivable {

        public const string FACE_OBJ = "Content\\Vanilla\\Models\\GyroscopeFace.obj";
        public const float ROT_POWER = 50E3f;
        public const float RADIUS = 0.4f;

#if UNITY_EDITOR
        public override void OnInspectorGUI(Object[] targets, SerializedObject serializedObject) {
            base.OnInspectorGUI(targets, serializedObject);

            GUI.enabled = false;
            EditorGUILayout.Vector3Field("Player Look Input", (targets[0] as Gyroscope).Driver.Look);

        }
#endif

        Rigidbody Rigidbody { get; set; }
        public GridDriver Driver { get; set; }

        public void Start() {
            if(!Driver) Driver = GetComponentInParent<GridDriver>();
            Rigidbody = GetComponentInParent<Rigidbody>();
        }

        public void FixedUpdate() {
            if(Driver) {

                var look = Driver.Look;
                var rigidbody = Rigidbody;

                if(Math.Abs(look.x) > 0.001f) ApplyForce.Gyroscope(rigidbody, transform.localPosition, Axis.Y, RADIUS, look.x * ROT_POWER, ForceMode.Force);
                if(Math.Abs(look.y) > 0.001f) ApplyForce.Gyroscope(rigidbody, transform.localPosition, Axis.X, RADIUS, look.y * ROT_POWER, ForceMode.Force);
                if(Math.Abs(look.z) > 0.001f) ApplyForce.Gyroscope(rigidbody, transform.localPosition, Axis.Z, RADIUS, look.z * ROT_POWER, ForceMode.Force);

            }
        }

        public class Preview : BlockPreview { }

    }
}
