using System;
using UnityEngine;
using UnityEngine.InputSystem;


namespace Unity_Project.Scripts.Player.Gear
{
    /// <summary>
    /// Allows for player control over an Equippable Gear.
    /// </summary>
    public class EquippableGearManager : MonoBehaviour
    {
        private PlayerInput m_PlayerInput;
        private InputAction m_MoveAction, m_LookAction, m_FireAction, m_JumpAction;

        public EquippableGearController EquippedGear;

        private void Awake()
        {
            m_PlayerInput = GetComponent<PlayerInput>();
            
            m_FireAction = m_PlayerInput.actions["Fire"];
        }

        private void OnValidate()
        {
            if (!EquippedGear)
            {
                var heldItemGO = transform.Find("HeldItem");
                var gearGO = heldItemGO.GetChild(0);
                if (gearGO)
                {
                    EquippedGear = gearGO.GetComponent<EquippableGearController>();
                }
            }
        }

        private void Update()
        {
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
