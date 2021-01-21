using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealthController : MonoBehaviour
{
    public static PlayerHealthController instance;

    public int currentHealth;
    public int maxHealth;
    public float invincibility = 1f;

    private float invCount;

    public GameObject[] uiHealth;

    public GameObject[] uiDamage;

    public GameObject deathScreen;

    void Awake()
    {
        instance = this;
    }
    // Start is called before the first frame update
    void Start()
    {
        currentHealth = maxHealth;

        for(int i=0;i<maxHealth;i++)
        {
            uiHealth[i].SetActive(true);
            uiDamage[i].SetActive(false);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(invCount > 0)
        {
            
            invCount -= Time.deltaTime;

            if(invCount <= 0)
                PlayerController.instance.bodySprite.color = new Color(1f, 1f, 1f, 1f);
        }
    }

    public void DamagePlayer()
    {
        if(invCount <= 0 && PlayerController.instance.isDashing == false)
        {
            currentHealth--;
            invCount = invincibility;

            PlayerController.instance.bodySprite.color = new Color(1f, 1f, 1f, 0.5f);

            int damageNumber = 0;
            PlayerController.instance.anim.SetTrigger("isHit");
            for (int i = 0; i < maxHealth; i++)
            {
                if (i >= currentHealth)
                {
                    uiHealth[i].SetActive(false);
                    uiDamage[damageNumber].SetActive(true);
                    damageNumber++;
                }
            }

            if (currentHealth <= 0)
            {
                PlayerController.instance.gameObject.SetActive(false);
                deathScreen.SetActive(true);
            }
        }


    }
}
