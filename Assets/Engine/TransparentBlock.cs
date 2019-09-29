using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

namespace KineticEnergy.Ships.Blocks {

    /// <summary>
    /// No sides are opaque.
    /// </summary>
    public abstract class TransparentBlock : Block {

        public override bool SideIsOpaque(Vector3Int localPosition, Face face) {
            //Definition of a transparent block: every side is non-opaque.
            return false;
        }

    }

}
