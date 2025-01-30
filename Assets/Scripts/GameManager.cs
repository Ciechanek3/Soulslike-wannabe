using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[DefaultExecutionOrder(-1000)]
public class GameManager : MonoBehaviour
{
    private DIContainer _diContainer;

    void Awake()
    {
        _diContainer = new DIContainer();

        var allObjets = FindObjectsOfType<MonoBehaviour>();
        var dependencyProviders = allObjets.OfType<IDependencyProvider>();

        foreach (var obj in dependencyProviders)
        {
            _diContainer.AutoRegister(obj);
        }

        foreach (var obj in allObjets)
        {
            _diContainer.InjectDependencies(obj);
        }
    }
}
