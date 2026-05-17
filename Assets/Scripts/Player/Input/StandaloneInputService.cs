using UnityEngine;

namespace Player.Input
{
    public class StandaloneInputService : InputService
    {
        public override Vector2 Axis
        {
            get
            {
                Vector2 axis = SimpleInputAxis();
                return (axis == Vector2.zero) ? UnityAxis() : axis;
            }
        }
    }
}