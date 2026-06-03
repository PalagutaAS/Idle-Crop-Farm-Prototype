using System;
using System.Collections.Generic;
using ObjectPool;
using Player.Interface;
using TargetZone.Interfaces;
using UI.ButtonService;
using UnityEngine;
using UnityEngine.UI;
using VContainer;

namespace UI.Panels
{
    public class InteractionButtonPanel : MonoBehaviour, IButtonPanel
    {
        [SerializeField] private Button _buttonPrefab;
        
        private Transform _buttonsContainer;
        private IPoolManager _poolManager;
        private IPlayer _player;
        private Dictionary<Button, IInteractionCommand> _buttons = new();
        private ButtonPrepareService _buttonPrepare;
        public event Action OnClickButton;

        [Inject]
        private void Constructor(IPoolManager poolManager, IPlayer player)
        {
            _buttonsContainer = transform;
            _player = player;
            _poolManager = poolManager;
            _buttonPrepare = new ButtonPrepareService(_buttonsContainer);
        }

        public void Open(List<IInteractionCommand> commands)
        {
            ClearButtons();
            foreach (IInteractionCommand command in commands)
            {
                PrepareButton(command);
            }
            gameObject.SetActive(true);
        }

        public void Close()
        {
            ClearButtons();
            gameObject.SetActive(false);
        }

        private void PrepareButton(IInteractionCommand command)
        {
            Button button = _poolManager.GetObject<Button>(_buttonPrefab.gameObject);

            _buttonPrepare.Prepare(button, command.Title, command.CanExecute(_player), () => ExecuteCommand(command));
            _buttons.Add(button, command);
        }

        private void ExecuteCommand(IInteractionCommand command)
        {
            foreach (var btnAndCmd in _buttons)
            {
                btnAndCmd.Key.interactable = false;
            }
            command.Execute(_player);
            OnClickButton?.Invoke();
        }

        private void ClearButtons()
        {
            foreach (var btnAndCmd in _buttons)
            {
                btnAndCmd.Key.onClick.RemoveAllListeners();
                _poolManager.ReturnObject(btnAndCmd.Key.gameObject);
            }
            _buttons.Clear();
        }
    }
}