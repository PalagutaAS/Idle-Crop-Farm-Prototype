using System;
using System.Collections;
using AI;
using UnityEngine;

namespace Offers
{
    public class OfferTimeout : MonoBehaviour, IOfferTimeout ,IDisposable
    {
        [SerializeField] private float _duration = 10f;
        private CustomerController _customer;
        private Coroutine _timeoutRoutine;
        public event Action OnTimerOut;

        public void AddTimer(CustomerController customer)
        {
            Dispose();
            _customer = customer;
            _timeoutRoutine = StartCoroutine(TimeoutCoroutine());
        }

        private IEnumerator TimeoutCoroutine()
        {
            yield return new WaitForSeconds(_duration);
            _customer.CancelDeal();
            Dispose();
            OnTimerOut?.Invoke();
        }

        public void Dispose()
        {
            if (_timeoutRoutine != null) 
                StopCoroutine(_timeoutRoutine);
            
            _customer = null;
        }
    }

    public interface IOfferTimeout
    {
        public void AddTimer(CustomerController customer);
    }
}