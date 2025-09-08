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
            EventsHolder.UpdateWallet.AddListener(Print);
            Print(0);
        }

        void Print(int _count)
        {
            _text.text = "COIN: " + _count;
        }
        
    }
}
