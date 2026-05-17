using UnityEngine;

namespace Player.Input
{
    public abstract class InputService : IInputService
    {
        protected const string Horizontal = "Horizontal";
        protected const string Vertical = "Vertical";
        protected const float Threshold = 0.33f;
        public abstract Vector2 Axis { get; }
        public bool AnyAxis => Axis.x > Threshold || Axis.y > Threshold || Axis.x < -Threshold || Axis.y < -Threshold;

        protected static Vector2 SimpleInputAxis() => 
            new Vector2(SimpleInput.GetAxis(Horizontal), SimpleInput.GetAxis(Vertical));
        
        protected static Vector2 UnityAxis() => 
            new Vector2(UnityEngine.Input.GetAxis(Horizontal), UnityEngine.Input.GetAxis(Vertical));
    }
}