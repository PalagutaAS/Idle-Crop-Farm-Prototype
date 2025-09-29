using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace UI.ButtonService
{
    public class ButtonPrepareService
    {
        private readonly Transform _buttonsContainer;
        public ButtonPrepareService(Transform buttonsContainer)
        {
            _buttonsContainer = buttonsContainer;
        }
        
        public void Prepare(Button button, string commandTitle, bool canExecute, UnityAction action)
        {                
            button.gameObject.transform.SetParent(_buttonsContainer);
            button.GetComponentInChildren<Text>().text = commandTitle;
            button.interactable = canExecute;
            button.gameObject.transform.localScale = Vector3.one;
            button.onClick.AddListener(action);
        }
    }
}