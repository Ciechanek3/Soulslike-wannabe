using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour, IEnemy, ILockable
{
    public Transform LockTransform => transform;
}
