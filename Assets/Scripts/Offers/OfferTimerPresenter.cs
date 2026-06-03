using System;
using UI;
using UnityEngine;
using VContainer.Unity;

namespace Offers
{
    public class OfferTimerPresenter : IOfferTimeout, IDisposable, ITickable
    {
        private float _duration = 10f;
        private IOfferTimerView _view;
        private OfferTimerModel _model;
        private Offer _offer;
        private Action _action;

        public OfferTimerPresenter(IOfferTimerView view)
        {
            _view = view;
        }
        
        public void StartTimer(Offer offer, Action onTimeout)
        {
            StopTimer();
            _offer = offer;
            _action = onTimeout;
            _model = new OfferTimerModel(_duration);
            _model.OnTimeout += HandleTimeout;
            _model.Start();

            _view.Show(_model.Duration);
            _view.UpdateFill(1f);
        }

        public void StopTimer()
        {
            if (_model == null) return;

            _model.OnTimeout -= HandleTimeout;
            _model.Stop();
            _view.Hide();
            _model = null;
            _offer = null;
            _action = null;
        }
        
        private void HandleTimeout()
        {
            _action?.Invoke();
            StopTimer();
        }

        public void Dispose() => StopTimer();
        
        public void Tick()
        {
            if (_offer != null && !_offer.Active)
                StopTimer();
            
            if (_model == null || !_model.IsActive) return;
            
            _view.UpdateFill(_model.Tick(Time.deltaTime));
        }
    }

    public interface IOfferTimeout
    {
        public void StartTimer(Offer offer, Action actionOnOut);
        public void StopTimer();
    }
}