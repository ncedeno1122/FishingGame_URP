using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Unity_Project.Scripts.Player.Gear.ConcreteGear.FishingRod
{
    public class FishingRodAiming : ConcreteGearState
    {
        public const float CAST_TIME_MINIMUM = 0.25f;
        public const float CAST_TIME_MAXIMUM = 1.5f;
        public float CastTimeValue = 0f;

        private readonly FishingRodGearContext m_FRGContext;

        public FishingRodAiming(ConcreteGearContext ctx) : base(ctx)
        {
            m_FRGContext = ctx as FishingRodGearContext;
        }

        public override void Enter()
        {
            // Animation
            m_FRGContext.Animator.SetBool("IsHoldingFire", true);

            // Bobber Preview
            m_FRGContext.BobberPreviewTransform.gameObject.SetActive(true);
        }

        public override void Exit()
        {
            m_FRGContext.Animator.SetBool("IsHoldingFire", false);
        }

        public override void AdvanceStateFromAnimation()
        {
            //
        }

        public override void OnFireHeld()
        {
            CastTimeValue += Time.deltaTime;

            m_FRGContext.CalculateTestPoints();
            for (int i = 1; i < m_FRGContext.KinematicTestArray.Length; i++)
            {
                var last = m_FRGContext.KinematicTestArray[i - 1];
                var curr = m_FRGContext.KinematicTestArray[i];
                Debug.DrawLine(last, curr, Color.red);
            }

            var collisionPoint = m_FRGContext.FindCollisionPointInTestPoints();
            if (collisionPoint != Vector3.zero)
            {
                m_FRGContext.BobberPreviewTransform.position = collisionPoint;
            }
        }

        public override void OnFirePressed()
        {
            //
        }

        public override void OnFireReleased()
        {
            if (CastTimeValue > CAST_TIME_MINIMUM)
            {
                var castPower = Mathf.Clamp01(CastTimeValue / CAST_TIME_MAXIMUM);
                //Debug.Log($"Valid Cast ({CastTimeValue}; {castPower *  100f}% Power

                m_Context.ChangeState(new FishingRodCasting(m_Context));

                var collisionPoint = m_FRGContext.FindCollisionPointInTestPoints();
                if (collisionPoint != Vector3.zero)
                {
                    m_FRGContext.BobberPreviewTransform.position = collisionPoint;
                }
            }
            else
            {
                //Debug.Log("Invalid Cast, return to Idle...");
                m_Context.ChangeState(new FishingRodIdle(m_Context));
            }
        }
    }
}