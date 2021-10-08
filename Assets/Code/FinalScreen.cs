using System.Collections;
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
    private float scoreMultiplier;
    public TMP_Text finalScore;
    public TMP_Text finalTime;
    public TMP_Text score;
    public TMP_Text multiplier;
    public TMP_Text highScore;
    public TMP_Text rank;

    public GameObject anyKeyText;

    public string mainMenuScreen;

    private void Awake()
    {
        dataManager = Object.FindObjectOfType<DataManager>();
    }

    // Start is called before the first frame update
    void Start()
    {
        Time.timeScale = 1f;
        score.text = "Score: " + GameManager.instance.playerScore.ToString();
        finalTime.text = GameManager.instance.finalTime;
        GetFinalScore();
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

    void GetFinalScore()
    {
        if (GameManager.instance.timerValue <= 100f)
            scoreMultiplier = 1.5f;
        else if (GameManager.instance.timerValue > 100f && GameManager.instance.timerValue <= 130f)
            scoreMultiplier = 1.25f;
        else
            scoreMultiplier = 1f;
        multiplier.text = "Time multiplier: X" + scoreMultiplier.ToString();
        finalScoreValue = GameManager.instance.playerScore * scoreMultiplier;
        finalScore.text = "Final Score: " + finalScoreValue.ToString();
        if (dataManager.Score < finalScoreValue)
        {
            highScore.text = "High Score: " + finalScoreValue.ToString();
            dataManager.Score = finalScoreValue;
            dataManager.Rank = GetRank();
        }
        else if (dataManager.Score > finalScoreValue)
        {
            highScore.text = "High Score: " + dataManager.Score.ToString();
        }
        dataManager.Save();
        rank.text = GetRank();
    }

    private string GetRank()
    {
        if (finalScoreValue > 290)
        {
            return "S";
        }
        else if (finalScoreValue > 250)
        {
            return "A";
        }
        else if (finalScoreValue > 180)
        {
            return "B";
        }
        else
        {
            return "C";
        }
    }
}
