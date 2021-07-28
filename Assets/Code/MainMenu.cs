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
    public GameObject mainMenu;
    public AudioMixer audioMixer;

    public string levelToLoad;
    public TMP_Dropdown dropdown;
    public List<string> dropdownOptions = new List<string>();

    // Start is called before the first frame update
    void Start()
    {
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
    }

    void Update()
    {

    }

    public void StartGame()
    {
        AudioManager.instance.PlaySound("uiClick");
        GameManager.instance.gameState = GameManager.GameState.Level;
        SceneManager.LoadScene(levelToLoad);
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
}
