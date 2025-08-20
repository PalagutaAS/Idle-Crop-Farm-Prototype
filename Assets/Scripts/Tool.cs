using UnityEngine;

public class Tool : MonoBehaviour
{
    [SerializeField] private float _radius = 2f;
    //[SerializeField] private float _timeOut = 5f;

    private SphereCollider _collider;
    private void Awake()
    {
        _collider = GetComponent<SphereCollider>();
        _collider.radius = _radius;
    }

    public void Upgrade()
    {
        _radius += 0.5f;
        _collider.radius = _radius;
    }
    
    

}
