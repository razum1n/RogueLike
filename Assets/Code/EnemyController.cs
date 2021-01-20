using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{

    public Rigidbody2D rb;
    public Animator anim;

    public enum EnemyType
    {
        Zombie,
        Demon
    }

    public EnemyType enemy;

    #region Zombie Variables
    
    private Vector3 moveDirection;
    public float moveSpeed;
    #endregion

    #region Demon Variables
    public bool shouldShoot;
    private float fireCounter;
    public float fireRate;
    public GameObject fireBall;
    public Transform firePoint;
    #endregion

    public GameObject[] deathEffect;

    public int health = 150;
    public float enemyActivationRange;
    public SpriteRenderer theBody;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        theBody = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        if(PlayerController.instance.gameObject.activeInHierarchy)
        {
            switch (enemy)
            {
                case EnemyType.Zombie:
                    if (Vector3.Distance(transform.position, PlayerController.instance.transform.position) < enemyActivationRange)
                    {
                        moveDirection = PlayerController.instance.transform.position - transform.position;
                    }
                    moveDirection.Normalize();
                    rb.velocity = moveDirection * moveSpeed;
                    if (moveDirection != Vector3.zero)
                    {
                        anim.SetBool("isMoving", true);
                    }
                    break;
                case EnemyType.Demon:
                    if (Vector3.Distance(transform.position, PlayerController.instance.transform.position) < enemyActivationRange && theBody.isVisible)
                    {
                        fireCounter -= Time.deltaTime;

                        if (fireCounter <= 0)
                        {
                            fireCounter = fireRate;
                            Instantiate(fireBall, firePoint.position, firePoint.rotation);
                        }
                    }
                    break;
            }
        }


    }

    public void DamageEnemy(int damage)
    {
        health -= damage;

        if(health <= 0)
        {
            Destroy(gameObject);

            int selectedSplatter = Random.Range(0, deathEffect.Length);

            Instantiate(deathEffect[selectedSplatter], transform.position, transform.rotation);
        }
    }
}
