using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPool<T> where T: MonoBehaviour, IPoolable
{
    private Queue<T> _pool = new Queue<T>();
    private T _prefab;
    private int _startingSize;

    public ObjectPool(T prefab, int size)
    {
        _prefab = prefab;
        _startingSize = size;
        
        for (int i = 0; i < _startingSize; i++)
        {
            CreateObject();
        }
    }

    public T Get(Vector3 position)
    {
        if(_pool.Count < 0)
        {
            CreateObject();
        }

        T obj = _pool.Dequeue();
        obj.Activate(position);
        return obj;
    }
    
    public void Return(T obj)
    {
        obj.Deactivate();
        _pool.Enqueue(obj);
    }

    private void CreateObject()
    {
        T obj = Object.Instantiate(_prefab);
        obj.Deactivate();
        _pool.Enqueue(obj);
    }
}
