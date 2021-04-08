using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{

    public static GameManager instance;

    public enum GameState {MainMenu, Level, Boss};
    public GameState gameState;
    public List<GameObject> stageEnemies = new List<GameObject>();
    public int playerScore = 0;
    public int currentHealth = 5;
    public string playerArrow = "ArrowOne";
    public float playerSpeed = 4f;
    public float timerValue = 0f;
    public string finalTime;
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
        timerValue = 0f;
        stageEnemies.Clear();
        gameState = GameState.MainMenu;
    }
}
