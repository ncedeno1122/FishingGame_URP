using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Unity_Project.Scripts.Player.Gear.ConcreteGear
{
    // Provides an abstract base class to describe how ConcreteGearStates are made.
    public abstract class ConcreteGearState : IFireInputListener
    {
        protected readonly ConcreteGearContext m_Context;
        //protected ConcreteGearState m_NextState; // Must I store this? Not exactly like a LinkedList...
        //protected ConcreteGearState m_PreviousState;

        protected ConcreteGearState(ConcreteGearContext ctx)
        {
            m_Context = ctx;
        }

        public abstract void Enter();

        public abstract void Exit();

        public abstract void AdvanceStateFromAnimation();

        public abstract void OnFireHeld();

        public abstract void OnFirePressed();

        public abstract void OnFireReleased();
    }
}