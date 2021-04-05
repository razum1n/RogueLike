using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public Rigidbody2D rb;
    public Animator anim;
    public GameObject key;

    public enum EnemyType
    {
        Zombie,
        Demon,
        Necromancer
    }

    public EnemyType enemy;

    #region Zombie Variables
    public float zombieChaseRange;
    private Vector3 moveDirection;
    public float zombieMoveSpeed;
    #endregion

    #region Demon Variables
    public float demonActivationRange;
    private float fireCounter;
    public float demonFireRate;
    public GameObject fireBall;
    public GameObject powerUp;
    #endregion

    #region Necromancer Variables
    public float necrFireRate;
    private float nextLaser;
    public GameObject laserBeam;
    private GameObject currentLaser;
    public float offSet;
    #endregion

    public GameObject[] deathEffect;
    public Transform firePoint;
    public bool enemyActive;
    public bool hasKey = false;
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
        if(PlayerController.instance.gameObject.activeInHierarchy && enemyActive)
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
                            powerUp.SetActive(true);
                        }
                    }
                    break;
                case EnemyType.Necromancer:
                    if (nextLaser > 0)
                    {
                        nextLaser -= Time.deltaTime;
                    }
                    else if(nextLaser <= 0)
                    {
                        Vector3 direction = PlayerController.instance.transform.position - transform.position;
                        direction.Normalize();
                        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
                        currentLaser = Instantiate(laserBeam, transform.position, Quaternion.Euler(0, 0, angle - offSet));
                        nextLaser = necrFireRate;
                    }
                    break;
            }
        }


    }

    public void DamageEnemy(int damage)
    {
        health -= damage;

        AudioManager.instance.PlaySound("ogreHit");

        if(health <= 0)
        {
            int selectedSplatter = Random.Range(0, deathEffect.Length);

            Instantiate(deathEffect[selectedSplatter], transform.position, transform.rotation);

            if(hasKey)
                Instantiate(key, transform.position, transform.rotation);
            if (enemy == EnemyType.Necromancer)
                Destroy(currentLaser);
            Destroy(gameObject);
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
