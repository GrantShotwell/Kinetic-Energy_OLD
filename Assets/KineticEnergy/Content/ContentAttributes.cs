using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KineticEnergy.Content {

    static class ContentAttributes {
        
        public abstract class ContentAttribute : Attribute {

        }

        public class ReplacesTypeAttribute : ContentAttribute {

            public Type ReplacedType { get; }

            public ReplacesTypeAttribute(Type replaced) => ReplacedType = replaced;

        }

    }

}
