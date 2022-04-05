using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Unity_Project.Scripts.Player.Gear.ConcreteGear.FishingRod
{
    public class FishingRodGearContext : ConcreteGearContext
    {
        public Animator Animator;
        public Transform BobberTransform;
        public Transform BobberPreviewTransform;
        public Rigidbody BobberRb;
        public BobberScript BobberScript;

        // Testing :)
        public List<Vector3> LaunchPath = new List<Vector3>();
        public Vector3 m_HitPoint;
        public const int NUM_TESTPOINTS = 10;
        public Vector3[] KinematicTestArray = new Vector3[NUM_TESTPOINTS];
        public bool ShowGizmos;

        // Horizontal Equation
        public float x_v0 = 6.5f; // Scaled by NormalizedCastPower!
        public float x_v1 = 0f;

        // Vertical Equation
        public float y_v0 = 5.5f; // Scaled by NormalizedCastPower
        public float y_a = -3.5f;

        private void Awake()
        {
            m_EquippableGearController = GetComponent<EquippableGearController>();
            Animator = GetComponent<Animator>();
            BobberTransform = transform.GetChild(0).GetChild(0);
            BobberPreviewTransform = transform.GetChild(1);
            BobberRb = BobberTransform.GetComponent<Rigidbody>(); // TODO: Get when Bobber collides with water/surface to change states.
            BobberScript = BobberTransform.GetComponent<BobberScript>();

            // Hide initially.
            BobberPreviewTransform.gameObject.SetActive(false);

            m_CurrentState = new FishingRodIdle(this);
        }

        private void OnDrawGizmos()
        {
            if (!ShowGizmos) return;

            Gizmos.color = Color.yellow;
            for (float i = 0f; i < 20f; i += 0.5f)
            {
                var testPointPosition = CreateRelativeTestPoint(transform, i);
                Gizmos.DrawSphere(testPointPosition, 0.15f);
                if (i > 0)
                {
                    Gizmos.DrawLine(CreateRelativeTestPoint(transform, i - 0.5f), testPointPosition);
                }
            }

            // Testing forward

            if (m_HitPoint == null) return;

            Gizmos.color = Color.blue;
            Gizmos.DrawSphere(m_HitPoint, 0.15f);
        }

        // + + + + | Functions | + + + +
        public float KinematicEquation1(float v0, float a, float t)
        {
            return v0 + (a * t); // Returns v1, missing deltaX
        }

        public float KEquation2(float v1, float v0, float t)
        {
            return t * ((v1 + v0) / 2); // Returns deltaX, missing a
        }

        public float KEquation3(float v0, float a, float t)
        {
            return (v0 * t) + ((a * Mathf.Pow(t, 2)) / 2); // Returns deltaX, missing v1
        }

        public float KEquation4(float v0, float a, float deltaX)
        {
            return Mathf.Sqrt(Mathf.Pow(v0, 2) + (2 * (a * deltaX))); // Returns v1, missing t
        }

        public float CalculateHorizontalOffset(float t)
        {
            var xRotationEulers = transform.rotation.eulerAngles.x;
            return KEquation2(x_v1, x_v0 * (Mathf.Sin(Mathf.Abs(xRotationEulers + 90f) * Mathf.Deg2Rad)), t); // deltaX
        }

        public float CalculateVerticalOffset(float t)
        {
            var xRotationEulers = transform.rotation.eulerAngles.x;
            return KEquation3(y_v0 * (Mathf.Sin(((xRotationEulers * -1) + 45f) * Mathf.Deg2Rad)), y_a, t); // deltaY
        }

        public Vector3 CreateRelativeTestPoint(Transform originTransform, float t)
        {
            var yRotationEulers = originTransform.rotation.eulerAngles.y;
            var localOffsetVector3 = new Vector3(0f, CalculateVerticalOffset(t), CalculateHorizontalOffset(t));
            var mixedVectorOnlyXZ = new Vector3(originTransform.position.x + (Mathf.Sin(Mathf.Deg2Rad * yRotationEulers) * localOffsetVector3.z),
                                                originTransform.position.y + localOffsetVector3.y,
                                                originTransform.position.z + (Mathf.Cos(Mathf.Deg2Rad * yRotationEulers) * localOffsetVector3.z));
            return mixedVectorOnlyXZ;
        }

        public void CalculateTestPoints()
        {
            for (int i = 0; i < NUM_TESTPOINTS; i++)
            {
                KinematicTestArray[i] = CreateRelativeTestPoint(transform, (float)i);
            }
        }

        public Vector3 FindCollisionPointInTestPoints()
        {
            LaunchPath.Clear();

            for (int i = 1; i < NUM_TESTPOINTS; i++)
            {
                var currTestPoint = KinematicTestArray[i];
                var lastTestPoint = KinematicTestArray[i - 1];

                LaunchPath.Add(lastTestPoint);

                if (Physics.Raycast(lastTestPoint, currTestPoint-lastTestPoint, out RaycastHit hitInfo, (lastTestPoint-currTestPoint).magnitude))
                {
                    m_HitPoint = hitInfo.point;
                    LaunchPath.Add(m_HitPoint);
                    return hitInfo.point;
                }
            }

            return Vector3.zero;
        }

        public void LaunchBobber()
        {
            Debug.Log("Launching Bobber from FishingRodGearContext!");
            BobberScript.HandleFlyAlongCastPath(ref LaunchPath);
        }

        /*
        public void FindTestPointsUntilCollision()
        {
            m_KinematicTestPoints.Clear();

            int currTestPointNum = 0;
            int maxTestPointNum = 20;
            float testPointSpacing = 0.5f;

            List<Vector3> testPointList = new List<Vector3>();
            m_KinematicTestPoints.Clear();

            for (int i = 0; i < maxTestPointNum; i++)
            {
                currTestPointNum = i;
                var currTestPoint = CreateRelativeTestPoint(transform, currTestPointNum * testPointSpacing);
                testPointList.Insert(currTestPointNum, currTestPoint);

                // Check for collision between points
                if (currTestPointNum > 0)
                {
                    var currPoint = testPointList[currTestPointNum];
                    var lastPoint = testPointList[currTestPointNum - 1];
                    if (Physics.Raycast(lastPoint, currPoint - lastPoint, out RaycastHit hitInfo, (lastPoint - currPoint).magnitude))
                    {
                        Debug.Log($"Hit {hitInfo.collider.gameObject.name}");
                        m_HitPoint = hitInfo.point;
                        BobberPreviewTransform.position = hitInfo.point;
                        break;
                    }
                }
            }

            m_KinematicTestPoints.AddRange(testPointList);
        }
        */
    }
}