﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public Rigidbody2D rb;
    public Animator anim;
    public GameObject key;
    public GameObject healthDrop;

    public enum EnemyType
    {
        Zombie,
        Demon,
        Necromancer
    }

    private Vector3 moveDirection;
    public EnemyType enemy;

    public float chaseRange;
    public float healthDropChance;

    public float moveSpeed;

    public float activationRange;
    public float activationTime;
    private float fireCounter;
    public float fireRate;
    public GameObject fireBall;
    public GameObject powerUp;

    public float laserRate;
    private float nextLaser = 1.5f;
    private float defaultActivationTime;
    public float laserOnTime;
    public GameObject laserBeam;
    private GameObject currentLaser;
    public float offSet;

    public GameObject[] deathEffect;
    public Transform firePoint;
    public bool enemyActive;
    public bool canAct;
    public bool hasKey = false;
    public int health = 150;
    public float score;
    public int touchDamage = 1;
    public SpriteRenderer theBody;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        theBody = GetComponent<SpriteRenderer>();
        defaultActivationTime = activationTime;
    }

    // Update is called once per frame
    void Update()
    {
        if(activationTime <= 0 && enemyActive)
        {
            canAct = true;
            enemyActive = false;
            activationTime = defaultActivationTime;
        }

        if(enemyActive)
        {
            activationTime -= Time.deltaTime;
        }

        if(PlayerController.instance.gameObject.activeInHierarchy && canAct)
        {
            switch (enemy)
            {
                case EnemyType.Zombie:
                    if (Vector3.Distance(transform.position, PlayerController.instance.transform.position) < chaseRange)
                    {
                        moveDirection = PlayerController.instance.transform.position - transform.position;
                    }
                    moveDirection.Normalize();
                    rb.velocity = moveDirection * moveSpeed;

                    if (moveDirection != Vector3.zero)
                    {
                        anim.SetBool("isMoving", true);
                    }

                    if (PlayerController.instance.transform.position.x > transform.position.x)
                        transform.localScale = new Vector3(1f,1f,1f);
                    else
                        transform.localScale = new Vector3(-1f, 1f, 1f);

                    break;
                case EnemyType.Demon:
                    if (Vector3.Distance(transform.position, PlayerController.instance.transform.position) < activationRange && theBody.isVisible)
                    {
                        fireCounter -= Time.deltaTime;

                        if (fireCounter <= 0)
                        {
                            fireCounter = fireRate;
                            powerUp.SetActive(true);
                        }

                        if (PlayerController.instance.transform.position.x > transform.position.x)
                            transform.localScale = new Vector3(1f, 1f, 1f);
                        else
                            transform.localScale = new Vector3(-1f, 1f, 1f);
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
                        currentLaser.GetComponent<LaserBeam>().onTimer = laserOnTime;
                        nextLaser = laserRate;
                    }
                    if (currentLaser == null)
                    {
                        if (PlayerController.instance.transform.position.x > transform.position.x)
                            transform.localScale = new Vector3(1f, 1f, 1f);
                        else
                            transform.localScale = new Vector3(-1f, 1f, 1f);
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
            GameManager.instance.Score(score);

            if(PlayerController.instance.playerScoreMultiplier < 3)
            {
                UIController.instance.multiplier.value += 0.2f;
            }

            UIController.instance.UpdateScoreText(GameManager.instance.playerScore);

            if(hasKey)
                Instantiate(key, transform.position, transform.rotation);
            if (enemy == EnemyType.Necromancer)
                Destroy(currentLaser);
            float dropChance = Random.Range(1f, 100f);
            if (dropChance < healthDropChance)
            {

                Instantiate(healthDrop, transform.position, transform.rotation);
            }
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
