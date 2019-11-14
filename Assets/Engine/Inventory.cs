using System.Collections.Generic;

namespace KineticEnergy.Inventory {

    /// <summary>
    /// Everything that hold <see cref="Item"/>s extends from this class.
    /// </summary>
    public abstract class Container {
        /// <summary>The total capacity of <see cref="Item"/>s that this <see cref="Container"/> can hold.</summary>
        public abstract Size Capacity { get; }
        /// <summary>The total capacity of <see cref="Item"/>s that this <see cref="Container"/> has used.</summary>
        public abstract Size UsedCapacity { get; }
        /// <summary>The total capacity of <see cref="Item"/>s that this <see cref="Container"/> has not used.</summary>
        public virtual Size UnusedCapacity => Capacity - UsedCapacity;
        /// <summary>The contents of this <see cref="Container"/>.</summary>
        public abstract Item[] Contents { get; }
        /// <summary>Attempts to add the given <see cref="Item"/> to the <see cref="Container"/>.
        /// Whatever cannot be added is returned.</summary>
        /// <param name="inputItem">The stack to try to add.</param>
        public abstract Item Add(Item inputItem);
        /// <summary>Attempts to take the given <see cref="Item"/> from the <see cref="Container"/>.
        /// Whatever can be taken is returned.</summary>
        /// <param name="outputItem">The stack to try to take.</param>
        public abstract Item Take(Item outputItem);
    }
    /// <summary>
    /// <see cref="Container"/> without the BS.
    /// </summary>
    public abstract class SimpleContainer : Container {

        /// <summary>The total capacity of <see cref="Item"/>s that this <see cref="Container"/> has not used.</summary>
        public override Size UsedCapacity {
            get {
                Size used = 0;
                foreach(Item item in containedItems)
                    used += item.StackSize;
                return used;
            }
        }

        List<Item> containedItems = new List<Item>();
        /// <summary>The contents of this <see cref="Container"/>.</summary>
        public override Item[] Contents => containedItems.ToArray();

        /// <summary>Attempts to add the given <see cref="Item"/> to the <see cref="Container"/>.
        /// Whatever cannot be added is returned.</summary>
        /// <param name="inputItem">The stack to try to add.</param>
        public override Item Add(Item inputItem) { return SimpleAdd(ref containedItems, ref inputItem, Capacity); }
        /// <summary>Attempts to add the given <see cref="Item"/> to the given <see cref="List{T}"/>.
        /// Whatever cannot be added is returned.</summary>
        /// <param name="inputItem">The stack to try to add.</param>
        public static Item SimpleAdd(ref List<Item> contents, ref Item inputItem, Size capacity) {
            foreach(Item containedItem in contents) {
                if(containedItem.GetType() == inputItem.GetType()) {
                    var addMax = containedItem.MaxToAdd(capacity);
                    if(inputItem.count < addMax) addMax = inputItem.count;
                    inputItem.count -= addMax;
                    containedItem.count += addMax;
                    return inputItem;
                }
            }
            return inputItem;
        }

        /// <summary>Attempts to take the given <see cref="Item"/> from the <see cref="Container"/>.
        /// Whatever can be taken is returned.</summary>
        /// <param name="outputItem">The stack to try to take.</param>
        public override Item Take(Item outputItem) { return SimpleTake(ref containedItems, ref outputItem, Capacity); }
        /// <summary>Attempts to take the given <see cref="Item"/> from the given <see cref="List{T}"/>.
        /// Whatever can be taken is returned.</summary>
        /// <param name="outputItem">The stack to try to take.</param>
        public static Item SimpleTake(ref List<Item> contents, ref Item outputItem, Size capacity) {
            foreach(Item containedItem in contents) {
                if(containedItem.GetType() == outputItem.GetType()) {
                    var subMax = containedItem.MaxToSub(capacity);
                    outputItem.count += subMax;
                    containedItem.count -= subMax;
                    return outputItem;
                }
            }
            outputItem.count = 0;
            return outputItem;
        }

    }



    /// <summary>
    /// Everything that is held by a <see cref="Container"/> extends from this class.
    /// <para/>
    /// One instance of this class represents a "stack" of the item.
    /// </summary>
    public abstract class Item {
        /// <summary>The amount of items that are in this stack.</summary>
        public ulong count = 0;
        /// <summary>The total size of this stack.</summary>
        public abstract Size StackSize { get; }
        /// <summary>Finds how many items can be added to this stack so it as close as possible to the given maximumSize.</summary>
        /// <param name="maximumSize">The preferred <see cref="Size"/>.</param>
        public abstract ulong MaxToAdd(Size maximumSize);
        /// <summary>Finds how many items can be removed from this stack so it as close as possible to the given minimumSize.</summary>
        /// <param name="maximumSize">The preferred <see cref="Size"/>.</param>
        public abstract ulong MaxToSub(Size minimumSize);
    }
    /// <summary>
    /// <see cref="Item"/> without the BS.
    /// </summary>
    public abstract class SimpleItem : Item {

        /// <summary>Gets the size of a single item.</summary>
        public abstract ulong ItemSize { get; }
        /// <summary>The total size of this stack.</summary>
        public override Size StackSize => ItemSize * count;

        /// <summary>Finds how many items can be added to this stack so it as close as possible to the given maximumSize.</summary>
        /// <param name="maximumSize">The preferred <see cref="Size"/>.</param>
        public override ulong MaxToAdd(Size maximumSize) {
            var currentSize = StackSize;
            if(currentSize > maximumSize) return 0;
            return (maximumSize.value - currentSize.value) / ItemSize;
        }

        /// <summary>Finds how many items can be removed from this stack so it as close as possible to the given minimumSize.</summary>
        /// <param name="maximumSize">The preferred <see cref="Size"/>.</param>
        public override ulong MaxToSub(Size minimumSize) {
            var currentSize = StackSize;
            if(currentSize < minimumSize) return 0;
            return (currentSize.value - minimumSize.value) / ItemSize;
        }

    }

    /// <summary>
    /// Essentially just a number with operators that check for overflow/underflow when using operators.
    /// </summary>
    public struct Size {
        /// <summary>20 units is twice as big as 10 units. Don't overthink it.</summary>
        public ulong value;
        public Size(ulong units) {
            value = units;
        }

        public static implicit operator Size(ulong value) { return new Size(value); }

        /// <summary>An addition operator that avoids overflow by checking if the result is less than either of the original values.
        /// If there is an overflow, the result will be <see cref="ulong.MaxValue"/>.</summary>
        public static Size operator +(Size left, Size right) {
            ulong result = left.value + right.value;
            if(result < left.value || result < right.value)
                result = ulong.MaxValue;
            return new Size(result);
        }

        /// <summary>A subtraction operator that avoids underflow by checking if the result is greater than either of the original values.
        /// If there is an underflow, the result will be <see cref="ulong.MinValue"/>.</summary>
        public static Size operator -(Size left, Size right) {
            ulong result = left.value - right.value;
            if(result > left.value || result < right.value)
                result = ulong.MinValue;
            return new Size(result);
        }

        /// <summary>A multiplication operator that avoids overflow by checking if the result is less than either of the original values.
        /// If there is an overflow, the result will be <see cref="ulong.MaxValue"/>.</summary>
        public static Size operator *(Size left, Size right) {
            ulong result = left.value * right.value;
            if(result < left.value || result < right.value)
                result = ulong.MaxValue;
            return new Size(result);
        }

        /// <summary>A multiplication operator that avoids overflow by checking if the result is less than either of the original values.
        /// If there is an overflow, the result will be <see cref="ulong.MaxValue"/>.</summary>
        public static Size operator *(Size size, ulong value) {
            ulong result = size.value * value;
            if(result < size.value || result < value)
                result = ulong.MaxValue;
            return new Size(result);
        }

        /// <summary>A division operator that avoids underflow by checking if the result is greater than either of the original values.
        /// If there is an underflow, the result will be <see cref="ulong.MinValue"/>.</summary>
        public static Size operator /(Size left, Size right) {
            ulong result = left.value / right.value;
            if(result > left.value || result > right.value)
                result = ulong.MinValue;
            return new Size(result);
        }

        /// <summary>A division operator that avoids underflow by checking if the result is greater than either of the original values.
        /// If there is an underflow, the result will be <see cref="ulong.MinValue"/>.</summary>
        public static Size operator /(Size size, ulong value) {
            ulong result = size.value / value;
            if(result > size.value || result > value)
                result = ulong.MinValue;
            return new Size(result);
        }

        public static bool operator <(Size left, Size right) {
            return left.value < right.value;
        }

        public static bool operator >(Size left, Size right) {
            return left.value > right.value;
        }

    }

}
