using System;
using KineticEnergy.Maths;
using UnityEngine;

namespace KineticEnergy.PhysicsHelpers {

    public static class ApplyForce {

        /// <summary>Applies a change in rotational energy of a gyroscope wheel with <see cref="Rigidbody.AddForceAtPosition(Vector3, Vector3, ForceMode)"/>.</summary>
        /// <param name="rigidbody">The <see cref="Rigidbody"/> to apply the forces to.</param>
        /// <param name="offset">The center of the gyroscope wheel relative to the <paramref name="rigidbody"/>.</param>
        /// <param name="axis">The axis of rotation of the gyroscope wheel.</param>
        /// <param name="radius">The radius of the gyroscope wheel.</param>
        /// <param name="force">The force to apply.</param>
        /// <param name="mode">The <see cref="ForceMode"/> argument.</param>
        public static void Gyroscope(Rigidbody rigidbody, Vector3 offset, Axis axis, float radius, float force, ForceMode mode) {
            switch(axis) {
                case Axis.X: Gyroscope(rigidbody, offset, new Vector3(0, 0, radius), new Vector3(0, -1, 0), force / 2, mode); break;
                case Axis.Y: Gyroscope(rigidbody, offset, new Vector3(radius, 0, 0), new Vector3(0, 0, -1), force / 2, mode); break;
                case Axis.Z: Gyroscope(rigidbody, offset, new Vector3(0, radius, 0), new Vector3(-1, 0, 0), force / 2, mode); break;
                default: throw new ArgumentException(string.Format("The given argument is not a valid {0}.", nameof(Axis)), nameof(axis));
            }
        }

        /// <summary>Applies a change in rotational energy of a gyroscope wheel with <see cref="Rigidbody.AddForceAtPosition(Vector3, Vector3, ForceMode)"/>.</summary>
        /// <param name="rigidbody">The <see cref="Rigidbody"/> to apply the forces to.</param>
        /// <param name="offset">The center of the gyroscope wheel relative to the <paramref name="rigidbody"/>.</param>
        /// <param name="radius">The radius of the gyroscope wheel.</param>
        /// <param name="cross">The cross product of <paramref name="radius"/> (normalized) and the gyroscope wheel's axis of rotation (also normalized). Order is not important.</param>
        /// <param name="halfForce">Half of the total magnitude of force to apply.</param>
        /// <param name="mode">The <see cref="ForceMode"/> argument.</param>
        public static void Gyroscope(Rigidbody rigidbody, Vector3 offset, Vector3 radius, Vector3 cross, float halfForce, ForceMode mode) {

            // Force 1
            Vector3 force1F = rigidbody.transform.TransformPoint(halfForce * -cross);
            Vector3 force1P = rigidbody.transform.TransformPoint(offset + radius);

            // Force 2
            Vector3 force2F = rigidbody.transform.TransformPoint(halfForce * cross);
            Vector3 force2P = rigidbody.transform.TransformPoint(offset - radius);

            //Debug.DrawLine(force1P, force1P + force1F, Color.blue, 0, false);
            //Debug.DrawLine(force2P, force2P + force2F, Color.cyan, 0, false);
            //Debug.DrawLine(force1P, force2P, halfForce > 0 ? Color.green : Color.red, 0, false);

            rigidbody.AddForceAtPosition(force1F, force1P, mode);
            rigidbody.AddForceAtPosition(force2F, force2P, mode);

        }

        /// <summary>Applies a change in rotational energy of a gyroscope wheel with <see cref="Rigidbody.AddForceAtPosition(Vector3, Vector3, ForceMode)"/>.</summary>
        /// <param name="rigidbody">The <see cref="Rigidbody"/> to apply the forces to.</param>
        /// <param name="offset">The center of the gyroscope wheel relative to the <paramref name="rigidbody"/>.</param>
        /// <param name="axis">The normalized axis of rotation of the gyroscope wheel.</param>
        /// <param name="radius">The radius of the gyroscope wheel as a vector perpendicular to the <paramref name="axis"/>.</param>
        /// <param name="force">The force to apply.</param>
        /// <param name="mode">The <see cref="ForceMode"/> argument.</param>
        public static void GyroscopeAxis(Rigidbody rigidbody, Vector3 offset, Vector3 axis, Vector3 radius, float force, ForceMode mode)
            => Gyroscope(rigidbody, offset, radius, Vector3.Cross(axis, radius.normalized), force, mode);

    }

}
