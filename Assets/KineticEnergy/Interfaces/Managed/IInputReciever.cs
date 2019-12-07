using System;
using System.Collections.Generic;
using KineticEnergy.Intangibles.Client;

namespace KineticEnergy.Interfaces.Managed {

    public interface IInputReciever {

        IEnumerable<int> Clients { get; }

        void SendInputs(int client, Inputs inputs);

    }

}
