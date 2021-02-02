using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public static CameraController instance;

    public float moveSpeed;

    public Transform target;

    public bool isTransitioning = false;


    void Awake()
    {
        instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(target != null)
            transform.position = Vector3.MoveTowards(transform.position, new Vector3(target.position.x,target.position.y,transform.position.z), moveSpeed * Time.deltaTime);

        Debug.Log(Vector2.Distance(target.position, transform.position));

        if(Vector2.Distance(transform.position,target.position) > 0)
        {
            PlayerController.instance.canMove = false;
        }
        else
        {
            PlayerController.instance.canMove = true;
        }
    }

    public void ChangeTarget(Transform newTarget)
    {
        target = newTarget;
    }
}
