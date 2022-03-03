using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Unity_Project.Scripts.Player.Gear
{
    /// <summary>
    /// Provides a simple interface that allows for multiple functions with
    /// the new input system.
    /// </summary>
    public interface IFireInputListener
    {
        void OnFirePressed();
        void OnFireHeld();
        void OnFireReleased();
    }
}