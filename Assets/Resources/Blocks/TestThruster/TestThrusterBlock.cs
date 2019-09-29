using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace KineticEnergy.Ships.Blocks {

    public class TestThrusterBlock : TransparentBlock {

        public void FixedUpdate() {
            if(grid != null) {
                float force = 100000;
                grid.Rigidbody.AddForceAtPosition(transform.up * -force, transform.position);
            }
        }

        public void OnDrawGizmosSelected() {
            Gizmos.color = Color.green;
            Gizmos.DrawLine(transform.position, transform.forward + transform.position);
        }

    }

}
