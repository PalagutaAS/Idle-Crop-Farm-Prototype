using Infrastructure.Services;
using TMPro;
using UnityEngine;
using VContainer;

namespace Logging
{
    public class DebugMenu : MonoBehaviour, IDebugMenu
    {
        [SerializeField] private TMP_Text _logText; 
        private IDebugLogService _debugLogService;
        private ISaveService _saveService;
        private IResetSaveService _resetSaveService;
        private IRestartGameService _restartGameService;

        [Inject]
        private void Constructor(IDebugLogService debugLogService, ISaveService saveService, IResetSaveService resetSaveService, IRestartGameService restartGameService)
        {
            _restartGameService = restartGameService;
            _resetSaveService = resetSaveService;
            _saveService = saveService;
            _debugLogService = debugLogService;
            _debugLogService.OnDrawDebug += UpdateLogs;
            gameObject.SetActive(false);
        }

        private void Awake()
        {
            gameObject.SetActive(false);
        }

        public void Open()
        {
            UpdateLogs();
            gameObject.SetActive(true);
        }

        public void Close()
        {
            gameObject.SetActive(false);
        }

        public void ClearLogs()
        {
            _debugLogService.Clear();
            _logText.text = "";
        }

        public void SavePrefs()
        {
            _saveService?.SaveProgress();
        }

        public void ClearPrefs()
        {
            _resetSaveService.ResetSave();
        }

        public void RestartGame()
        {
            _restartGameService.DoRestartGame();
        }

        private void UpdateLogs()
        {
            _logText.text = _debugLogService?.LogText;
        }

        private void OnDestroy()
        {
            _debugLogService.OnDrawDebug -= UpdateLogs;
        }
    }

    public interface IDebugMenu
    {
        public void Open();
        public void Close();
    }
}