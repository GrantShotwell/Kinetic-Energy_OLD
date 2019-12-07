
namespace KineticEnergy.Maths {

    /// <summary>General direction in terms of left/right/up/down. More specifically, the normalized direction on either the X or Y axis.</summary>
    public enum Direction { Up, Down, Right, Left }

    /// <summary>Positive = 1, Negative = -1.</summary>
    public enum Sign { Negative = -1, Zero = 0, Positive = 1 }

    /// <summary>x : y : : horizontal : vertical</summary>
    public enum Axis {
        Horizontal = 0,
        Vertical = 1,
        X = 0, Y = 1, Z = 2
    }

    /// <summary>Enumerator for radians or degrees.</summary>
    public enum AngleType { Radians, Degrees }

}
