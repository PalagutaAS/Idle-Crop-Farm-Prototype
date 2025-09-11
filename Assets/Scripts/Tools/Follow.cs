using UnityEngine;

namespace Tools
{
    public class Follow
    {
        private readonly Tool _tool;
        private readonly Transform _model;
        private readonly Transform _targetFollow;
        
        public Follow(Tool tool, Transform model, Transform targetFollow)
        {
            _tool = tool;
            _model = model;
            _targetFollow = targetFollow;
        }

        public void ToSlot()
        {
            _model.transform.position = Vector3.Lerp(_model.transform.position, _targetFollow.transform.position, _tool.SpeedFollow * Time.deltaTime);
        }
    }
}