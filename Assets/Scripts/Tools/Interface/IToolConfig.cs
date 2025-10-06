using UnityEditor.Animations;
using UnityEngine;

namespace Tools.Interface
{
    public interface IToolConfig
    {
        public int Level { get; set; }
        public ToolType Type { get; set; }
        public int Cost { get; }
        public GameObject Model { get; }
        public float Radius { get; }
        float TimeOut { get; }
        public CropType HarvestableCrops { get; }
        public AnimatorController AnimatorController { get; set; }
    }
}