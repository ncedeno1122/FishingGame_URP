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

            // Create Test Points! Should probably happen in Aiming though...
            //SetTestPoints(10);
            FindTestPointsUntilCollision();
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

        // + + + + | Functions | + + + + 

        private float KinematicEquation1(float v0, float a, float t)
        {
            return v0 + (a * t); // Returns v1, missing deltaX
        }

        private float KEquation2(float v1, float v0, float t)
        {
            return t * ((v1 + v0) / 2); // Returns deltaX, missing a
        }

        private float KEquation3(float v0, float a, float t)
        {
            return (v0 * t) + ((a * Mathf.Pow(t, 2)) / 2); // Returns deltaX, missing v1
        }

        private float KEquation4(float v0, float a, float deltaX)
        {
            return Mathf.Sqrt(Mathf.Pow(v0, 2) + (2 * (a * deltaX))); // Returns v1, missing t
        }

        private float CalculateHorizontalOffset(float t)
        {
            return KEquation2(x_v1, x_v0 * m_NormalizedCastPower, t); // deltaX
        }

        private float CalculateVerticalOffset(float t)
        {
            return KEquation3(y_v0, y_a, t); // deltaY
        }

        private Vector3 CreateRelativeTestPoint(Transform originTransform, float t)
        {
            var localOffsetVector3 = new Vector3(0f, CalculateVerticalOffset(t), CalculateHorizontalOffset(t));
            var transformedOffsetVector3 = originTransform.TransformPoint(localOffsetVector3);
            var worldOffsetVector3 = originTransform.position + transformedOffsetVector3;

            return worldOffsetVector3;
        }

        private void FindTestPointsUntilCollision()
        {
            m_FRGContext.m_KinematicTestPoints.Clear();

            int currTestPointNum = 0;
            int maxTestPointNum = 20;
            float testPointSpacing = 0.5f;

            List<Vector3> testPointList = new List<Vector3>();
            m_FRGContext.m_KinematicTestPoints.Clear();

            for (int i = 0; i < maxTestPointNum; i++)
            {
                currTestPointNum = i;
                var currTestPoint = CreateRelativeTestPoint(m_FRGContext.transform, currTestPointNum * testPointSpacing);
                testPointList.Insert(currTestPointNum, currTestPoint);

                // Check for collision between points
                if (currTestPointNum > 0)
                {
                    var currPoint = testPointList[currTestPointNum];
                    var lastPoint = testPointList[currTestPointNum - 1];
                    if (Physics.Raycast(lastPoint,  currPoint-lastPoint, out RaycastHit hitInfo, (lastPoint - currPoint).magnitude))
                    {
                        Debug.Log($"Hit {hitInfo.collider.gameObject.name}");
                        m_FRGContext.m_HitPoint = hitInfo.point;
                        break;
                    }
                }
            }

            m_FRGContext.m_KinematicTestPoints.AddRange(testPointList);
        }

        private void SetTestPoints(int numPoints)
        {
            m_FRGContext.m_KinematicTestPoints.Clear();

            for (int i = 0; i < numPoints; i++)
            {
                var finalTestPoint = CreateRelativeTestPoint(m_FRGContext.transform, (float)i);

                m_FRGContext.m_KinematicTestPoints.Add(finalTestPoint);
            }
        }

    }
}