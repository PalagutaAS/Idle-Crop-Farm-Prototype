using UnityEngine;
using DG.Tweening;
using Player.Interface;

namespace Crops
{
    public class GrassSway : MonoBehaviour
    {
        [SerializeField] private float maxAngle = 10f;
        [SerializeField] private float baseMoveDuration = 0.25f;
        [SerializeField] private int cycles = 3;
        [SerializeField] private float decayFactor = 0.6f;
        [SerializeField] private float timeDecayFactor = 0.7f; // новый параметр: уменьшение длительности каждого следующего цикла

        private Quaternion originalRotation;
        private bool _isSwaying;
        private Tweener _currentTween;

        private void OnTriggerEnter(Collider other)
        {
            if (other.TryGetComponent(out IPlayer _) && !_isSwaying)
                StartAnim();
        }

        [ContextMenu("StartAnim")]
        public void StartAnim()
        {
            if (_isSwaying) return;
            _isSwaying = true;
            originalRotation = transform.localRotation;

            // Генерируем случайное направление
            float randX = Random.Range(-1f, 1f);
            float randY = 0;// Random.Range(-1f, 1f);
            Vector2 direction = new Vector2(randX, randY).normalized;
            Vector3 baseTilt = new Vector3(direction.x * maxAngle, direction.y * maxAngle, 0f);

            // Начинаем с полной амплитуды, базовой длительности
            SwayStep(originalRotation, baseTilt, 1, cycles, baseMoveDuration);
        }

        /// <param name="fromRot">Текущий поворот</param>
        /// <param name="currentTilt">Вектор наклона относительно исходного</param>
        /// <param name="step">Номер движения (нечётный - в случайную сторону, чётный - в противоположную)</param>
        /// <param name="remainingCycles">Сколько полных колебаний ещё предстоит (включая текущее)</param>
        /// <param name="duration">Длительность этого движения</param>
        private void SwayStep(Quaternion fromRot, Vector3 currentTilt, int step, int remainingCycles, float duration)
        {
            // Определяем целевой поворот
            Quaternion toRot = (step % 2 == 1) 
                ? originalRotation * Quaternion.Euler(currentTilt)
                : originalRotation * Quaternion.Euler(-currentTilt);

            // Анимируем поворот
            _currentTween = transform.DOLocalRotateQuaternion(toRot, duration)
                .SetEase(Ease.InOutSine)
                .OnComplete(() =>
                {
                    // После чётного шага (возврат в противоположную точку) уменьшаем счётчик циклов
                    if (step % 2 == 0)
                    {
                        int newRemaining = remainingCycles - 1;
                        if (newRemaining <= 0)
                        {
                            // Все циклы завершены - возвращаемся в исходное положение
                            transform.DOLocalRotateQuaternion(originalRotation, duration)
                                .SetEase(Ease.OutQuad)
                                .OnComplete(() => _isSwaying = false);
                            return;
                        }

                        // Генерируем новое случайное направление с уменьшенной амплитудой
                        float amplitude = maxAngle * Mathf.Pow(decayFactor, step / 2);
                        float randX = Random.Range(-1f, 1f);
                        float randY = Random.Range(-1f, 1f);
                        Vector2 newDir = new Vector2(randX, randY).normalized;
                        Vector3 newTilt = new Vector3(newDir.x * amplitude, newDir.y * amplitude, 0f);

                        // Длительность следующего движения уменьшаем
                        float newDuration = duration * timeDecayFactor;

                        // Переход в новую случайную точку (нечётный шаг)
                        SwayStep(toRot, newTilt, step + 1, newRemaining, newDuration);
                    }
                    else // нечётный шаг - просто идём в противоположную сторону с тем же наклоном
                    {
                        // Длительность возврата тоже уменьшаем (можно оставить как есть или тоже уменьшить)
                        float returnDuration = duration * timeDecayFactor;
                        SwayStep(toRot, currentTilt, step + 1, remainingCycles, returnDuration);
                    }
                });
        }

        private void OnDestroy()
        {
            _currentTween?.Kill();
            DOTween.Kill(transform);
        }
    }
}