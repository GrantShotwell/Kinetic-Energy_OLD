using static KineticEnergy.Maths.Methods;

namespace KineticEnergy.Assets.KineticEnergy.Math {

    /// <summary>Stores data and methods for a triangle.</summary>
    public struct Triangle {
        public float A, B, C; //angles
        public float a, b, c; //sides

        /// <summary>
        /// Returns a complete triangle given the side lengths.
        /// </summary>
        /// <param name="_a">side length 'a'</param>
        /// <param name="_b">side length 'b'</param>
        /// <param name="_c">side length 'c'</param>
        public Triangle(float _a, float _b, float _c) {
            a = _a;
            b = _b;
            c = _c;
            A = LawOfCosForAngleA(a, b, c);
            B = LawOfCosForAngleB(a, b, c);
            C = LawOfCosForAngleC(a, b, c);
        }

        public void SolveForAngles() {
            A = LawOfCosForAngleA(a, b, c);
            B = LawOfCosForAngleB(a, b, c);
            C = LawOfCosForAngleC(a, b, c);
        }

        public override string ToString() {
            return base.ToString();
        }
    }

}
