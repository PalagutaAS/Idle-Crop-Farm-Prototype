using System;
using System.Collections.Generic;
using Player;
using TargetZone.Interfaces;
using UnityEngine;
using UnityEngine.UI;

namespace UI.Panels
{
    public class InteractionPanel : MonoBehaviour, IPanel
    {
        [SerializeField] private ThirdPersonController _player;
        [SerializeField] private Button _buttonPrefab;
        [SerializeField] private Transform _buttonsContainer;
        
        private Dictionary<Button, IInteractionCommand> _buttons = new();

        public event Action OnClickButton;
        
        public void Open(List<IInteractionCommand> commands)
        {
            ClearButtons();
            foreach (var command in commands)
            {
                Button button = Instantiate(_buttonPrefab, _buttonsContainer);
                button.GetComponentInChildren<Text>().text = command.Title;
                
                button.interactable = command.CanExecute(_player);
                
                button.onClick.AddListener(() =>
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
                Destroy(btnAndCmd.Key.gameObject);
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