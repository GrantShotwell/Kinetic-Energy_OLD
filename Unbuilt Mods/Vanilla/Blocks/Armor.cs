using KineticEnergy.Grids.Blocks;

namespace KineticEnergy.Mods.Vanilla.Blocks {

    [BlockAttributes.BasicInfo(
        "Armor", 1, 1, 1,
        10000, 0, 0, 0,
        true
    )]
    [BlockAttributes.BoxCollider(0, 0, 0, 1, 1, 1)]
    [BlockAttributes.FlatFace(Face.PosX)]
    [BlockAttributes.FlatFace(Face.NegX)]
    [BlockAttributes.FlatFace(Face.PosY)]
    [BlockAttributes.FlatFace(Face.NegY)]
    [BlockAttributes.FlatFace(Face.PosZ)]
    [BlockAttributes.FlatFace(Face.NegZ)]
    [BlockAttributes.Material(
        "Content\\Vanilla\\Textures\\armor-diffuse.png", true,
        "Content\\Vanilla\\Textures\\armor-height.png", false, 1)]
    public class Armor : OpaqueBlock {

        public class Preview : BlockPreview { }

    }

}
