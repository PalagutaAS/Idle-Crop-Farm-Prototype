using DG.Tweening;
using UnityEngine;

namespace Infrastructure
{
    public class ScreenLoading : MonoBehaviour
    {
        [SerializeField] private float _fadeDuration = 0.5f;
    
        private CanvasGroup _canvas;

        private void Awake()
        {
            _canvas = GetComponent<CanvasGroup>();
        }

        public void Show()
        {
            gameObject.SetActive(true);
            _canvas.alpha = 1;
        }

        public void Hide()
        {
            _canvas.DOKill();
        
            _canvas.DOFade(0, _fadeDuration)
                .OnComplete(() => gameObject.SetActive(false));
        }

        public void ShowAppear()
        {
            gameObject.SetActive(true);
            _canvas.DOFade(1, _fadeDuration);
        }

        private void OnDestroy()
        {
            _canvas.DOKill();
        }
    }
}