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

        [SerializeField] private bool _flag = false;
        
        private List<Button> _buttons = new();
        
        public void Open(List<IInteractionCommand> commands)
        {
            foreach (var command in commands)
            {
                Button button = Instantiate(_buttonPrefab, _buttonsContainer);
                button.GetComponentInChildren<Text>().text = command.Title;
                
                button.interactable = command.CanExecute(_player);
                
                button.onClick.AddListener(() =>
                {
                    foreach (var btn in _buttons)
                    {
                        btn.interactable = false;
                    }

                    command.Execute(_player);
                    foreach (var btn in _buttons)
                    {
                        btn.interactable = command.CanExecute(_player);
                    }
                });
                _buttons.Add(button);
            }
            gameObject.SetActive(true);
        }

        private void ClearButtons()
        {
            foreach (var button in _buttons)
            {
                Destroy(button.gameObject);
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