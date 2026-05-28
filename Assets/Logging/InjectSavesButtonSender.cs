using Infrastructure.DI;
using Infrastructure.PersistenceProgress;
using Infrastructure.Services;
using SavesData;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using VContainer;

namespace Logging
{
    public class InjectSavesButtonSender : MonoBehaviour
    {
        public TMP_InputField _inputField;
        public Button _button;

        void Start()
        {
            _button.onClick.AddListener(OnButtonClick);
        }
//{"InventoryData":{"Crops":{"keys":[2],"values":[1]}},"WalletData":{"Money":{"keys":[1],"values":[50]}},"FieldData":{"Fields":{"keys":[2],"values":[1]}},"ToolsData":{"Tools":{"keys":[0,1,2,4],"values":[0,1,0,1]}}}
        void OnButtonClick()
        {
            string textFromInput = _inputField.text;
            if (textFromInput == "")
            {
                this.Log($"Inject reject. Entered text: {textFromInput}", LogType.Warning);
                return;
            }
            ProcessInput(textFromInput);
        }

        void ProcessInput(string text)
        {
            var container = FindObjectOfType<GameLifetimeScope>().Container;
            var persistenceProgressService = container.Resolve<IPersistenceProgressService>();
            var restartGame = container.Resolve<IRestartGameService>();
            try
            {
                persistenceProgressService.Progress = JsonUtility.FromJson<GameProgress>(text);
            }
            catch (System.Exception ex)
            {
                this.Log($"Error when deserializing JSON: {ex.Message}", LogType.Error);
                return;
            }
            persistenceProgressService.SaveCloudYG();
            restartGame.DoRestartGame();
        }
    }
}