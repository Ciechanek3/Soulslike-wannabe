using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IInputObserver
{
    void OnInputChanged(Vector3 moveVector);
}
