using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static ExtensionMethods;

namespace Unity_Project.Scripts.Player.Gear.ConcreteGear.FishingRod
{
    public class FishingRodGearContext : ConcreteGearContext
    {
        public Animator Animator;
        public Transform BobberTransform;
        public Transform BobberPreviewTransform;
        public Rigidbody BobberRb;
        public BobberScript BobberScript;

        // Horizontal Equation
        public float x_v0 = 6.5f; // Scaled by NormalizedCastPower!
        public float x_v1 = 0f;

        // Vertical Equation
        public float y_v0 = 5.5f; // Scaled by NormalizedCastPower
        public float y_a = Physics.gravity.y;

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

        // + + + + | Functions | + + + 

        public void LaunchBobber()
        {
            Debug.Log("Launching Bobber from FishingRodGearContext!");
            BobberScript.CastBobberNonKinematic(transform.forward);
        }
    }
}