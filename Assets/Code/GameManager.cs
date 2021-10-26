using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Analytics;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{

    public static GameManager instance;

    public enum GameState {MainMenu, Level, Boss};
    public GameState gameState;
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
        DifficultyController.instance.enemyDifficultyDecreased = false;
        DifficultyController.instance.enemyDifficulty = DifficultyController.EnemyDifficulty.Easy;
        DifficultyController.instance.roomDifficulty = DifficultyController.RoomDifficulty.Easy;
        DifficultyController.instance.selectedDifficulty = DifficultyController.SelectedDifficulty.Easy;
    }

    public void SendDataDamage()
    {
        
        AnalyticsResult result = AnalyticsEvent.Custom("PlayerDamage", new Dictionary<string, object> { 
            { "CurrentRoom", PlayerController.instance.currentRoomID}
        } );

    }

    public void SendDataDeath()
    {
        Scene currentScene = SceneManager.GetActiveScene();
        AnalyticsResult result = AnalyticsEvent.Custom("PlayerDeath" + DifficultyController.instance.selectedDifficulty.ToString(), new Dictionary<string, object> {
            { "Level", currentScene.name}
        });
    }

    public void SendDataLevelComplete()
    {
        Scene currentScene = SceneManager.GetActiveScene();
        AnalyticsResult result = AnalyticsEvent.Custom("LevelComplete: " + DifficultyController.instance.selectedDifficulty.ToString(), new Dictionary<string, object> {
            { "Level", currentScene.name}
        });

    }

    public void SendDataGameComplete()
    {
        AnalyticsResult result = AnalyticsEvent.Custom("GameComplete", new Dictionary<string, object> {
            { "Difficulty", DifficultyController.instance.selectedDifficulty.ToString()},
            {"Score", playerScore },
            {"Time", timerValue },
            {"Health", currentHealth }
        });
    }

    public void Score(float score)
    {
        playerScore += (score * PlayerController.instance.playerScoreMultiplier);
    }
}
