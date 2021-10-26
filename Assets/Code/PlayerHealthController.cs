using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealthController : MonoBehaviour
{
    public static PlayerHealthController instance;

    public int currentHealth;
    public int stageStartHealth;
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
        currentHealth = GameManager.instance.currentHealth;
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
            PlayerController.instance.playerScoreMultiplier = 1;
            UIController.instance.multiplier.value = 0;
            UIController.instance.multiplierText.text = "X" + PlayerController.instance.playerScoreMultiplier.ToString();
            AudioManager.instance.PlaySound("PlayerHit");
            UIController.instance.UpdatePlayerHealth(maxHealth, currentHealth);

            GameManager.instance.SendDataDamage();

            if (currentHealth <= 0)
            {
                StartCoroutine("PlayerDeath");
            }
        }
    }

    public void DashIFrames()
    {
        invCount = 0.5f;
        PlayerController.instance.bodySprite.color = new Color(1f, 1f, 1f, 0.5f);
    }

    public void HealPlayer(int healAmount)
    {
        currentHealth += healAmount;
        UIController.instance.UpdatePlayerHealth(maxHealth, currentHealth);

    }

    private IEnumerator PlayerDeath()
    {
        PlayerController.instance.canMove = false;
        Timer.instance.EndTimer();
        PlayerController.instance.StopPlayer();
        PlayerController.instance.dissolve.isDissolving = true;
        PlayerController.instance.GetComponent<Collider2D>().enabled = false;
        Music.instance.TriggerTransition("FadeOut");
        yield return new WaitForSeconds(2.5f);
        PlayerController.instance.gameObject.SetActive(false);
        UIController.instance.StartFadeToBlack();
        AudioManager.instance.PlaySound("death");
        yield return new WaitForSeconds(2);
        GameManager.instance.SendDataDeath();
        UIController.instance.ActivateDeathScreen();
    }
}
