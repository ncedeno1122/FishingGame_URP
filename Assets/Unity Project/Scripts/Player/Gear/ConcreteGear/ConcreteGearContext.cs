using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Unity_Project.Scripts.Player.Gear.ConcreteGear
{
    /// <summary>
    /// Serves as the contexts for handling ConcreteGearStates that are controlled by EquippableGearControllers.
    /// </summary>
    [RequireComponent(typeof(EquippableGearController))]
    public abstract class ConcreteGearContext : MonoBehaviour, IFireInputListener
    {
        protected EquippableGearController m_EquippableGearController;

        protected ConcreteGearState m_CurrentState; // TODO: Initialize with default states per subclass.

        // + + + + | Functions | + + + + 

        public void ChangeState(ConcreteGearState newState)
        {
            m_CurrentState.Exit();
            m_CurrentState = newState;
            m_CurrentState.Enter();
        }

        public void OnFireHeld()
        {
            m_CurrentState.OnFireHeld();
        }

        public void OnFirePressed()
        {
            m_CurrentState.OnFirePressed();
        }

        public void OnFireReleased()
        {
            m_CurrentState.OnFireReleased();
        }
    }
}