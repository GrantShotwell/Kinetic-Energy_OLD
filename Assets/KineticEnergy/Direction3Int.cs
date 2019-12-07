using System;
using KineticEnergy.Maths;
using UnityEngine;

namespace KineticEnergy.CodeTools {

    public static class Direction3Int {
        public static Vector3Int PosX = new Vector3Int(+1, 0, 0);
        public static Vector3Int NegX = new Vector3Int(-1, 0, 0);
        public static Vector3Int PosY = new Vector3Int(0, +1, 0);
        public static Vector3Int NegY = new Vector3Int(0, -1, 0);
        public static Vector3Int PosZ = new Vector3Int(0, 0, +1);
        public static Vector3Int NegZ = new Vector3Int(0, 0, -1);
    }

}
