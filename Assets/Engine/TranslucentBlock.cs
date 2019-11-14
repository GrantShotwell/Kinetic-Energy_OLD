using System;
using UnityEngine;

namespace KineticEnergy.Ships.Blocks {

    /// <summary>
    /// Some sides are opaque.
    /// </summary>
    public class TranslucentBlock : Block {

        public override bool SideIsOpaque(Vector3Int localPosition, Face face) {
            //Definition of a translucent block (in this context): some sides are opaque.
            throw new NotImplementedException();
        }

    }

}
