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
                for (int i = 0; i < center.hazards.Count; i++)
                {
                    if (center.hazards[i] != null)
                    {
                        center.hazards[i].GetComponent<Hazard>().activeHazard = true;
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
                for (int i = 0; i < center.hazards.Count; i++)
                {
                    if (center.hazards[i] != null)
                    {
                        center.hazards[i].GetComponent<Hazard>().activeHazard = false;
                    }
                }
            }

        }
    }
}
