using Infrastructure.DI;
using UnityEngine;

public class InitBootstrapper : MonoBehaviour
{
    [SerializeField] private BootstrapperLifetimeScope _bootstrapper;
    void Awake()
    {
        DontDestroyOnLoad(Instantiate(_bootstrapper));
    }
}
