using KineticEnergy.Grids.Blocks;

namespace KineticEnergy.Mods.Vanilla.Blocks {

    [BlockAttributes.BasicInfo(
        "Fuel Container", 3, 3, 6,
        740000, 0, 0, 0, true)]
    [BlockAttributes.Mesh("Content\\Vanilla\\Models\\3x3x6-tank.obj")]
    [BlockAttributes.Material("Content\\Vanilla\\Textures\\armor-diffuse.png", true)]
    [BlockAttributes.MeshCollider()]
    class FuelTank : TransparentBlock {

        public int fuel = 0;

        public void FixedUpdate() {

        }

        public class Preview : BlockPreview { }

    }

}
