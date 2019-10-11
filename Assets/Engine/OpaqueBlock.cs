using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using KineticEnergy.CodeTools;

namespace KineticEnergy.Ships.Blocks {

    /// <summary>
    /// All sides are opaque.
    /// </summary>
    public abstract class OpaqueBlock : Block {

        new public Collider collider;
        public Faces faces;

        public override bool SideIsOpaque(Vector3Int localPosition, Face face) {
            //Definition of an opaque block: every side is opaque.
            return true;
        }

        public override void OnGridSet() {
            UpdateFaces();
        }

        public override void OnNearbyPieceAdded(Vector3Int relativePosition) {
            UpdateFaces();
        }

        public override void OnNearbyPieceRemoved(Vector3Int relativePosition) {
            UpdateFaces();
        }

        public void UpdateFaces() {
            byte enabledFaces = WhichFacesShown();
            faces.ToggleFaces(enabledFaces);
            collider.enabled = (enabledFaces & 0b111111) != 0;
        }

    }

}
