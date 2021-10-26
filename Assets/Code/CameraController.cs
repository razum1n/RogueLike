using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public static CameraController instance;

    public float moveSpeed;

    public Transform target;

    public bool isTransitioning = false;

    public float xOffset = 0.5f;
    public float yOffset = 0f;

    void Awake()
    {
        instance = this;
    }

    void Update()
    {
        if(target != null)
            transform.position = Vector3.MoveTowards(transform.position,
                new Vector3(target.position.x + xOffset,target.position.y+yOffset,
                transform.position.z), moveSpeed * Time.deltaTime);
    }

    public void ChangeTarget(Transform newTarget)
    {
        target = newTarget;
    }
}
