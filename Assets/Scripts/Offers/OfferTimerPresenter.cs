using System;
using UI;
using UnityEngine;
using VContainer.Unity;

namespace Offers
{
    public class OfferTimerPresenter : IOfferTimeout, IDisposable, ITickable
    {
        private readonly IOfferTimerView _view;
        private float _duration = 10f;
        private OfferTimerModel _model;
        private Offer _offer;
        private Action _actionOnTimeout;

        public OfferTimerPresenter(IOfferTimerView view)
        {
            _view = view;
        }
        
        public void StartTimer(Offer offer, Action onTimeout)
        {
            StopTimer();
            _offer = offer;
            _offer.OnCancel += OnCancelDeal;
            _actionOnTimeout = onTimeout;
            _model = new OfferTimerModel(_duration);
            _model.OnTimeout += HandleTimeout;
            _model.Start();

            _view.Show();
            _view.UpdateFill(1f);
        }

        public void StopTimer()
        {
            if (_model == null) return;

            _model.OnTimeout -= HandleTimeout;
            _offer.OnCancel -= OnCancelDeal;
            _model.Stop();
            _view.Hide();
            _model = null;
            _offer = null;
            _actionOnTimeout = null;
        }

        private void OnCancelDeal(IOfferDisplayData obj) => StopTimer();

        public void Dispose() => StopTimer();

        private void HandleTimeout()
        {
            _actionOnTimeout?.Invoke();
            Dispose();
        }

        public void Tick()
        {
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