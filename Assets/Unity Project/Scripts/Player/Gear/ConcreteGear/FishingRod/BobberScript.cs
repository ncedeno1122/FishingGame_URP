using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Unity_Project.Scripts.Player.Gear.ConcreteGear.FishingRod
{
    public class BobberScript : MonoBehaviour
    {
        public bool IsBobberActive;
        public const float CAST_FORCE_MIN = 10f;
        public const float CAST_FORCE_SCALAR = 10f;

        public Transform m_BobberOriginTransform;
        public FishingRodGearContext m_FRGContext;
        public Rigidbody m_Rigidbody;

        private void Awake()
        {
            m_Rigidbody = GetComponent<Rigidbody>();
        }

        private void Start()
        {
            AttachBobber();
        }

        // + + + + | Functions | + + + +

        public void CastBobberNonKinematic(Vector3 playerForward, float normalizedCastForce)
        {
            // Cast!
            DeactivateKinematic();

            var castPowerFinal = CAST_FORCE_MIN + (CAST_FORCE_SCALAR * normalizedCastForce);
            m_Rigidbody.AddForce(playerForward.normalized * castPowerFinal, ForceMode.Impulse);
            
            IsBobberActive = true;
            Debug.Log($"Launching Bobber with force of {castPowerFinal}! normalizedCastForce is {normalizedCastForce}.");
        }

        public void HandleReturnBobber()
        {
            ActivateKinematic();
            IsBobberActive = false;
        }

        private void ActivateKinematic() => m_Rigidbody.isKinematic = true;
        private void DeactivateKinematic() => m_Rigidbody.isKinematic = false;

        private void AttachBobber()
        {
            // Prepare bobber
            m_Rigidbody.isKinematic = true;

            // Move bobber
            m_Rigidbody.position = m_BobberOriginTransform.position;
        }

        // + + + + | Collision Handling | + + + +


    }
}