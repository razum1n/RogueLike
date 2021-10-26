using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using UnityEngine;

public class Breakables : MonoBehaviour
{
    public GameObject breakEffect;
    public float effectYOffset;
    public bool shouldDropItem;
    public GameObject[] itemsToDrop;
    public float itemDropPercent;
    public Transform effectSpawn;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.tag == "PlayerArrow")
        {
            Destroy(gameObject);
            Instantiate(breakEffect,effectSpawn.position,effectSpawn.rotation);

            if(shouldDropItem)
            {
                float dropChance = Random.Range(0f, 100f);

                if(dropChance < itemDropPercent)
                {
                    int randomItem = Random.Range(0, itemsToDrop.Length);

                    Instantiate(itemsToDrop[randomItem], transform.position, transform.rotation);
                }
            }
        }
    }
}
