using Inventor;
using UI;
using UnityEngine;
using VContainer;

namespace TargetZone
{
    [RequireComponent(typeof(TriggerZone))]
    public class ZoneFacade : MonoBehaviour
    {
        [SerializeField] private ZonePanelUI _zoneUI;
        [SerializeField] private BaseZoneLogic _baseLogic;
        
        private TriggerZone _triggerZone;
        private IZoneInteractionLogic _logic;
        [Inject]
        private IWallet _wallet;

        void Awake()
        {
            _triggerZone = GetComponent<TriggerZone>();
            _logic = _baseLogic;
            RegisterEvents();
        }

        private void RegisterEvents()
        {
            _triggerZone.OnEnter += Enter;
            _triggerZone.OnExit += Exit;
            _logic.OnContextUpdated += OnContextUpdated;
            _wallet.OnChangedByTypeForUI += OnMoneyChanged;
        }

        private void Enter(GameObject obj) => _logic.HandleEnter(obj);
        private void Exit(GameObject obj) => _logic.HandleExit(obj);

        private void OnContextUpdated()
        {
            if (_logic.CanActivate)
                _zoneUI.Show(_logic.GenerateContext());
            else
                _zoneUI.Close();
        }

        private void OnMoneyChanged(MoneyType type, int count) => OnContextUpdated();
        private void OnDestroy()
        {
            _triggerZone.OnEnter -= Enter;
            _triggerZone.OnExit -= Exit;
            _logic.OnContextUpdated -= OnContextUpdated;
            _wallet.OnChangedByTypeForUI -= OnMoneyChanged;
        }
    }
}