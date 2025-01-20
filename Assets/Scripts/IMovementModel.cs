using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IMovementModel
{
    void GetVelocityAndRotation(ref Vector3 velocity, ref Quaternion rotation);
}
