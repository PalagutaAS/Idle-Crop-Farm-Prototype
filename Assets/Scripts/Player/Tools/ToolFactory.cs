using Tools.Interface;
using UnityEngine;

namespace Player.Tools
{
    public class ToolFactory : IToolFactory
    {
        private readonly GameObject _toolPrefab;
        private readonly Transform _parent;

        public ToolFactory(GameObject toolPrefab, Transform parent)
        {
            _toolPrefab = toolPrefab;
            _parent = parent;
        }

        public ITool CreateTool() //type
        {
            GameObject toolInstance = Object.Instantiate(_toolPrefab, _parent);
            return toolInstance.GetComponent<ITool>();
        }
    }
    
}
