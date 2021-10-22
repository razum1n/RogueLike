using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Analytics;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{

    public static GameManager instance;

    public enum GameState {MainMenu, Level, Boss};
    public enum Difficulty {Easy, Normal, Hard};
    public GameState gameState;
    public Difficulty difficulty;
    public List<GameObject> stageEnemies = new List<GameObject>();
    public float playerScore = 0;
    public int currentHealth = 5;
    public string playerArrow = "ArrowOne";
    public float playerSpeed = 4f;
    public int playerSpeedLevel = 1;
    public float timerValue = 0f;
    public bool showTimer = true;
    public int playerControlType;

    void Awake()
    {
        if (instance == null)
            instance = this;
        else if (instance != this)
            Destroy(gameObject);
        gameState = GameState.MainMenu;
        DontDestroyOnLoad(this.gameObject);
        playerControlType = 0;
    }

    public void GenerateKey()
    {
        int enemyWithKey = Random.Range(0, stageEnemies.Count);
        stageEnemies[enemyWithKey].GetComponent<EnemyController>().hasKey = true;
    }

    public void ResetGame()
    {
        playerScore = 0;
        currentHealth = 5;
        playerArrow = "ArrowOne";
        playerSpeed = 4f;
        playerSpeedLevel = 1;
        timerValue = 0f;
        stageEnemies.Clear();
        gameState = GameState.MainMenu;
    }

    public void SendData()
    {
        Scene currenScene = SceneManager.GetActiveScene();
        AnalyticsResult result = AnalyticsEvent.Custom("LevelEndStats", new Dictionary<string, object> { 
            { "Level", currenScene.name},
            {"PlayerHealth", PlayerHealthController.instance.currentHealth },
            {"PlayerCurrentTime", timerValue },
            {"PlayerScore", playerScore }
        } );

        Debug.Log("Analytics result: " + result);
    }

    public void Score(float score)
    {
        playerScore += (score * PlayerController.instance.playerScoreMultiplier);
    }
}
