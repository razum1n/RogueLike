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
            Music.instance.TriggerTransition("FadeIn");
        }
        else if(currentScene.name == "Level 2")
        {
            Music.instance.TriggerTransition("DefaultVolume");
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetButtonDown("Cancel"))
        {
            PauseUnpause();
        }
    }

    public IEnumerator LevelEnd()
    {
        PlayerController.instance.canMove = false;

        UIController.instance.StartFadeToBlack();
        Music.instance.TriggerTransition("LowerVolume");
        yield return new WaitForSeconds(waitToLoad);
        GameManager.instance.currentHealth = PlayerHealthController.instance.currentHealth;
        GameManager.instance.stageEnemies.Clear();
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
            UIController.instance.settingsMenu.SetActive(false);
            isPaused = false;

            Time.timeScale = 1f;
        }
    }

    public void LoadNextLevel()
    {
        SceneManager.LoadScene(nextLevel);
    }

}
