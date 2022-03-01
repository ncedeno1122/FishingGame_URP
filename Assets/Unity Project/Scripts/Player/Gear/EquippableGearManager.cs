using System;
using UnityEngine;
using UnityEngine.InputSystem;


namespace Unity_Project.Scripts.Player.Gear
{
    public class EquippableGearManager : MonoBehaviour
    {
        private PlayerInput m_PlayerInput;
        private InputAction m_MoveAction, m_LookAction, m_FireAction, m_JumpAction;

        public EquippableGear EquippedGear;

        private void Awake()
        {
            m_PlayerInput = GetComponent<PlayerInput>();
            
            m_FireAction = m_PlayerInput.actions["Fire"];
        }

        private void Update()
        {
            //
            if (!EquippedGear)
            {
                return;
            }
            if (m_FireAction.IsPressed() && (!m_FireAction.WasPressedThisFrame() && !m_FireAction.WasReleasedThisFrame()))
            {
                EquippedGear.OnFireHeld();
            }
        }

        // + + + + | InputAction Callbacks | + + + +

        public void OnFire(InputAction.CallbackContext ctx)
        {
            if (m_FireAction.WasPressedThisFrame())
            {
                EquippedGear.OnFirePressed();
            }

            if (m_FireAction.WasReleasedThisFrame())
            {
                EquippedGear.OnFireReleased();
            }
        }
    }
}
