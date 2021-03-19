using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UIController : MonoBehaviour
{
    public static UIController instance;

    public GameObject deathScreen;
    public GameObject pauseMenu;
    public GameObject levelEndScreen;
    public GameObject settingsMenu;

    public Image fadeScreen;
    public float fadeSpeed;
    public bool fadeToBlack, fadeOutBlack;

    public string newGameScene;
    public string mainMenuScreen;

    public GameObject[] uiHealth;

    public GameObject[] uiDamage;

    void Awake()
    {
        instance = this;
    }
    // Start is called before the first frame update
    void Start()
    {
        fadeOutBlack = true;
        fadeToBlack = false;
    }

    // Update is called once per frame
    void Update()
    {
        if(fadeOutBlack)
        {
            fadeScreen.color = new Color(fadeScreen.color.r, fadeScreen.color.g, fadeScreen.color.b, Mathf.MoveTowards(fadeScreen.color.a, 0f, fadeSpeed * Time.deltaTime));
            if(fadeScreen.color.a <= 0f)
            {
                fadeOutBlack = false;
            }
        }

        if(fadeToBlack)
        {
            fadeScreen.color = new Color(fadeScreen.color.r, fadeScreen.color.g, fadeScreen.color.b, Mathf.MoveTowards(fadeScreen.color.a, 1f, fadeSpeed * Time.deltaTime));
            if (fadeScreen.color.a <= 0f)
            {
                fadeToBlack = false;
            }
        }
    }

    public void StartFadeToBlack()
    {
        fadeToBlack = true;
        fadeOutBlack = false;
    }

    public void UpdatePlayerHealth(int maxHealth,int currentHealth)
    {
        int damageNumber = 0;

        for (int i = 0; i < maxHealth; i++)
        {
            if (i >= currentHealth)
            {
                uiHealth[i].SetActive(false);
                uiDamage[damageNumber].SetActive(true);
                damageNumber++;
            }
            else
            {
                uiHealth[i].SetActive(true);
                uiDamage[i].SetActive(false);
            }
        }
    }

    public void NewGame()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(newGameScene);
    }

    public void Quit()
    {
        Application.Quit();
    }

    public void ReturnToMainMenu()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(mainMenuScreen);
    }
    public void ActivateDeathScreen()
    {
        deathScreen.SetActive(true);
    }

    public void HealPlayer()
    {
        if(PlayerStats.instance.currentHealth < PlayerHealthController.instance.maxHealth)
        {
            PlayerStats.instance.currentHealth += 1;
            LevelManager.instance.LoadNextLevel();
        }
    }

    public void UpgradeSpeed()
    {
        PlayerStats.instance.playerSpeed += 0.5f;
        LevelManager.instance.LoadNextLevel();
    }

    public void UpgradeDamage()
    {
        if (PlayerStats.instance.playerArrow == "ArrowOne")
        {
            PlayerStats.instance.playerArrow = "ArrowTwo";
            LevelManager.instance.LoadNextLevel();
        }
        else if (PlayerController.instance.arrowType == "ArrowTwo")
        {
            PlayerStats.instance.playerArrow = "ArrowThree";
            LevelManager.instance.LoadNextLevel();
        }
        else
            Debug.Log("Can't upgrade arrows further");
    }

    public void SettingsToggle()
    {
        settingsMenu.SetActive(!settingsMenu.activeSelf);
        pauseMenu.SetActive(!pauseMenu.activeSelf);
    }

    public void ResumePlay()
    {
        LevelManager.instance.PauseUnpause();
    }
}
