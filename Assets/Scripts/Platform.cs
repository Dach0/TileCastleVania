using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Platform : MonoBehaviour
{

    public Transform posLeft, posRight;

    Vector3 nextPos = default;
    // Start is called before the first frame update
    void Start()
    {
        if (UnityEngine.Random.value < 0.5f)
        {
            nextPos = posLeft.localPosition;
        }
        else
        {
            nextPos = posRight.localPosition;
        }
    }

    // Update is called once per frame
    void Update()
    {
        MovePlatformHorizontally();
    }

    private void MovePlatformHorizontally()
    {
        if (transform.localPosition == posLeft.localPosition)
        {
            nextPos = posRight.localPosition;
        }
        if (transform.localPosition == posRight.localPosition)
        {
            nextPos = posLeft.localPosition;
        }

        transform.localPosition = Vector3.MoveTowards(transform.localPosition, nextPos,UnityEngine.Random.Range(1, 4) * Time.deltaTime);
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawLine(posLeft.localPosition, posRight.localPosition);
    }
}
