using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Unity_Project.Scripts.Player.Gear.ConcreteGear.FishingRod
{
    public class BobberScript : MonoBehaviour
    {
        public const float CAST_FORCE_MIN = 10f;
        public const float CAST_FORCE_SCALAR = 10f;
        public Vector3 OriginPoint;

        public Transform m_BobberOriginTransform;
        public FishingRodGearContext m_FRGContext;
        public Rigidbody m_Rigidbody;

        private void Awake()
        {
            m_BobberOriginTransform = transform.parent;
            m_FRGContext = transform.parent.parent.parent.GetComponent<FishingRodGearContext>();
            m_Rigidbody = GetComponent<Rigidbody>();
        }

        private void Start()
        {
            OriginPoint = transform.position;
        }

        // + + + + | Functions | + + + +

        public void CastBobberNonKinematic(Vector3 playerForward, float normalizedCastForce)
        {
            // Cast!
            DetachFromFRGContext();
            DeactivateKinematic();
            var castPowerFinal = CAST_FORCE_MIN + (CAST_FORCE_SCALAR * normalizedCastForce);
            Debug.Log($"Launching Bobber with force of {castPowerFinal}! normalizedCastForce is {normalizedCastForce}.");
            m_Rigidbody.AddForce(playerForward.normalized * castPowerFinal, ForceMode.Impulse);

        }

        private void ActivateKinematic() => m_Rigidbody.isKinematic = true;
        private void DeactivateKinematic() => m_Rigidbody.isKinematic = false;

        private void DetachFromFRGContext()
        {
            transform.parent = null;
        }

        private void ReattachToFRGContext()
        {
            transform.parent = m_BobberOriginTransform;
        }

        private void ReturnToFRGContext()
        {

        }

        // + + + + | Collision Handling | + + + +


    }
}