using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static ExtensionMethods;

namespace Unity_Project.Scripts.Player.Gear.ConcreteGear.FishingRod
{
    public class FishingRodGearContext : ConcreteGearContext
    {
        public Animator Animator;
        public Transform BobberOriginTransform;
        public Transform BobberTransform;
        public Rigidbody BobberRb;
        public BobberScript BobberScript;

        private void Awake()
        {
            m_EquippableGearController = GetComponent<EquippableGearController>();
            Animator = GetComponent<Animator>();

            // Bobber Components
            BobberOriginTransform = transform.GetChild(0).GetChild(0);
            BobberTransform = BobberOriginTransform.GetChild(0);
            BobberRb = BobberTransform.GetComponent<Rigidbody>(); // TODO: Get when Bobber collides with water/surface to change states.
            BobberScript = BobberTransform.GetComponent<BobberScript>();

            m_CurrentState = new FishingRodIdle(this);
        }

        // + + + + | Functions | + + + 

        public void LaunchBobber(float normalizedCastForce)
        {
            Debug.Log($"Launching Bobber from FishingRodGearContext with normalizedCastForce of {normalizedCastForce}!");
            BobberScript.CastBobberNonKinematic(transform.forward, normalizedCastForce);
        }
    }
}