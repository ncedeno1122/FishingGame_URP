using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Unity_Project.Scripts.Player.Gear.ConcreteGear.FishingRod
{
    public class FishingRodIdle : ConcreteGearState
    {
        public FishingRodIdle(ConcreteGearContext ctx) : base(ctx)
        {

        }

        public override void Enter()
        {
            Debug.Log("Entering FishingRodIdle");
        }

        public override void Exit()
        {
            Debug.Log("Exiting FishingRodIdle");
        }

        public override void OnFireHeld()
        {
            Debug.Log("Holding...");
        }

        public override void OnFirePressed()
        {
            Debug.Log("Pressed!");
        }

        public override void OnFireReleased()
        {
            Debug.Log("Released!");
        }
    }
}