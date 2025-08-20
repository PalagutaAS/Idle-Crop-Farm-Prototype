using UnityEngine;

public class SmoothCameraFollow : MonoBehaviour
{
    [Header("Target Settings")]
    [SerializeField] private Transform _target;
    
    [Header("Position Settings")]
    [SerializeField] private float _fixedHeight = 10f;
    [SerializeField] private float _offsetZ = -10f;
    
    [Header("Smooth Settings")]
    [Range(0.01f, 1f)]
    [SerializeField] private float _smoothSpeed = 0.1f;

    private Vector3 _targetPosition;

    void LateUpdate()
    {
        if (_target == null)
        {
            Debug.LogWarning("Camera target not assigned!");
            return;
        }

        Vector3 targetPosition = new Vector3(
            _target.position.x,
            _fixedHeight,
            _target.position.z + _offsetZ
        );

        transform.position = Vector3.Lerp(
            transform.position,
            targetPosition,
            _smoothSpeed * Time.deltaTime * 60
        );
    }
}
