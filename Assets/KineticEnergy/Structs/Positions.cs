using System;
using KineticEnergy.Grids;
using KineticEnergy.Grids.Blocks;
using KineticEnergy.Maths;
using UnityEngine;

namespace KineticEnergy.Structs {

    /// <summary>Represents a position within a <see cref="BlockGrid"/> with a possible offset.</summary>
    [Serializable]
    public struct GridLocation {

        [SerializeField] private Vector3Int vector, offset;
        [SerializeField] private BlockGrid grid;

        /// <summary>Creates a new <see cref="GridLocation"/> with no offset.</summary>
        public GridLocation(BlockGrid grid, Vector3Int vector) {
            this.grid = grid;
            this.vector = vector;
            offset = Vector3Int.zero;
        }
        /// <summary>Creates a new <see cref="GridLocation"/> with an offset.</summary>
        public GridLocation(BlockGrid grid, Vector3Int vector, Vector3Int offset) {
            this.grid = grid;
            this.vector = vector;
            this.offset = offset;
        }

        /// <summary>The value of this position in grid space.</summary>
        public Vector3Int GridVector {
            get => vector + Offset;
            set => vector = value - Offset;
        }

        /// <summary>The value of this position in array space.</summary>
        public Vector3Int ArrayVector {
            get => GridVector + Grid.Offset;
            set => GridVector = value - Grid.Offset;
        }

        /// <summary>The defined offset that is added to the <see cref="vector"/>.</summary>
        public Vector3Int Offset { get => offset; set => offset = value; }

        /// <summary>what do you think</summary>
        public BlockGrid Grid { get => grid; set => grid = value; }

        /// <summary>Does this particular <see cref="GridLocation"/> have an offset defined?</summary>
        public bool HasOffset => Offset != Vector3Int.zero;

        /// <summary>The current value of this position in world space.</summary>
        public Vector3 GetCurrentWorldValue() => Grid.LocalGrid_to_WorldPoint(GridVector);

        public static implicit operator Vector3Int(GridLocation pos) => pos.GridVector;

    }

    /// <summary>Represents a position within a <see cref="KineticEnergy.Grids.Blocks.Block"/>.</summary>
    [Serializable]
    public struct BlockPosition {

        [SerializeField] private Vector3Int vector;

        public BlockPosition(Block block, Vector3Int value) {
            Block = block;
            vector = value;
        }

        /// <summary>The value of this position with respect to <see cref="Block"/>.</summary>
        public Vector3Int RelativeValue { get => vector; set => vector = value; }
        /// <summary>The value of this position after applying <see cref="Block"/>'s rotation.</summary>
        public Vector3Int RotatedValue => Block.LocalPoint_to_RelativePoint(vector).Rounded();

        /// <summary>The <see cref="Grids.Blocks.Block"/> this <see cref="BlockPosition"/> is for.</summary>
        public Block Block { get; set; }

        public static implicit operator GridLocation(BlockPosition pos)
            => new GridLocation(pos.Block.Location.Grid, pos.RotatedValue, pos.Block.Location);

    }

}
