using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;
using UnityEngine.Audio;

public class MainMenu : MonoBehaviour
{

    public GameObject optionsMenu;
    public GameObject controlsMenu;
    public GameObject creditsMenu;
    public GameObject highScoreMenu;
    public GameObject mainMenu;
    public AudioMixer audioMixer;
    private bool startingGame;

    public string levelToLoad;
    public TMP_Dropdown dropdown;
    public TMP_Text scoreText;
    public TMP_Text rankText;
    public List<string> dropdownOptions = new List<string>();

    // Start is called before the first frame update
    void Start()
    {
        startingGame = false;
        dropdown.options.Clear();
        dropdownOptions.Add("M. and Keyboard");
        dropdownOptions.Add("Controller");

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

    public void StartGame()
    {
        if(startingGame == false)
        {
            AudioManager.instance.PlaySound("uiClick");
            StartCoroutine("StartGameLoading"); 
        }

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

        highScore = PlayerPrefs.GetFloat("HighScore");
        int score = Mathf.RoundToInt(highScore);
        scoreText.text = score.ToString();

        if (highScore > 290)
        {
            rankText.text = "S";
        }
        else if (highScore > 250)
        {
            rankText.text = "A";
        }
        else if (highScore > 180)
        {
            rankText.text = "B";
        }
        else
        {
            rankText.text = "C";
        }
    }
}
