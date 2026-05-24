using UnityEngine;

namespace UI
{
    public class PrintMoney : PrintCount
    {
        [SerializeField] private MoneyType _type;

        public MoneyType Type => _type;
    }
}
