using UnityEngine;
using UnityEngine.UI;
using VContainer;
using Wallets;

namespace UI
{
    public class PrintCount : MonoBehaviour
    {
        [SerializeField] private CropType _type;
        private Text _text;

        [Inject]
        private void Constructor(IChangedInventory inventoryItem, IWallet wallet)
        {
            _text = GetComponent<Text>();
            if (_type == CropType.None)
            {
                wallet.OnChanged += PrintCoin;
                Print(wallet.Count.ToString());
            }
            else
            {
                inventoryItem.OnChangedByType += PrintByType; 
            }
        }

        private void PrintByType(CropType type, int count)
        {
            if (_type == type)
            {
                Print(count.ToString());
            }
        }
        
        private void PrintCoin(int count)
        {
            Print(count.ToString());
        }

        private void Print(string text)
        {
            _text.text = text;
        }
        
    }
}
