using System.Collections.Generic;

namespace KineticEnergy.Inventory {

    /// <summary>Represents multiple items. Types are differentiated by <see cref="FullName"/>.</summary>
    public struct Item {

        /// <summary>This item is the equivalent of a null, and is the only hard-coded <see cref="Item"/> type. .</summary>
        public static Item NullItem => new Item(0, "\\null", "\\null", "Nothing");

        /// <summary>The mod name.</summary>
        public string Origin { get; }
        /// <summary>The name of this item for identification.</summary>
        public string Name { get; }
        /// <summary>The name of this item that is visible to the user.</summary>
        public string DisplayName { get; }
        /// <summary>The full identification of this <see cref="Item"/>.</summary>
        public string FullName { get; }

        /// <summary>The default size of a stack, where <c>100</c> is a normal value.</summary>
        /// <remarks>Try to make these values like <c>1</c>, <c>10</c>, <c>50</c>, <c>100</c>, or <c>200</c>.
        /// Items can stack above (but not below) this value.</remarks>
        public int StackSize { get; }

        /// <summary>The amount of items in this <see cref="Stack"/>.</summary>
        public int Count { get; set; }

        /// <summary>Creates a new <see cref="Item"/>.</summary>
        /// <param name="stackSize"><see cref="StackSize"/></param>
        /// <param name="name"><see cref="Name"/></param>
        /// <param name="origin"><see cref="Origin"/></param>
        /// <param name="displayName"><see cref="DisplayName"/></param>
        public Item(int stackSize, string name, string origin, string displayName) {
            StackSize = stackSize;
            Name = name;
            Origin = origin;
            FullName = string.Format("{0}:{1}", Origin, Name);
            DisplayName = displayName;
            Count = 0;
        }

        /// <summary>Duplicates the given <see cref="Item"/>.</summary>
        /// <param name="item">Values to duplicate.</param>
        /// <param name="count"><see cref="Count"/></param>
        public Item(Item item, int count) : this(item.StackSize, item.Name, item.Origin, item.DisplayName) => Count = count;

        public static Item operator +(Item left, Item right) => new Item(left, left.Count + right.Count);
        public static Item operator -(Item left, Item right) => new Item(left, left.Count - right.Count);

        /// <summary>Tests if both <see cref="Item"/>s have the same <see cref="FullName"/>.</summary>
        public static bool operator ==(Item left, Item right) => left.FullName == right.FullName;
        /// <summary>Tests if both <see cref="Item"/>s do not have the same <see cref="FullName"/>.</summary>
        public static bool operator !=(Item left, Item right) => left.FullName != right.FullName;

        /// <summary>Tests if both <see cref="Item"/>s have the same <see cref="FullName"/>.</summary>
        public override bool Equals(object obj) => obj is Item item && Count == item.Count && FullName == item.FullName;
        /// <summary>Uses <see cref="FullName"/> to generate a hash code.</summary>
        public override int GetHashCode() => 733961487 + EqualityComparer<string>.Default.GetHashCode(FullName);

    }

}
