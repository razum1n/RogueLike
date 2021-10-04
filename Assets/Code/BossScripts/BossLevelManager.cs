using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BossLevelManager : MonoBehaviour
{
    public static BossLevelManager instance;

    public GameObject mainCamera;
    public GameObject door;
    public GameObject doorSpriteL;
    public GameObject doorSpriteR;
    public BossController boss;
    public float waitToLoad = 3f;
    public float transitionSpeed = 10f;
    public bool startBoss = false;
    public bool endTimer = false;

    void Start()
    {
        instance = this;
        GameManager.instance.gameState = GameManager.GameState.Boss;
        Music.instance.ChangeTrack(3);
        Music.instance.TriggerTransition("DefaultVolume");
    }
    public void ActivateBoss()
    {
        door.SetActive(true);
        doorSpriteL.SetActive(false);
        doorSpriteR.SetActive(false);
        startBoss = true;
    }

    void Update()
    {
        float speed = transitionSpeed * Time.deltaTime;
        if (startBoss && mainCamera.transform.position.y <= 20f)
            mainCamera.transform.position += new Vector3(0f,speed,0f);
        else if(mainCamera.transform.position.y >= 20f)
        {
            boss.bossActive = true;
            doorSpriteL.SetActive(true);
            doorSpriteR.SetActive(true);
            UIController.instance.bossHealth.SetActive(true);
        }

        if(endTimer)
        {
            if (waitToLoad > 0)
                waitToLoad -= Time.deltaTime;
            else
                LevelEnd();
        }

    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            ActivateBoss();
        }
    }

    public void LevelEnd()
    {
        Timer.instance.FinalTime();
        SceneManager.LoadScene(4);
    }
}
