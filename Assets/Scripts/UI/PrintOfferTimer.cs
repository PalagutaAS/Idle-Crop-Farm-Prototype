using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class PrintOfferTimer : MonoBehaviour
    {
        [SerializeField] private Image _image;
        private float _duration;

        private void Awake()
        {
            Close();
        }

        public void ShowUI(float duration)
        {
            _duration = duration;
            gameObject.SetActive(true);
        }

        public void Tick(float elapsed)
        {
            if (elapsed <= 0f || _duration <= 0f)
            {
                _image.fillAmount = 0;
                return;
            }
            
            float t = 1f - Mathf.Clamp01(elapsed / _duration);
            _image.fillAmount = t;
        }

        public void Close()
        {
            _duration = 0;
            gameObject.SetActive(false);
        }
    }
}