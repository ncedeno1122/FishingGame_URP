using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Unity_Project.Scripts.Player.Gear.ConcreteGear.FishingRod
{
    public class FishingRodReeling : ConcreteGearState
    {
        private const float REELING_FORCE = 10f;
        private float m_DistanceFromOrigin;

        private readonly Transform m_BobberOrigin;
        private readonly Rigidbody m_BobberRB;
        private readonly FishingRodGearContext m_FRGContext;

        public FishingRodReeling(ConcreteGearContext ctx) : base(ctx)
        {
            m_FRGContext = ctx as FishingRodGearContext;
            m_BobberOrigin = m_FRGContext.BobberOriginTransform;
            m_BobberRB = m_FRGContext.BobberRb;
        }

        public override void AdvanceStateFromAnimation()
        {
            //
        }

        public override void Enter()
        {
            Debug.Log("Entering FishingRodReeling!");
        }

        public override void Exit()
        {
            Debug.Log("Exiting FishingRodReeling!");
            m_FRGContext.Animator.SetBool("HasLineBeenCast", false);
        }

        public override void OnFireHeld()
        {
            var distToOrigin = Vector3.Distance(m_BobberOrigin.position, m_BobberRB.position);
            
            if (distToOrigin < 0.5f)
            {
                Debug.Log("Reeled back in!");

                // Reel it back in!
                m_FRGContext.ReturnBobber();
                m_FRGContext.ChangeState(new FishingRodIdle(m_Context));
            }
            else
            {
                Debug.Log($"Distance to reel: {distToOrigin}");
                var forceToOriginVector = m_BobberOrigin.position - m_BobberRB.position;
                m_BobberRB.AddForce(forceToOriginVector.normalized * REELING_FORCE);
            }
        }

        public override void OnFirePressed()
        {
            m_FRGContext.Animator.SetBool("IsHoldingFire", true);
        }

        public override void OnFireReleased()
        {
            m_FRGContext.Animator.SetBool("IsHoldingFire", false);
        }
    }
}
