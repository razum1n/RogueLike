using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class FinalScreen : MonoBehaviour
{

    public float waitForAnyKey = 3f;
    public TMP_Text finalScore;
    public TMP_Text finalTime;

    public GameObject anyKeyText;

    public string mainMenuScreen;

    // Start is called before the first frame update
    void Start()
    {
        Time.timeScale = 1f;
        finalScore.text = "Final Score: " + GameManager.instance.playerScore.ToString();
        finalTime.text = GameManager.instance.finalTime;
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
}
