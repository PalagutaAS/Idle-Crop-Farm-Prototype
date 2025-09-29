using DefaultNamespace;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class PrintCoinCount : MonoBehaviour
    {
        [SerializeField] private Text _text;
        void Awake()
        {
            Print(0);
            EventsHolder.UpdateWallet.AddListener(Print);
        }

        void Print(int _count)
        {
            // GCollector++
            _text.text = $"COIN: {_count}" ;
        }
        
    }
}
