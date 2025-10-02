using UnityEngine;
using UnityEngine.UI;
using VContainer;
using Wallets;

namespace UI
{
    public class PrintCoinCount : MonoBehaviour
    {
        [SerializeField] private Text _text;

        [Inject]
        private void Constructor(Wallet wallet)
        {
            wallet.OnChangedCoin += Print;
            Print(wallet.Count);
        }

        void Print(int count)
        {
            _text.text = $"COIN: {count}";
        }
        
    }
}
