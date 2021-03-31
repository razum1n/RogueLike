using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{

    public static GameManager instance;

    public enum GameState {MainMenu, Level};
    public GameState gameState;
    public List<GameObject> stageEnemies = new List<GameObject>();
    public int playerScore, currentHealth;
    public string playerArrow;
    public float playerSpeed;
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

    // Update is called once per frame
    void Update()
    {
        
    }

    public void GenerateKey()
    {
        int enemyWithKey = Random.Range(0, stageEnemies.Count);
        stageEnemies[enemyWithKey].GetComponent<EnemyController>().hasKey = true;
    }
}
