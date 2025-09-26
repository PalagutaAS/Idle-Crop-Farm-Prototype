using System.Collections.Generic;
using AI;
using TargetZone.Command;
using TargetZone.Interfaces;
using UnityEngine;

namespace TargetZone
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
                _panel.Close();
            }
            
            base.OnTriggerExit(other);
        }

        protected override bool CanOpenPanel()
        {
            return _player != null && _currentCustomer != null;
        }

        protected override List<IInteractionCommand> GenerateCommands()
        {
            return new List<IInteractionCommand>
            {
                new MakeDealCommand(_currentCustomer),
                new BreakDealCommand(_currentCustomer)
            };
        }
    }
}
