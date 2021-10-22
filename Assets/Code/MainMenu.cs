using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;
using UnityEngine.Audio;

public class MainMenu : MonoBehaviour
{
    private DataManager dataManager;

    private TimeSpan fastestTime;

    public GameObject optionsMenu;
    public GameObject controlsMenu;
    public GameObject creditsMenu;
    public GameObject highScoreMenu;
    public GameObject mainMenu;
    public GameObject difficultyMenu;

    public AudioMixer audioMixer;

    private bool startingGame;

    public string levelToLoad;

    public TMP_Dropdown dropdown;

    public TMP_Text scoreText;
    public TMP_Text bestTimeText;

    public List<string> dropdownOptions = new List<string>();


    private void Awake()
    {
        dataManager = FindObjectOfType<DataManager>();
        Debug.Log(Application.persistentDataPath);
    }

    // Start is called before the first frame update
    void Start()
    {
        startingGame = false;
        dropdown.options.Clear();
        dropdownOptions.Add("M. and Keyboard");
        dropdownOptions.Add("Controller");
        Debug.Log(dataManager);

        foreach (var option in dropdownOptions)
        {
            dropdown.options.Add(new TMP_Dropdown.OptionData() { text = option });
        }
        dropdown.onValueChanged.AddListener(delegate { DropdownItemSelected(dropdown); });
        GameManager.instance.ResetGame();
        Music.instance.ChangeTrack(1);
        Music.instance.TriggerTransition("FadeIn");
        GetHighScore();
    }

    public void StartGameEasy()
    {
        if(startingGame == false)
        {
            AudioManager.instance.PlaySound("uiClick");
            GameManager.instance.difficulty = GameManager.Difficulty.Easy;
            StartCoroutine("StartGameLoading"); 
        }

    }

    public void StartGameNormal()
    {
        if (startingGame == false)
        {
            AudioManager.instance.PlaySound("uiClick");
            GameManager.instance.difficulty = GameManager.Difficulty.Normal;
            StartCoroutine("StartGameLoading");
        }

    }

    public void StartGameHard()
    {
        if (startingGame == false)
        {
            AudioManager.instance.PlaySound("uiClick");
            GameManager.instance.difficulty = GameManager.Difficulty.Hard;
            StartCoroutine("StartGameLoading");
        }

    }

    public void DifficultyMenuToggle()
    {
        difficultyMenu.SetActive(!difficultyMenu.activeSelf);
        mainMenu.SetActive(!mainMenu.activeSelf);
    }


    public void ExitGame()
    {
        AudioManager.instance.PlaySound("uiClick");
        Application.Quit();
    }

    public void OptionsToggle()
    {
        optionsMenu.SetActive(!optionsMenu.activeSelf);
        mainMenu.SetActive(!mainMenu.activeSelf);
    }

    public void ControlsToggle()
    {
        controlsMenu.SetActive(!controlsMenu.activeSelf);
        mainMenu.SetActive(!mainMenu.activeSelf);
    }

    public void CreditsToggle()
    {
        creditsMenu.SetActive(!creditsMenu.activeSelf);
        mainMenu.SetActive(!mainMenu.activeSelf);
    }

    public void HighScoreToggle()
    {
        mainMenu.SetActive(!mainMenu.activeSelf);
        highScoreMenu.SetActive(!highScoreMenu.activeSelf);
    }

    public void DeleteSaveFile()
    {
        dataManager.Delete();
    }

    public void DropdownItemSelected(TMP_Dropdown dropdown)
    {
        GameManager.instance.playerControlType = dropdown.value;
    }

    public void SetVolume(float volume)
    {
        audioMixer.SetFloat("Volume", volume);

        if (volume < -45f)
            audioMixer.SetFloat("Volume", -80f);
    }

    public void ShowTimer()
    {
        GameManager.instance.showTimer = !GameManager.instance.showTimer;
    }

    private IEnumerator StartGameLoading()
    {
        Music.instance.TriggerTransition("FadeOut");
        yield return new WaitForSeconds(1);
        GameManager.instance.gameState = GameManager.GameState.Level;
        SceneManager.LoadScene(levelToLoad);
    }

    private void GetHighScore()
    {
        float highScore;

        if(dataManager != null)
        {
            dataManager.Load();
            highScore = dataManager.Score;
            fastestTime = TimeSpan.FromSeconds(dataManager.BestTime);
            string bestTimeStr = fastestTime.ToString("mm':'ss'.'ff");
            int score = Mathf.RoundToInt(highScore);
            scoreText.text = score.ToString();
            bestTimeText.text = bestTimeStr;
        }
    }
}
