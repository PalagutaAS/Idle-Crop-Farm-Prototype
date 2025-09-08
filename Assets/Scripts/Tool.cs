using System.Collections;
using UnityEngine;

public class Tool : MonoBehaviour
{
    [SerializeField] private ThirdPersonController _player;
    [SerializeField] private float _radius = 2f;
    [SerializeField] private float _timeOut = 5f;
    [SerializeField] private GameObject _toolModel;

    private bool _isCooldown = false;
    
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


    public void CropHarvest(Crop.Crop crop)
    {
        if (_isCooldown) return;
        
        int cropCount = crop.OnHarvest();
        _player.Inventory.Add(crop.Type, cropCount);
        _isCooldown = true;
        StartCoroutine(CooldownCoroutine());
    }
    
    private IEnumerator CooldownCoroutine()
    {
        yield return new WaitForSeconds(_timeOut);
        
        _isCooldown = false;
    }
}
