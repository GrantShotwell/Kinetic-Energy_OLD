using KineticEnergy.Structs;
using KineticEnergy.Intangibles.Behaviours;
using KineticEnergy.Intangibles.Client;
using KineticEnergy.Interfaces.Input;
using KineticEnergy.Ships;
using KineticEnergy.Ships.Blocks;
using UnityEngine;

namespace KineticEnergy.Entities {

    #region Unity Editor
    #endregion

    public class Player : Entity, ISingleInputReciever {

        #region Properties

        /// <summary>The possible gamemode of a <see cref="Player"/>.</summary>
        public enum Gamemode { Creative, Survival, Spectator }
        /// <summary>The current <see cref="Gamemode"/> of this player.</summary>
        public Gamemode gamemode;

        public Inputs Inputs { get; set; }
        public bool holdInputs = false;

        public float movePower = 3.0f;
        public float jumpPower = 3.0f;

        /// <summary>This player's <see cref="TenSlots"/>.</summary>
        public TenSlots Hotbar { get; private set; } = new TenSlots();

        public AnimationCurve lookSensitivityCurve = new AnimationCurve(new Keyframe(0f, 0.5f, 0f, 5f), new Keyframe(1f, 2.5f, 0f, 0f));
        public float lookSensitivityMultiplier = 2.0f;

        public AnimationCurve rotateSensitivityCurve = new AnimationCurve(new Keyframe(0f, 0.5f, 0f, 5f), new Keyframe(1f, 2.5f, 0f, 0f));
        public float rotateSensitivityMultiplier = 5.0f;

        bool IsGrounded => false;

        public int Client => Global.clients[0];

        public Rigidbody Rigidbody { get; set; }
        public BlockGridEditor BlockGridEditor { get; set; }

        public GlobalPaletteManager palettes;

        #endregion

        #region Setup

        public void Start() {

            //Get Rigidbody.
            Rigidbody = GetComponent<Rigidbody>();

            //Get BlockGridEditor.
            BlockGridEditor = GetComponent<BlockGridEditor>();

            //Get GlobalPaletteManager.
            palettes = Global.Palettes;

            //Temporary hotbar setup.
            var blockPaletteSize = palettes.blocks.Count;
            for(int i = 0; i < blockPaletteSize && i < 10; i++)
                Hotbar.Slots[i] = palettes.blocks[i];

            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;

        }

        #endregion

        #region Behaviour


        Vector3 m_blockrotInput = Vector3.zero;
        bool m_rbX, m_rbY, m_rbZ;
        bool m_selected = false;
        private GameObject GetPreview() => BlockGridEditor.Preview ? BlockGridEditor.Preview.gameObject : null;
        public void FixedUpdate() {

            if(holdInputs) Inputs = default;
            Inputs inputs = Inputs;

            #region Preview
            var preview = GetPreview();

            //Find which hotbar item is being selected.
            var hotbarInput = inputs.hotbar;

            //If no number button was pushed, hotbarInput will be -1.
            if(hotbarInput != -1) { //if it was not-not pressed...

                //If the selected index hasn't changed, then toggle 'm_selected'.
                if(Hotbar.SelectedIndex == hotbarInput) {
                    m_selected = !m_selected;
                    if(m_selected) goto CreatePreview;
                    else if(preview) Destroy(preview);

                    //If the selected index has changed, then update the preview.
                } else {
                    Hotbar.SelectedIndex = hotbarInput;
                    goto CreatePreview;
                }

                //Will stop here unless this line was skipped with 'goto CreatePreview'.
                preview = GetPreview();
                goto Break1;

                CreatePreview:
                //Delete the old, create the new.
                if(preview) Destroy(preview);
                if(Hotbar.Selected == null) {
                    preview = null;
                } else {
                    var prefab = Instantiate(Hotbar.Selected.prefabBlock_preview);
                    BlockGridEditor.Preview = prefab.GetComponent<BlockPreview>();
                    preview = GetPreview();
                    Global.PolishBehaviour(preview.GetComponent<BlockPreview>());
                    if(!preview) {
                        Debug.LogWarningFormat("Tried to instantiate '{0}' as a BlockPreview prefab, but it had no BlockPreview component!", prefab);
                        Destroy(prefab);
                    }
                }
            }
            Break1:

            //Do stuff with the preview if it exists.
            if(preview) {

                //Spin Preview//
                {
                    Vector3 blockrot;

                    //If rotating on a grid, rotate by 90 degree intervals.
                    if(BlockGridEditor.Grid != null) {

                        //"GetAxisRaw --> GetButtonDown" using booleans stored across frames.

                        var rbX = inputs.spin.x; // X //
                        m_blockrotInput.x = m_rbX ? rbX : 0; m_rbX = rbX == 0;

                        var rbY = inputs.spin.y; // Y //
                        m_blockrotInput.y = m_rbY ? rbY : 0; m_rbY = rbY == 0;

                        var rbZ = inputs.spin.z; // Z //
                        m_blockrotInput.z = m_rbZ ? rbZ : 0; m_rbZ = rbZ == 0;

                        //Inputs are normalized (+/-1), so just multiply by 90 to get 90 degree rotations.
                        blockrot = m_blockrotInput * 90;
                        //Apply the rotation.
                        if(blockrot != Vector3.zero) {

                            //Apply the rotation to the preview.
                            preview.transform.RotateAround(preview.transform.position, this.transform.right, blockrot.x);
                            preview.transform.RotateAround(preview.transform.position, this.transform.up, blockrot.y);
                            preview.transform.RotateAround(preview.transform.position, this.transform.forward, blockrot.z);

                            //Round the preview's rotation to a multiple of 90 degrees.
                            var qF = preview.transform.rotation;
                            var vF = qF.eulerAngles;
                            vF.x = CodeTools.Math.Geometry.RoundToMultiple(vF.x, 90f);
                            vF.y = CodeTools.Math.Geometry.RoundToMultiple(vF.y, 90f);
                            vF.z = CodeTools.Math.Geometry.RoundToMultiple(vF.z, 90f);
                            qF = Quaternion.Euler(vF);

                            //Apply the rotation to the editor.
                            BlockGridEditor.Rotation = qF;

                        }

                        //If we are not rotating on a grid, then do it smooooooooothly.
                    } else {

                        //Get the inputs. Nothing fancy here.
                        m_blockrotInput = inputs.spin;

                        //Aformentioned "smoooooooooth"-ness.
                        blockrot = m_blockrotInput * rotateSensitivityCurve.Evaluate(m_blockrotInput.magnitude) * rotateSensitivityMultiplier;
                        //Apply the rotation.
                        preview.transform.RotateAround(preview.transform.position, transform.right, blockrot.x);
                        preview.transform.RotateAround(preview.transform.position, transform.up, blockrot.y);
                        preview.transform.RotateAround(preview.transform.position, transform.forward, blockrot.z);

                    }
                }

                //Place Preview//
                {
                    if(inputs.primary.Down && !inputs.terminal) BlockGridEditor.TryPlaceBlock();
                    else preview.SetActive(inputs.primary.IsFree);
                }

            }
            #endregion

            // TEMP: selectables
            if(!preview) {
                if(Physics.Raycast(new Ray(transform.position + new Vector3(0.0f, 0.7f, 0.0f), transform.forward), out var hit, mTemp_mask)) {

                    var selected = hit.collider.gameObject;
                    if(selected.layer == 11) {
                        var selectable = selected.GetComponent<Selectable>();
                        if(selectable) {
                            selectable.show = true;
                            if(inputs.primary.Down)
                                selectable.Select(this);
                        } else Debug.LogWarningFormat("A GameObject ('{0}') on layer 11 did not have a '{1}' component.", selected, nameof(Selectable));
                    }

                }
            }

            if(Inputs.scndary.Down)
                BlockGridEditor.RemoveBlock();

            if(Input.GetKeyDown(KeyCode.Escape)) {
#if UNITY_EDITOR
                UnityEditor.EditorApplication.isPaused = true;
#else
                UnityEngine.Application.Quit();
#endif
            }

            #region Movement
            var move = inputs.move;
            if(IsGrounded) {
                move.x *= movePower;
                move.y *= jumpPower;
                move.z *= movePower;
            } else {
                move *= movePower;
            }
            move = transform.rotation * move;
            Rigidbody.AddForce(move * Rigidbody.mass);
            #endregion

            #region Rotation
            var look = new Vector3 {
                x = -inputs.look.y,
                y = +inputs.look.x
            };
            look *= lookSensitivityMultiplier * lookSensitivityCurve.Evaluate(look.magnitude);
            look.z = inputs.look.z;
            transform.Rotate(look);
            #endregion

            //// TEMP: get camera to move for terminal view. //
            //mTemp_terminal = Inputs.terminal;
            //if(Inputs.terminal.Down) {
            //    Cursor.lockState = CursorLockMode.Confined;
            //    camera.transform.localPosition = (Vector3.back * 25) + mTemp_originalCamPos;
            //} else
            //if(Inputs.terminal.Held) {
            //    Cursor.lockState = CursorLockMode.None;
            //    Cursor.visible = true;
            //    camera.transform.localPosition = (Vector3.back * 25) + mTemp_originalCamPos;
            //} else {
            //    Cursor.lockState = CursorLockMode.Locked;
            //    Cursor.visible = false;
            //    camera.transform.localPosition = mTemp_originalCamPos;
            //}

        }

        // TEMP: selectables
        public LayerMask mTemp_mask;

        public class TenSlots {
            public BlockPalette.Sample[] Slots { get; set; } = new BlockPalette.Sample[10];
            public int SelectedIndex { get; set; } = 0;
            public BlockPalette.Sample Selected => Slots[SelectedIndex];
        }

        #endregion

        public override Prefab BuildEntityPrefab() {
            var prefab = new Prefab(Master.prefabs.player.Instantiate().gameObject);
            prefab.gameObject.SetActive(true);
            return prefab;
        }
    }

}
