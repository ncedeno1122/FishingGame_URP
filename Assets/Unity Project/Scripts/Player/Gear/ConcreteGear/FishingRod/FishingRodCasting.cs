using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Unity_Project.Scripts.Player.Gear.ConcreteGear.FishingRod
{
    /// <summary>
    /// A state for when actually casting and launching the Bobber rigidbody out.
    /// </summary>
    public class FishingRodCasting : ConcreteGearState
    {
        private float m_NormalizedCastForce;

        private readonly FishingRodGearContext m_FRGContext;

        public FishingRodCasting(ConcreteGearContext ctx, float normalizedCastForce) : base(ctx)
        {
            m_FRGContext = ctx as FishingRodGearContext;
            m_NormalizedCastForce = normalizedCastForce;
        }

        public override void AdvanceStateFromAnimation()
        {
            m_Context.ChangeState(new FishingRodReeling(m_Context));
        }

        public override void Enter()
        {
            Debug.Log("Entering FishingRodCasting!");
            m_FRGContext.Animator.SetBool("HasLineBeenCast", true);
            m_FRGContext.LaunchBobber(m_NormalizedCastForce);
        }

        public override void Exit()
        {
            Debug.Log("Exiting FishingRodCasting!");
        }

        public override void OnFireHeld()
        {
            //
        }

        public override void OnFirePressed()
        {
            //
        }

        public override void OnFireReleased()
        {
            //
        }
    }
}