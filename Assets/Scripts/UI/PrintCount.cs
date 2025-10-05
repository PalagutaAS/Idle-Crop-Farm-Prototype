using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class PrintCount : MonoBehaviour
    {
        [SerializeField] private InventoryType _type;
        [SerializeField] private Text _text;
        public InventoryType GetSupportType => _type;
        
        public void Print(int count)
        {
            _text.text = count.ToString();
        }
    }
}
