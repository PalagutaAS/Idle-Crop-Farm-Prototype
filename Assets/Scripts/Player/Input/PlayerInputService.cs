using Player.Input;
using UnityEngine;

public class PlayerInputService : InputService
{
    //public float Horizontal1 => Input.GetAxis("Horizontal");
    //public float Vertical1 => Input.GetAxis("Vertical");
    public Vector2 Axis { get; }

    public bool AnyAxis => (Input.GetKey(KeyCode.W) ||
                            Input.GetKey(KeyCode.A) ||
                            Input.GetKey(KeyCode.S) ||
                            Input.GetKey(KeyCode.D)
                           );
}

public class MobileInputService : InputService
{
    public Vector2 Axis => SimpleInputAxis();
}

public class StandaloneInputService : InputService
{
    public Vector2 Axis
    {
        get
        {
            Vector2 axis = SimpleInputAxis();
            return (axis == Vector2.zero) ? UnityAxis() : axis;
        }
    }

    private static Vector2 UnityAxis() => 
        new Vector2(Input.GetAxis(Horizontal), Input.GetAxis(Vertical));
}