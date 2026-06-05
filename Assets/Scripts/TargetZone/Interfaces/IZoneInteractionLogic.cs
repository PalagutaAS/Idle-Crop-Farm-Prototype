using System;
using TargetZone.Interfaces;
using UnityEngine;

namespace TargetZone
{
    public interface IZoneInteractionLogic 
    {
        public bool CanActivate { get; }
        public IZoneContext GenerateContext();
        public event Action OnContextUpdated;
        public void HandleEnter(GameObject obj);
        public void HandleExit(GameObject obj);
    }
}