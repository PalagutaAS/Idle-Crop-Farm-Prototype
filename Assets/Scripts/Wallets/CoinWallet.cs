using Crop;
using UnityEngine;

namespace Wallets
{
    public class CoinWallet : IWallet
    {
        private int _count;
        public int Count => _count;

        public bool Payment(int count)
        {
            if (count > _count) 
                return false;

            _count -= count;
            return true;
        }

        public void Payout(int count)
        {
            _count += count;
            Debug.Log("COIN:" + _count);
        }

        public void AddToWallet(PlayerWallet wallet, int amount)
        {
            throw new System.NotImplementedException();
        }
    }
}
