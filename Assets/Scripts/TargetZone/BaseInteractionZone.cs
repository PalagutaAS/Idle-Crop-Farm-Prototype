using System.Collections.Generic;
using Player;
using Player.Interface;
using TargetZone.Interfaces;
using UI;
using UnityEngine;

namespace TargetZone
{
    public abstract class BaseInteractionZone : MonoBehaviour
    {
        [SerializeField] protected GameObject _gameObjectPanel;
        
        protected IPlayer _player;
        protected IPanel _panel;

        protected virtual void Awake()
        {
            _panel = _gameObjectPanel.GetComponent<IPanel>();
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
            if (other.TryGetComponent(out ThirdPersonController player))
            {
                _player = null;
                OnPlayerExit();
                _panel.Close();
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
            // Базовая реализация - закрытие панели
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