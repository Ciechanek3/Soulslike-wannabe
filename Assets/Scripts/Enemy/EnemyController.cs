using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    private List<IEnemy> _enemies;

    [Header("Pool settings")]
    [SerializeField] private List<EnemyPoolSettings> enemyPools;

    [System.Serializable]
    public struct EnemyPoolSettings
    {
        public GameObject EnemyPrefab;
        public int InitialAmount;
    }
}
