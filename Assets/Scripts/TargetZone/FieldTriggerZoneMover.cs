using System.Collections.Generic;
using System;
using Fields;
using TargetZone.Zones;
using UnityEngine;
using VContainer;

namespace TargetZone
{
    public class FieldTriggerZoneMover : MonoBehaviour
    {
        [SerializeField] private FieldZoneLogic[] _zoneLogic;

        private IFieldCollectProvider _fieldCollectProvider;
        private Dictionary<FieldZoneLogic, Action> _unsubscribers;

        [Inject]
        private void Constructor(IFieldCollectProvider fieldCollectProvider)
        {
            _fieldCollectProvider = fieldCollectProvider;
            _unsubscribers = new Dictionary<FieldZoneLogic, Action>();

            foreach (FieldZoneLogic logic in _zoneLogic)
            {
                CropType type = logic.Type;
                
                Action handler = () => MoveTrigger(type);
                logic.OnContextUpdated += handler;
                _unsubscribers[logic] = handler;
            }
        }

        private bool TryGetFieldLogic(CropType type, out FieldZoneLogic logic)
        {
            logic = null;
            for (int i = 0; i < _zoneLogic.Length; i++)
            {
                if (_zoneLogic[i].Type == type)
                {
                    logic = _zoneLogic[i];
                    return true;
                }
            }

            return false;
        }
        
        private void MoveTrigger(CropType type)
        {
            // из-за расхода/пополнения денег у нас вызывается метод два раза, придумать что-то
            if (!TryGetFieldLogic(type, out FieldZoneLogic logic))
                return;
            
            if (!_fieldCollectProvider.FieldsDictionary.TryGetValue(type, out List<IField> fields))
            {
                logic.gameObject.SetActive(false);
                return;
            }
            
            foreach (IField field in fields)
            {
                if (!field.ActiveSelf)
                {
                    logic.gameObject.SetActive(true);
                    logic.transform.position =
                        field.GameObj.transform.position + new Vector3(0, 0.5f, 0);
                    return;
                }
            }

            logic.gameObject.SetActive(false);
        }

        public void MoveTriggers()
        {
            foreach (FieldZoneLogic logic in _zoneLogic)
            {
                MoveTrigger(logic.Type);
            }
        }

        private void OnDestroy()
        {
            if (_unsubscribers != null)
            {
                foreach (var pair in _unsubscribers)
                {
                    pair.Key.OnContextUpdated -= pair.Value;
                }
                _unsubscribers.Clear();
            }
        }
    }
}