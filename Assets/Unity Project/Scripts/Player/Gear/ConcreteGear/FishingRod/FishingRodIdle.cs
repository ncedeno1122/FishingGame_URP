using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Unity_Project.Scripts.Player.Gear.ConcreteGear.FishingRod
{
    public class FishingRodIdle : ConcreteGearState
    {
        private readonly FishingRodGearContext m_FRGContext;

        public FishingRodIdle(ConcreteGearContext ctx) : base(ctx)
        {
            m_FRGContext = ctx as FishingRodGearContext;
        }

        public override void Enter()
        {
            //Debug.Log("Entering FishingRodIdle");
            //m_FRGContext.Animator.ResetTrigger("PlayPrecast");
            //m_FRGContext.Animator.ResetTrigger("ReturnFromPrecast");
            //m_FRGContext.Animator.ResetTrigger("PlayCast");
            //m_FRGContext.Animator.ResetTrigger("PlayReturn");
        }

        public override void Exit()
        {
            //Debug.Log("Exiting FishingRodIdle");
        }
        public override void AdvanceStateFromAnimation()
        {
            //
        }

        public override void OnFireHeld()
        {
            //Debug.Log("Holding...");
        }

        public override void OnFirePressed()
        {
            //Debug.Log("Pressed!");

            // Transition to FishingRodAiming
            m_Context.ChangeState(new FishingRodAiming(m_Context));
        }

        public override void OnFireReleased()
        {
            //Debug.Log("Released!");
        }

    }
}