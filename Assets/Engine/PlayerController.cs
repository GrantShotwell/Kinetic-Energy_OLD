using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using KineticEnergy.Intangibles;
using KineticEnergy.Inventory;
using KineticEnergy.Ships.Blocks;
using KineticEnergy.Ships;
using KineticEnergy.Intangibles.Global;

namespace KineticEnergy.Entities {

    #region Unity Editor
    #endregion

    // TODO: //
    // Change PlayerController into ClientInputManager.
    // Only one ClientInputManager will exist per computer, but every computer will not have the same ClientInputManager.
    // (that's why it's called the CLIENT)
    // As of writing this, I don't know what exactly I need to do for multiplayer, but I know that it's inputs being sent to the server.
    // Changing PlayerController to ClientInputManager should help disconnect the inputs from the actual game world.

    public class PlayerController : MonoBehaviour {

        /// <summary></summary>
        public enum Gamemode { Creative, Survival, Spectator }
        /// <summary>The current <see cref="Gamemode"/> of this player.</summary>
        public Gamemode gamemode;

        public float movePower = 3.0f;
        public float jumpPower = 3.0f;

        /// <summary>The <see cref="Player"/> that is related to this <see cref="PlayerController"/>.</summary>
        //public Player player { get; private set; }
        /// <summary>This player's <see cref="Hotbar"/>.</summary>
        public Hotbar hotbar { get; private set; } = new Hotbar();

        public AnimationCurve lookSensitivityCurve = new AnimationCurve(new Keyframe(0f, 0.5f, 0f, 5f), new Keyframe(1f, 2.5f, 0f, 0f));
        public float lookSensitivityMultiplier = 2.0f;

        public AnimationCurve rotateSensitivityCurve = new AnimationCurve(new Keyframe(0f, 0.5f, 0f, 5f), new Keyframe(1f, 2.5f, 0f, 0f));
        public float rotateSensitivityMultiplier = 5.0f;

        bool isGrounded => false;

        new Rigidbody rigidbody;
        BlockGridEditor blockGridEditor;
        GlobalPaletteManager palettes;
        public void Start() {
            //Get Rigidbody.
            rigidbody = GetComponent<Rigidbody>();
            //Get BLockGridEditor.
            blockGridEditor = GetComponent<BlockGridEditor>();
            //Get GlobalPaletteManager.
            palettes = FindObjectOfType<GlobalPaletteManager>();
            //Temporary hotbar setup.
            var blockPaletteSize = palettes.blocks.Count;
            for(int i = 0; i < blockPaletteSize && i < 10; i++)
                hotbar.slots[i] = palettes.blocks[i];
            Cursor.visible = false;
            Cursor.lockState = CursorLockMode.Locked;
        }

        int m_hotbarInput = 0;
        Vector3 m_blockrotInput = Vector3.zero;
        bool m_rbX, m_rbY, m_rbZ;
        public void Update() {
            var preview = blockGridEditor.preview == null ? null : blockGridEditor.preview.gameObject;
            
            //Find which hotbar item is being selected.
            /**/ if(Input.GetButtonDown("Hotbar 1")) m_hotbarInput = 1;
            else if(Input.GetButtonDown("Hotbar 2")) m_hotbarInput = 2;
            else if(Input.GetButtonDown("Hotbar 3")) m_hotbarInput = 3;
            else if(Input.GetButtonDown("Hotbar 4")) m_hotbarInput = 4;
            else if(Input.GetButtonDown("Hotbar 5")) m_hotbarInput = 5;
            else m_hotbarInput = -5; if(Input.GetButton("Hotbar ALT")) m_hotbarInput += 5;
            //If no number button was pushed, hotbarInput will be 0 or -5.
            if(m_hotbarInput > 0) {
                //Array index is one less than the item in the list.
                var hotbarIndex = m_hotbarInput - 1;
                //If the selected index has changed, delete the old and create the new.
                if(hotbar.selectedIndex != hotbarIndex) {
                    hotbar.selectedIndex = hotbarIndex;
                    if(preview) Destroy(preview);
                    if(hotbar.selected != null)
                        blockGridEditor.preview = Instantiate(
                            hotbar.selected.prefabBlock_preview).GetComponent<BlockPreview>();
                }
            }

            //Rotate the preview.
            if(preview != null) {
                Vector3 blockrot;
                //If rotating on a grid, rotate by 90 degree intervals.
                if(blockGridEditor.grid != null) {
                    //"GetAxisRaw --> GetButtonDown" using booleans stored across frames.

                    /* X */ var rbX = Input.GetAxisRaw("Rotate Block X");
                    m_blockrotInput.x = m_rbX ? rbX : 0; m_rbX = rbX == 0;

                    /* Y */ var rbY = Input.GetAxisRaw("Rotate Block Y");
                    m_blockrotInput.y = m_rbY ? rbY : 0; m_rbY = rbY == 0;

                    /* Z */ var rbZ = Input.GetAxisRaw("Rotate Block Z");
                    m_blockrotInput.z = m_rbZ ? rbZ : 0; m_rbZ = rbZ == 0;

                    //Inputs are normalized (+/-1), so just multiply by 90 to get 90 degree rotations.
                    blockrot = m_blockrotInput * 90;
                    //Apply the rotation.
                    if(blockrot != Vector3.zero) blockGridEditor.rotation *= Quaternion.Euler(blockrot);

                //If we are not rotating on a grid, then do it smooooooooothly.
                } else {

                    //Get the axis. Nothing fancy here.
                    m_blockrotInput.x = Input.GetAxis("Rotate Block X");
                    m_blockrotInput.y = Input.GetAxis("Rotate Block Y");
                    m_blockrotInput.z = Input.GetAxis("Rotate Block Z");

                    //Aformentioned "smoooooooooth"-ness.
                    blockrot = m_blockrotInput * rotateSensitivityCurve.Evaluate(m_blockrotInput.magnitude) * rotateSensitivityMultiplier;
                    //Apply the rotation.
                    preview.transform.RotateAround(preview.transform.position, transform.right  , blockrot.x);
                    preview.transform.RotateAround(preview.transform.position, transform.up     , blockrot.y);
                    preview.transform.RotateAround(preview.transform.position, transform.forward, blockrot.z);

                }
            }


            // Temporary input gathering.

            if(Input.GetButtonDown("Primary"))
                blockGridEditor.PlaceBlock();
            else preview?.SetActive(!Input.GetButton("Primary"));

            if(Input.GetButtonUp("Secondary"))
                blockGridEditor.RemoveBlock();

            if(Input.GetKeyDown(KeyCode.Escape)) {
#if UNITY_EDITOR
                EditorApplication.isPaused = true;
#endif
#if UNITY_STANDALONE
                Application.Quit();
#endif
            }
        }

        Vector3 m_moveInput = Vector3.zero;
        Vector2 m_lookInput = Vector2.zero;
        float m_rotationInput = 0.0f;
        public void FixedUpdate() {

            //Get movement inputs.
            m_moveInput.x = Input.GetAxis("Move X");
            m_moveInput.y = Input.GetAxis("Move Y");
            m_moveInput.z = Input.GetAxis("Move Z");
            //Temporary movement.
            if(isGrounded) {
                m_moveInput.x *= movePower;
                m_moveInput.y *= jumpPower;
                m_moveInput.z *= movePower;
            } else {
                m_moveInput *= movePower;
            }
            m_moveInput = transform.rotation * m_moveInput;
            rigidbody.AddForce(m_moveInput);

            //Get rotation inputs.
            m_lookInput.x = +Input.GetAxis("Look X");
            m_lookInput.y = -Input.GetAxis("Look Y");
            m_lookInput *= lookSensitivityMultiplier * lookSensitivityCurve.Evaluate(m_lookInput.magnitude);
            //Temporary rotation.
            m_rotationInput = +Input.GetAxis("Rotate");
            var rotate = new Vector3(m_lookInput.y, m_lookInput.x, m_rotationInput);
            transform.Rotate(rotate);
        }

        public class Hotbar {
            public BlockPalette.Sample[] slots = new BlockPalette.Sample[10];
            public int selectedIndex = 0;
            public BlockPalette.Sample selected => slots[selectedIndex];
        }

    }

}
