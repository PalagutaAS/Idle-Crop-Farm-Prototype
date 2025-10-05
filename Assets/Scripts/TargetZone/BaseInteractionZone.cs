using System.Collections.Generic;
using Inventor;
using Player.Interface;
using TargetZone.Interfaces;
using UI;
using UnityEngine;
using VContainer;
using Wallets;

namespace TargetZone
{
    public abstract class BaseInteractionZone : MonoBehaviour
    {
        [SerializeField] protected GameObject _gameObjectPanel;

        protected IValueSource _wallet;
        protected IPlayer _player;
        protected IPanel _panel;
        
        [Inject]
        private void Constructor(Wallet wallet)
        {
            _wallet = wallet;
            _panel = _gameObjectPanel.GetComponent<IPanel>();
        }
        
        protected void NeedRefreshByOnClickButton()
        {
            _panel.OnClickButton += RefreshPanel;
        }
        
        protected void NeedRefreshByChangedMoney()
        {
            
            _wallet.OnChangedByType += (_,_) => RefreshPanel();
        }

        protected virtual void OnTriggerEnter(Collider other)
        {
            if (_player == null && other.TryGetComponent(out IPlayer player))
            {
                _player = player;
            }
            OnPlayerEnter();
        }

        protected virtual void OnTriggerExit(Collider other)
        {
            if (other.TryGetComponent(out IPlayer player))
            {
                OnPlayerExit();
            }
        }

        protected virtual void OnPlayerEnter()
        {
            if (CanOpenPanel())
            {
                var commands = GenerateCommands();
                _panel.Open(commands);
            }
        }

        protected virtual void OnPlayerExit()
        {
            _player = null;
            _panel.Close();
        }

        protected virtual void RefreshPanel()
        {
            if (_player != null && CanOpenPanel())
            {
                var commands = GenerateCommands();
                _panel.Open(commands);
            }
        }

        protected abstract bool CanOpenPanel();
        protected abstract List<IInteractionCommand> GenerateCommands();
    }
}