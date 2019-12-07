using UnityEngine;

namespace KineticEnergy.Maths {

    /// <summary>Stores data and methods for a circle.</summary>
    public struct Circle {
        public Vector2 center;
        public float radius;
        public Circle(Vector2 center, float radius) {
            this.center = center;
            this.radius = radius;
        }
    }

}
