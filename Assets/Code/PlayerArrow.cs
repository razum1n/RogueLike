using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerArrow : MonoBehaviour
{

    public float speed = 7.5f;
    public Rigidbody2D rb;

    public GameObject impactEffect;
    public GameObject enemyImpactEffect;

    public int damage;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        rb.velocity = transform.right * speed;
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.tag == "Enemy")
        {
            Instantiate(enemyImpactEffect, transform.position, transform.rotation);
            other.GetComponent<EnemyController>().DamageEnemy(damage);
            this.gameObject.SetActive(false);
        }
        else if(other.tag == "Untagged")
        {
            Instantiate(impactEffect, transform.position, transform.rotation);
            this.gameObject.SetActive(false);
        }

    }

    private void OnBecameInvisible()
    {
        this.gameObject.SetActive(false);
    }
}
