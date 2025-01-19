using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IMovementModel
{
    (Vector3, Quaternion) GetVelocityAndRotation { get; }
}
