using System;
using KineticEnergy.Content;
using KineticEnergy.Intangibles;
using KineticEnergy.Interfaces.Managed;
using KineticEnergy.Structs;
using UnityEngine;

namespace KineticEnergy.Grids.Blocks {

    /// <summary>Static class which contains <see cref="AppliesTo"/>, <see cref="Order"/>, and <see cref="BlockAttribute"/>s.</summary>
    public static class BlockAttributes {

        /// <summary>What does this attribute apply to?</summary>
        public enum AppliesTo {
            /// <summary>Signifies that this attribute is to only be applied to the original block, not the preview.</summary>
            Original,
            /// <summary>Signifies that this attribute is to only be applied to the preview block, not the original.</summary>
            Preview,
            /// <summary>Signifies that this attribute is to be applied to both the preview and the original.</summary>
            Both
        }

        /// <summary>What order should this attribute be applied?</summary>
        public enum Order {
            /// <summary>A special case for the <see cref="BlockAttributes.BasicInfo"/> attribute.</summary>
            BasicInfo = 99,
            /// <summary>Signifies that this attribute must be amung the first to be applied.</summary>
            First = 1,
            /// <summary>Signifies that this attribute has normal priority.</summary>
            Default = 0,
            /// <summary>Signifies that this attribute must be amung the last to be applied.</summary>
            Last = -1
        }

        /// <summary>Abstract base class for <see cref="Block"/> and <see cref="BlockPreview"/> attributes.</summary>
        /// <remarks>For use with <see cref="Block"/> types only.
        /// Multiple attributes are allowed if the associated component can have duplicated.
        /// Attributes are not inherited.
        /// If an attribute gives you the option to set <see cref="Targets"/>, then the attribute is useful for both.
        /// <para/>If you are implementing this class, do not check the given "isPreview" boolean inside <see cref="ApplyTo"/>.
        /// This is done already by the <see cref="BlockAttribute"/> class, so if <see cref="ApplyTo"/> is being called then the attribute needs to be applied.
        /// Instead, use that given value to change values such as <see cref="Collider.isTrigger"/>.</remarks>
        public abstract class BlockAttribute : Attribute {

            /// <summary>The <see cref="BlockAttributes.Order"/> of this <see cref="BlockAttribute"/>.</summary>
            public abstract Order Order { get; }

            /// <summary>Does this <see cref="BlockAttribute"/> apply to originals, previews, or both?</summary>
            public AppliesTo Targets { get; }
            public int Identifier { get; }
            /// <summary>Shorthand for "<c>this.targets == AppliesTo.Both || this.targets == AppliesTo.Original</c>".</summary>
            public bool AppliesToOriginal => Targets == AppliesTo.Both || Targets == AppliesTo.Original;
            /// <summary>Shorthand for "<c>this.targets == AppliesTo.Both || this.targets == AppliesTo.Preview</c>".</summary>
            public bool AppliesToPreview => Targets == AppliesTo.Both || Targets == AppliesTo.Preview;

            protected BlockAttribute(int identifier, AppliesTo targets) {
                Targets = targets;
                Identifier = identifier;
            }

            /// <summary>Applies this <see cref="BlockAttribute"/> to the given <see cref="GameObject"/>, regardless of <see cref="Targets"/.></summary>
            /// <param name="block">The <see cref="Block"/> or <see cref="BlockPreview"/>'s <see cref="GameObject"/>.</param>
            /// <param name="asPreview">Is the given <see cref="GameObject"/> a <see cref="BlockPreview"/> or a <see cref="Block"/>?</param>
            public abstract void ApplyTo(GameObject block, Master master, bool asPreview);

            /// <summary>Applies this <see cref="BlockAttribute"/> if <see cref="AppliesToOriginal"/> is true.</summary>
            /// <param name="original">The <see cref="GameObject"/> to apply this <see cref="BlockAttribute"/> to.</param>
            /// <param name="master">The one, the only...</param>
            public void ApplyToOriginal(GameObject original, Master master) { if(AppliesToOriginal) ApplyTo(original, master, false); }

            /// <summary>Applies this <see cref="BlockAttribute"/> if <see cref="AppliesToPreview"/> is true.</summary>
            /// <param name="preview">The <see cref="GameObject"/> to apply this <see cref="BlockAttribute"/> to.</param>
            /// <param name="master">The one, the only...</param>
            public void ApplyToPreview(GameObject preview, Master master) { if(AppliesToPreview) ApplyTo(preview, master, true); }

            protected static void SendToPrefabEditors<T>(Master master, GameObject block, T prefabComponent, bool asPreview, BlockAttribute sender) {
                foreach(Component component in block.GetComponents<Component>())
                    if(component is IPrefabEditor<T> prefabEditor)
                        prefabEditor.OnPrefab(master, prefabComponent, asPreview, sender);
            }

        }

        /// <summary>Required attribute for all <see cref="Block"/>s.</summary>
        /// <remarks>Inherits from <see cref="BlockAttribute"/>.</remarks>
        [AttributeUsage(AttributeTargets.Class, AllowMultiple = false, Inherited = false)]
        public class BasicInfo : BlockAttribute {
            public override Order Order => Order.BasicInfo;

            /// <summary>The name of the block.</summary>
            public string Name { get; }
            /// <summary>The name of the preview.</summary>
            public string Name_preview { get; }
            /// <summary>The value that <see cref="Block.Mass"/> will be set to.</summary>
            public Mass Mass { get; }
            /// <summary>The value that <see cref="Block.Dimensions"/> will be set to.</summary>
            public Vector3Int Dimensions { get; }
            /// <summary>The value that <see cref="Block.TransformOffset"/> will be set to.</summary>
            public Vector3 TransformOffset { get; }
            /// <summary></summary>
            public readonly bool hasFixedRotation;

            /// <summary>Gives this block a display name, mass, and dimensions. Every block should have this.</summary>
            /// <param name="name">The name of the block.</param>
            /// <remarks>The name of the preview is <c>name + "_preview"</c>.</remarks>
            public BasicInfo(string name, int sizeX, int sizeY, int sizeZ, long mass, float comX, float comY, float comZ, bool fixedRotation, AppliesTo targets = AppliesTo.Both)
                : base(0, targets) {
                Name = name;
                Name_preview = name + "_preview";
                Dimensions = new Vector3Int(sizeX, sizeY, sizeZ);
                Mass = new Mass(mass, new Vector3(comX, comY, comZ));
                hasFixedRotation = fixedRotation;
            }

            public override void ApplyTo(GameObject block, Master master, bool asPreview) {
                block.name = asPreview ? Name_preview : Name;
                if(!asPreview) {
                    var component = block.GetComponent<Block>();
                    if(component == null) throw new BlockAttributeException("'BasicInfo' attribute was being applied as \"original\", but a Block component could not be found.");
                    component.Dimensions = Dimensions;
                    component.Mass = Mass;
                }
            }
        }

        /// <summary>Attribute for a <see cref="Block"/> to have a <see cref="UnityEngine.BoxCollider"/>.</summary>
        /// <remarks>Inherits from <see cref="BlockAttribute"/>.</remarks>
        [AttributeUsage(AttributeTargets.Class, AllowMultiple = true, Inherited = false)]
        public class BoxCollider : BlockAttribute {
            public override Order Order => Order.Default;

            /// <summary>The <see cref="UnityEngine.BoxCollider.center"/>.</summary>
            public Vector3 Center { get; }
            /// <summary>The <see cref="UnityEngine.BoxCollider.size"/>.</summary>
            public Vector3 Size { get; }

            /// <summary>Gives this <see cref="Block"/> a <see cref="UnityEngine.BoxCollider"/>.</summary>
            /// <param name="centerX">The 'x' component of <see cref="UnityEngine.BoxCollider.center"/>.</param>
            /// <param name="centerY">The 'y' component of <see cref="UnityEngine.BoxCollider.center"/>.</param>
            /// <param name="centerZ">The 'z' component of <see cref="UnityEngine.BoxCollider.center"/>.</param>
            /// <param name="sizeX">The 'x' component of <see cref="UnityEngine.BoxCollider.size"/>.</param>
            /// <param name="sizeY">The 'y' component of <see cref="UnityEngine.BoxCollider.size"/>.</param>
            /// <param name="sizeZ">The 'z' component of <see cref="UnityEngine.BoxCollider.size"/>.</param>
            /// <param name="targets">Does this <see cref="BlockAttribute"/> apply to just the preview, just the original, or both?</param>
            /// <remarks>Originals use this for collision. Previews use this for detecting if the block placement is valid.</remarks>
            public BoxCollider(float centerX, float centerY, float centerZ, float sizeX, float sizeY, float sizeZ, int identifier = 0, AppliesTo targets = AppliesTo.Both)
                : base(identifier, targets) {
                Center = new Vector3(centerX, centerY, centerZ);
                Size = new Vector3(sizeX, sizeY, sizeZ);
            }

            public override void ApplyTo(GameObject block, Master master, bool asPreview) {

                var collider = block.AddComponent<UnityEngine.BoxCollider>();
                collider.center = Center;
                collider.size = Size;
                collider.isTrigger = asPreview;

                var component = block.GetComponent<Block>();
                if(component is OpaqueBlock opaque) {
                    opaque.Collider = collider;
                }

                SendToPrefabEditors(master, block, collider, asPreview, this);

            }

        }

        /// <summary>Attribute for a <see cref="Block"/> to have a uniform <see cref="UnityEngine.Mesh"/>.</summary>
        /// <remarks>Inherits from <see cref="BlockAttribute"/>.</remarks>
        [AttributeUsage(AttributeTargets.Class, AllowMultiple = false, Inherited = false)]
        public class Mesh : BlockAttribute {
            public override Order Order => Order.Default;

            /// <summary>The file path to a '.obj' file for a mesh.</summary>
            public string Path { get; }

            /// <summary>Gives this <see cref="Block"/> a <see cref="MeshFilter"/> with the given <see cref="UnityEngine.Mesh"/>.</summary>
            /// <param name="objPath"></param>
            /// <param name="targets">Does this attribute apply to just the preview, just the original, or both?</param>
            public Mesh(string objPath, int identifier = 0, AppliesTo targets = AppliesTo.Both) : base(identifier, targets) {
                Path = objPath;
            }

            public override void ApplyTo(GameObject block, Master master, bool asPreview) {

                var filter = block.AddComponent<MeshFilter>();
                filter.mesh = ContentLoader.GetObjFile(Path);
                var render = block.AddComponent<MeshRenderer>();

                SendToPrefabEditors(master, block, filter, asPreview, this);
                SendToPrefabEditors(master, block, render, asPreview, this);

            }

        }

        /// <summary>Attribute for a <see cref="Block"/> to have a <see cref="UnityEngine.MeshCollider"/>.</summary>
        /// <remarks>Inherits from <see cref="BlockAttribute"/>.</remarks>
        [AttributeUsage(AttributeTargets.Class, AllowMultiple = false, Inherited = false)]
        public class MeshCollider : BlockAttribute {
            public override Order Order => Order.Last;

            public MeshCollider(int identifier = 0, AppliesTo targets = AppliesTo.Both) : base(identifier, targets) { }

            public override void ApplyTo(GameObject block, Master master, bool asPreview) {

                var collider = block.AddComponent<UnityEngine.MeshCollider>();
                collider.convex = true;
                collider.isTrigger = asPreview;

                var component = block.GetComponent<Block>();
                if(component is OpaqueBlock opaque)
                    opaque.Collider = collider;

                SendToPrefabEditors(master, block, collider, asPreview, this);

            }

        }

        /// <summary>Attribute for an <see cref="OpaqueBlock"/> to have a face with a custom mesh.</summary>
        /// <remarks>Inherits from <see cref="BlockAttribute"/>.</remarks>
        /// <seealso cref="FlatFace"/>
        [AttributeUsage(AttributeTargets.Class, AllowMultiple = true, Inherited = false)]
        public class Face : BlockAttribute {
            public override Order Order => Order.Default;

            /// <summary>The file path to a '.obj' file for a mesh.</summary>
            public string MeshPath { get; }

            /// <summary>The <see cref="Block.Face"/> that this face is.</summary>
            public readonly Block.Face face;

            /// <summary>The local position of the face.</summary>
            public Vector3 Position { get; }

            /// <summary>The local rotation of the face.</summary>
            public Quaternion Rotation { get; }

            /// <summary>Assumes a 1x1x1 <see cref="Block"/> for a <see cref="Face"/>.</summary>
            /// <param name="face">Which <see cref="Block.Face"/> is this attribute describing?</param>
            /// <param name="objPath">File path the the '.obj' file for the mesh of this block face.</param>
            /// <param name="targets">Does this attribute apply to just the preview, just the original, or both?</param>
            public Face(Block.Face face, string objPath, int identifier = 0, AppliesTo targets = AppliesTo.Both)
                : this(face,

                      posX: (int)face == +1 ? +0.5f : (int)face == -1 ? -0.5f : 0,
                      posY: (int)face == +2 ? +0.5f : (int)face == -2 ? -0.5f : 0,
                      posZ: (int)face == +3 ? +0.5f : (int)face == -3 ? -0.5f : 0,

                      rotX: (int)face == +3 ? +90f : (int)face == -3 ? -90f
                          : (int)face == +2 ? 000f : (int)face == -2 ? 180f : 0,
                      rotY: 0,
                      rotZ: (int)face == +1 ? -90f : (int)face == -1 ? +90f : 0,

                      objPath, identifier, targets) { }

            /// <summary>Assumes a 1x1x1 <see cref="Block"/> for a <see cref="Face"/>.</summary>
            /// <param name="face">Which <see cref="Block.Face"/> is this attribute describing?</param>
            /// <param name="posX">The 'x' component of the relative position of this face. (ie. the location of this face)</param>
            /// <param name="posY">The 'y' component of the relative position of this face. (ie. the location of this face)</param>
            /// <param name="posZ">The 'z' component of the relative position of this face. (ie. the location of this face)</param>
            /// <param name="rotX">The 'x' component of the relative rotation of this face. (ie. the spin around the x-axis)</param>
            /// <param name="rotY">The 'y' component of the relative rotation of this face. (ie. the spin around the y-axis)</param>
            /// <param name="rotZ">The 'z' component of the relative rotation of this face. (ie. the spin around the z-axis)</param>
            /// <param name="objPath">File path the the '.obj' file for the mesh of this block face.</param>
            /// <param name="targets">Does this attribute apply to just the preview, just the original, or both?</param>
            public Face(Block.Face face,
                float posX, float posY, float posZ,
                float rotX, float rotY, float rotZ,
                string objPath, int identifier = 0, AppliesTo targets = AppliesTo.Both)
                : base(identifier, targets) {

                this.face = face;
                Position = new Vector3(posX, posY, posZ);
                Rotation = Quaternion.Euler(rotX, rotY, rotZ);
                MeshPath = objPath;

            }

            public override void ApplyTo(GameObject block, Master master, bool asPreview) {

                //Create the face.
                var face = new GameObject("Face");
                face.transform.parent = block.transform;
                face.transform.localPosition = Position;
                face.transform.localRotation = Rotation;

                face.AddComponent<MeshFilter>().mesh = ContentLoader.GetObjFile(MeshPath);
                face.AddComponent<MeshRenderer>().material = new UnityEngine.Material(
                    asPreview ? master.materials.preview
                              : master.materials.block);

                //Add the refrence to the block.
                if(!asPreview) {
                    var opaque = block.GetComponent<OpaqueBlock>();
                    if(opaque) {
                        if(opaque.Faces == null) opaque.Faces = new Block.FaceRefs();
                        switch(this.face) {
                            case Block.Face.PosX: opaque.Faces.right = face; break;
                            case Block.Face.NegX: opaque.Faces.left = face; break;
                            case Block.Face.PosY: opaque.Faces.top = face; break;
                            case Block.Face.NegY: opaque.Faces.bottom = face; break;
                            case Block.Face.PosZ: opaque.Faces.front = face; break;
                            case Block.Face.NegZ: opaque.Faces.back = face; break;
                            default: throw new BlockAttributeException("Invalid 'face' paramater for the 'Face' attribute.");
                        }
                    } else throw new BlockAttributeException("The 'Face' attribute can only be applied on opaque blocks.");
                }

                SendToPrefabEditors(master, block, face, asPreview, this);

            }

        }

        /// <summary>Attribute for an <see cref="OpaqueBlock"/> to have a face with a flat mesh.</summary>
        /// <remarks>Inherits from <see cref="BlockAttribute"/>.</remarks>
        /// <seealso cref="Face"/>
        [AttributeUsage(AttributeTargets.Class, AllowMultiple = true, Inherited = false)]
        public class FlatFace : BlockAttribute {
            public override Order Order => Order.Default;

            /// <summary>The <see cref="Block.Face"/> that this face is.</summary>
            public readonly Block.Face face;

            /// <summary>The local position of the face.</summary>
            public Vector3 position;
            /// <summary>The local rotation of the face.</summary>
            public Quaternion rotation;

            /// <summary>Assumes a 1x1x1 <see cref="Block"/> for a <see cref="FlatFace"/>.</summary>
            /// <param name="face">Which <see cref="Block.Face"/> is this attribute describing?</param>
            /// <param name="targets">Does this attribute apply to just the preview, just the original, or both?</param>
            public FlatFace(Block.Face face, int identifier = 0, AppliesTo targets = AppliesTo.Both)
                : this(face,

                      posX: (int)face == +1 ? +0.5f : (int)face == -1 ? -0.5f : 0,
                      posY: (int)face == +2 ? +0.5f : (int)face == -2 ? -0.5f : 0,
                      posZ: (int)face == +3 ? +0.5f : (int)face == -3 ? -0.5f : 0,

                      rotX: (int)face == +3 ? +90f : (int)face == -3 ? -90f
                          : (int)face == +2 ? 000f : (int)face == -2 ? 180f : 0,
                      rotY: 0,
                      rotZ: (int)face == +1 ? -90f : (int)face == -1 ? +90f : 0,

                      identifier, targets) { }

            //todo: make it actually do what it says in that summary
            /// <summary>Gives this <see cref="OpaqueBlock"/> a mesh that can be combined and hidden with other blocks of the same type.</summary>
            /// <param name="face">Which <see cref="Block.Face"/> is this attribute describing?</param>
            /// <param name="posX">The 'x' component of the relative position of this face. (ie. the location of this face)</param>
            /// <param name="posY">The 'y' component of the relative position of this face. (ie. the location of this face)</param>
            /// <param name="posZ">The 'z' component of the relative position of this face. (ie. the location of this face)</param>
            /// <param name="rotX">The 'x' component of the relative rotation of this face in degrees. (ie. the spin around the x-axis)</param>
            /// <param name="rotY">The 'y' component of the relative rotation of this face in degrees. (ie. the spin around the y-axis)</param>
            /// <param name="rotZ">The 'z' component of the relative rotation of this face in degrees. (ie. the spin around the z-axis)</param>
            /// <param name="targets">Does this attribute apply to just the preview, just the original, or both?</param>
            public FlatFace(Block.Face face, float posX, float posY, float posZ, float rotX, float rotY, float rotZ, int identifier = 0, AppliesTo targets = AppliesTo.Both)
                : base(identifier, targets) {
                this.face = face;
                position = new Vector3(posX, posY, posZ);
                rotation = Quaternion.Euler(rotX, rotY, rotZ);
            }

            public override void ApplyTo(GameObject block, Master master, bool asPreview) {

                //Create the face.
                GameObject face = master.meshes.plane.Instantiate().gameObject;
                face.transform.parent = block.transform;
                face.transform.localPosition = position;
                face.transform.localRotation = rotation;

                //Add the refrence to the block.
                if(!asPreview) {
                    var opaque = block.GetComponent<OpaqueBlock>();
                    if(opaque) {
                        if(opaque.Faces == null) opaque.Faces = new Block.FaceRefs();
                        switch(this.face) {
                            case Block.Face.PosX: opaque.Faces.right = face; break;
                            case Block.Face.NegX: opaque.Faces.left = face; break;
                            case Block.Face.PosY: opaque.Faces.top = face; break;
                            case Block.Face.NegY: opaque.Faces.bottom = face; break;
                            case Block.Face.PosZ: opaque.Faces.front = face; break;
                            case Block.Face.NegZ: opaque.Faces.back = face; break;
                            default: throw new BlockAttributeException("Invalid 'face' paramater for the 'Face' attribute.");
                        }
                    } else throw new BlockAttributeException("The 'FlatFace' attribute can only be applied on opaque blocks.");
                }

                SendToPrefabEditors(master, block, face, asPreview, this);

            }

        }

        /// <summary>Attribute for a <see cref="Block"/> to have a <see cref="UnityEngine.Material"/>.</summary>
        /// <remarks>Inherits from <see cref="BlockAttribute"/>.</remarks>
        [AttributeUsage(AttributeTargets.Class, AllowMultiple = false, Inherited = false)]
        public class Material : BlockAttribute {
            public override Order Order => Order.Last;

            public struct Tex {
                /// <summary>The file path to a '.png' file.</summary>
                public string path;
                /// <summary>Use point filter for this texture?</summary>
                public bool point;
            }

            /// <summary>Information about the diffuse texture for the <see cref="UnityEngine.Material"/>.</summary>
            public Tex Diffuse { get; private set; } = new Tex { path = null };
            /// <summary>Information about the height texture for the <see cref="UnityEngine.Material"/>.</summary>
            public Tex Height { get; private set; } = new Tex { path = null };

            private byte ToWho { //The Doctor, that's who!
                set {
                    if(value == 3) {
                        ToChildren = false;
                        ToParent = false;
                    } else {
                        if(value > 0) ToChildren = true;
                        if(value != 1) ToParent = true;
                    }
                }
            }

            /// <summary>Apply this material to the parent?</summary>
            public bool ToParent { get; private set; } = false;
            /// <summary>Apply this material to the children?</summary>
            public bool ToChildren { get; private set; } = false;

            /// <summary>Gives this <see cref="Block"/>'s <see cref="Renderer"/> a <see cref="Material"/>.</summary>
            /// <param name="diffusePath"><c>ex. "Content\\Vanilla\\Blocks\\ArmorBlock\\diffuse.png"</c></param>
            /// <param name="toWho">0: only parent, 1: only children, 2: both, 3: nothing (use with <see cref="IPrefabEditor{T}"/> where T is <see cref="Material"/>)</param>
            /// <param name="targets">Does this <see cref="BlockAttribute"/> apply to just the preview, just the original, or both?</param>
            public Material(string diffusePath, bool diffusePoint, byte toWho = 2, int identifier = 0, AppliesTo targets = AppliesTo.Both)
                : base(identifier, targets) {
                Diffuse = new Tex { path = diffusePath, point = diffusePoint };
                ToWho = toWho;
            }

            /// <summary>Gives this <see cref="Block"/>'s <see cref="Renderer"/> a <see cref="Material"/>.</summary>
            /// <param name="diffusePath">ex. <c>"Content\\Vanilla\\Blocks\\ArmorBlock\\height.png"</c></param>
            /// <param name="heightPath">ex. <c>"Content\\Vanilla\\Blocks\\ArmorBlock\\height.png"</c></param>
            /// <param name="toWho">0: only parent, 1: only children, 2: both, 3: none</param>
            /// <param name="targets">Does this <see cref="BlockAttribute"/> apply to just the preview, just the original, or both?</param>
            public Material(string diffusePath, bool diffusePoint, string heightPath, bool heightPoint, byte toWho = 2, int identifier = 0, AppliesTo targets = AppliesTo.Both)
                : base(identifier, targets) {
                Diffuse = new Tex { path = diffusePath, point = diffusePoint };
                Height = new Tex { path = heightPath, point = heightPoint };
                ToWho = toWho;
            }

            public override void ApplyTo(GameObject block, Master master, bool asPreview) {

                var material = new UnityEngine.Material(
                    asPreview ? master.materials.preview
                              : master.materials.block);

                if(Diffuse.path != null && !asPreview) {
                    Texture2D diffuse = ContentLoader.GetPngFile(Diffuse.path, TextureFormat.RGBA32, false);
                    diffuse.filterMode = Diffuse.point ? FilterMode.Point : FilterMode.Bilinear;
                    diffuse.wrapMode = TextureWrapMode.Clamp;
                    material.SetTexture("BlockDiffuse", diffuse);
                }

                if(Height.path != null && !asPreview) {
                    Texture2D normal = ContentLoader.GetPngFile(Height.path, TextureFormat.RGBA32, false);
                    normal.filterMode = Height.point ? FilterMode.Point : FilterMode.Bilinear;
                    normal.wrapMode = TextureWrapMode.Clamp;
                    material.SetTexture("BlockNormal", normal);
                }

                if(ToParent) {
                    var renderer = block.GetComponent<Renderer>();
                    if(renderer) renderer.material = material;
                }
                if(ToChildren) {
                    foreach(Renderer renderer in block.GetComponentsInChildren<Renderer>())
                        renderer.material = material;
                }

                SendToPrefabEditors(master, block, material, asPreview, this);

            }

        }

        /// <summary>Attribute for a <see cref="Block"/> to have a <see cref="UnityEngine.ParticleSystem"/>.</summary>
        /// <remarks><see cref="UnityEngine.ParticleSystem"/> can only be edited through <see cref="IPrefabEditor{T}"/> of type <see cref="UnityEngine.ParticleSystem"/>.</remarks>
        [AttributeUsage(AttributeTargets.Class, AllowMultiple = true, Inherited = false)]
        public class ParticleSystem : BlockAttribute {
            public override Order Order => Order.Default;

            public ParticleSystem(int identifier = 0, AppliesTo targets = AppliesTo.Original) : base(identifier, targets) { }

            public override void ApplyTo(GameObject block, Master master, bool asPreview) {
                var particles = block.AddComponent<UnityEngine.ParticleSystem>();
                SendToPrefabEditors(master, block, particles, asPreview, this);
            }

        }

        [AttributeUsage(AttributeTargets.Class, AllowMultiple = true, Inherited = false)]
        public class ThrusterVFX : BlockAttribute {
            public override Order Order => Order.Default;

            public ThrusterVFX(int identifier = 0, AppliesTo targets = AppliesTo.Original) : base(identifier, targets) { }

            public override void ApplyTo(GameObject block, Master master, bool asPreview) {
                Instantiated<UnityEngine.VFX.VisualEffect> vfx = master.prefabs.thrustVFX.Instantiate(block.transform);
                SendToPrefabEditors(master, block, vfx.component, asPreview, this);
            }

        }

        [AttributeUsage(AttributeTargets.Class, AllowMultiple = true, Inherited = false)]
        public class SelectableBox : BlockAttribute {
            public override Order Order => Order.Default;

            public readonly Vector3 center;
            public readonly Vector3 size;

            public readonly int identifier = 0;
            public SelectableBox(float centerX, float centerY, float centerZ, float sizeX, float sizeY, float sizeZ, int identifier = 0, AppliesTo targets = AppliesTo.Original)
                : base(identifier, targets) {
                center = new Vector3(centerX, centerY, centerZ);
                size = new Vector3(sizeX, sizeY, sizeZ);
            }

            public override void ApplyTo(GameObject block, Master master, bool asPreview) {
                var selectable = master.prefabs.selectableBox.Instantiate();
                selectable.gameObject.transform.SetParent(block.transform);
                selectable.gameObject.transform.localPosition = center;
                selectable.gameObject.transform.localScale = size;
                selectable.component2.material = master.materials.selectable;
                SendToPrefabEditors(master, block, selectable, asPreview, this);
            }

        }

        public class BlockAttributeException : Exception {
            public BlockAttributeException(string message) : base(message) { }
        }

    }

}