using KineticEnergy.Entities;
using KineticEnergy.Grids.Blocks;
using KineticEnergy.Intangibles;
using static KineticEnergy.Entities.EntityAttributes;
using static KineticEnergy.Grids.Blocks.BlockAttributes;

namespace KineticEnergy.Interfaces.Managed {

    /// <summary>To be inherited by a <see cref="Block"/> or <see cref="Entity"/> class.</summary>
    /// <typeparam name="T">The type of data to recieve from <see cref="OnPrefab"/>.
    /// <para/>Check the <see cref="BlockAttribute"/> or <see cref="EntityAttribute"/> you want to recieve from.</typeparam>
    /// <remarks>The <see cref="OnPrefab"/> method is <b>functionally a static method</b>
    /// and should not be used to set properties or fields within your <see cref="Block"/> object.</remarks>
    /// <see cref="OnPrefab(Master, T, bool, BlockAttribute)"/>
    public interface IPrefabEditor<T> {

        /// <summary>A method that should be treated as <b>static</b> when implemented.</summary>
        /// <param name="master">The one, the only.</param>
        /// <param name="prefabData">The data requested by <typeparamref name="T"/>.</param>
        /// <param name="asPreview">Is the given <paramref name="prefabData"/> from a <see cref="BlockPreview"/>?</param>
        /// <param name="sender">The <see cref="BlockAttribute"/> that is sending <paramref name="prefabData"/>.</param>
        void OnPrefab(Master master, T prefabData, bool asPreview, BlockAttribute sender);

    }

}
