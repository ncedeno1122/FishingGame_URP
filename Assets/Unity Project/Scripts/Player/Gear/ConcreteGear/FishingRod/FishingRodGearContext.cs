using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Unity_Project.Scripts.Player.Gear.ConcreteGear.FishingRod
{
    public class FishingRodGearContext : ConcreteGearContext
    {
        public Animator Animator;
        public Transform BobberTransform;
        public Rigidbody BobberRb;

        // Testing :)
        public List<Vector3> m_KinematicTestPoints = new List<Vector3>();
        public Vector3? m_HitPoint = null;

        private void Awake()
        {
            m_EquippableGearController = GetComponent<EquippableGearController>();
            Animator = GetComponent<Animator>();
            BobberTransform = transform.GetChild(0).GetChild(0).GetComponent<Transform>();
            BobberRb = BobberTransform.GetComponent<Rigidbody>(); // TODO: Get when Bobber collides with water/surface to change states.

            m_CurrentState = new FishingRodIdle(this);
        }

        private void OnDrawGizmos()
        {
            // Testing forward
            //Gizmos.DrawLine(transform.position, transform.position + (transform.TransformPoint(Vector3.up) * 2));
            
            if (m_KinematicTestPoints.Count <= 0) return;

            
            for(int i = 0; i < m_KinematicTestPoints.Count; i++)
            {
                Gizmos.color = Color.red;
                Gizmos.DrawSphere(m_KinematicTestPoints[i], 0.15f);

                Gizmos.color = Color.green;
                if (i + 1 < m_KinematicTestPoints.Count)
                {
                    Gizmos.DrawLine(m_KinematicTestPoints[i], m_KinematicTestPoints[i + 1]);
                }
            }

            if (m_HitPoint == null) return;

            Gizmos.color = Color.blue;
            Gizmos.DrawSphere((Vector3)m_HitPoint, 0.15f);
        }
    }
}