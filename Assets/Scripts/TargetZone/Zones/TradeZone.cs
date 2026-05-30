using System.Collections.Generic;
using AI;
using Offers;
using TargetZone.Command;
using TargetZone.Interfaces;
using UI;
using UnityEngine;
using VContainer;

namespace TargetZone.Zones
{
    public class TradeZone : BaseInteractionZone
    {
        private CustomerController _currentCustomer;
        private BreakDealCommand _breakDealCommand;

        [Inject] private OfferTimeout _offerTimeout;
        [Inject] private TradeCanvas _tradeCanvas;
        
        protected override void OnTriggerEnter(Collider other)
        {
            if (_currentCustomer == null && other.TryGetComponent(out CustomerController customer))
            {
                _currentCustomer = customer;
                _tradeCanvas.Show();
                _offerTimeout.AddTimer(_currentCustomer, actionOnOut: ExecuteCommand);
            }

            base.OnTriggerEnter(other);
        }

        protected override void OnTriggerExit(Collider other)
        {
            if (_currentCustomer != null && other.TryGetComponent(out CustomerController customer))
            {
                _currentCustomer = null;
                _tradeCanvas.Close();
            }
            
            base.OnTriggerExit(other);
        }

        private void ExecuteCommand()
        {
            _breakDealCommand?.Execute();
            RefreshPanel();
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
