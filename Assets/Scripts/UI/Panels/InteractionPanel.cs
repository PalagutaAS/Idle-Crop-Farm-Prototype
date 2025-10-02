using System;
using System.Collections.Generic;
using ObjectPull;
using Player;
using TargetZone.Interfaces;
using UI.ButtonService;
using UnityEngine;
using UnityEngine.UI;
using VContainer;

namespace UI.Panels
{
    public class InteractionPanel : MonoBehaviour, IPanel
    {
        [SerializeField] private ThirdPersonController _player;
        [SerializeField] private Button _buttonPrefab;
        [SerializeField] private Transform _buttonsContainer;
        
        private PoolManager _poolManager;
        private Dictionary<Button, IInteractionCommand> _buttons = new();
        private ButtonPrepareService _buttonPrepare;
        public event Action OnClickButton;

        [Inject]
        private void Constructor(PoolManager poolManager)
        {
            _poolManager = poolManager;
            _buttonPrepare = new ButtonPrepareService(_buttonsContainer);
        }

        public void Open(List<IInteractionCommand> commands)
        {
            ClearButtons();
            foreach (var command in commands)
            {
                Button button = _poolManager.GetObject<Button>(_buttonPrefab.gameObject);
                
                _buttonPrepare.Prepare(button, command.Title, command.CanExecute(_player), () =>
                {
                    foreach (var btnAndCmd in _buttons)
                    {
                        btnAndCmd.Key.interactable = false;
                    }

                    command.Execute(_player);
                    OnClickButton?.Invoke();
                });

                _buttons.Add(button, command);
            }
            gameObject.SetActive(true);
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
        
        public void Close()
        {
            ClearButtons();
            gameObject.SetActive(false);
        }
    }
}