using System;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Unity_Project.Scripts.Player
{
    public class PlayerController : MonoBehaviour
    {
        private const float WALK_SPEED = 10f;
        private const float LOOK_SENSITIVITY = 0.125f;
        private const float GRAVITY = -25f;
        private const float JUMP_HEIGHT = 1.35f;

        private bool m_IsGrounded;
        private float m_CameraYaw;

        private Vector2 m_InputLookVector2 = Vector2.zero;
        private Vector3 m_InputMovementVector3 = Vector3.zero;
        private Vector3 m_PlayerVelocity = Vector3.zero;

        private CharacterController m_CharacterController;
        [SerializeField]
        private Camera m_PlayerCamera;
        public Transform PlayerCameraTransform;

        private InputAction m_MoveAction, m_LookAction, m_FireAction, m_JumpAction;
        private PlayerInput m_PlayerInput;

        private void Awake()
        {
            m_CharacterController = GetComponent<CharacterController>();
            m_PlayerCamera = PlayerCameraTransform.GetComponent<Camera>();
            m_PlayerInput = GetComponent<PlayerInput>();
            
            // Get InputActions
            m_MoveAction = m_PlayerInput.actions["Move"];
            m_LookAction = m_PlayerInput.actions["Look"];
            m_FireAction = m_PlayerInput.actions["Fire"];
            m_JumpAction = m_PlayerInput.actions["Jump"];
            
            // Lock Mouse
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }

        private void Update()
        {
            // Looking Rotations
            transform.Rotate(Vector3.up, m_InputLookVector2.x);
            PlayerCameraTransform.localRotation = Quaternion.Euler(m_CameraYaw * -1, 0f, 0f);

            // Movement & Gravity
            // TODO: Implement my own CharacterController for this game... I don't like the Unity one much.
            m_CharacterController.Move(transform.TransformDirection(m_InputMovementVector3 * (WALK_SPEED * Time.deltaTime)));
            
            if (m_IsGrounded && m_PlayerVelocity.y < 0)
            {
                m_PlayerVelocity.y = 0f;
            }
            
            m_PlayerVelocity.y += GRAVITY * Time.deltaTime;
            m_CharacterController.Move(m_PlayerVelocity * Time.deltaTime);
            m_IsGrounded = m_CharacterController.isGrounded;
        }
        
        // + + + + | InputAction Callbacks | + + + +
        
        public void OnMove(InputAction.CallbackContext ctx)
        {
            var inputVec2 = m_MoveAction.ReadValue<Vector2>();
            m_InputMovementVector3 = new Vector3(inputVec2.x, 0f, inputVec2.y);
        }
        
        public void OnLook(InputAction.CallbackContext ctx)
        {
            var inputVec2 = m_LookAction.ReadValue<Vector2>();
            
            m_InputLookVector2 = inputVec2 * LOOK_SENSITIVITY;
            
            m_CameraYaw += inputVec2.y * LOOK_SENSITIVITY;
            m_CameraYaw = Mathf.Clamp(m_CameraYaw, -90f, 90f);
        }
        
        public void OnJump(InputAction.CallbackContext ctx)
        {
            if (m_IsGrounded && m_JumpAction.WasPressedThisFrame())
            {
                m_PlayerVelocity.y += Mathf.Sqrt(JUMP_HEIGHT * -3f * GRAVITY);
            }
        }

        // public void OnFire(InputAction.CallbackContext ctx)
        // {
        //     //
        // }
    }
}
