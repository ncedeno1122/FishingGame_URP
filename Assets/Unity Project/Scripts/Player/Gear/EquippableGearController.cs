using System;
using System.Collections;
using System.Collections.Generic;
using Unity_Project.Scripts.Player.Gear.ConcreteGear;
using UnityEngine;

namespace Unity_Project.Scripts.Player.Gear
{
    /// <summary>
    /// Allows control over an equippable gear GameObject this script is bound to.
    /// </summary>
    public class EquippableGearController : MonoBehaviour, IFireInputListener
    {
        [SerializeField]
        private ConcreteGearContext m_GearContext;


        private void Awake()
        {
            m_GearContext = GetComponent<ConcreteGearContext>();
        }

        private void OnValidate()
        {
            if (!m_GearContext)
            {
                m_GearContext = GetComponent<ConcreteGearContext>();
            }
        }

        // + + + + | Functions | + + + + 

        public void OnFireHeld()
        {
            m_GearContext.OnFireHeld();
        }

        public void OnFirePressed()
        {
            m_GearContext.OnFirePressed();
        }

        public void OnFireReleased()
        {
            m_GearContext.OnFireReleased();
        }
    }
}