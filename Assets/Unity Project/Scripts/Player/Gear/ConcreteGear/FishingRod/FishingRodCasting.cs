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
        // Horizontal Equation
        private const float x_v0 = 10f; // Scaled by NormalizedCastPower!
        private const float x_v1 = 0f;

        // Vertical Equation
        private const float y_v0 = 5f; // Scaled by NormalizedCastPower
        private const float y_a = -3.5f;

        private float m_NormalizedCastPower;

        private readonly FishingRodGearContext m_FRGContext;

        public FishingRodCasting(ConcreteGearContext ctx, float normalizedCastPower) : base(ctx)
        {
            m_FRGContext = ctx as FishingRodGearContext;
            m_NormalizedCastPower = normalizedCastPower;
        }

        public override void AdvanceStateFromAnimation()
        {
            //
        }

        public override void Enter()
        {
            Debug.Log("Entering FishingRodCasting!");
            m_FRGContext.Animator.SetBool("HasLineBeenCast", true);
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