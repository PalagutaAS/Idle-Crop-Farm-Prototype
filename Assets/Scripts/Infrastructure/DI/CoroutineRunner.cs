using System.Collections;
using UnityEngine;

namespace Infrastructure.DI
{
    public interface ICoroutineRunner
    {
        Coroutine StartCoroutine(IEnumerator coroutine);
    }
    public class CoroutineRunner : MonoBehaviour, ICoroutineRunner { }
}