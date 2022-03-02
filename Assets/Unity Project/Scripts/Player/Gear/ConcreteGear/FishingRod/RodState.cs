using System.Collections;
using System.Collections.Generic;
using Unity_Project.Scripts.Player.Gear;
using UnityEngine;

public abstract class RodState : EquippableGear
{
    protected readonly EquippableGearManager m_EquippableGearManager;

    protected RodState(EquippableGearManager eqm)
    {
        m_EquippableGearManager = eqm;
    }

    public abstract void Enter();
    public abstract void Exit();
}
