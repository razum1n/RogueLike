using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelExit : MonoBehaviour
{
    public string levelToLoad;
    public GameObject openingChest;
    public GameObject closedChest;

    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.tag == "Player")
        {
            if (other.GetComponent<PlayerController>().hasKey)
            {
                closedChest.SetActive(false);
                openingChest.SetActive(true);
                Timer.instance.EndTimer();
                StartCoroutine(LevelManager.instance.LevelEnd());
            }
            else
                UIController.instance.keyInfo.SetActive(true);
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            UIController.instance.keyInfo.SetActive(false);
        }
    }
}
