using Infrastructure.PersistenceProgress;
using Infrastructure.Services;
using TMPro;
using UnityEngine;
using VContainer;

namespace Logging
{
    public class DebugMenu : MonoBehaviour ,IDebugMenu
    {
        [SerializeField] private TMP_Text _logText; 
        private IDebugLogService _debugLogService;
        private ISaveService _saveService;
        private IPersistenceProgressService _progressService;

        [Inject]
        private void Constructor(IDebugLogService debugLogService, ISaveService saveService, IPersistenceProgressService progressService)
        {
            _progressService = progressService;
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

        private void UpdateLogs()
        {
            _logText.text = _debugLogService?.LogText;
        }
        
        public void Close()
        {
            gameObject.SetActive(false);
        }

        public void Clear()
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
            _progressService.ResetCloudYG();
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