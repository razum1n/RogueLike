using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Timer : MonoBehaviour
{

    public static Timer instance;

    public TMP_Text timerText;

    private TimeSpan timePlayed;
    private bool timerOn;

    private float elapsedTime;

    void Awake()
    {
        instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        elapsedTime = GameManager.instance.timerValue;
        timePlayed = TimeSpan.FromSeconds(elapsedTime);
        string timePlayedStr = "Time: " + timePlayed.ToString("mm':'ss'.'ff");
        timerOn = false;
        BeginTimer();
    }

    public void BeginTimer()
    {
        timerOn = true;

        StartCoroutine(UpdateTimer());
    }

    public void EndTimer()
    {
        timerOn = false;
        GameManager.instance.timerValue = elapsedTime;
    }

    private IEnumerator UpdateTimer()
    {
        while(timerOn)
        {
            elapsedTime += Time.deltaTime;
            timePlayed = TimeSpan.FromSeconds(elapsedTime);
            string timePlayedStr = "Time: " + timePlayed.ToString("mm':'ss'.'ff");
            timerText.text = timePlayedStr;

            yield return null;
        }
    }

    public void FinalTime()
    {
        timePlayed = TimeSpan.FromSeconds(elapsedTime);
        string timePlayedStr = "Final Time: " + timePlayed.ToString("mm':'ss'.'ff");
        GameManager.instance.finalTime = timePlayedStr;
        timerOn = false;
    }
}
