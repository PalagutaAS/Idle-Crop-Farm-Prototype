using System;
using UnityEngine;

namespace Offers
{
    public class OfferTimerModel
    {
        public float Duration { get; }
        public float Elapsed { get; private set; }
        public bool IsActive { get; private set; }

        public event Action OnTimeout;

        public OfferTimerModel(float duration)
        {
            Duration = duration;
        }

        public void Start()
        {
            Elapsed = 0f;
            IsActive = true;
        }

        public void Stop()
        {
            IsActive = false;
        }

        public float Tick(float deltaTime)
        {
            if (!IsActive) return 0;

            Elapsed += deltaTime;
            if (Elapsed >= Duration)
            {
                Elapsed = Duration;
                IsActive = false;
                OnTimeout?.Invoke();
            }

            return GetRemainingRatio();
        }

        private float GetRemainingRatio() => 
            Duration > 0f ? Mathf.Clamp01(1f - Elapsed / Duration) : 0f;
    }
}