using System;
using System.Collections;
using AI;
using UI;
using UnityEngine;

namespace Offers
{
    public class OfferTimeout : MonoBehaviour, IOfferTimeout, IDisposable
    {
        [SerializeField] private float _duration = 10f;
        [SerializeField] private PrintOfferTimer _printTimer;
        private CustomerController _customer;
        private Coroutine _timeoutRoutine;
        private Action _action;

        public void AddTimer(CustomerController customer, Action actionOnOut)
        {
            Dispose();
            _printTimer.ShowUI(_duration);
            _action = actionOnOut;
            _customer = customer;
            _timeoutRoutine = StartCoroutine(TimeoutCoroutine());
        }

        private IEnumerator TimeoutCoroutine()
        {
            float elapsedTime = 0;
            while (elapsedTime < _duration && _customer.Offer.Active)
            {
                elapsedTime += Time.deltaTime;
                _printTimer.Tick(elapsedTime);
                yield return null;
            }
            
            if (_customer.Offer.Active)
            {
                _customer.CancelDeal();
                _action?.Invoke();
            }
            Dispose();
        }
        
        public void Dispose()
        {
            if (_timeoutRoutine != null) 
                StopCoroutine(_timeoutRoutine);

            _printTimer.Close();
            _customer = null;
            _action = null;
        }
    }

    public interface IOfferTimeout
    {
        public void AddTimer(CustomerController customer, Action actionOnOut);
    }
}