using Offers;
using TargetZone.Interfaces;
using UI.Panels;
using UnityEngine;

namespace UI
{
    public class ZonePanelUI : MonoBehaviour
    {
        [SerializeField] private InteractionButtonPanel _buttonPanel;
        [SerializeField] private OfferIconsDisplay _offerDisplay;
        
        public void Show(IZoneContext context)
        {
            gameObject.SetActive(true);
            if (context.OfferDisplayData != null)
                _offerDisplay.Show(context.OfferDisplayData);
            else
                _offerDisplay?.Close();

            _buttonPanel.Open(context.Commands);
        }

        public void Close()
        {
            gameObject.SetActive(false);
            _buttonPanel?.Close();
            _offerDisplay?.Close();
        }
    }
}