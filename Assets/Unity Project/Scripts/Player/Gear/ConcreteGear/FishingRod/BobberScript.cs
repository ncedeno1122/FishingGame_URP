using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Unity_Project.Scripts.Player.Gear.ConcreteGear.FishingRod
{
    public class BobberScript : MonoBehaviour
    {
        public const float CASTTIME_MAXIMUM = 3f;
        public Vector3 OriginPoint;

        public FishingRodGearContext m_FRGContext;
        public Rigidbody m_Rigidbody;

        private void Awake()
        {
            m_FRGContext = transform.parent.parent.GetComponent<FishingRodGearContext>();
            m_Rigidbody = GetComponent<Rigidbody>();
        }

        private void Start()
        {
            OriginPoint = transform.position;
        }

        // + + + + | Functions | + + + +

        public void CastBobberNonKinematic(Vector3 playerForward)
        {
            // Cast!
            DetachFromFRGContext();
            DeactivateKinematic();
            m_Rigidbody.AddForce(playerForward.normalized * 10f, ForceMode.Impulse);

        }

        private void ActivateKinematic() => m_Rigidbody.isKinematic = true;
        private void DeactivateKinematic() => m_Rigidbody.isKinematic = false;

        private void DetachFromFRGContext()
        {
            transform.parent = null;
        }

        private void ReattachToFRGContext()
        {
            transform.parent = m_FRGContext.transform.GetChild(0);
        }
    }
}