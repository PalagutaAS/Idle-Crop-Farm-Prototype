using System;
using TargetZone.Interfaces;
using UnityEngine;

namespace TargetZone
{
    public abstract class BaseZoneLogic : MonoBehaviour, IZoneInteractionLogic
    {
        public abstract bool CanActivate { get; }
        public abstract IZoneContext GenerateContext();
        public abstract event Action OnContextUpdated;
        public abstract void HandleEnter(GameObject obj);
        public abstract void HandleExit(GameObject obj);
    }
}