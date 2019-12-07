using KineticEnergy.Grids;
using KineticEnergy.Grids.Blocks;
using UnityEngine;

namespace KineticEnergy.Mods.Vanilla {

    public class GridDriver : MonoBehaviour {

        public Vector3 Move { get; set; }
        public Vector3 Look { get; set; }
        public BlockGrid Grid { get; set; }

        public void Start() {
            if(!Grid) Grid = GetComponent<BlockGrid>();
            foreach(Block block in Grid) {
                if(block is IGridDrivable drivable)
                    drivable.Driver = this;
            }
        }

    }

    public interface IGridDrivable {
        GridDriver Driver { set; }
    }

}
