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
        Demon,
        Runner
    }

    public EnemyType enemy;

    #region Zombie Variables
    public float zombieChaseRange;
    private Vector3 moveDirection;
    public float zombieMoveSpeed;
    #endregion

    #region Demon Variables
    public bool shouldShoot;
    public float demonActivationRange;
    private float fireCounter;
    public float demonFireRate;
    public GameObject fireBall;
    public Transform firePoint;
    #endregion


    #region Runner Variables
    public float runningEnemySpeed;
    public float runnerActivationRange;
    public float runnerMoveSpeed;
    #endregion



    public GameObject[] deathEffect;

    public int health = 150;
    public int touchDamage = 1;
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
                    if (Vector3.Distance(transform.position, PlayerController.instance.transform.position) < zombieChaseRange)
                    {
                        moveDirection = PlayerController.instance.transform.position - transform.position;
                    }
                    moveDirection.Normalize();
                    rb.velocity = moveDirection * zombieMoveSpeed;
                    if (moveDirection != Vector3.zero)
                    {
                        anim.SetBool("isMoving", true);
                    }
                    break;
                case EnemyType.Demon:
                    if (Vector3.Distance(transform.position, PlayerController.instance.transform.position) < demonActivationRange && theBody.isVisible)
                    {
                        fireCounter -= Time.deltaTime;

                        if (fireCounter <= 0)
                        {
                            fireCounter = demonFireRate;
                            Instantiate(fireBall, firePoint.position, firePoint.rotation);
                        }
                    }
                    break;
                case EnemyType.Runner:
                    if(Vector3.Distance(transform.position,PlayerController.instance.transform.position)< runnerActivationRange)
                    {
                        moveDirection = transform.position - PlayerController.instance.transform.position;
                    }
                    moveDirection.Normalize();
                    rb.velocity = moveDirection * runnerMoveSpeed;
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

    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.tag == "Player")
        {
            PlayerHealthController.instance.DamagePlayer();
        }
    }
}
