using UnityEngine;

public class ChildModelChanger
{
    private GameObject _currentModel;
    private readonly Transform _parent;

    public ChildModelChanger(Transform parent, GameObject currentModel = null)
    {
        _parent = parent;
        if (currentModel != null)
        {
            ChangeModel(currentModel);
        }
    }

    /// <summary>
    /// Заменяет текущую дочернюю модель на новую
    /// </summary>
    /// <param name="newModelPrefab">Префаб новой модели (будет создан как дочерний объект)</param>
    public void ChangeModel(GameObject newModelPrefab)
    {
        if (newModelPrefab == null) return;
        
        if (_currentModel != null)
        {
            Object.Destroy(_currentModel);
        }
        
        _currentModel = Object.Instantiate(newModelPrefab, _parent);
        _currentModel.transform.localPosition = Vector3.zero;
        _currentModel.transform.localRotation = Quaternion.identity;
        
    }
}