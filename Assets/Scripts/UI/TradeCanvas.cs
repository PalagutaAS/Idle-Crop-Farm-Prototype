using Offers;
using UnityEngine;

namespace UI
{
    public class TradeCanvas : MonoBehaviour
    {
        
        private void Awake()
        {
            Close();
        }

        public void Show(Offer offer)
        {
            gameObject.SetActive(true);
            
        }

        public void Close()
        {
            gameObject.SetActive(false);
        }
    }
}