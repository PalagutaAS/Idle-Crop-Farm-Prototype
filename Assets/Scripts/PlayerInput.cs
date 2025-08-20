using UnityEngine;

public class PlayerInput : MonoBehaviour, IInput
{
    public float Horizontal => Input.GetAxis("Horizontal");
    public float Vertical => Input.GetAxis("Vertical");

    public bool GetKey => (Input.GetKey(KeyCode.W) ||
                           Input.GetKey(KeyCode.A) ||
                           Input.GetKey(KeyCode.S) ||
                           Input.GetKey(KeyCode.D)
                           );
}
