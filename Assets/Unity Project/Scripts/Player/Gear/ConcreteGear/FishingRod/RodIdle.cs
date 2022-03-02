using System.Collections;
using System.Collections.Generic;
using Unity_Project.Scripts.Player.Gear;
using UnityEngine;

public class RodIdle : RodState
{
    public RodIdle(EquippableGearManager eqm) : base(eqm)
    {
        //
    }

    public override void Enter()
    {
        Debug.Log("Entering RodIdle!");
    }

    public override void Exit()
    {
        Debug.Log("Exiting RodIdle!");
    }

    public override void OnFireHeld()
    {
        // TODO: Switch state to RodAim
    }

    public override void OnFirePressed()
    {
        //
    }

    public override void OnFireReleased()
    {
        //
    }
}

/* Animation States
 * 
 * Idle
 * Aim
 * Casting
 * Reeling
 * Idle
 */