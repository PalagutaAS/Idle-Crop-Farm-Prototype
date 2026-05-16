using UnityEngine;

namespace Player.Input
{
    public abstract class InputService : IInputService
    {
        protected const string Horizontal = "Horizontal";
        protected const string Vertical = "Vertical";
        private float _horizontal;
        private float _vertical;
        public Vector2 Axis => new Vector2(_horizontal, _vertical);
        public bool AnyAxis => _horizontal > 0.01f || _vertical > 0.01f || _horizontal < -0.01f || _vertical < -0.01f;

        protected static Vector2 SimpleInputAxis() => 
            new Vector2(SimpleInput.GetAxis(Horizontal), SimpleInput.GetAxis(Vertical));
    }
}