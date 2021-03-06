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
        //private float m_NormalizedCastPower;

        private readonly FishingRodGearContext m_FRGContext;

        public FishingRodCasting(ConcreteGearContext ctx) : base(ctx)
        {
            m_FRGContext = ctx as FishingRodGearContext;
        }

        public override void AdvanceStateFromAnimation()
        {
            //
        }

        public override void Enter()
        {
            Debug.Log("Entering FishingRodCasting!");
            m_FRGContext.Animator.SetBool("HasLineBeenCast", true);
            m_FRGContext.LaunchBobber();
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