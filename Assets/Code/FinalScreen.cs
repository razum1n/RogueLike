using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;

public class FinalScreen : MonoBehaviour
{

    public float waitForAnyKey = 3f;
    public float finalScoreValue;
    public float scoreMultiplier;
    public TMP_Text finalScore;
    public TMP_Text finalTime;
    public TMP_Text score;
    public TMP_Text multiplier;
    public TMP_Text highScore;
    public TMP_Text rank;

    public GameObject anyKeyText;

    public string mainMenuScreen;

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
        if (PlayerPrefs.GetFloat("HighScore") < finalScoreValue)
        {
            highScore.text = "High Score: " + finalScoreValue.ToString();
            PlayerPrefs.SetFloat("HighScore", finalScoreValue);
        }
        else if (PlayerPrefs.GetFloat("HighScore", 0) > finalScoreValue)
        {
            highScore.text = "High Score: " + PlayerPrefs.GetFloat("HighScore").ToString();
        }

        if(finalScoreValue > 290)
        {
            rank.text = "S";
        }
        else if (finalScoreValue > 250)
        {
            rank.text = "A";
        }
        else if (finalScoreValue > 180)
        {
            rank.text = "B";
        }
        else
        {
            rank.text = "C";
        }
    }
}
