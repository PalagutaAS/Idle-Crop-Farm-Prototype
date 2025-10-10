using DG.Tweening;
using UnityEngine;

namespace Tools
{
    public class AnimatorHarvest
    {
        private readonly Transform _model;
        private readonly Animator _animation;
        private readonly float _animDuration;
        private readonly Vector3[] _path = new Vector3[3];

        private Tweener _currentTween;
        public AnimatorHarvest(Transform model, float animDuration, RuntimeAnimatorController animatorController)
        {
            _model = model;
            _animDuration = animDuration;
            _animation = _model.GetComponent<Animator>();
            _animation.runtimeAnimatorController = animatorController;
        }

        public void MoveTo(Vector3 targetPosition)
        {
            _currentTween?.Kill();
            StartAnimationMoveTo(targetPosition);
            StartAnimationHarvest();
        }

        private void StartAnimationMoveTo(Vector3 targetPosition)
        {
            targetPosition += new Vector3(0, _model.transform.localScale.y, -_model.transform.localScale.z/2);
        
            _path[0] = _model.transform.position;
            _path[1] = CalculateArcControlPoint(_model.transform.position, targetPosition, Random.Range(-2,2));
            _path[2] = targetPosition;

            _currentTween = _model.transform.DOPath(_path, _animDuration, PathType.CatmullRom)
                .SetEase(Ease.OutQuad);
        }

        private void StartAnimationHarvest()
        {
            _animation.speed = (2/_animDuration)/4;
            _animation.SetTrigger("farming");
        }

        private Vector3 CalculateArcControlPoint(Vector3 start, Vector3 end, float height)
        {
            Vector3 mid = (start + end) / 2;
            mid.x += height/2;
            mid.z += height/2;
            return mid;
        }
        
    }
}