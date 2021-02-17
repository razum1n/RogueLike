using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hazard : MonoBehaviour
{

    public float speed;
    public Transform[] movePoints;
    private int currentMovePoint = 0;

    Vector3 moveDirection;

    // Start is called before the first frame update
    void Start()
    {
        moveDirection = movePoints[currentMovePoint].position - transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        
        transform.position += moveDirection * speed * Time.deltaTime;
        if (moveDirection != Vector3.zero)
        {
            float angle = Mathf.Atan2(moveDirection.y, moveDirection.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
        }

        if (Vector3.Distance(transform.position, movePoints[currentMovePoint].position) < .5f)
        {
            
            currentMovePoint++;
            if (currentMovePoint >= movePoints.Length)
            {
                currentMovePoint = 0;
            }
            moveDirection = movePoints[currentMovePoint].position - transform.position;
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.tag == "Player")
        {
            PlayerHealthController.instance.DamagePlayer();
        }
    }
}
