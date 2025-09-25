using UnityEngine;

namespace Tools.Interface
{
    public interface IToolConfig
    {
        public int Level {  get; }
        public int Cost { get; }
        public GameObject Model { get; }
        public float Radius { get; }
        float TimeOut { get; }
    }
}