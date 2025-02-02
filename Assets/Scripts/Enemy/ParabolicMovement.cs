using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParabolicMovement : MonoBehaviour
{
    [SerializeField] private Transform _target;
    [SerializeField] private float _moveSpeed;
    [SerializeField] private float _maxHeight;
    [SerializeField] private Transform bottomTransform;
    private Vector3 startPos;
    private Vector3 endPos;

    private float progress = 0f;

    public bool ShouldMove;

    private void Start()
    {
        startPos = transform.position;
        endPos = _target.position;
    }

    private void Update()
    {
        if(ShouldMove)
        {
            if (progress < 1f)
            {
                progress += Time.deltaTime * _moveSpeed;

                float height = Mathf.Sin(progress * Mathf.PI) * _maxHeight;
                transform.position = Vector3.Lerp(startPos, endPos, progress) + Vector3.up * height;
            }
            else
            {
                FinishMove();
            }
        }
    }

    private void FinishMove()
    {
        ShouldMove = false;
        progress = 0;
    }

    private void OnCollisionEnter(Collision collision)
    {
        FinishMove();
    }
}
