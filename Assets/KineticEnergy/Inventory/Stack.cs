using System;

namespace KineticEnergy.Inventory {

    public abstract class Stack {

        /// <summary>The <see cref="Item"/> that is in this <see cref="Stack"/>.</summary>
        public Item Item { get; set; } = new Item();

        /// <summary>A stack size of <c>1</c> will hold 100 items,
        /// while a size of <c>2</c> will hold 200 items.</summary>
        public virtual int CapacityMultiplier => 1;

        public int CurrentMax => Item.StackSize * CapacityMultiplier;

        public int EmptyCount => CurrentMax - Item.Count;

        /// <summary>For adding <see cref="Inventory.Item"/>s.</summary>
        /// <param name="outside">The items outside of this <see cref="Stack"/>.
        /// <para/><b>Before Execution:</b> The items that want to be added.
        /// <para/><b>After Execution:</b> The leftover items that were not added in.</param>
        /// <returns>Returns if anything was added.</returns>
        public bool Add(ref Item outside) {
            Item inside = Item;

            int count = Math.Min(outside.Count, EmptyCount);
            if(count == 0) return false;
            inside.Count += count;
            outside.Count -= count;

            Item = inside;
            return true;

        }

        /// <summary>For removing <see cref="Inventory.Item"/>s.</summary>
        /// <param name="outside">The items outside of this <see cref="Stack"/>.
        /// <para/><b>Before Execution:</b> The items that want to be taken.
        /// <para/><b>After Execution:</b> The items that were taken out.</param>
        /// <returns>Returns if anything was taken.</returns>
        public bool Remove(ref Item outside) {
            Item inside = Item;

            int count = Math.Min(outside.Count, inside.Count);
            if(count == 0) return false;
            inside.Count -= count;
            outside.Count += count;

            Item = inside;
            return true;

        }

    }

}
