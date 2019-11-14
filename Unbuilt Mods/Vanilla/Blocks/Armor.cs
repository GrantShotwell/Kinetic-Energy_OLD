using UnityEngine;
using KineticEnergy.Ships.Blocks;

namespace KineticEnergy.Mods.Vanilla.Blocks {

    [BlockAttributes.BasicInfo(
        "Armor", 1, 1, 1,
        10000, 0, 0, 0
    )]
    [BlockAttributes.BoxCollider(0, 0, 0, 1, 1, 1)]
    [BlockAttributes.FlatFace(Face.PosX)]
    [BlockAttributes.FlatFace(Face.NegX)]
    [BlockAttributes.FlatFace(Face.PosY)]
    [BlockAttributes.FlatFace(Face.NegY)]
    [BlockAttributes.FlatFace(Face.PosZ)]
    [BlockAttributes.FlatFace(Face.NegZ)]
    [BlockAttributes.Material(
        "Content\\Vanilla\\Blocks\\ArmorBlock\\diffuse.png", true,
        "Content\\Vanilla\\Blocks\\ArmorBlock\\normal.png", true, 1)]
    public class Armor : OpaqueBlock {
        public class Preview : BlockPreview { }
    }

}
