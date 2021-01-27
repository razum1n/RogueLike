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

    void Awake()
    {
        instance = this;
    }
    // Start is called before the first frame update
    void Start()
    {
        currentHealth = maxHealth;

        UIController.instance.UpdatePlayerHealth(maxHealth, currentHealth);
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
            PlayerController.instance.anim.SetTrigger("isHit");
            UIController.instance.UpdatePlayerHealth(maxHealth, currentHealth);

            if (currentHealth <= 0)
            {
                PlayerController.instance.gameObject.SetActive(false);
                UIController.instance.ActivateDeathScreen();
            }
        }
    }

    public void HealPlayer(int healAmount)
    {
        currentHealth += healAmount;
        UIController.instance.UpdatePlayerHealth(maxHealth, currentHealth);
        if (currentHealth > maxHealth)
        {
            currentHealth = maxHealth;
        }
        
    }
}
