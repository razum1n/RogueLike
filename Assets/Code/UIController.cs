﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class UIController : MonoBehaviour
{
    public static UIController instance;

    public GameObject deathScreen;
    public GameObject pauseMenu;
    public GameObject levelEndScreen;
    public GameObject settingsMenu;
    public GameObject finalStats;
    public GameObject keyInfo;
    public GameObject keyImg;
    public GameObject bossHealth;
    public GameObject timer;

    public TMP_Text scoreText;
    public TMP_Text finalScore;
    public TMP_Text finalTime;
    public Image fadeScreen;
    public Transform bossHealthBar;
    public float fadeSpeed;
    public bool fadeToBlack, fadeOutBlack;

    public string newGameScene;
    public string mainMenuScreen;

    public GameObject[] uiHealth;

    public GameObject[] uiDamage;

    public AudioMixer audioMixer;

    void Awake()
    {
        instance = this;
    }
    // Start is called before the first frame update
    void Start()
    {
        fadeOutBlack = true;
        fadeToBlack = false;
        timer.SetActive(GameManager.instance.showTimer);
        scoreText.text = "Score: " + GameManager.instance.playerScore.ToString();
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
        AudioManager.instance.PlaySound("uiClick");
        Application.Quit();
    }

    public void ReturnToMainMenu()
    {
        AudioManager.instance.PlaySound("uiClick");
        Time.timeScale = 1f;
        SceneManager.LoadScene(mainMenuScreen);
    }
    public void ActivateDeathScreen()
    {
        deathScreen.SetActive(true);
    }

    public void HealPlayer()
    {
        if(PlayerHealthController.instance.currentHealth < PlayerHealthController.instance.maxHealth)
        {
            GameManager.instance.currentHealth += 1;
            LevelManager.instance.LoadNextLevel();
        }
    }

    public void UpgradeSpeed()
    {
        GameManager.instance.playerSpeed += 0.5f;
        LevelManager.instance.LoadNextLevel();
    }

    public void UpgradeDamage()
    {
        if (GameManager.instance.playerArrow == "ArrowOne")
        {
            GameManager.instance.playerArrow = "ArrowTwo";
            LevelManager.instance.LoadNextLevel();
        }
        else if (PlayerController.instance.arrowType == "ArrowTwo")
        {
            GameManager.instance.playerArrow = "ArrowThree";
            LevelManager.instance.LoadNextLevel();
        }
        else
            Debug.Log("Can't upgrade arrows further");
    }

    public void SettingsToggle()
    {
        AudioManager.instance.PlaySound("uiClick");
        settingsMenu.SetActive(!settingsMenu.activeSelf);
        pauseMenu.SetActive(!pauseMenu.activeSelf);
    }

    public void ResumePlay()
    {
        AudioManager.instance.PlaySound("uiClick");
        LevelManager.instance.PauseUnpause();
        if (settingsMenu.activeSelf)
            settingsMenu.SetActive(false);
    }

    public void SetVolume(float volume)
    {
        audioMixer.SetFloat("Volume", volume);

        if (volume < -45f)
            audioMixer.SetFloat("Volume", -80f);
    }

    public void SetBossHealth(float damage)
    {
        damage = Mathf.Clamp(damage, 0f, 1);
        bossHealthBar.localScale = new Vector3(damage, 1, 1);
    }

    public void UpdateScoreText(int score)
    {
        scoreText.text = "Score: " + score.ToString();
    }
}
