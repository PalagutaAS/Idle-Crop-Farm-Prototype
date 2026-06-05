using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class PrintOfferTimerView : MonoBehaviour, IOfferTimerView
    {
        [SerializeField] private Image _image;

        public void Show()
        {
            gameObject.SetActive(true);
        }

        public void UpdateFill(float remainingRatio)
        {
            _image.fillAmount = remainingRatio;
        }

        public void Hide()
        {
            gameObject.SetActive(false);
            _image.fillAmount = 1f;
        }
    }
    
    public interface IOfferTimerView
    {
        void Show();
        void UpdateFill(float remainingRatio);
        void Hide();
    }
}