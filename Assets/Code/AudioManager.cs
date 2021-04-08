using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{

    public static AudioManager instance;
    private float currentVolume;
    public AudioClip uiSound, arrowSound, ogreSound, arrowMiss, fireBall, keyPickUp, playerHit;

    AudioSource audioSource;


    void Awake()
    {
        if (instance == null)
            instance = this;
        else if (instance != this)
            Destroy(gameObject);
        DontDestroyOnLoad(this.gameObject);
    }

    // Use this for initialization
    void Start()
    {

        uiSound = Resources.Load<AudioClip>("uiSound");
        arrowSound = Resources.Load<AudioClip>("arrowSound");
        ogreSound = Resources.Load<AudioClip>("ogreSound");
        playerHit = Resources.Load<AudioClip>("ogreSound");
        arrowMiss = Resources.Load<AudioClip>("arrowMiss");
        fireBall = Resources.Load<AudioClip>("fireball");
        keyPickUp = Resources.Load<AudioClip>("keyPickUp");

        audioSource = GetComponent<AudioSource>();
    }

    public void PlaySound(string clip)
    {
        currentVolume = audioSource.volume;
        audioSource.pitch = 1f;
        switch (clip)
        {
            case "uiClick":
                
                audioSource.PlayOneShot(uiSound);
                break;
            case "arrow":
                audioSource.volume /= 2;
                audioSource.PlayOneShot(arrowSound);
                break;
            case "ogreHit":
                audioSource.PlayOneShot(ogreSound);
                break;
            case "PlayerHit":
                audioSource.pitch = 1.5f;
                audioSource.PlayOneShot(playerHit);
                break;
            case "arrowMiss":
                audioSource.PlayOneShot(arrowMiss);
                break;
            case "fireball":
                audioSource.PlayOneShot(fireBall);
                break;
            case "key":
                audioSource.PlayOneShot(keyPickUp);
                break;

        }
        audioSource.volume = currentVolume;
    }
}
