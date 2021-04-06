using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttack : MonoBehaviour
{
    public float speed;
    public Vector3 direction;
    public bool hoaming = true;
    public GameObject hitEffect;

    // Update is called once per frame
    void Update()
    {
        if(hoaming)
        {
            transform.position += direction * speed * Time.deltaTime;
            if (direction != Vector3.zero)
            {
                float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
                transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
            }
        }
        else if(!hoaming)
        {
            direction = Vector3.right;
            //if (direction != Vector3.zero)
            //{
            //    float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
            //    transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
            //}
            transform.Translate(direction *speed *Time.deltaTime, Space.Self);
        }

    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.tag == "Player")
        {
            PlayerHealthController.instance.DamagePlayer();
            Instantiate(hitEffect, transform.position, transform.rotation);
        }
        this.gameObject.SetActive(false);
        if(!hoaming)
        {
            Destroy(this.gameObject);
        }
    }

    public void GetDirection()
    {
        direction = PlayerController.instance.transform.position - transform.position;
        direction.Normalize();
    }

    void OnBecameInvisible()
    {
        this.gameObject.SetActive(false);
    }
}
