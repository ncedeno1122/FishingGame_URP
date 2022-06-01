using System;
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

            m_CurrentState = new FishingRodIdle(this);
        }

        private void OnValidate()
        {
            if (!Animator)
            {
                Animator = GetComponent<Animator>();
            }

            if (BobberOriginTransform != null && !BobberOriginTransform)
            {
                BobberOriginTransform = transform.GetChild(0).GetChild(0);
            }
            
            if (BobberScript)
            {
                BobberTransform = BobberRb.transform;
                BobberRb = BobberScript.transform.GetComponent<Rigidbody>();
                BobberScript.FishingLineBase = BobberOriginTransform;
                BobberScript.m_FRGContext = this;
            }
        }

        // + + + + | Functions | + + + 

        public void LaunchBobber(float normalizedCastForce)
        {
            Debug.Log($"Launching Bobber from FishingRodGearContext with normalizedCastForce of {normalizedCastForce}!");
            BobberScript.CastBobberNonKinematic(transform.forward, normalizedCastForce);
        }

        public void ReturnBobber()
        {
            Debug.Log("Returning Bobber!");
            BobberScript.HandleReturnBobber();
        }
    }
}