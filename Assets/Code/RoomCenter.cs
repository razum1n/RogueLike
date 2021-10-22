using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomCenter : MonoBehaviour
{
    public Room room;
    public int roomID;
    public bool noEnemies;
    public enum RoomType { Basic, Horizontal, Vertical};
    public RoomType roomType;

    public List<GameObject> enemies = new List<GameObject>();
    public List<GameObject> hazards = new List<GameObject>();

    void Start()
    {
        room.center = this.GetComponent<RoomCenter>();
    }
}
