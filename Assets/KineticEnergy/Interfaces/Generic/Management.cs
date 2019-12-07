using System.Collections.Generic;

namespace KineticEnergy.Interfaces.Generic {

    /// <summary>An interface for classes that manage <see cref="IManaged{TManaged}"/>.</summary>
    /// <typeparam name="TManaged">The common base class between all similar <see cref="IManaged{T}"/>s that this <see cref="IManager{T}"/> manages.</typeparam>
    public interface IManager<TManaged> where TManaged : class {

        /// <summary>Tells if all <see cref="IManaged{T}"/>s that this <see cref="IManager{T}"/> manages have run <see cref="IManaged{T}.OnSetup()"/>.</summary>
        bool AllSetup { get; }

        /// <summary>Returns all <see cref="IManaged{T}"/>s that this <see cref="IManager{T}"/> manages.</summary>
        IEnumerable<TManaged> Managed { get; }

    }

    /// <summary>A <see cref="IManager{TManaged}"/> where the manager adds, but the managed remove.</summary>
    /// <typeparam name="TManaged">The common base class between all similar
    /// <see cref="IManaged{TManager}"/>s that this <see cref="IManager{TManaged}"/> manages.</typeparam>
    public interface IDynamicManager<TManaged> : IManager<TManaged> where TManaged : class {

        /// <summary>Method for a <see cref="IManaged{TManager}"/> to request that they be removed from the <see cref="IManager{TManaged}.Managed"/> collection.</summary>
        void RemoveMe(TManaged me);

    }

    /// <summary>An interface for classes that require a <see cref="IManager{TManaged}"/>.</summary>
    /// <typeparam name="TManager">The class of this <see cref="IManaged{TManager}"/>'s <see cref="IManager{TManaged}"/>.</typeparam>
    public interface IManaged<TManager> where TManager : class {

        /// <summary>The <see cref="IManager{T}"/> of this <see cref="IManaged{T}"/>.</summary>
        TManager Manager { get; set; }

        /// <summary>Method called by the <see cref="IManager{TManaged}"/>
        /// to signify that this <see cref="IManaged{TManager}"/> is setup.</summary>
        void OnSetup();

        /// <summary>Method called by the <see cref="IManager{TManaged}"/>
        /// to signify that all <see cref="IManaged{TManager}"/> in its <see cref="IManager{TManaged}.Managed"/> is setup.</summary>
        void OnAllSetup();

    }

}
