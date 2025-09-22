using System.Collections.Generic;
using Player;
using TargetZone.Interfaces;
using UI;
using UnityEngine;

namespace TargetZone
{
    public class UpgradeTool : MonoBehaviour
    {
        [SerializeField] private int _price;
        [SerializeField] private GameObject _gameObjectPanel; 
        
        private IPanel _panel;
        private ThirdPersonController _player;

        private void Awake()
        {
            _panel = _gameObjectPanel.GetComponent<IPanel>();
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.TryGetComponent(out ThirdPersonController player))
            {
                _player = player;
                var commands = new List<IInteractionCommand>();
                _panel.Open(commands);
            }
        }
        private void OnTriggerExit(Collider other)
        {
            if (other.TryGetComponent(out ThirdPersonController player))
            {
                _player = null;
                _panel.Close();
            }
        }

        public void Upgrade()
        {
            if (_player.Wallet.Count >= _price)
            {
                //_player.Tools.Upgrade();
                _player.Wallet.Payment(_price);
            }
        }
        
        public void BuyTool()
        {
            if (_player.Wallet.Count >= _price && _player.TryBuyTool())
            {
                _player.Wallet.Payment(_price);
            }
        }
    }
}
