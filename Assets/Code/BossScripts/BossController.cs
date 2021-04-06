using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossController : MonoBehaviour
{

    public GameObject laserBeam;
    public GameObject fireBall;
    public float maxHealth;
    public float currentHealth;
    public int laserNumber;
    public int fireballCount;
    public float laserTimer;
    public float laserOffset;
    private float fireOffset = 90f;
    public float fireballRate;
    public float laserRate;
    private float nextLaser;
    private float nextFire;
    private GameObject currentLaser;
    // Start is called before the first frame update
    void Start()
    {
        nextLaser = laserRate;
        nextFire = fireballRate;
        currentHealth = maxHealth;
    }

    // Update is called once per frame
    void Update()
    {
        if(nextLaser > 0)
        {
            nextLaser -= Time.deltaTime;
        }
        else
        {
            LaserAttack();
            nextLaser = laserRate;
        }

        if (nextFire > 0)
        {
            nextFire -= Time.deltaTime;
        }
        else
        {
            SpawnFireBalls();
            nextFire = fireballRate;
        }

        if(Input.GetKeyDown(KeyCode.Space))
        {
            TakeDamage(15f);
        }
    }

    void LaserAttack()
    {
        for(int i=0;i<laserNumber;i++)
        {
            currentLaser = Instantiate(laserBeam, transform.position, transform.rotation * Quaternion.Euler(0f, 0f, laserOffset * i));
            if (i != 0)
                currentLaser.GetComponent<AudioSource>().enabled = false;
            currentLaser.GetComponent<LaserBeam>().onTimer = laserTimer;
            currentLaser.GetComponent<rotate>().rotationActive = true;
            currentLaser.GetComponent<rotate>().speed = 10f;
        }
    }

    void SpawnFireBalls()
    {
        AudioManager.instance.PlaySound("fireball");
        for(int i=0;i<fireballCount;i++)
        {
            GameObject currentFireBall = Instantiate(fireBall, transform.position, transform.rotation * Quaternion.Euler(0f, 0f, fireOffset * i));
            currentFireBall.GetComponent<EnemyAttack>().hoaming = false;
        }
        fireOffset = Random.Range(5f,180f);
    }

    public void TakeDamage(float damage)
    {
        currentHealth -= damage;
        UIController.instance.SetBossHealth(currentHealth / maxHealth);
    }
}
