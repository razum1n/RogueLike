using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{

    public static LevelManager instance;

    public float waitToLoad = 1f;

    public string nextLevel;

    public bool isPaused =false;

    void Awake()
    {
        if(instance == null)
            instance = this;

    }

    // Start is called before the first frame update
    void Start()
    {
        Time.timeScale = 1f;
        Scene currentScene = SceneManager.GetActiveScene();
        if(currentScene.name == "Level 1")
        {
            Music.instance.ChangeTrack(2);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            PauseUnpause();
        }
    }

    public IEnumerator LevelEnd()
    {
        PlayerController.instance.canMove = false;

        UIController.instance.StartFadeToBlack();

        yield return new WaitForSeconds(waitToLoad);
        GameManager.instance.currentHealth = PlayerHealthController.instance.currentHealth;
        GameManager.instance.stageEnemies.Clear();
        Timer.instance.EndTimer();
        UIController.instance.levelEndScreen.SetActive(true);
    }

    public void PauseUnpause()
    {
        if(!isPaused)
        {
            UIController.instance.pauseMenu.SetActive(true);

            isPaused = true;

            Time.timeScale = 0f;
        }
        else
        {
            UIController.instance.pauseMenu.SetActive(false);

            isPaused = false;

            Time.timeScale = 1f;
        }
    }

    public void LoadNextLevel()
    {
        SceneManager.LoadScene(nextLevel);
    }

}
