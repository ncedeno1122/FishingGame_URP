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
        public Vector3 OriginPoint;
        public Vector3 OriginalScale;
        public Quaternion OriginalRotation;

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
            OriginPoint = m_Rigidbody.position;
            OriginalScale = transform.lossyScale;
            OriginalRotation = m_Rigidbody.rotation;
        }

        // + + + + | Functions | + + + +

        public void CastBobberNonKinematic(Vector3 playerForward, float normalizedCastForce)
        {
            // Cast!
            DetachFromFRGContext();
            DeactivateKinematic();

            var castPowerFinal = CAST_FORCE_MIN + (CAST_FORCE_SCALAR * normalizedCastForce);
            m_Rigidbody.AddForce(playerForward.normalized * castPowerFinal, ForceMode.Impulse);
            
            IsBobberActive = true;
            Debug.Log($"Launching Bobber with force of {castPowerFinal}! normalizedCastForce is {normalizedCastForce}.");
        }

        public void HandleReturnBobber()
        {
            ActivateKinematic();
            ReattachToFRGContext();
            IsBobberActive = false;
        }

        private void ActivateKinematic() => m_Rigidbody.isKinematic = true;
        private void DeactivateKinematic() => m_Rigidbody.isKinematic = false;

        private void DetachFromFRGContext()
        {
            transform.parent = null;
        }

        private void ReattachToFRGContext()
        {
            //transform.SetParent(m_BobberOriginTransform, false);
            m_Rigidbody.position = OriginPoint;
            m_Rigidbody.rotation = OriginalRotation;
            transform.localScale = OriginalScale;
        }

        // + + + + | Collision Handling | + + + +


    }
}