using DG.Tweening;
using UnityEngine;

namespace Crops.Animations
{
    public class CropGrowthAnimator : MonoBehaviour
    {
        [Header("Animation Settings")]
        [SerializeField] private float _duration = 0.5f;
        [SerializeField] private Ease _ease = Ease.OutBack;
        [SerializeField] private Vector3 _startScale = Vector3.zero;
        [SerializeField] private Vector3 _targetScale = Vector3.one;

        private Tweener _currentTween;

        /// <summary>
        /// Запускает анимацию роста. При необходимости сбрасывает масштаб в начальное значение.
        /// </summary>
        public void PlayGrowAnimation()
        {
            _currentTween?.Kill();

            transform.localScale = _startScale;

            _currentTween = transform.DOScale(_targetScale, _duration)
                .SetEase(_ease)
                .OnComplete(() => _currentTween = null);
        }


        public void SetTargetScaleImmediate()
        {
            _currentTween?.Kill();
            transform.localScale = _targetScale;
        }

        private void OnDestroy()
        {
            _currentTween?.Kill();
        }
    }
}