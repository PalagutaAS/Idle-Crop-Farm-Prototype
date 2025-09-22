using System.Collections.Generic;
using AI;
using Player;
using TargetZone.Command;
using TargetZone.Interfaces;
using UI;
using UnityEngine;

namespace TargetZone
{
    public class TradeZone : MonoBehaviour
    {
        [SerializeField] private CustomerSpawner _spawner;
        [SerializeField] private GameObject _gameObjectPanel;
        
        private CustomerController _currentCustomer;
        
        private ThirdPersonController _player;

        private IPanel _panel;

        private void Awake()
        {
            _panel = _gameObjectPanel.GetComponent<IPanel>();
        }

        private void OnTriggerEnter(Collider other)
        {
            if (_currentCustomer == null && other.TryGetComponent(out CustomerController customer))
            {
                _currentCustomer = customer;
            }
            if (_player == null && other.TryGetComponent(out ThirdPersonController player))
            {
                _player = player;
            }

            if (_player != null && _currentCustomer != null)
            {
                var commands = new List<IInteractionCommand>();
                commands.Add(new MakeDealCommand(_currentCustomer));
                commands.Add(new BreakDealCommand(_currentCustomer));
                _panel.Open(commands);
            }
        }
        
        private void OnTriggerExit(Collider other)
        {
            if (_currentCustomer != null && other.TryGetComponent(out CustomerController customer))
            {
                _currentCustomer = null;
                _panel.Close();
            }
            
            if (other.TryGetComponent(out ThirdPersonController player))
            {
                _player = null;
                _panel.Close();
            }

        }
    }
}
