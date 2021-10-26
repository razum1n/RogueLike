using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Room : MonoBehaviour
{
    public bool closeWhenEntered;
    public bool onlyVerticalEntry;
    public bool onlyHorizontalEntry;
    public bool noEnemies = false;

    [HideInInspector]
    public bool roomActive;

    public GameObject roomHider;
    public RoomCenter center;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.tag == "Player")
        {
            CameraController.instance.ChangeTarget(transform);
            roomActive = true;
            roomHider.SetActive(false);
            DifficultyController.instance.CheckEnemyDifficulty();

            if (noEnemies != true)
            {
                PlayerController.instance.currentRoomID = center.roomID;
                for (int i = 0; i < center.enemies.Count; i++)
                {
                    if (center.enemies[i] != null)
                    {
                        center.enemies[i].GetComponent<EnemyController>().enemyActive = true;
                    }
                }
                for (int i = 0; i < center.spikes.Count; i++)
                {
                    if (center.spikes[i] != null)
                    {
                        center.spikes[i].GetComponent<Spike>().startingAnim = true;
                    }
                }
            }

        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            roomActive = false;
            if (noEnemies != true)
            {
                for (int i = 0; i < center.enemies.Count; i++)
                {
                    if (center.enemies[i] != null)
                    {
                        center.enemies[i].GetComponent<EnemyController>().enemyActive = false;
                    }
                }
            }

        }
    }
}
