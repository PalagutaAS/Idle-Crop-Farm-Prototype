using System.Linq;
using Player.Interface;
using Player.Slots;
using Tools.Interface;
using UnityEngine;

namespace Player.Tools
{
    public class PlayerTools : MonoBehaviour, IToolManager
    {
        [SerializeField] private Slot[] _slots;
        [SerializeField] private GameObject _toolPrefab;
        [SerializeField] private Transform _parentForTool;
    
        private IToolFactory _toolFactory;
        private IPlayer _player;
    
        private void Awake()
        {
            _toolFactory = new ToolFactory(_toolPrefab, _parentForTool);
        }
        
        public void Init(IPlayer player)
        {
            _player = player;
        }
    
        public bool TrySetupNewTool()
        {
            ISlot freeSlot = GetEmptySlot();
            if (freeSlot == null) return false;

            ITool newTool = _toolFactory.CreateTool();
            newTool.Initialize(_player, freeSlot);
        
            freeSlot.SetTool(newTool);
            return true;
        }

        public ISlot GetEmptySlot()
        {
            return _slots.FirstOrDefault(s => !s.IsOccupied);
        }

    }
}