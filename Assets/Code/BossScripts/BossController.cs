using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossController : MonoBehaviour
{

    public GameObject laserBeam;
    public GameObject fireBall;
    public GameObject deathEffect;
    public bool bossActive = false;
    public float maxHealth;
    public float currentHealth;
    public int laserNumber;
    public int fireballCount;
    private int currentLaserNumber;
    private int currentFireballCount;
    public float laserTimer;
    public float laserOffset;
    private float fireOffset = 90f;
    public float fireballRate;
    public float laserRate;
    public float laserRotateSpeed;
    private float nextLaser;
    private float nextFire;
    [SerializeField]private float waitTime;
    private GameObject currentLaser;
    private Dissolve dissolve;
    private List<GameObject> lasers = new List<GameObject>();

    // Start is called before the first frame update
    void Start()
    {
        dissolve = GetComponent<Dissolve>();
        nextLaser = laserRate;
        nextFire = fireballRate;
        currentHealth = maxHealth;
        currentLaserNumber = laserNumber;
        currentFireballCount = fireballCount;

        if (DifficultyController.instance.selectedDifficulty == DifficultyController.SelectedDifficulty.Hard)
        {
            fireballCount = 12;
            fireballRate = 1;
            laserRotateSpeed = 15f;
            currentHealth = 19000f;
        }
        else if(DifficultyController.instance.enemyDifficulty == DifficultyController.EnemyDifficulty.Easy)
        {
            laserNumber = 3;
            fireballRate = 3;
            currentHealth = 15000;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(currentHealth == maxHealth / 2)
        {
            currentLaserNumber = laserNumber * 2;
            currentFireballCount = fireballCount * 2;
        }

        if(bossActive)
        {
            if (nextLaser > 0)
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
        }

    }

    void LaserAttack()
    {
        lasers.Clear();
        for(int i=0;i< currentLaserNumber; i++)
        {
            currentLaser = Instantiate(laserBeam, transform.position, transform.rotation * Quaternion.Euler(0f, 0f, laserOffset * i));
            if (i != 0)
                currentLaser.GetComponent<AudioSource>().enabled = false;
            currentLaser.GetComponent<LaserBeam>().onTimer = laserTimer;
            currentLaser.GetComponent<rotate>().rotationActive = true;
            currentLaser.GetComponent<rotate>().speed = laserRotateSpeed;
            lasers.Add(currentLaser);
        }
    }

    void SpawnFireBalls()
    {
        AudioManager.instance.PlaySound("fireball");
        for(int i=0;i<currentFireballCount;i++)
        {
            GameObject currentFireBall = Instantiate(fireBall, transform.position, transform.rotation * Quaternion.Euler(0f, 0f, fireOffset * i));
            currentFireBall.GetComponent<EnemyAttack>().hoaming = false;
        }
        fireOffset = Random.Range(5f,180f);
    }

    public void TakeDamage(float damage)
    {
        currentHealth -= damage;
        if (currentHealth <= 0)
        {
            StartCoroutine(BossEnd());
        }
        else
        {
            UIController.instance.SetBossHealth(currentHealth / maxHealth);
        }
    }

    private IEnumerator BossEnd()
    {
        dissolve.isDissolving = true;
        if(lasers[0] != null)
        {
            foreach (GameObject laser in lasers)
            {
                laser.SetActive(false);
            }
        }
        GetComponent<Collider2D>().enabled = false;
        BossLevelManager.instance.endTimer = true;
        bossActive = false;
        Music.instance.TriggerTransition("LowerVolume");
        yield return new WaitForSeconds(waitTime);
        UIController.instance.StartFadeToBlack();
    }
}


