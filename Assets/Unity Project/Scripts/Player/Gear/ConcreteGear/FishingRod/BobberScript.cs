using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.Serialization;

namespace Unity_Project.Scripts.Player.Gear.ConcreteGear.FishingRod
{
    [RequireComponent(typeof(Rigidbody))]
    //[RequireComponent(typeof(FixedJoint))]
    public class BobberScript : MonoBehaviour
    {
        public bool IsBobberActive;
        private const float CAST_FORCE_MIN = 10f;
        private const float CAST_FORCE_SCALAR = 10f;
        private readonly Vector3 m_BobberOffsetPosition = new Vector3(0f, -1f, 0.3f);

        public Transform FishingLineBase;
        public FishingRodGearContext m_FRGContext;
        private Rigidbody m_Rigidbody;
        public Rigidbody Rigidbody
        {
            get => m_Rigidbody;
            private set => m_Rigidbody = value;
        }

        private FixedJoint m_OriginFixedJoint;

        private void Awake()
        {
            m_Rigidbody = GetComponent<Rigidbody>();
            m_OriginFixedJoint = GetComponent<FixedJoint>();
        }

        private void Start()
        {
            AttachBobber();
        }

        // + + + + | Functions | + + + +

        public void CastBobberNonKinematic(Vector3 playerForward, float normalizedCastForce)
        {
            // Destroy FixedJoint
            var fixedJoint = GetComponent<FixedJoint>();
            Destroy(fixedJoint);
            
            // Cast!
            //DeactivateKinematic();

            var castPowerFinal = CAST_FORCE_MIN + (CAST_FORCE_SCALAR * normalizedCastForce);
            m_Rigidbody.AddForce(playerForward.normalized * castPowerFinal, ForceMode.Impulse);
            
            IsBobberActive = true;
            Debug.Log($"Launching Bobber with force of {castPowerFinal}! normalizedCastForce is {normalizedCastForce}.");
        }

        public void HandleReturnBobber()
        {
            AttachBobber();
            IsBobberActive = false;
        }

        private void ActivateKinematic() => m_Rigidbody.isKinematic = true;
        private void DeactivateKinematic() => m_Rigidbody.isKinematic = false;

        private void AttachBobber()
        {
            // Activate Joint
            var fixedJoint = gameObject.AddComponent<FixedJoint>();
            fixedJoint.connectedBody = FishingLineBase.GetComponent<Rigidbody>();
            fixedJoint.autoConfigureConnectedAnchor = false;
            fixedJoint.connectedAnchor = new Vector3(0f,-1f,0.3f);
        }

        // + + + + | Collision Handling | + + + +


    }
}