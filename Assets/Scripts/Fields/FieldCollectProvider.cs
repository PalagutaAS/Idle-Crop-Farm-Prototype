using System.Collections.Generic;
using Fields;
using UnityEngine;
using VContainer.Unity;

public class FieldCollectProvider : MonoBehaviour, IFieldCollectProvider, IInitializable
{
    [SerializeField] private Field[] _fields;
    private void Awake()
    {
        IField[] fields = GetComponentsInChildren<IField>(true);
    }

    public Dictionary<CropType, int> GetAllFields()
    {
        return new();
    }

    public void Initialize()
    {
        Debug.Log("INIT");
    }
}

public interface IFieldCollectProvider
{
    Dictionary<CropType, int> GetAllFields();
}
