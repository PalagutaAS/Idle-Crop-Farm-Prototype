using System.Collections.Generic;
using Fields;
using UnityEngine;
using VContainer.Unity;

public class FieldCollectProvider : MonoBehaviour, IFieldCollectProvider, IInitializable
{
    [SerializeField] private Field[] _fields;
    public Dictionary<CropType, List<IField>> FieldsDictionary { get; private set; }

    public Dictionary<CropType, int> GetAllFields()
    {
        return new();
    }

    public void Initialize()
    {
        FieldsDictionary = new();
        foreach (IField field in _fields)
        {
            CropType fieldType = field.Type;

            if (!FieldsDictionary.ContainsKey(fieldType))
                FieldsDictionary[fieldType] = new List<IField>();

            FieldsDictionary[fieldType].Add(field);
        }
    }
}

public interface IFieldCollectProvider
{
    public Dictionary<CropType, List<IField>> FieldsDictionary { get; }
}
