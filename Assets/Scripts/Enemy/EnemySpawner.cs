using System;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [Enemy]
    [SerializeField] private string enemyType;

    public Type A => Type.GetType(enemyType);
}
