using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using KineticEnergy.Ships.Blocks.Interfaces.Terminal;

namespace KineticEnergy.Ships.Blocks {

    public class TestThrusterBlock : TransparentBlock {

        public float force = 50000;

        public bool thrusting = false;
        public float power = 1.00f;

#pragma warning disable IDE0052 // Remove unread private members
        private BlockObject blockObj;
#pragma warning restore IDE0052
        public void Awake() {
            blockObj = new BlockObject(this);
        }

        //Apply thruster force.
        public void FixedUpdate() {
            if(grid != null && thrusting) {
                var f = force * power;
                grid.Rigidbody.AddForceAtPosition(transform.up * -f, transform.position);
            }
        }

        private class BlockObject : IBlockTerminal {

            public BlockObject(TestThrusterBlock block) {
                trmEnabled = new TerminalItem(typeof(bool), value => block.thrusting = (bool)value);
                trmPower = new TerminalItem(typeof(float), value => block.power = (float)value);
            }

            readonly TerminalItem trmEnabled, trmPower;
            TerminalItem[] IBlockTerminal.TerminalItems => new TerminalItem[] { trmEnabled, trmPower };

        }

    }

}
