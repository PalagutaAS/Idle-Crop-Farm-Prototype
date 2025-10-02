using System;

namespace Wallets
{
    public class Wallet : IWallet
    {
        private int _count;
        
        public event Action<int> OnChangedCoin;

        public Wallet(int count = 0)
        {
            Count = count;
        }

        public int Count
        {
            get => _count;
            private set
            {
                _count = value; 
                OnChangedCoin?.Invoke(_count);
            }
        }

        public bool Payment(int count)
        {
            if (count > _count) return false;

            Count -= count;
            return true;
        }

        public void Payout(int count)
        {
            Count += count;
        }
    }
}