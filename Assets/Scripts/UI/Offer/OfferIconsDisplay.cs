using System.Collections.Generic;
using Crops.ScriptableObjects;
using ObjectPull;
using UnityEngine;
using VContainer;

namespace Offers
{
    public class OfferIconsDisplay : MonoBehaviour, IOfferDisplay
    {
        //Переделать на интерфейсы
        [SerializeField] private OfferIconView _iconPrefab;
        [SerializeField] private Transform _iconsContainer;

        private LibraryCropConfigs _cropConfigs;
        private IPoolManager _poolManager;
        private List<OfferIconView> _activeIcons = new();

        [Inject]
        private void Construct(IPoolManager poolManager, LibraryCropConfigs cropConfigs)
        {
            _cropConfigs = cropConfigs;
            _poolManager = poolManager;
        }

        public void Show(Offer currentOffer)
        {
            Clear();
            _iconsContainer.gameObject.SetActive(true);
            foreach (OfferLine offerLine in currentOffer.Lines)
            {
                var config = _cropConfigs.GetConfigByType(offerLine.Type);
                OfferIconView iconView = _poolManager.GetObject<OfferIconView>(_iconPrefab.gameObject);
                Prepare(iconView).Setup(config.Sprite, offerLine.Count);
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
        void Show(Offer currentOffer);
        void Close();
    }
}