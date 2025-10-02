using Tools;
using Tools.Interface;
using UnityEngine;

namespace Player.Tools
{
    public class ToolFactory : IToolFactory
    {
        private readonly Tool _toolPrefab;
        private readonly Transform _parent;

        protected ToolFactory(Tool toolPrefab)
        {
            _parent = new GameObject("TOOLS").transform;
            _toolPrefab = toolPrefab;
        }

        public ITool CreateTool() //type
        {
            GameObject toolInstance = Object.Instantiate(_toolPrefab.gameObject, _parent);
            return toolInstance.GetComponent<ITool>();
        }
    }
    
}
