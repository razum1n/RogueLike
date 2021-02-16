using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Room : MonoBehaviour
{
    public bool closeWhenEntered;
    public bool onlyVerticalEntry;
    public bool onlyHorizontalEntry;

    public GameObject[] doors;

    [HideInInspector]
    public bool roomActive;

    public GameObject roomHider;

    public void OpenDoors()
    {
        foreach (GameObject door in doors)
        {
            door.SetActive(false);

            closeWhenEntered = false;
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.tag == "Player")
        {
            CameraController.instance.ChangeTarget(transform);
            roomActive = true;
            roomHider.SetActive(false);
            if(closeWhenEntered)
            {
                foreach(GameObject door in doors)
                {
                    door.SetActive(true);
                }
            }
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if(other.tag == "Player")
        {
            roomActive = false;
        }
    }
}
