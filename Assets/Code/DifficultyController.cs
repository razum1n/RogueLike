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

    private float healthDropRate;
    public bool enemyDifficultyDecreased;

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
                    if (enemy != null)
                    {
                        EnemyController currentEnemy = enemy.GetComponent<EnemyController>();
                        currentEnemy.fireRate = 2f;
                        currentEnemy.moveSpeed = 2f;
                        currentEnemy.laserRate = 5f;
                        currentEnemy.healthDropChance = healthDropRate;
                    }
                }
                break;
            case EnemyDifficulty.Normal:
                foreach (GameObject enemy in GameManager.instance.stageEnemies)
                {
                    if (enemy != null)
                    {
                        EnemyController currentEnemy = enemy.GetComponent<EnemyController>();
                        currentEnemy.fireRate = 1f;
                        currentEnemy.moveSpeed = 3f;
                        currentEnemy.laserRate = 4f;
                        currentEnemy.healthDropChance = healthDropRate;
                    }
                }
                break;
            case EnemyDifficulty.Hard:
                foreach (GameObject enemy in GameManager.instance.stageEnemies)
                {
                    if(enemy != null)
                    {
                        EnemyController currentEnemy = enemy.GetComponent<EnemyController>();
                        currentEnemy.fireRate = 0.5f;
                        currentEnemy.moveSpeed = 4f;
                        currentEnemy.laserRate = 2f;
                        currentEnemy.laserOnTime = 0.5f;
                    }
        }
                break;
        }
    }

    public void CheckEnemyDifficulty()
    {
        switch(selectedDifficulty)
        {
            case SelectedDifficulty.Easy:
                if(PlayerHealthController.instance.currentHealth <= 2)
                {
                    healthDropRate = 25f;
                    SetEnemyDifficulty();
                }
                else
                {
                    healthDropRate = 0f;
                    SetEnemyDifficulty();
                }
                break;
            case SelectedDifficulty.Normal:
                if((PlayerHealthController.instance.stageStartHealth - PlayerHealthController.instance.currentHealth) >= 3)
                {
                    DecreaseEnemyDifficulty();
                    healthDropRate = 21f;
                    SetEnemyDifficulty();
                }
                else if (PlayerHealthController.instance.currentHealth < 2)
                {
                    healthDropRate = 21f;
                    SetEnemyDifficulty();
                }
                else
                {
                    healthDropRate = 0f;
                    SetEnemyDifficulty();
                }
                break;
            case SelectedDifficulty.Hard:
                if ((PlayerHealthController.instance.stageStartHealth - PlayerHealthController.instance.currentHealth) >= 3 && !enemyDifficultyDecreased)
                {
                    DecreaseEnemyDifficulty();
                    enemyDifficultyDecreased = true;
                    SetEnemyDifficulty();
                }
                else
                {
                    SetEnemyDifficulty();
                }
                break;
        }
    }

    public void SetRoomDifficulty()
    {
        switch(selectedDifficulty)
        {
            case SelectedDifficulty.Easy:
                if(SceneManager.GetActiveScene().buildIndex == 2 && PlayerHealthController.instance.currentHealth >= 3)
                {
                    IncreaseRoomDifficulty();
                    if(PlayerHealthController.instance.currentHealth >= 4)
                    {
                        IncreaseEnemyDifficulty();
                    }
                }
                break;
            case SelectedDifficulty.Normal:
                if (SceneManager.GetActiveScene().buildIndex == 1 )
                {
                    IncreaseRoomDifficulty();
                }
                else if(SceneManager.GetActiveScene().buildIndex == 2)
                {
                    if(PlayerHealthController.instance.currentHealth >= 3)
                        IncreaseRoomDifficulty();

                    if (PlayerHealthController.instance.currentHealth >= 4)
                        IncreaseEnemyDifficulty();
                    else if(PlayerHealthController.instance.currentHealth == 1)
                        DecreaseEnemyDifficulty();
                }
                break;
            case SelectedDifficulty.Hard:
                if (SceneManager.GetActiveScene().buildIndex == 1)
                {
                    IncreaseRoomDifficulty();
                }
                else if(SceneManager.GetActiveScene().buildIndex == 2 && PlayerHealthController.instance.currentHealth < 2 && !enemyDifficultyDecreased)
                {
                    DecreaseEnemyDifficulty();
                    enemyDifficultyDecreased = true;
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

    public void IncreaseEnemyDifficulty()
    {
        if(enemyDifficulty == EnemyDifficulty.Easy)
        {
            enemyDifficulty = EnemyDifficulty.Normal;
        }
        else if(enemyDifficulty == EnemyDifficulty.Normal)
        {
            enemyDifficulty = EnemyDifficulty.Hard;
        }
    }

    public void DecreaseEnemyDifficulty()
    {
        if(enemyDifficulty == EnemyDifficulty.Normal)
        {
            enemyDifficulty = EnemyDifficulty.Easy;
        }
        else if(enemyDifficulty == EnemyDifficulty.Hard)
        {
            enemyDifficulty = EnemyDifficulty.Normal;
        }
    }
}
