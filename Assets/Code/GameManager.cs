using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{

    public static GameManager instance;

    public enum GameState {MainMenu, Level};
    public GameState gameState;

    // Start is called before the first frame update
    void Awake()
    {
        if (instance == null)
            instance = this;
        else if (instance != this)
            Destroy(gameObject);
        gameState = GameState.MainMenu;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
