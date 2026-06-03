using System.Collections.Generic;
using Crops.ScriptableObjects;
using Inventor;
using ObjectPull;
using UnityEngine;
using VContainer;

namespace Offers
{
    public class OfferIconsDisplay : MonoBehaviour, IOfferDisplay
    {
        //Переделать на интерфейсы
        [SerializeField] private OfferIconView _iconPrefab;
        
        private Transform _iconsContainer;
        private ILibraryCropConfigs _cropConfigs;
        private IPoolManager _poolManager;
        private List<OfferIconView> _activeIcons = new();
        private IInventory _inventory;

        [Inject]
        private void Construct(IPoolManager poolManager, ILibraryCropConfigs cropConfigs, IInventory inventory)
        {
            _iconsContainer = transform;
            _inventory = inventory;
            _cropConfigs = cropConfigs;
            _poolManager = poolManager;
        }

        public void Show(IOfferDisplayData currentOffer)
        {
            Clear();
            _iconsContainer.gameObject.SetActive(true);
            foreach (OfferLine offerLine in currentOffer.Lines)
            {
                var config = _cropConfigs.GetConfigByType(offerLine.Type);
                OfferIconView iconView = _poolManager.GetObject<OfferIconView>(_iconPrefab.gameObject);
                Prepare(iconView).Setup(config.Sprite, offerLine.Count, _inventory.CheckCountByType(offerLine.Type));
                _activeIcons.Add(iconView);
            }
        }
        
        public void Close()
        {
            _iconsContainer.gameObject.SetActive(false);
        }

        private OfferIconView Prepare(OfferIconView offerIconView)
        {
            offerIconView.gameObject.transform.SetParent(_iconsContainer);
            offerIconView.gameObject.SetActive(true);
            return offerIconView;
        }
        
        private void Clear()
        {
            foreach (var iconView in _activeIcons)
            {
                GameObject iconGO = iconView.gameObject;
                iconGO.SetActive(false);
                _poolManager.ReturnObject(iconGO);
            }
            _activeIcons.Clear();
        }
    }

    public interface IOfferDisplay
    {
        void Show(IOfferDisplayData currentOffer);
        void Close();
    }
}