using System;
using System.IO;
using System.Reflection;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using KineticEnergy.Ships.Blocks;
using KineticEnergy.CodeTools.Content;
using KineticEnergy.Intangibles.Behaviours;
using static KineticEnergy.CodeTools.Math.Geometry;
using KineticEnergy.Intangibles.Client;
using KineticEnergy.Entities;

namespace KineticEnergy {

    #region Prefab

    /// <summary>Stores any <see cref="GameObject"/>, as if it is a prefab.</summary>
    /// <seealso cref="Instantiated"/>
    /// <seealso cref="Prefab{TComponent1}"/>
    /// <seealso cref="Prefab{TComponent1, TComponent2}"/>
    public struct Prefab {

        public GameObject gameObject;

        /// <summary>Creates a new <see cref="Prefab"/>.</summary>
        public Prefab(GameObject gameObject) {
            this.gameObject = gameObject;
            this.gameObject.SetActive(false);
        }

        /// <summary>Equivalent function to <see cref="UnityEngine.Object.Instantiate"/>.</summary>
        /// <returns>Returns <see cref="Instantiated"/>.</returns>
        public Instantiated Instantiate(bool active = true) {
            var instantiated = UnityEngine.Object.Instantiate(gameObject);
            instantiated.name = gameObject.name;
            instantiated.SetActive(active);
            return instantiated;
        }

        /// <summary>Equivalent function to <see cref="UnityEngine.Object.Instantiate"/>.</summary>
        /// <param name="parent">Sets <see cref="Transform.parent"/> of the new <see cref="GameObject"/>.</param>
        /// <returns>Returns <see cref="Instantiated"/>.</returns>
        public Instantiated Instantiate(Transform parent, bool active = true) {
            var instantiated = UnityEngine.Object.Instantiate(gameObject);
            instantiated.name = gameObject.name;
            instantiated.transform.parent = parent;
            instantiated.SetActive(active);
            return instantiated;
        }

        public static implicit operator Prefab(GameObject gameObject) => new Prefab(gameObject);
        public static implicit operator GameObject(Prefab prefab) => prefab.gameObject;

    }

    /// <summary>Stores the result of <see cref="Prefab.Instantiate"/>.</summary>
    public struct Instantiated {

        public GameObject gameObject;

        public Instantiated(GameObject gameObject) {
            this.gameObject = gameObject;
        }

        public static implicit operator Instantiated(GameObject gameObject) => new Instantiated(gameObject);

    }

    /// <summary>Stores any <see cref="GameObject"/>, as if it is a prefab, that has at least one defined <see cref="Component"/>.</summary>
    /// <seealso cref="Instantiated{TComponent1}"/>
    /// <seealso cref="Prefab"/>
    /// <seealso cref="Prefab{TComponent1, TComponent2}"/>
    public struct Prefab<TComponent1> where TComponent1 : Component {

        public GameObject gameObject;
        public TComponent1 component;

        /// <summary>Creates a new <see cref="Prefab{TComponent1}"/> by using <see cref="GameObject.GetComponent"/>.</summary>
        public Prefab(GameObject gameObject) {
            this.gameObject = gameObject;
            gameObject.SetActive(false);
            component = gameObject.GetComponent<TComponent1>();
        }

        /// <summary>Creates a new <see cref="Prefab{TComponent1}"/> by using <see cref="Component.gameObject"/>.</summary>
        public Prefab(TComponent1 component) {
            this.component = component;
            gameObject = component.gameObject;
            gameObject.SetActive(false);
        }

        /// <summary>Equivalent function to <see cref="UnityEngine.Object.Instantiate"/>.</summary>
        /// <returns>Returns <see cref="Instantiated{TComponent1}"/>.</returns>
        public Instantiated<TComponent1> Instantiate(bool active = true) {
            var instantiated = UnityEngine.Object.Instantiate(gameObject);
            instantiated.name = gameObject.name;
            instantiated.SetActive(active);
            return instantiated;
        }

        /// <summary>Equivalent function to <see cref="UnityEngine.Object.Instantiate"/>.</summary>
        /// <param name="parent">Sets <see cref="Transform.parent"/> of the new <see cref="GameObject"/>.</param>
        /// <returns>Returns <see cref="Instantiated{TComponent1}"/>.</returns>
        public Instantiated<TComponent1> Instantiate(Transform parent, bool active = true) {
            var instantiated = UnityEngine.Object.Instantiate(gameObject);
            instantiated.name = gameObject.name;
            instantiated.transform.SetParent(parent, false);
            instantiated.SetActive(active);
            return instantiated;
        }

        public static implicit operator Prefab(Prefab<TComponent1> prefab) => new Prefab(prefab.gameObject);
        public static implicit operator Prefab<TComponent1>(GameObject gameObject) => new Prefab<TComponent1>(gameObject);
        public static implicit operator Prefab<TComponent1>(TComponent1 component) => new Prefab<TComponent1>(component);
        public static implicit operator GameObject(Prefab<TComponent1> prefab) => prefab.gameObject;
        public static implicit operator TComponent1(Prefab<TComponent1> prefab) => prefab.component;

    }


    /// <summary>Stores the result of <see cref="Prefab{TComponent}.Instantiate"/>.</summary>
    public struct Instantiated<TComponent> where TComponent : Component {

        public GameObject gameObject;
        public TComponent component;

        public Instantiated(GameObject gameObject) {
            this.gameObject = gameObject;
            component = gameObject.GetComponent<TComponent>();
        }

        public Instantiated(TComponent component) {
            gameObject = component.gameObject;
            this.component = component;
        }

        public static implicit operator Instantiated(Instantiated<TComponent> instantiated) => new Instantiated(instantiated.gameObject);
        public static implicit operator Instantiated<TComponent>(GameObject gameObject) => new Instantiated<TComponent>(gameObject);
        public static implicit operator TComponent(Instantiated<TComponent> instantiated) => instantiated.component;

    }

    /// <summary>Stores any <see cref="GameObject"/>, as if it is a prefab, with at least two defined <see cref="Component"/>s.</summary>
    /// <seealso cref="Instantiated{TComponent1, TComponent2}"/>
    /// <seealso cref="Prefab"/>
    /// <seealso cref="Prefab{TComponent1}"/>
    public struct Prefab<TComponent1, TComponent2> where TComponent1 : Component where TComponent2: Component {

        public GameObject gameObject;
        public TComponent1 component1;
        public TComponent2 component2;

        /// <summary>Creates a new <see cref="Prefab{TComponent1, TComponent2}"/> by using <see cref="GameObject.GetComponent"/>.</summary>
        public Prefab(GameObject gameObject) {
            this.gameObject = gameObject;
            gameObject.SetActive(false);
            component1 = gameObject.GetComponent<TComponent1>();
            component2 = gameObject.GetComponent<TComponent2>();
        }

        /// <summary>Creates a new <see cref="Prefab{TComponent1, TComponent2}"/> by using <see cref="Component.gameObject"/>.</summary>
        public Prefab(TComponent1 component1, TComponent2 component2) {
            this.component1 = component1;
            this.component2 = component2;
            gameObject = component1.gameObject;
            gameObject.SetActive(false);
        }

        /// <summary>Equivalent function to <see cref="UnityEngine.Object.Instantiate"/>.</summary>
        /// <returns>Returns <see cref="Instantiated{TComponent1, TComponent2}"/>.</returns>
        public Instantiated<TComponent1, TComponent2> Instantiate(bool active = true) {
            var instantiated = UnityEngine.Object.Instantiate(gameObject);
            instantiated.name = gameObject.name;
            instantiated.SetActive(active);
            return instantiated;
        }

        /// <summary>Equivalent function to <see cref="UnityEngine.Object.Instantiate"/>.</summary>
        /// <param name="parent">Sets <see cref="Transform.parent"/> of the new <see cref="GameObject"/>.</param>
        /// <returns>Returns <see cref="Instantiated{TComponent1, TComponent2}"/>.</returns>
        public Instantiated<TComponent1, TComponent2> Instantiate(Transform parent, bool active = true) {
            var instantiated = UnityEngine.Object.Instantiate(gameObject);
            instantiated.name = gameObject.name;
            instantiated.transform.parent = parent;
            instantiated.SetActive(active);
            return instantiated;
        }

        public static implicit operator Prefab(Prefab<TComponent1, TComponent2> prefab) => new Prefab(prefab.gameObject);
        public static implicit operator Prefab<TComponent1>(Prefab<TComponent1, TComponent2> prefab) => new Prefab<TComponent1>(prefab.component1);
        public static implicit operator Prefab<TComponent2>(Prefab<TComponent1, TComponent2> prefab) => new Prefab<TComponent2>(prefab.component2);
        public static implicit operator Prefab<TComponent1, TComponent2>(GameObject gameObject) => new Prefab<TComponent1, TComponent2>(gameObject);
        public static implicit operator GameObject(Prefab<TComponent1, TComponent2> prefab) => prefab.gameObject;
        public static implicit operator TComponent1(Prefab<TComponent1, TComponent2> prefab) => prefab.component1;
        public static implicit operator TComponent2(Prefab<TComponent1, TComponent2> prefab) => prefab.component2;

    }

    /// <summary>Stores the result of <see cref="Prefab{TComponent1, TComponent2}.Instantiate"/>.</summary>
    public struct Instantiated<TComponent1, TComponent2> where TComponent1 : Component where TComponent2 : Component {

        public GameObject gameObject;
        public TComponent1 component1;
        public TComponent2 component2;

        public Instantiated(GameObject gameObject) {
            this.gameObject = gameObject;
            component1 = gameObject.GetComponent<TComponent1>();
            component2 = gameObject.GetComponent<TComponent2>();
        }

        public Instantiated(TComponent1 component1, TComponent2 component2) {
            gameObject = component1.gameObject;
            this.component1 = component1;
            this.component2 = component2;
        }

        public static implicit operator Instantiated (Instantiated<TComponent1, TComponent2> instantiated) => new Instantiated(instantiated.gameObject);
        public static implicit operator Instantiated<TComponent1> (Instantiated<TComponent1, TComponent2> instantiated) => new Instantiated<TComponent1>(instantiated.component1);
        public static implicit operator Instantiated<TComponent1, TComponent2>(GameObject gameObject) => new Instantiated<TComponent1, TComponent2>(gameObject);
        public static implicit operator TComponent1(Instantiated<TComponent1, TComponent2> instantiated) => instantiated.component1;
        public static implicit operator TComponent2(Instantiated<TComponent1, TComponent2> instantiated) => instantiated.component2;

    }

    #endregion

    namespace CodeTools {

        namespace Math {


            /// <summary>A collection of math functions and objects related to Geometry.</summary>
            public static class Geometry {

                #region Tests
                /// <summary>
                /// On a 1-Dimentional line, the direction from X1 to X2 is equal to the sign of the difference: X2 - X1.
                /// </summary>
                /// <param name="input">X1 - X2</param>
                /// <returns>Returns the sign of the input (-1 or +1).</returns>
                public static int LinearDirection(float input) {
                    if(input < 0) return -1;
                    else return 1;
                }

                /// <summary>
                /// On a 1-Dimentional line, the direction from X1 to X2 is equal to the sign of the input.
                /// </summary>
                /// <param name="input">X1 - X2</param>
                /// <returns>Returns the sign of the input (-1 or +1).</returns>
                public static Direction LinearDirection(float input, Axis axis) {
                    if(input < 0) {
                        if(axis == Axis.Horizontal) return Direction.Down;
                        else return Direction.Left;
                    } else {
                        if(axis == Axis.Horizontal) return Direction.Up;
                        else return Direction.Right;
                    }
                }

                /// <summary>
                /// Gives which direction the vector is more facing on the given axis.
                /// </summary>
                /// <param name="vector">The vector to compare with the given axis.</param>
                /// <param name="axis">The axis to compare the given vector to.</param>
                /// <returns>Returns the sign of the input (-1 or +1).</returns>
                public static Direction LinearDirection(Vector2 vector, Axis axis) {
                    if(axis == Axis.Horizontal)
                        return LinearDirection(vector.x, axis);
                    else
                        return LinearDirection(vector.y, axis);
                }

                /// <summary>
                /// Gives the Geometry.Direction of an angle relative to the given Geometry.Axis.
                /// </summary>
                /// <param name="angle">The input angle in degrees.</param>
                /// <param name="axis">The axis to make the output direction relative to.</param>
                /// <returns>Axis.Horizontal relates to Quadrant 1 and 2 for positive. Axis.Vertical relates to Quadrant 1 and 4 for positive.</returns>
                public static Direction AngleDirection(Angle angle, Axis axis = Axis.Horizontal) {
                    float theta = angle.Degrees;
                    if(axis == Axis.Horizontal) {
                        if(theta == -90) return Direction.Right;
                        if(theta == 90) return Direction.Left;
                        if(-90 > angle && angle > 90) return Direction.Right;
                        return Direction.Left;
                    } else {
                        if(theta == 0) return Direction.Right;
                        if(theta == 180) return Direction.Left;
                        if(0 > angle && angle > 180) return Direction.Up;
                        else return Direction.Down;
                    }
                }

                /// <summary>
                /// Are these two lines parallel?
                /// </summary>
                /// <returns>Returns 'true' if (l1.a * l2.b) - (l2.a * l1.b) == 0.</returns>
                public static bool AreParallel(Line line1, Line line2) {
                    float delta = (line1.a * line2.b) - (line2.a * line1.b);
                    if(delta == 0) return true;
                    else return false;
                }

                /// <summary>
                /// Determines if the given value exists.
                /// </summary>
                /// <returns>Returns 'true' if the number is not Infinity and is not NaN.</returns>
                public static bool Exists(float number) { return (!float.IsInfinity(number)) && (!float.IsNaN(number)); }

                /// <summary>
                /// Determines if the given value exists.
                /// </summary>
                /// <returns>Returns 'true' if the components are not Infinity nor NaN.</returns>
                public static bool Exists(Vector2 vector) { return Exists(vector.x) && Exists(vector.y); }

                /// <summary>
                /// Checks if the input is on the interval [-range, +range].
                /// </summary>
                /// <param name="range">[-range, +range]</param>
                /// <returns>Returns true if input is on the interval [-range, +range]</returns>
                public static bool IsBetweenRange(float input, float range) {
                    return -range <= input && input <= range;
                }
                #endregion

                #region Conversions
                //todo: replace the 'while()' with math
                /// <summary>
                /// Normalizes an angle on the interval [0, 360].
                /// </summary>
                /// <param name="degr">Input angle in degrees.</param>
                /// <returns>Returns an equivalent angle on the interval [0, 360].</returns>
                public static float NormalizeDegree1(float degree) {
                    while(degree < 0) degree += 360;
                    while(degree > 360) degree -= 360;
                    return degree;
                }

                //todo: replace the 'while()' with math
                /// <summary>
                /// Normalizes an angle on the interval [-360, +360].
                /// </summary>
                /// <param name="degr">Input angle in degrees.</param>
                /// <returns>Returns an equivalent angle on the interval [-360, +360].</returns>
                public static float NormalizeDegree2(float degree) {
                    while(degree < -360) degree += 360;
                    while(degree > +360) degree -= 360;
                    return degree;
                }

                //todo: replace the 'while()' with math
                /// <summary>
                /// Normalizes an angle on the interval [-180, +180]
                /// </summary>
                /// <param name="degree">Input angle in degrees.</param>
                /// <returns>Returns an equivalent angle on the interval [-180, +180].</returns>
                public static float NormalizeDegree3(float degree) {
                    while(degree < -180) degree += 360;
                    while(degree > +180) degree -= 360;
                    return degree;
                }

                /// <summary>
                /// Rounds the given number to the nearest multiple of another number.
                /// </summary>
                /// <param name="number">Input number.</param>
                /// <param name="multiple">Input number will be rounded to some (multiple * n).</param>
                /// <returns>Returns the multiple closest to number.</returns>
                public static float RoundToMultiple(float number, float multiple) => Mathf.RoundToInt(number / multiple) * multiple;

                #endregion

                #region Finders
                /// <summary>
                /// Returns the angle between v1 and v2 with respect to the X/Y axies.
                /// </summary>
                /// <param name="v1">Vector 1 (angle from)</param>
                /// <param name="v2">Vector 2 (angle to)</param>
                public static Angle GetAngle(Vector2 v1, Vector2 v2) {
                    return Mathf.Atan2(v1.y - v2.y, v1.x - v2.x);
                }

                /// <summary>
                /// Generates a line given two points.
                /// </summary>
                /// <param name="p1">Point 1</param>
                /// <param name="p2">Point 2</param>
                /// <returns>Returns a line that intersects the two given points.</returns>
                public static Line LineFromTwoPoints(Vector2 point1, Vector2 point2) {
                    float a = (point1.y - point2.y) / (point1.x - point2.x);
                    float c = point1.y - (a * point1.x);
                    if(float.IsInfinity(a)) return new Line(-a, 1, c, point1.x);
                    else return new Line(-a, 1, c);
                }

                /// <summary>
                /// Generates a line given one point and an angle.
                /// </summary>
                /// <returns></returns>
                public static Line LineFromAngle(Vector2 point, Angle angle) {
                    float a = Mathf.Tan(angle.Radians);
                    float c = point.y - (a * point.x);
                    if(float.IsInfinity(a)) return new Line(-a, 1, c, point.x);
                    else return new Line(-a, 1, c);
                }

                /// <summary>
                /// Generates a line identical to the original, but shifted left/right by distance.
                /// </summary>
                /// <param name="distance"></param>
                /// <param name="line"></param>
                public static Line LineFromShift(Vector2 distance, Line line) {
                    var p1 = new Vector2(1, line.YFromX(1));
                    var p2 = new Vector2(2, line.YFromX(2));
                    p1 += distance; p2 += distance;
                    return LineFromTwoPoints(p1, p2);
                }

                /// <summary>
                /// Uses the Law of Cosines to find an angle of a triangle given only the side lenghs.
                /// </summary>
                /// <param name="a">Side length A.</param>
                /// <param name="b">Side length B.</param>
                /// <param name="c">Side length C.</param>
                /// <returns>Returns the angle opposite of side a.</returns>
                public static float LawOfCosForAngleA(float a, float b, float c) { return LawOfCosForAngleC(c, b, a); }

                /// <summary>
                /// Uses the Law of Cosines to find an angle of a triangle given only the side lenghs.
                /// </summary>
                /// <param name="a">Side length A.</param>
                /// <param name="b">Side length B.</param>
                /// <param name="c">Side length C.</param>
                /// <returns>Returns the angle opposite of side b.</returns>
                public static float LawOfCosForAngleB(float a, float b, float c) { return LawOfCosForAngleC(a, c, b); }

                /// <summary>
                /// Uses the Law of Cosines to find an angle of a triangle given only the side lenghs.
                /// </summary>
                /// <param name="a">Side length A.</param>
                /// <param name="b">Side length B.</param>
                /// <param name="c">Side length C.</param>
                /// <returns>Returns the angle opposite of side c.</returns>
                public static float LawOfCosForAngleC(float a, float b, float c) { return Mathf.Acos((Mathf.Pow(a, 2) + Mathf.Pow(b, 2) - Mathf.Pow(c, 2)) / (2 * a * b)) * Mathf.Rad2Deg; }

                /// <summary>
                /// Creates a new Vector2 with the given angle and magnitude.
                /// </summary>
                /// <param name="angle">The angle of the vector. Specify degrees or radians with 'units'.</param>
                /// <param name="magnitude">The magnitude of the vector.</param>
                /// <param name="units">Is the angle in degrees or radians?</param>
                /// <returns>Returns 'new Vector2(Cos(radians) * magnitude, Sin(radians) * magnitude)'</returns>
                public static Vector2 Vector2FromAngle(Angle angle, float magnitude) {
                    float theta = angle.Radians;
                    return new Vector2(
                        Mathf.Cos(theta) * magnitude,
                        Mathf.Sin(theta) * magnitude
                    );
                }

                /// <summary>
                /// Creates a new Vector2 with the given angle and a magnitude of 1.
                /// </summary>
                /// <param name="angle">The angle of the vector. Specify degrees or radians with 'units'.</param>
                /// <param name="units">Is the angle in degrees or radians?</param>
                /// <returns>Returns 'new Vector2(Cos(radians) * magnitude, Sin(radians) * magnitude)'</returns>
                public static Vector2 Vector2FromAngle(Angle angle) {
                    float theta = angle.Radians;
                    return new Vector2(
                        Mathf.Cos(theta),
                        Mathf.Sin(theta)
                    );
                }

                public static Vector2 VectorDirection(Direction direction) {
                    /**/
                    if(direction == Direction.Right) return Vector2.right;
                    else if(direction == Direction.Left) return Vector2.left;
                    else if(direction == Direction.Up) return Vector2.up;
                    else if(direction == Direction.Down) return Vector2.down;
                    else return Vector2.zero;
                }

                /// <summary>
                /// Finds the angle between v1 and v2 if v2's tail is placed on the head of v1.
                /// This angle can also be described as finding the angle of a bend in a line.
                /// </summary>
                /// <param name="v1">The first vector in the angle.</param>
                /// <param name="v2">The second vector in the angle.</param>
                public static Angle HeadToTailAngle(Vector2 v1, Vector2 v2) {
                    return Angle.radLeft - v1.Heading().value + v2.Heading().value;
                }

                /// <summary>
                /// Finds the shortest vector that goes from the given point to the given line.
                /// </summary>
                /// <param name="point">The point where to find the path from.</param>
                /// <param name="line">The line to find the path to.</param>
                public static Vector2 PathToLine(Vector2 point, Line line) {
                    Line towards = LineFromAngle(point, line.Angle().Mirror());
                    Vector2 intersection = Intersection(line, towards);
                    Vector2 direction = intersection - point;
                    return direction;
                }
                #endregion

                #region Setters
                /// <summary>
                /// Limits the input to [+/-] limit.
                /// </summary>
                /// <param name="input">The input value.</param>
                /// <param name="limit">The limiting value.</param>
                public static float LimitTo(float input, float limit) {
                    if(Mathf.Abs(input) > Mathf.Abs(limit)) input = limit * LinearDirection(input);
                    return input;
                }

                /// <summary>
                /// Changes the heading of v2 so that if v2's tail is placed on the head of v1, the angle between those two vectors is 'degrees'.
                /// This can also be described as setting the angle of a bend in a line.
                /// </summary>
                /// <param name="v1">Unchanged vector.</param>
                /// <param name="v2">Changed vector.</param>
                /// <param name="degrees">The angle in degrees.</param>
                /// <returns>Returns v2.SetHeading(180.0f - v1.Heading() - degrees).</returns>
                public static Vector2 HeadToTailAngle(Vector2 v1, Vector2 v2, Angle angle) {
                    return v2.SetHeading(Angle.radLeft - v1.Heading().value - angle.Radians);
                }
                #endregion

                #region Intersections
                /// <summary>
                /// Finds the intersection between two or more Geometry objects.
                /// </summary>
                /// <param name="l1">Line 1</param>
                /// <param name="l2">Line 2</param>
                /// <returns>Returns the intersection between l1 and l2.</returns>
                public static Vector2 Intersection(Line l1, Line l2) {
                    if(l1.isVertical != l2.isVertical) {
                        if(l1.isVertical) {
                            return new Vector2(l1.X, l2.YFromX(l1.X));
                        }
                        if(l2.isVertical) {
                            return new Vector2(l2.X, l1.YFromX(l2.X));
                        }
                    }
                    float delta = (l1.a * l2.b) - (l2.a * l1.b);
                    float x = (l2.b * l1.c - l1.b * l2.c) / delta;
                    float y = (l1.a * l2.c - l2.a * l1.c) / delta;
                    return new Vector2(x, y);
                }

                /// <summary>
                /// Finds the intersections between two or more Geometry objects.
                /// </summary>
                /// <param name="c1">Circle 1</param>
                /// <param name="c2">Circle 2</param>
                /// <returns>Returns [the closest] two intersections between the two circles.</returns>
                public static Vector2[] Intersection(Circle c1, Circle c2) {
                    float r1 = c1.radius, r2 = c2.radius;
                    float d = Vector2.Distance(c1.center, c2.center);
                    if(d > c1.radius + c2.radius) {
                        Vector2[] i = {   // only gets here if there is no real intersection
                Vector2.Lerp(c1.center, c2.center, c1.radius / d),
                Vector2.Lerp(c1.center, c2.center, c2.radius / d)
            };
                        return i;
                    }

                    // squared versions of the variables, because we use them a lot.
                    float d_2 = d * d, r1_2 = r1 * r1, r2_2 = r2 * r2;

                    float b = d_2 - r1_2 + r2_2;
                    float x = b / (2 * d);
                    float a = (1 / d) * Mathf.Sqrt((4 * d_2 * r2_2) - (b * b));
                    float y = (a / 2);

                    float angle = GetAngle(c1.center, c2.center);

                    Vector2[] intersections = new Vector2[2];
                    intersections[0] = new Vector2(x, +y).Rotate(angle) + c1.center;
                    intersections[1] = new Vector2(x, -y).Rotate(angle) + c1.center;

                    return intersections;
                }

                /// <summary>
                /// Finds the intersections between two or more Geometry objects.
                /// </summary>
                /// <param name="c1">Circle 1.</param>
                /// <param name="c2">Circle 2.</param>
                /// <param name="c3">Circle 3.</param>
                /// <returns>Returns [the closest] intersection shared by all three circles.</returns>
                public static Vector2 Intersection(Circle c1, Circle c2, Circle c3) {
                    var i1 = Intersection(c1, c2);
                    var i2 = Intersection(c1, c3);

                    int smallest = 0;
                    float[] D = new float[4];
                    D[0] = Vector2.Distance(i1[0], i2[0]);
                    D[1] = Vector2.Distance(i1[0], i2[1]);
                    D[2] = Vector2.Distance(i1[1], i2[0]);
                    D[3] = Vector2.Distance(i1[1], i2[1]);

                    for(int j = 1; j < 4; j++)
                        if(D[smallest] > D[j]) smallest = j;

                    return i2[smallest % 2]; //not 100% sure on this part, might be i1 instead?
                }
                #endregion

                #region Enumerators
                /// <summary>General direction in terms of left/right/up/down. More specifically, the normalized direction on either the X or Y axis.</summary>
                public enum Direction { Up, Down, Right, Left }

                /// <summary>Positive = 1, Negative = -1.</summary>
                public enum Sign { Negative = -1, Zero = 0, Positive = 1 }

                /// <summary>x : y : : horizontal : vertical</summary>
                public enum Axis { Horizontal, Vertical }

                /// <summary>Enumerator for radians or degrees.</summary>
                public enum AngleType { Radians, Degrees }
                #endregion

                #region Classes
                /// <summary>
                /// Ties together a value and a unit for an angle.
                /// </summary>
                public struct Angle {

                    /// <summary>The value of the angle with unknown units.</summary>
                    public float value;

                    /// <summary>The units of the angle.</summary>
                    public AngleType type;

                    /// <summary>
                    /// Creates a new angle
                    /// </summary>
                    /// <param name="value">The value of the angle in the units given.</param>
                    /// <param name="type">The units of the angle.</param>
                    public Angle(float value, AngleType type = AngleType.Radians) {
                        this.value = value;
                        this.type = type;
                    }

                    public Angle(Angle original) {
                        value = original.value;
                        type = original.type;
                    }

                    /// <summary>
                    /// Changes the units and applies the conversion.
                    /// </summary>
                    /// <param name="type">The new units of the angle.</param>
                    public Angle ConvertType(AngleType type) {
                        if(this.type != type) {
                            /**/
                            if(type == AngleType.Degrees)
                                value *= Mathf.Rad2Deg;
                            else if(type == AngleType.Radians)
                                value *= Mathf.Deg2Rad;
                            this.type = type;
                        }
                        return this;
                    }

                    /// <summary>Use this only if you don't know the units for sure. If you do, then use 'Angle.value'.</summary>
                    public float Radians =>
                        type == AngleType.Radians ? value
                        : value * Mathf.Deg2Rad;

                    /// <summary>Use this only if you don't know the units for sure. If you do, then use 'Angle.value'.</summary>
                    public float Degrees =>
                        type == AngleType.Degrees ? value
                        : value * Mathf.Rad2Deg;

                    public Angle Normalize() {
                        float circle = type == AngleType.Degrees ? degRight : radRight;
                        value %= circle;
                        return this;
                    }

                    public Angle Center() {
                        float circle = type == AngleType.Degrees ? degRight : radRight;
                        value %= circle;
                        float halfCircle = circle / 2f;
                        if(value > halfCircle) value -= circle;
                        if(value < halfCircle) value += circle;
                        return this;
                    }

                    public Angle Mirror() {
                        value += type == AngleType.Degrees ? degUp : radUp;
                        return this;
                    }

                    public Angle Abs() {
                        if(value < 0) return new Angle(-value, type);
                        else return new Angle(this);
                    }

                    /// <summary>Casts a float into a new Angle with units of radians.</summary>
                    public static implicit operator Angle(float value) { return new Angle(value, AngleType.Radians); }
                    /// <summary>Casts an angle into a float with 'Angle.radians'.</summary>
                    public static implicit operator float(Angle angle) { return angle.Radians; }
                    /// <summary>Casts an int into a new Angle with units of degrees.</summary>
                    public static implicit operator Angle(int value) { return new Angle(value, AngleType.Degrees); }
                    /// <summary>Casts an angle into an int with 'Angle.degrees'.</summary>
                    public static explicit operator int(Angle angle) { return (int)angle.Degrees; }

                    public static Angle operator +(Angle left, Angle right) { return new Angle(left.Radians + right.Radians, AngleType.Radians).ConvertType(left.type); }
                    public static Angle operator -(Angle left, Angle right) { return new Angle(left.Radians - right.Radians, AngleType.Radians).ConvertType(left.type); }
                    public static Angle operator *(Angle left, Angle right) { return new Angle(left.Radians * right.Radians, AngleType.Radians).ConvertType(left.type); }
                    public static Angle operator /(Angle left, Angle right) { return new Angle(left.Radians / right.Radians, AngleType.Radians).ConvertType(left.type); }
                    public static Angle operator %(Angle left, Angle right) { return new Angle(left.Radians % right.Radians, AngleType.Radians).ConvertType(left.type); }
                    public static bool operator >(Angle left, Angle right) { return left.Radians > right.Radians; }
                    public static bool operator <(Angle left, Angle right) { return left.Radians < right.Radians; }


                    public override string ToString() {
                        return value + (
                            type == AngleType.Degrees ?
                            " degrees" : " radians"
                        );
                    }


                    /// <summary>1/2 PI</summary>
                    public static float radUp = Mathf.PI / 2f;
                    /// <summary>1 PI</summary>
                    public static float radLeft = Mathf.PI;
                    /// <summary>2/3 PI</summary>
                    public static float radDown = 2f * Mathf.PI / 3f;
                    /// <summary>2 PI</summary>
                    public static float radRight = 2f * Mathf.PI;

                    public static float degUp = 90.0f;
                    public static float degLeft = 180.0f;
                    public static float degDown = 270.0f;
                    public static float degRight = 360.0f;

                    public static float RadDirection(Direction direction) {
                        switch(direction) {
                            case Direction.Up: return radUp;
                            case Direction.Left: return radLeft;
                            case Direction.Down: return radDown;
                            case Direction.Right: return radRight;
                            default: return 0f;
                        }
                    }

                    public static float DegDirection(Direction direction) {
                        switch(direction) {
                            case Direction.Up: return degUp;
                            case Direction.Left: return degLeft;
                            case Direction.Down: return degDown;
                            case Direction.Right: return degRight;
                            default: return 0f;
                        }
                    }
                }

                /// <summary>
                /// Stores data and methods for a line.
                /// </summary>
                public class Line {
                    /// <summary>ax + by = c;</summary>
                    public float a, b, c; //Ax + By = C

                    /// <summary>If the line is horizontal, the equation of the line is X = [a constant].</summary>
                    public float X = 0;

                    /// <summary>Is the line horizontal?</summary>
                    public bool isVertical;

                    /// <summary>
                    /// Creates a vertical line given A, B, C, and it's x-coordinate.
                    /// </summary>
                    /// <param name="A">Ax + by = c</param>
                    /// <param name="B">ax + By = c</param>
                    /// <param name="C">ax + by = C</param>
                    /// <param name="x">X = this (horizontal line)</param>
                    public Line(float A, float B, float C, float x) {
                        isVertical = true;
                        a = A;
                        b = B;
                        c = C;
                        X = x;
                    }

                    /// <summary>
                    /// Creates a line given A, B, and C in Ax + By = C
                    /// </summary>
                    /// <param name="A">Ax + by = c</param>
                    /// <param name="B">ax + By = c</param>
                    /// <param name="C">ax + by = C</param>
                    public Line(float A, float B, float C) {
                        isVertical = false;
                        a = A;
                        b = B;
                        c = C;
                    }

                    /// <summary>The slope of the line.</summary>
                    public float slope => -a;

                    /// <summary>Angle of the slope.</summary>
                    public Angle Angle(AngleType units = AngleType.Degrees) {
                        if(units == AngleType.Radians) return new Angle(Mathf.Atan(slope), units);
                        else return new Angle(Mathf.Atan(slope) * Mathf.Rad2Deg, units);
                    }

                    public float YFromX(float x) {
                        return (c - a * x) / b;
                    }
                    public float XFromY(float y) {
                        if(isVertical) return X;
                        else return (c - b * y) / a;
                    }

                    /// <summary>
                    /// Shifts the line left/right by a distance of 'd'.
                    /// </summary>
                    /// <param name="d">distance to shift the line</param>
                    public void ShiftX(float d) {
                        if(isVertical) X += d;
                        else c += d * a;
                    }

                    /// <summary>
                    /// Shifts the line upwards/downwards by a distance of 'd'.
                    /// </summary>
                    /// <param name="d">distance to shift the line.</param>
                    public void ShiftY(float d) {
                        if(!isVertical) c += d * b;
                    }

                    /// <summary>Returns (x, y) given x.</summary>
                    /// <param name="x">the x-coordinate of the point</param>
                    public Vector2 PointFromX(float x) {
                        return new Vector2(x, YFromX(x));
                    }

                    /// <summary>Returns (x, y) given y.</summary>
                    /// <param name="y">the y-coordinate of the point</param>
                    public Vector2 PointFromY(float y) {
                        return new Vector2(XFromY(y), y);
                    }

                    /// <summary>
                    /// Finds a point on this line from a start point, distance, and direction.
                    /// </summary>
                    /// <param name="point">distance from this point</param>
                    /// <param name="distance">distance from point 'point'</param>
                    /// <param name="direction">direction on the line (does not need to align with the lign)</param>
                    /// <returns></returns>
                    public Vector2 PointFromDistance(Vector2 point, float distance, Vector2 direction) {
                        return PointFromDistance(point, distance, Geometry.LinearDirection(direction.x - point.x));
                    }

                    /// <summary>
                    /// Finds a point on this line from a start point, distance, and direction.
                    /// </summary>
                    /// <param name="point">Distance from this point.</param>
                    /// <param name="distance">Distance from point 'point'.</param>
                    /// <param name="direction">Left = -1, Right = +1.</param>
                    /// <returns></returns>
                    public Vector2 PointFromDistance(Vector2 point, float distance, int direction) {
                        float theta = Angle(AngleType.Radians) * direction;
                        var point2 = new Vector2(Mathf.Cos(theta) * distance, Mathf.Sin(theta) * distance);
                        point2 = new Vector2(point2.x * direction, point2.y);
                        point2 += point;
                        return point2;
                    }

                    /// <summary>
                    /// Finds the intersection of this and l2.
                    /// </summary>
                    /// <returns>Returns the point of intersection.</returns>
                    public Vector2 Intersection(Line l2) {
                        Line l1 = this;
                        if(l1.isVertical != l2.isVertical) {
                            if(l1.isVertical) {
                                return new Vector2(l1.X, l2.YFromX(l1.X));
                            }
                            if(l2.isVertical) {
                                return new Vector2(l2.X, l1.YFromX(l2.X));
                            }
                        }
                        float delta = (l1.a * l2.b) - (l2.a * l1.b);
                        float x = (l2.b * l1.c - l1.b * l2.c) / delta;
                        float y = (l1.a * l2.c - l2.a * l1.c) / delta;
                        return new Vector2(x, y);
                    }
                }

                /// <summary>
                /// Stores data and methods for a triangle.
                /// </summary>
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
                        A = Geometry.LawOfCosForAngleA(a, b, c);
                        B = Geometry.LawOfCosForAngleB(a, b, c);
                        C = Geometry.LawOfCosForAngleC(a, b, c);
                    }

                    public void SolveForAngles() {
                        A = Geometry.LawOfCosForAngleA(a, b, c);
                        B = Geometry.LawOfCosForAngleB(a, b, c);
                        C = Geometry.LawOfCosForAngleC(a, b, c);
                    }

                    public override string ToString() {
                        return base.ToString();
                    }
                }

                /// <summary>
                /// Stores data and methods for a box with a position.
                /// </summary>
                [Serializable]
                public struct Box {
                    public Range x, y;

                    public Vector2 Center => new Vector2(x.Center, y.Center);

                    /// <summary>Clockwise, starting from bottom-left.</summary>
                    public Vector2[] Points => new Vector2[] {
            new Vector2(x.min, y.min),
            new Vector2(x.min, y.max),
            new Vector2(x.max, y.max),
            new Vector2(x.max, y.min)
        };

                    /// <summary>
                    /// Creates a new box given two corners (any order).
                    /// </summary>
                    /// <param name="corner1">A corner of the box.</param>
                    /// <param name="corner2">A corner of the box.</param>
                    public Box(Vector2 corner1, Vector2 corner2) {
                        x = new Range(
                            corner1.x < corner2.x ? corner1.x : corner2.x,
                            corner1.x > corner2.x ? corner1.x : corner2.x
                        );
                        y = new Range(
                            corner1.y < corner2.y ? corner1.y : corner2.y,
                            corner1.y > corner2.y ? corner1.y : corner2.y
                        );
                    }

                    public Box(Range x, Range y) {
                        this.x = x;
                        this.y = y;
                    }

                    /// <summary>
                    /// Checks if the given point lies within the box.
                    /// </summary>
                    /// <param name="point">Point to test.</param>
                    /// <returns>Returns true if the point lies within the box.</returns>
                    public bool Contains(Vector2 point) {
                        return x.Contains(point.x) && y.Contains(point.y);
                    }

                    public bool Contains(Box box) {
                        return x.Contains(box.x) && y.Contains(box.y);
                    }

                    public bool Overlaps(Box box) {
                        return x.Overlaps(box.x) && y.Overlaps(box.y);
                    }

                    /// <summary>
                    /// If nessesary, shrinks the given box to fit within this box. The center is kept the same.
                    /// </summary>
                    /// <param name="box">Box to place inside.</param>
                    /// <returns>Returns a new box that fits inside the original.</returns>
                    public Box Place(Box box) {
                        float deltaLt = box.x.min - x.min;
                        float deltaRt = x.max - box.x.max;
                        float deltaDn = box.y.min - y.min;
                        float deltaUp = y.max - box.y.max;

                        float deltaHz = deltaLt > deltaRt ? deltaLt : deltaRt;
                        float deltaVt = deltaDn > deltaUp ? deltaUp : deltaDn;

                        box.x.ChangeByAmount(deltaHz);
                        box.y.ChangeByAmount(deltaVt);

                        return box;
                    }

                    /// <summary>
                    /// Gives two boxes that are the original box split at the given y value.
                    /// </summary>
                    /// <param name="value">Y value to split the box at.</param>
                    /// <returns>Returns an array of two boxes.</returns>
                    public Box[] SplitAtY(float value) {
                        return new Box[] {
                new Box( //Top
                    new Vector2(x.min, value),
                    new Vector2(x.max, y.max)
                ),
                new Box( //Bottom
                    new Vector2(x.min, y.min),
                    new Vector2(x.max, value)
                )
            };
                    }

                    /// <summary>
                    /// Gives two boxes that are the original box split at the given x value.
                    /// </summary>
                    /// <param name="value">X value to split the box at.</param>
                    /// <returns>Returns an array of two boxes.</returns>
                    public Box[] SplitAtX(float value) {
                        return new Box[] {
                new Box( //Left
                    new Vector2(x.min, y.min),
                    new Vector2(value, y.max)
                ),
                new Box( //Right
                    new Vector2(value, y.min),
                    new Vector2(x.max, y.max)
                )
            };
                    }

                    #region Operators
                    public static bool operator ==(Box left, Box right) { return left.x == right.x && left.y == right.y; }
                    public static bool operator !=(Box left, Box right) { return left.x != right.x && left.y != right.y; }

                    #region misc.
                    public override bool Equals(object obj) {
                        return base.Equals(obj);
                    }

                    public override int GetHashCode() {
                        return base.GetHashCode();
                    }

                    public override string ToString() {
                        return base.ToString();
                    }
                    #endregion
                    #endregion
                }

                /// <summary>
                /// Stores data and methods for a circle.
                /// </summary>
                public struct Circle {
                    public Vector2 center;
                    public float radius;
                    public Circle(Vector2 center, float radius) {
                        this.center = center;
                        this.radius = radius;
                    }
                }

                /// <summary>
                /// Defines an area from boxes.
                /// </summary>
                [Serializable]
                public class Area {
                    public Box[] boxes;

                    public Area(Box[] boxes) {
                        this.boxes = boxes;
                    }

                    public Box Place(Box box) {
                        foreach(Box limit in boxes)
                            if(limit.Contains(box.Center))
                                box = limit.Place(box);
                        return box;
                    }

                    public bool Contains(Box box) {
                        foreach(Box limit in boxes) {
                            if(limit.Contains(box.Center) && limit.Contains(box))
                                return true;
                        }
                        return false;
                    }
                }

                #endregion

            }


            #region Ranges
            /// <summary>
            /// Class for an inclusive range.
            /// </summary>
            [Serializable]
            public struct Range {
                /// <summary>Unlimited range.</summary>
                public static Range infinite = new Range(Mathf.NegativeInfinity, Mathf.Infinity);

                /// <summary>Size of 1: (-0.5, +0.5)</summary>
                public static Range half = new Range(-0.5f, 0.5f);

                public float Size => max - min;
                public float Center => min + (Size / 2);
                public float Random => UnityEngine.Random.Range(min, max);
                public float RandomSpread => (UnityEngine.Random.value * UnityEngine.Random.Range(-1.0f, +1.0f) / 2 + 0.5f) * (max - min) + min;

                public float min, max;
                public Range(float minimum = Mathf.NegativeInfinity, float maximum = Mathf.Infinity) {
                    min = minimum;
                    max = maximum;
                }

                /// <summary>
                /// Adds twice the given value to the size.
                /// </summary>
                /// <param name="value">The value to add to the size.</param>
                /// <returns>Returns a new range with the new size and the same center.</returns>
                public Range ChangeByAmount(float value) {
                    value /= 2;
                    return new Range(min - value, max + value);
                }

                /// <summary>
                /// Multiplies the size by the given value.
                /// </summary>
                /// <param name="value">The value to multiply the size by.</param>
                /// <returns>Returns a new range with the new size and same center.</returns>
                public Range ChangeByFactor(float value) {
                    return ChangeByAmount(Size * value);
                }

                /// <summary>
                /// Checks if the given value is within the range.
                /// </summary>
                /// <returns>Returns 'true' if the given value is on the interval [min, max].</returns>
                public bool Contains(float value) {
                    return min <= value && value <= max;
                }

                /// <summary>
                /// Finds the value's distance to the closest edge of the range.
                /// </summary>
                /// <param name="value"></param>
                /// <returns></returns>
                public float EdgeDistance(float value) {
                    float deltaMin = Mathf.Abs(value - min);
                    float deltaMax = Mathf.Abs(value - max);
                    return deltaMin < deltaMax ? deltaMin : deltaMax;
                }

                /// <summary>
                /// Checks if the given range is completely encompassed by this range.
                /// </summary>
                /// <param name="range"></param>
                /// <returns></returns>
                public bool Contains(Range range) {
                    return EdgeDistance(range.Center) >= range.Size / 2;
                }

                public bool Overlaps(Range range) {
                    return range.min <= max && range.max >= min;
                }

                /// <summary>
                /// Places the given value to the nearest value on the interval [min, max].
                /// </summary>
                /// <returns>If the given value is lesser/greater than min/max then it returns min/max. Otherwise, the value is unchanged.</returns>
                public float Place(float value) {
                    if(min > value) value = min;
                    if(max < value) value = max;
                    return value;
                }

                /// <summary>
                /// Places the given value to either the minimum or maximum: whichever is closer.
                /// </summary>
                /// <returns>Returns the closest min/max to the given value.</returns>
                public float PlaceOutside(float value) {
                    if(Contains(value)) return value;
                    if(Mathf.Abs(min - value) > Mathf.Abs(max - value))
                        return min;
                    return max;
                }

                public override string ToString() {
                    return "[" + min + ", " + max + "]";
                }

                #region Operators
                public static Range operator +(Range range, float value) { return new Range(range.min + value, range.max + value); }
                public static Range operator -(Range range, float value) { return new Range(range.min - value, range.max - value); }
                public static Range operator *(Range range, float value) { return new Range(range.min * value, range.max * value); }
                public static Range operator /(Range range, float value) { return new Range(range.min / value, range.max / value); }
                public static Range operator %(Range range, float value) { return new Range(range.min % value, range.max % value); }
                public static Range operator +(Range left, Range right) { return new Range(left.min + right.min, left.max + right.max); }
                public static Range operator -(Range left, Range right) { return new Range(left.min - right.min, left.max - right.max); }
                public static Range operator *(Range left, Range right) { return new Range(left.min * right.min, left.max * right.max); }
                public static Range operator /(Range left, Range right) { return new Range(left.min / right.min, left.max / right.max); }
                public static Range operator %(Range left, Range right) { return new Range(left.min % right.min, left.max % right.max); }
                public static bool operator ==(Range left, Range right) { return left.min == right.min && left.max == right.max; }
                public static bool operator !=(Range left, Range right) { return left.min != right.min && left.max != right.max; }

                public static explicit operator Vector2(Range range) { return new Vector2(range.min, range.max); }

                #region Stuff the compiler generated and will complain if it's not here.
                public override bool Equals(object obj) {
                    if(!(obj is Range)) {
                        return false;
                    }

                    var range = (Range)obj;
                    return min == range.min &&
                           max == range.max;
                }
                public override int GetHashCode() {
                    var hashCode = -897720056;
                    hashCode = hashCode * -1521134295 + min.GetHashCode();
                    hashCode = hashCode * -1521134295 + max.GetHashCode();
                    return hashCode;
                }
                #endregion
                #endregion
            }

            /// <summary>
            /// Class for an inclusive Vector2 range for x and y components.
            /// </summary>
            [Serializable]
            public struct Range2D {
                /// <summary>Unlimited range.</summary>
                public static Range2D infinite = new Range2D(Range.infinite, Range.infinite);

                /// <summary>Area of 1.</summary>
                public static Range2D half = new Range2D(Range.half, Range.half);

                /// <summary>The range of this vector component.</summary>
                public Range x, y;

                /// <summary>
                /// Creates a new Range2D.
                /// </summary>
                /// <param name="x">Range for the x component.</param>
                /// <param name="y">Range for the y component.</param>
                public Range2D(Range x, Range y) {
                    this.x = x;
                    this.y = y;
                }

                /// <summary>
                /// Creates a new Range2D.
                /// </summary>
                public Range2D(float x_min, float x_max, float y_min, float y_max) {
                    x = new Range(x_min, x_max);
                    y = new Range(y_min, y_max);
                }

                /// <summary>
                /// Checks if the given value is within the range.
                /// </summary>
                /// <returns>Returns 'true' if the given value is on the interval [min, max].</returns>
                public bool Contains(Vector2 value) {
                    return x.Contains(value.x) && y.Contains(value.y);
                }

                /// <summary>
                /// Applies 'Range.Place(float)' to both components of the given vector to their respective range.
                /// </summary>
                /// <returns>Returns a new Vector2(x.Place(value.x), y.Place(value.y))</returns>
                public Vector2 Place(Vector2 value) {
                    return new Vector2(
                        x.Place(value.x),
                        y.Place(value.y)
                    );
                }

                public Vector2 PlaceOutside(Vector2 value) {
                    return new Vector2(
                        x.PlaceOutside(value.x),
                        y.PlaceOutside(value.y)
                    );
                }

                public Vector2 Random() {
                    return new Vector2(x.Random, y.Random);
                }

                public Vector2 RandomSpread() {
                    return new Vector2(x.RandomSpread, y.RandomSpread);
                }

                public override string ToString() {
                    return "[" + x + ", " + y + "]";
                }
            }

            #endregion


            #region Extentions

            public static class Extentions {
                #region Vector2
                /// <summary>
                /// The heading of the Vector. Default units of the angle are radians.
                /// </summary>
                /// <returns>Returns the degrees of this vector from Vector2.right.</returns>
                public static Angle Heading(this Vector2 v) { return Mathf.Atan2(v.y, v.x); }

                /// <summary>
                /// Sets the heading of the vector.
                /// </summary>
                /// <param name="v">Original vector.</param>
                /// <param name="angle">Amount of rotation in degrees.</param>
                /// <returns>Returns a new vector is a heading of v.Heading + degrees and magnitude of the original.</returns>
                public static Vector2 SetHeading(this Vector2 v, Angle angle) { return new Vector2(v.magnitude, 0).Rotate(angle); }

                /// <summary>
                /// Sets the magnitude of the vector.
                /// </summary>
                /// <param name="v">Original vector.</param>
                /// <param name="magnitude">New magnitude of the vector.</param>
                /// <returns>Returns a new vector with the heading of the original but with the new magnitude.</returns>
                public static Vector2 SetMagnitude(this Vector2 v, float magnitude) { return new Vector2(magnitude, 0).Rotate(v.Heading()); }

                /// <summary>
                /// Rotates a vector by an angle.
                /// </summary>
                /// <param name="vector">Vector to rotate.</param>
                /// <param name="angle">The rotation angle.</param>
                /// <returns>Returns a vector of the same magnitude, but rotated.</returns>
                public static Vector2 Rotate(this Vector2 vector, Angle angle) {
                    float theta = angle.Radians;
                    float sin = Mathf.Sin(theta), cos = Mathf.Cos(theta);
                    float x = vector.x, y = vector.y;
                    vector.x = (cos * x) - (sin * y);
                    vector.y = (sin * x) + (cos * y);
                    return vector;
                }

                /// <summary>
                /// Rotates v1 towards v2.
                /// </summary>
                /// <param name="v1">Original vector.</param>
                /// <param name="v2">Vector to.</param>
                /// <param name="angle">Amount of rotation.</param>
                /// <returns>Returns v1.Rotate([+/-]degrees).</returns>
                public static Vector2 RotateTo(this Vector2 v1, Vector2 v2, Angle angle) {
                    float theta = angle.Radians;
                    if(Vector2.SignedAngle(v1, v2) > 0) return Rotate(v1, theta);
                    else return Rotate(v1, -theta);
                }

                /// <summary>
                /// Rotates v1 away from v2.
                /// </summary>
                /// <param name="v1">Original vector.</param>
                /// <param name="v2">Vector from.</param>
                /// <param name="theta">Amount of rotation.</param>
                /// <returns>Returns v1.Rotate([-/+]degrees).</returns>
                public static Vector2 RotateFrom(this Vector2 v1, Vector2 v2, Angle angle) {
                    float theta = angle.Radians;
                    if(Vector2.SignedAngle(v1, v2) < 0) return Rotate(v1, theta);
                    else return Rotate(v1, -theta);
                }

                /// <summary>
                /// Sets the heading of v1 equal to the heading of v2, then rotates it.
                /// </summary>
                /// <param name="v1">Original vector.</param>
                /// <param name="v2">Vector from.</param>
                /// <param name="angle">Change in angle from v2.</param>
                /// <returns>Returns a new vector with the heading of (v2.heading + degrees) and the magnitude of v1.</returns>
                public static Vector2 FromRotation(this Vector2 v1, Vector2 v2, Angle angle) {
                    float theta = angle.Radians;
                    float rotation = Vector2.SignedAngle(Vector2.right, v2);
                    return v1.SetHeading(theta + rotation);
                }
                #endregion
            }

            #endregion


        }

        namespace Enumerators {


            public class ArrayPointsEnumerator : IEnumerable<Vector3Int>, IEnumerator<Vector3Int> {

                Vector3Int max;
                public ArrayPointsEnumerator(Vector3Int size) {
                    max = size - Vector3Int.one;
                }

                static Vector3Int point_default = new Vector3Int(0, 0, -1);
                Vector3Int point = point_default;
                public Vector3Int Current => point;
                object IEnumerator.Current => point;

                public IEnumerator<Vector3Int> GetEnumerator() => this;
                IEnumerator IEnumerable.GetEnumerator() => this;

                public bool MoveNext() {

                    // (same functionality as:) // // // // //
                    // for(int x = 0; x < max.x + 1; x++)   //
                    //  for(int y = 0; y < max.y + 1; y++)  //
                    //   for(int z = 0; z < max.z + 1; z++) //

                    if(point.z < max.z) point.z++;
                    else {
                        point.z = 0;
                        if(point.y < max.y) point.y++;
                        else {
                            point.y = 0;
                            if(point.x < max.x) point.x++;
                            else return false;
                        }
                    }

                    return true;

                }

                public void Reset() => point = point_default;

                public void Dispose() {
                    // Nothing to dispose of.
                }
            }

            public class PropertyEnumerable<T> : IEnumerable<T> {

                public readonly T[] properties;
                public PropertyEnumerable(params T[] properties) {
                    this.properties = properties;
                }

                public IEnumerator<T> GetEnumerator() => ((IEnumerable<T>)properties).GetEnumerator();

                IEnumerator IEnumerable.GetEnumerator() => properties.GetEnumerator();

            }

        }

    }

    namespace Content {

        public class ContentLoader {

            #region Directories

            const string CONTENT_FOLDER = "Content";
            private readonly Func<Type, string, string>[] recognizers;
            private Assembly[] assemblies;
            internal ContentLoader() {
                behaviours = new BehaviourContent();
                m_blockPreviews = new List<Content<BlockPreview>>();
                m_attachables = new List<Content<AttachedBehaviour>>();
                recognizers = new Func<Type, string, string>[] {

                    // Behaviours //
                    (type, origin) => {
                        if(behaviours.Add(type, origin))
                            return "ClientBehaviour";
                        return "";
                    },

                    // Blocks //
                    (type, origin) => {
                        if(type.IsSubclassOf(typeof(BlockPreview))) {
                            m_blockPreviews.Add(new Content<BlockPreview>(type, origin));
                            return "BlockPreview";
                        } return "";
                    },

                    // Entities //
                    (type, origin) => {
                        if(type.IsSubclassOf(typeof(Entity))) {
                            m_entities.Add(new Content<Entity>(type, origin));
                            return "Entity";
                        } return "";
                    },

                    // Attachables //
                    (type, origin) => {
                        if(type.IsSubclassOf(typeof(AttachedBehaviour))) {
                            m_attachables.Add(new Content<AttachedBehaviour>(type, origin));
                            return "AttachedBehaviour";
                        } return "";
                    },

                    // MonoBehaviour //
                    // - always last
                    (type, origin) => {
                        if(type.IsSubclassOf(typeof(MonoBehaviour))) {
                            m_monoBehaviours.Add(new Content<MonoBehaviour>(type, origin));
                            return "MonoBehaviour";
                        } return "";
                    }

                };
            }

            /// <summary>Finds all mod directories and then 'processes' them.</summary>
            /// <seealso cref="ProcessDirectory(string)"/>
            internal void FindAndProcessAll(out string[] directories) {
                directories = Directory.GetDirectories(CONTENT_FOLDER);
                var dirCount = directories.Length;
                var assemblies = new List<Assembly>();
                for(int i = 0; i < dirCount; i++) {
                    ProcessDirectory(directories[i], out var assembly);
                    if(assembly != null) assemblies.Add(assembly);
                }
                this.assemblies = assemblies.ToArray();
            }

            internal void UnloadAll() {

                foreach(Assembly assembly in assemblies) {
                    // ?
                }

            }

            /// <summary>In this context, a directory is a set of content/mod.</summary>
            /// <param name="directory">The path to the directory.</param>
            /// <seealso cref="FindAndProcessAll()"/>
            internal void ProcessDirectory(string directory, out Assembly assembly) {

                Debug.LogFormat(" /* Processing directory '{0}'.", directory);

                string file = directory + "\\Assembly.dll";

                if(File.Exists(file)) {

                    assembly = Assembly.LoadFrom(file);
                    Debug.LogFormat("Assembly '{0}' at '{1}' was found for this directory.", assembly.FullName, file);

                    if(AssemblyIsLegal(in assembly, out string why)) {
                        var recognizedTypes = 0; var totalTypes = 0;

                        try {

                            Type[] types = assembly.GetTypes();
                            string origin = directory;
                            foreach(Type type in types) {
                                bool unrecognized = true; totalTypes++;
                                foreach(Func<Type, string, string> recognizer in recognizers) {
                                    string tName = recognizer(type, origin);
                                    if(tName != "") {
                                        Debug.LogFormat("Found type '{0}' from '{1}' and recognized it as '{2}'.", type.FullName, file, tName);
                                        unrecognized = false; recognizedTypes++; break;
                                    }
                                }
                                if(unrecognized) Debug.LogFormat("Found type '{0}' from '{1}', but did not recognize it.", type.FullName, file);
                            }

                        } catch(Exception e) {
                            Debug.LogErrorFormat("Assembly was returned as legal, but was unable to be loaded because an error occured." +
                                " Given exception: \"{0}\".", e);
                        }

                        Debug.LogFormat("{0}: Found {1} {2}, and {3} of them {4} recognized.",
                            recognizedTypes == 0 ? "Warning" : "Results", totalTypes, totalTypes > 1 ? "total types" : "type",
                            recognizedTypes == 0 ? "none" : recognizedTypes.ToString(), recognizedTypes == 1 ? "was" : "were");

                    } else Debug.LogErrorFormat("Assembly '{0}' was returned as illegal." +
                        " Given reason: '{1}'.", file, why);

                } else {
                    Debug.LogWarningFormat("Could not find '{0}'.", file);
                    assembly = null;
                }

                Debug.LogFormat("Finished processing directory '{0}'. */", directory);

            }

            internal bool AssemblyIsLegal(in Assembly assembly, out string why) {
                why = "Not illegal.";
                return true;
            }

            #region Behaviours
            public readonly BehaviourContent behaviours;
            public class BehaviourContent {

                #region Lists

                /// <summary>The loaded <see cref="GlobalBehaviour"/>s.</summary>
                internal readonly List<Content<GlobalBehaviour>> global = new List<Content<GlobalBehaviour>>();
                /// <summary>Returns a new array of <see cref="GlobalBehaviour"/> <see cref="Content{T}"/>.</summary>
                public Content<GlobalBehaviour>[] Global => global.ToArray();

                /// <summary>The loaded <see cref="ClientBehaviour"/>s.</summary>
                internal readonly List<Content<ClientBehaviour>> client = new List<Content<ClientBehaviour>>();
                /// <summary>Returns a new array of <see cref="ClientBehaviour"/> <see cref="Content{T}"/>.</summary>
                public Content<ClientBehaviour>[] Client => client.ToArray();

                /// <summary>The loaded <see cref="ServerBehaviour"/>s.</summary>
                internal readonly List<Content<ServerBehaviour>> server = new List<Content<ServerBehaviour>>();
                /// <summary>Returns a new array of <see cref="ServerBehaviour"/> <see cref="Content{T}"/>.</summary>
                public Content<ServerBehaviour>[] Server => server.ToArray();

                #endregion

                public bool Add(Type type, string directory) {

                    if(type.IsSubclassOf(typeof(GlobalBehaviour))) {
                        global.Add(new Content<GlobalBehaviour>(type, directory));
                        return true;
                    }
                    if(type.IsSubclassOf(typeof(ClientBehaviour))) {
                        client.Add(new Content<ClientBehaviour>(type, directory));
                        return true;
                    }
                    if(type.IsSubclassOf(typeof(ServerBehaviour))) {
                        server.Add(new Content<ServerBehaviour>(type, directory));
                        return true;
                    }

                    return false;

                }

                /// <summary>Gets the list of type <see cref="DetachedBehaviour"/> passed.</summary>
                /// <typeparam name="T"><see cref="DetachedBehaviour"/> type, such as
                /// <see cref="GlobalBehaviour"/>, <see cref="ClientBehaviour"/>, or <see cref="ServerBehaviour"/>.</typeparam>
                public List<Content<T>> Get<T>() where T : DetachedBehaviour {

                    if(typeof(T) == typeof(GlobalBehaviour))
                        return global as List<Content<T>>;
                    if(typeof(T) == typeof(ClientBehaviour))
                        return client as List<Content<T>>;
                    if(typeof(T) == typeof(ServerBehaviour))
                        return server as List<Content<T>>;

                    return null;

                }

            }
            #endregion

            //Blocks
            internal readonly List<Content<BlockPreview>> m_blockPreviews;
            public IEnumerator<Content<BlockPreview>> EnumBlockPreviews => m_blockPreviews.GetEnumerator();
            public Content<BlockPreview>[] BlockPreviews => m_blockPreviews.ToArray();
            //Entities
            internal readonly List<Content<Entity>> m_entities = new List<Content<Entity>>();
            public IEnumerator<Content<Entity>> EnumEntities => m_entities.GetEnumerator();
            public Content<Entity>[] Entities => m_entities.ToArray();
            //Attachables
            internal readonly List<Content<AttachedBehaviour>> m_attachables;
            public IEnumerator<Content<AttachedBehaviour>> EnumAttachedBehaviours => m_attachables.GetEnumerator();
            public Content<AttachedBehaviour>[] AttachedBehaviours => m_attachables.ToArray();
            //MonoBehaviours
            internal readonly List<Content<MonoBehaviour>> m_monoBehaviours = new List<Content<MonoBehaviour>>();
            public IEnumerator<Content<MonoBehaviour>> EnumMonoBehaviours => m_monoBehaviours.GetEnumerator();
            public Content<MonoBehaviour>[] MonoBehaviours => m_monoBehaviours.ToArray();

            #endregion

            #region Static Use

            /// <summary>Uses <see cref="ObjImporter"/> to get the mesh from the '*.obj' at the given path.</summary>
            /// <param name="file">The path to the file.</param>
            public static Mesh GetObjFile(string file) {
                var importer = new ObjImporter();
                var obj = importer.ImportFile(file);
                obj.name = file;
                return obj;
            }

            /// <summary>Uses <see cref="File.ReadAllBytes(string)"/> to get the image from the '*.png' at the given path.</summary>
            /// <param name="file">The path to the file.</param>
            public static Texture2D GetPngFile(string file) {
                var png = new Texture2D(0, 0);
                png.LoadImage(File.ReadAllBytes(file));
                png.name = file;
                return png;
            }

            public static T GetResource<T>(string path) where T : UnityEngine.Object {
                return Resources.Load<T>(path);
            }

            #endregion

        }

        #region Content struct

        /// <summary>Stores a <see cref="Type"/> variable along with the name of the directory it came from.</summary>
        /// <typeparam name="T">The base type for this content. <see cref="ClientBehaviour"/>, <see cref="BlockPreview"/>, etc.</typeparam>
        /// <remarks>Implicitly casts to <see cref="Type"/>.
        /// <para/>Explicitly casts to <see cref="T"/>.</remarks>
        public struct Content<T> {
            /// <summary>The <see cref="Type"/> found.</summary>
            public Type type;
            /// <summary>The given name of the <see cref="Assembly"/> this <see cref="Type"/> came from.</summary>
            public string origin;
            /// <summary>Creates a new <see cref="Content"/> with the given properties.</summary>
            public Content(Type type, string origin) {
                this.type = type;
                this.origin = origin;
            }

            /// <summary>Returns the <see cref="Content{Base}.type"/>.</summary>
            /// <param name="content">The <see cref="Content{Base}"/> to look at.</param>
            public static implicit operator Type(Content<T> content) => content.type;

            /// <summary>Creates a new object of type <see cref="T"/>.</summary>
            public T New() {
                ConstructorInfo constructor = type.GetConstructor(Type.EmptyTypes);
                return (T)constructor.Invoke(null);
            }

            /// <summary>Creates a new object of the given type.</summary>
            public U New<U>() where U: T {
                ConstructorInfo constructor = type.GetConstructor(Type.EmptyTypes);
                return (U)constructor.Invoke(null);
            }

        }

        #endregion

    }

    namespace Interfaces {

        namespace Manager {

            /// <summary>An interface for classes that manage <see cref="IManageable{T}"/>.</summary>
            /// <typeparam name="TManaged">The common base class between all similar
            /// <see cref="IManaged{T}"/>s that this <see cref="IManager"/> manages.</typeparam>
            public interface IManager<TManaged> where TManaged : class {

                /// <summary>Tells if all <see cref="IManageable"/>s this <see cref="IManager"/> manages have run <see cref="IManaged.OnSetup()"/>.</summary>
                bool AllSetup { get; }
                /// <summary>Returns all <see cref="IManageable"/>s this <see cref="IManager"/> manages.</summary>
                IEnumerator<TManaged> Managed { get; }

            }

            /// <summary>A <see cref="IManager{TManaged}"/> where the manager adds, but the managed remove.</summary>
            /// <typeparam name="TManaged">The common base class between all similar
            /// <see cref="IManaged{T}"/>s that this <see cref="IManager"/> manages.</typeparam>
            public interface IDynamicManager<TManaged> : IManager<TManaged> where TManaged : class {
                void RemoveMe(TManaged me);
            }

            /// <summary>An interface for classes that require a manager.</summary>
            /// <typeparam name="TManager">The class of this <see cref="IManaged{T, U}"/>'s <see cref="IManager{T, U}"/>.</typeparam>
            public interface IManaged<TManager> where TManager : class {

                /// <summary>The <see cref="IManager{T}"/> of this <see cref="IManaged{T}"/>.</summary>
                TManager Manager { get; set; }
                /// <summary>Method called by the <see cref="IManager{TManaged}"/>.</summary>
                void OnSetup();
                /// <summary>Method called by the <see cref="IManager{TManaged}"/>.</summary>
                void OnAllSetup();

            }

        }


        namespace Input {

            /// <summary>Interface for objects that recieve <see cref="Intangibles.Client.Inputs"/> from a single user.</summary>
            /// <seealso cref="IMultipleInputReciever"/>
            public interface ISingleInputReciever {

                /// <summary>Which client should be listened to?</summary>
                int Client { get; }
                /// <summary>Sends the <see cref="ISingleInputReciever"/> <see cref="Inputs"/> data.</summary>
                Inputs Inputs { set; }

            }

            /// <summary>Interface for objects that recieve <see cref="Intangibles.Client.Inputs"/> from multiple users.</summary>
            /// <seealso cref="ISingleInputReciever"/>
            public interface IMultipleInputReciever {

                /// <summary>Which clients should be listened to?</summary>
                int[] Clients { get; }
                /// <summary>Sends the <see cref="ISingleInputReciever"/> <see cref="Inputs"/> data.</summary>
                Inputs[] Inputs { set; }

            }

        }

        public interface IPrefabComponentEditor<TComponent> where TComponent : Component {
            void OnPrefab(TComponent component);
        }

    }

}
