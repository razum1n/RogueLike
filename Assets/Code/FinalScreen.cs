using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;

public class FinalScreen : MonoBehaviour
{
    private DataManager dataManager;

    [SerializeField]
    private float waitForAnyKey = 3f;

    private float finalScoreValue;
    private TimeSpan timePlayed;
    public TMP_Text finalTime;
    public TMP_Text score;
    public TMP_Text highScore;
    public TMP_Text bestTimeTxt;

    public GameObject anyKeyText;

    public string mainMenuScreen;

    private void Awake()
    {
        dataManager = FindObjectOfType<DataManager>();
    }

    // Start is called before the first frame update
    void Start()
    {
        Time.timeScale = 1f;
        Debug.Log(ConvertTimeToString(GameManager.instance.timerValue));
        score.text = GameManager.instance.playerScore.ToString();
        finalTime.text = ConvertTimeToString(GameManager.instance.timerValue);
        Debug.Log(GameManager.instance.timerValue);
        CheckSaveFile();
    }

    // Update is called once per frame
    void Update()
    {
        if(waitForAnyKey > 0)
        {
            waitForAnyKey -= Time.deltaTime;
            if(waitForAnyKey <= 0)
            {
                anyKeyText.SetActive(true);
            }
        }
        else
        {
            if(Input.anyKeyDown)
            {
                SceneManager.LoadScene(mainMenuScreen);
            }
        }
    }

    void CheckSaveFile()
    {
        if (dataManager.Score < GameManager.instance.playerScore)
        {
            highScore.text = GameManager.instance.playerScore.ToString();
            dataManager.Score = GameManager.instance.playerScore;
        }
        else if (dataManager.Score > GameManager.instance.playerScore)
        {
            highScore.text = dataManager.Score.ToString();
        }
        if(dataManager.BestTime > GameManager.instance.timerValue)
        {
            dataManager.BestTime = GameManager.instance.timerValue;
            bestTimeTxt.text = ConvertTimeToString(GameManager.instance.timerValue);
        }
        else if (dataManager.BestTime < GameManager.instance.timerValue)
        {
            bestTimeTxt.text = ConvertTimeToString(dataManager.BestTime);
        }

        dataManager.Save();
    }

    public string ConvertTimeToString(float timerValue)
    {
        timePlayed = TimeSpan.FromSeconds(timerValue);
        string timePlayedStr = timePlayed.ToString("mm':'ss'.'ff");
        return timePlayedStr;
    }
}
