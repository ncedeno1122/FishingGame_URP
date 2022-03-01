using UnityEngine;

namespace Unity_Project.Scripts.Player.Gear
{
    public abstract class EquippableGear
    {
        public abstract void OnFirePressed();
        public abstract void OnFireHeld();
        public abstract void OnFireReleased();
    }
}
