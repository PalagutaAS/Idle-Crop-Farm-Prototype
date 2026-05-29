using System.Collections.Generic;
using AI;
using TargetZone.Command;
using TargetZone.Interfaces;
using UnityEngine;

namespace TargetZone.Zones
{
    public class TradeZone : BaseInteractionZone
    {
        private CustomerController _currentCustomer;
        
        protected override void OnTriggerEnter(Collider other)
        {
            if (_currentCustomer == null && other.TryGetComponent(out CustomerController customer))
            {
                _currentCustomer = customer;
            }

            base.OnTriggerEnter(other);
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
            return (_currentCustomer) ? new List<IInteractionCommand>
            {
                new MakeDealCommand(_currentCustomer),
                new BreakDealCommand(_currentCustomer)
            } : new List<IInteractionCommand>();

        }
    }
}
