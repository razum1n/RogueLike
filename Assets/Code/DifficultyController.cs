using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DifficultyController : MonoBehaviour
{
    public static DifficultyController instance;

    public enum RoomDifficulty { Easy, Normal, Hard};
    public RoomDifficulty roomDifficulty;

    public enum EnemyDifficulty { Easy, Normal, Hard};
    public EnemyDifficulty enemyDifficulty;

    public enum SelectedDifficulty { Easy, Normal, Hard};
    public SelectedDifficulty selectedDifficulty;

    private void Awake()
    {
        if (instance == null)
            instance = this;
        else if (instance != this)
            Destroy(gameObject);
        DontDestroyOnLoad(this.gameObject);
    }

    public void SetEnemyDifficulty()
    {
        switch(enemyDifficulty)
        {
            case EnemyDifficulty.Easy:
                foreach(GameObject enemy in GameManager.instance.stageEnemies)
                {
                    EnemyController currentEnemy = enemy.GetComponent<EnemyController>();
                    currentEnemy.fireRate = 2f;
                    currentEnemy.moveSpeed = 2f;
                    currentEnemy.laserRate = 5f;
                    currentEnemy.health -= 50;
                }
                break;
            case EnemyDifficulty.Hard:
                foreach (GameObject enemy in GameManager.instance.stageEnemies)
                {
                    EnemyController currentEnemy = enemy.GetComponent<EnemyController>();
                    currentEnemy.fireRate = 0.5f;
                    currentEnemy.moveSpeed = 4f;
                    currentEnemy.laserRate = 2f;
                    currentEnemy.laserOnTime = 0.5f;
                    currentEnemy.health += 50;
                }
                break;
        }
    }

    public void SetRoomDifficulty()
    {
        switch(selectedDifficulty)
        {
            case SelectedDifficulty.Easy:
                if(SceneManager.GetActiveScene().buildIndex == 2)
                {
                    IncreaseRoomDifficulty();
                }
                break;
            case SelectedDifficulty.Normal:
                if (SceneManager.GetActiveScene().buildIndex == 1 )
                {
                    IncreaseRoomDifficulty();
                }
                else if(SceneManager.GetActiveScene().buildIndex == 2)
                {
                    IncreaseRoomDifficulty();
                }
                break;
            case SelectedDifficulty.Hard:
                if (SceneManager.GetActiveScene().buildIndex == 1)
                {
                    IncreaseRoomDifficulty();
                }
                break;
        }
    }

    public void IncreaseRoomDifficulty()
    {
        if(roomDifficulty == RoomDifficulty.Easy)
        {
            roomDifficulty = RoomDifficulty.Normal;
        }
        else if (roomDifficulty == RoomDifficulty.Normal)
        {
            roomDifficulty = RoomDifficulty.Hard;
        }
    }

    public void DecreaseRoomDifficulty()
    {
        if(roomDifficulty == RoomDifficulty.Normal)
        {
            roomDifficulty = RoomDifficulty.Easy;
        }
        else if(roomDifficulty == RoomDifficulty.Hard)
        {
            roomDifficulty = RoomDifficulty.Normal;
        }
    }
}
