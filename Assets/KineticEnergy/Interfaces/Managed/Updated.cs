using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KineticEnergy.Interfaces.Managed {

    public interface IPhysicsUpdated {
        void OnUpdate(float dT);
    }

    public interface IFrameUpdated {
        void OnUpdate(float dT);
    }

}
