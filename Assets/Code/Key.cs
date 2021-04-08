using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Key : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.tag == "Player")
        {
            PlayerController.instance.hasKey = true;
            UIController.instance.keyImg.SetActive(true);
            AudioManager.instance.PlaySound("key");
            Destroy(gameObject);
        }
    }
}
