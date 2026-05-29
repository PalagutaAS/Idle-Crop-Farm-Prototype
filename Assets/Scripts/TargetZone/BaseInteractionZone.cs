using System;
using System.Collections.Generic;
using Inventor;
using Logging;
using Player.Interface;
using TargetZone.Interfaces;
using UI;
using UnityEngine;
using VContainer;

namespace TargetZone
{
    public abstract class BaseInteractionZone : MonoBehaviour
    {
        [SerializeField] protected GameObject _gameObjectPanel;

        protected IValueSource<MoneyType> Wallet;
        protected IPlayer Player;
        protected IPanel Panel;
        
        private Action<MoneyType, int> _onChangedHandler; 
        
        [Inject]
        private void Constructor(IWallet wallet)
        {
            Wallet = wallet;
            Panel = _gameObjectPanel.GetComponent<IPanel>();
        }
        
        protected void NeedRefreshByOnClickButton()
        {
            Panel.OnClickButton += RefreshPanel;
        }
        
        protected void NeedRefreshByChangedMoney()
        {
            _onChangedHandler = (_, _) => RefreshPanel();
            Wallet.OnChangedByTypeForUI += _onChangedHandler;
        }

        protected virtual void OnTriggerEnter(Collider other)
        {
            if (Player == null && other.TryGetComponent(out IPlayer player))
            {
                Player = player;
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
            RefreshPanel();
        }

        protected virtual void OnPlayerExit()
        {
            Player = null;
            Panel.Close();
        }

        protected virtual void RefreshPanel()
        {
            if (Player != null && CanOpenPanel())
            {
                this.Log("Refresh and Generate Commands");
                var commands = GenerateCommands();
                Panel.Open(commands);
            }
        }

        private void OnDestroy()
        {
            Panel.OnClickButton -= RefreshPanel;
            Wallet.OnChangedByTypeForUI -= _onChangedHandler;
        }

        protected abstract bool CanOpenPanel();
        protected abstract List<IInteractionCommand> GenerateCommands();
    }
}