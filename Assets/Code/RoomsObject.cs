using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Rooms", menuName = "Custom Asset", order = 0)]
public class RoomsObject : ScriptableObject
{
    public RoomCenter[] easyRooms;
    public RoomCenter[] normalRooms;
    public RoomCenter[] hardRooms;
}
