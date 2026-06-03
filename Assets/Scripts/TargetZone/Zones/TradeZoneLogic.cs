using System;
using System.Collections.Generic;
using AI;
using Offers;
using Player.Interface;
using TargetZone.Command;
using TargetZone.Interfaces;
using UnityEngine;
using VContainer;

namespace TargetZone.Zones
{
    public class TradeZoneLogic : BaseZoneLogic
    {
        [Inject] private IOfferTimeout _offerTimerPresenter;
        private CustomerController _currentCustomer;
        private BreakDealCommand _breakDealCommand;
        private bool _playerInside;

        public override event Action OnContextUpdated;
        public override bool CanActivate => _playerInside && _currentCustomer != null;
        
        public override IZoneContext GenerateContext()
        {
            if (!CanActivate) return ZoneContext.EmptyContext();
            _breakDealCommand = new BreakDealCommand(_currentCustomer);
            var commands = new List<IInteractionCommand> { new MakeDealCommand(_currentCustomer), _breakDealCommand };
            return new ZoneContext(commands, _currentCustomer.Offer);
        }

        public override void HandleEnter(GameObject obj)
        {
            if (obj.TryGetComponent<IPlayer>(out _))
                _playerInside = true;
            else if (obj.TryGetComponent<CustomerController>(out var c))
            {
                _currentCustomer = c;
                _offerTimerPresenter.StartTimer(c.Offer, OnOfferExpired);
            }
            NotifyContextUpdated();
        }

        public override void HandleExit(GameObject obj)
        {
            if (obj.TryGetComponent<IPlayer>(out _)) _playerInside = false;
            else if (obj.TryGetComponent<CustomerController>(out var c) && c == _currentCustomer)
            {
                _currentCustomer = null;
                _offerTimerPresenter.StopTimer();
            }
            NotifyContextUpdated();
        }

        private void OnOfferExpired()
        {
            _breakDealCommand?.Execute();
            _currentCustomer = null;
            NotifyContextUpdated();
        }

        private void NotifyContextUpdated() => OnContextUpdated?.Invoke();
    }
}