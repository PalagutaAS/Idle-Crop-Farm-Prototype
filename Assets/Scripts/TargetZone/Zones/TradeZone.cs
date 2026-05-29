using System.Collections.Generic;
using AI;
using Offers;
using TargetZone.Command;
using TargetZone.Interfaces;
using UnityEngine;
using VContainer;

namespace TargetZone.Zones
{
    public class TradeZone : BaseInteractionZone
    {
        private CustomerController _currentCustomer;
        private BreakDealCommand _breakDealCommand;

        [Inject] private OfferTimeout _offerTimeout;
        
        protected override void OnTriggerEnter(Collider other)
        {
            if (_currentCustomer == null && other.TryGetComponent(out CustomerController customer))
            {
                _currentCustomer = customer;
                _offerTimeout.AddTimer(_currentCustomer);
                _offerTimeout.OnTimerOut += ExecuteCommand;
            }

            base.OnTriggerEnter(other);
        }

        private void ExecuteCommand()
        {
            _breakDealCommand.Execute();
            _offerTimeout.OnTimerOut -= ExecuteCommand;
            RefreshPanel();
        }

        protected override void OnTriggerExit(Collider other)
        {
            if (_currentCustomer != null && other.TryGetComponent(out CustomerController customer))
            {
                _currentCustomer = null;
                RefreshPanel();
            }
            
            base.OnTriggerExit(other);
        }

        protected override bool CanOpenPanel()
        {
            return Player != null;
        }

        protected override List<IInteractionCommand> GenerateCommands()
        {
            if (_currentCustomer == null)
                return new List<IInteractionCommand>();
            
            _breakDealCommand = new BreakDealCommand(_currentCustomer);
            
            return new List<IInteractionCommand>
            {
                new MakeDealCommand(_currentCustomer),
                _breakDealCommand,
            };
        }
    }
}
