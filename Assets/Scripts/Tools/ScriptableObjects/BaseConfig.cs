using Tools.Interface;
using UnityEngine;

namespace Tools.ScriptableObjects
{
    public abstract class BaseConfig : IToolConfig
    {
        public abstract int Level { get; set; }
        public abstract ToolType Type { get; set; }
        public abstract int Cost { get; }
        public abstract GameObject Model { get; }
        public abstract float Radius { get; }
        public abstract float TimeOut { get; }
        public abstract CropType HarvestableCrops { get; }
    }
}