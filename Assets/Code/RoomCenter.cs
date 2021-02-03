using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomCenter : MonoBehaviour
{
    public bool openWhenEnemiesCleared;
    public Room room;

    public List<GameObject> enemies = new List<GameObject>();

    // Start is called before the first frame update
    void Start()
    {
        if(openWhenEnemiesCleared)
        {
            room.closeWhenEntered = true;
        }

    }

    // Update is called once per frame
    void Update()
    {
        if (enemies.Count > 0 && room.roomActive && openWhenEnemiesCleared)
        {
            for (int i = 0; i < enemies.Count; i++)
            {
                if (enemies[i] == null)
                {
                    enemies.RemoveAt(i);
                    i--;
                }
            }

            if (enemies.Count == 0)
            {
                room.OpenDoors();
            }
        }
    }
}
