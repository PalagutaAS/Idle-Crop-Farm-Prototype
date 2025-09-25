using System;
using Tools.Interface;
using UnityEngine;

namespace Tools.ScriptableObjects
{
    [CreateAssetMenu(fileName = "New Pickaxe Config", menuName = "Custom/Pickaxe Config")]

    public class PickaxeConfig : ScriptableObject, IToolConfig
    {
        [SerializeField, Range(1, 10)] private int _level = 1;
        [SerializeField] private GameObject _model;
        [SerializeField] private float _timeOut = 5f;
        [SerializeField] private float _radius = 2f;
        [SerializeField] private int _cost = 10;


        public GameObject Model => _model;
        public float TimeOut => _timeOut;
        public float Radius => _radius;
        public int Cost => _cost;
        public int Level => _level;
    }
}