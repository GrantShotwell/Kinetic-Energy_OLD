using UnityEngine;
using KineticEnergy.Ships;
using KineticEnergy.Ships.Blocks;
using KineticEnergy.Interfaces.Input;
using KineticEnergy.Intangibles.Client;
using KineticEnergy.Intangibles.Behaviours;

namespace KineticEnergy.Entities {

    #region Unity Editor
    #endregion

    // TODO: //
    // Add support for stuff other than blocks in the hotbar.

    public class Player : Entity, ISingleInputReciever {

        #region Properties

        /// <summary></summary>
        public enum Gamemode { Creative, Survival, Spectator }
        /// <summary>The current <see cref="Gamemode"/> of this player.</summary>
        public Gamemode gamemode;

        /// <summary><see cref="Intangibles.Client.Inputs"/></summary>
        public Inputs Inputs { get; set; }

        public float movePower = 3.0f;
        public float jumpPower = 3.0f;

        /// <summary>This player's <see cref="Hotbar"/>.</summary>
        public Hotbar hotbar { get; private set; } = new Hotbar();

        public AnimationCurve lookSensitivityCurve = new AnimationCurve(new Keyframe(0f, 0.5f, 0f, 5f), new Keyframe(1f, 2.5f, 0f, 0f));
        public float lookSensitivityMultiplier = 2.0f;

        public AnimationCurve rotateSensitivityCurve = new AnimationCurve(new Keyframe(0f, 0.5f, 0f, 5f), new Keyframe(1f, 2.5f, 0f, 0f));
        public float rotateSensitivityMultiplier = 5.0f;

        bool isGrounded => false;

        public int Client => global.clients[0];
        GlobalBehavioursManager global;

        new Camera camera;

        #endregion

        #region Setup

        new Rigidbody rigidbody;
        BlockGridEditor blockGridEditor;
        GlobalPaletteManager palettes;
        public void Start() {

            // TEMP: terminal view
            camera = Camera.main;
            mTemp_originalCamPos = camera.transform.localPosition;

            //Get Rigidbody.
            rigidbody = GetComponent<Rigidbody>();
            //Get BLockGridEditor.
            blockGridEditor = GetComponent<BlockGridEditor>();
            //Get GlobalBehavioursManager.
            global = FindObjectOfType<GlobalBehavioursManager>();
            global.PolishBehaviour(this);
            //Get GlobalPaletteManager.
            palettes = global.gameObject.GetComponent<GlobalPaletteManager>();
            //Temporary hotbar setup.
            var blockPaletteSize = palettes.blocks.Count;
            for(int i = 0; i < blockPaletteSize && i < 10; i++)
                hotbar.slots[i] = palettes.blocks[i];
        }

        #endregion

        #region Behaviour

        Vector3 m_blockrotInput = Vector3.zero;
        bool m_rbX, m_rbY, m_rbZ;
        public void FixedUpdate() {
            var preview = blockGridEditor.preview == null ? null : blockGridEditor.preview.gameObject;

            //Find which hotbar item is being selected.
            var _hotbar = Inputs.hotbar;
            //If no number button was pushed, hotbarInput will be 0 or -5.
            if(_hotbar > -1) {
                //If the selected index has changed, delete the old and create the new.
                if(hotbar.selectedIndex != _hotbar) {
                    hotbar.selectedIndex = _hotbar;
                    if(preview) Destroy(preview);
                    if(hotbar.selected != null) {
                        var prefab = Instantiate(hotbar.selected.prefabBlock_preview);
                        blockGridEditor.preview = prefab.GetComponent<BlockPreview>();
                        preview = blockGridEditor.preview == null ? null : blockGridEditor.preview.gameObject;
                        if(!preview) {
                            Debug.LogWarningFormat("Tried to instantiate '{0}' as a BlockPreview prefab, but it had no BlockPreview component!", prefab);
                            Destroy(prefab);
                        }
                    }
                }
            }

            //Do stuff with the preview if it exists.
            if(preview != null) {

                //Spin Preview//
                {
                    Vector3 blockrot;

                    //If rotating on a grid, rotate by 90 degree intervals.
                    if(blockGridEditor.grid != null) {

                        //"GetAxisRaw --> GetButtonDown" using booleans stored across frames.

                        /* X */ var rbX = Inputs.spin.x;
                        m_blockrotInput.x = m_rbX ? rbX : 0; m_rbX = rbX == 0;

                        /* Y */ var rbY = Inputs.spin.y;
                        m_blockrotInput.y = m_rbY ? rbY : 0; m_rbY = rbY == 0;

                        /* Z */ var rbZ = Inputs.spin.z;
                        m_blockrotInput.z = m_rbZ ? rbZ : 0; m_rbZ = rbZ == 0;

                        //Inputs are normalized (+/-1), so just multiply by 90 to get 90 degree rotations.
                        blockrot = m_blockrotInput * 90;
                        //Apply the rotation.
                        if(blockrot != Vector3.zero) {

                            //Apply the rotation to the preview.
                            preview.transform.RotateAround(preview.transform.position, this.transform.right, blockrot.x);
                            preview.transform.RotateAround(preview.transform.position, this.transform.up, blockrot.y);
                            preview.transform.RotateAround(preview.transform.position, this.transform.forward, blockrot.z);

                            //Round the preview's rotation.
                            var qF = preview.transform.rotation;
                            var vF = qF.eulerAngles;
                            vF.x = CodeTools.Math.Geometry.RoundToMultiple(vF.x, 90f);
                            vF.y = CodeTools.Math.Geometry.RoundToMultiple(vF.y, 90f);
                            vF.z = CodeTools.Math.Geometry.RoundToMultiple(vF.z, 90f);
                            qF = Quaternion.Euler(vF);

                            //Apply the rotation to the editor.
                            blockGridEditor.rotation = qF;

                        }

                    //If we are not rotating on a grid, then do it smooooooooothly.
                    } else {

                        //Get the inputs. Nothing fancy here.
                        m_blockrotInput = Inputs.spin;

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
                    if(Inputs.primary.Down && !Inputs.terminal) blockGridEditor.TryPlaceBlock();
                    else preview.SetActive(Inputs.primary.IsFree);
                }

            }

            if(Inputs.scndary.Down)
                blockGridEditor.RemoveBlock();

            if(Input.GetKeyDown(KeyCode.Escape)) {
                #if UNITY_EDITOR
                UnityEditor.EditorApplication.isPaused = true;
                #elif UNITY_STANDALONE
                UnityEngine.Application.Quit();
                #endif
            }

            // Movement //
            var _move = Inputs.move;
            if(isGrounded) {
                _move.x *= movePower;
                _move.y *= jumpPower;
                _move.z *= movePower;
            } else {
                _move *= movePower;
            }
            _move = transform.rotation * _move;
            rigidbody.AddForce(_move);

            // Rotation //
            if(!mTemp_terminal) {
                var _look = new Vector3 {
                    x = -Inputs.look.y,
                    y = +Inputs.look.x
                };
                _look *= lookSensitivityMultiplier * lookSensitivityCurve.Evaluate(_look.magnitude);
                _look.z = Inputs.look.z;
                transform.Rotate(_look);
            }

            // TEMP: get camera to move for terminal view. //
            mTemp_terminal = Inputs.terminal;
            if(Inputs.terminal.Down) {
                Cursor.lockState = CursorLockMode.Confined;
                camera.transform.localPosition = (Vector3.back * 25) + mTemp_originalCamPos;
            } else
            if(Inputs.terminal.Held) {
                Cursor.lockState = CursorLockMode.None;
                Cursor.visible = true;
                camera.transform.localPosition = (Vector3.back * 25) + mTemp_originalCamPos;
            } else {
                Cursor.lockState = CursorLockMode.Locked;
                Cursor.visible = false;
                camera.transform.localPosition = mTemp_originalCamPos;
            }
            
        }

        // TEMP: get camera to move for terminal view. //
        bool mTemp_terminal = false;
        Vector3 mTemp_originalCamPos;
        //Vector3 mTemp_currentCamDelta;

        public class Hotbar {
            public BlockPalette.Sample[] slots = new BlockPalette.Sample[10];
            public int selectedIndex = 0;
            public BlockPalette.Sample selected => slots[selectedIndex];
        }

        #endregion

        public override GameObject BuildEntityPrefab() {
            var prefab = Instantiate(Resources.Load<GameObject>("Prefabs\\Player"));
            //prefab.SetActive(false);
            return prefab;
        }

    }

}
