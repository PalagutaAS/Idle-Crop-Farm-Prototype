using UnityEngine.Events;

namespace DefaultNamespace
{
    public class EventsHolder
    {
        public static UnityEvent<int> UpdateWallet = new();
    }
}