using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{

    public static AudioManager instance;
    private float currentVolume;
    public AudioClip uiSound, arrowSound, ogreSound, arrowMiss, fireBall;

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
        arrowMiss = Resources.Load<AudioClip>("arrowMiss");
        fireBall = Resources.Load<AudioClip>("fireball");

        audioSource = GetComponent<AudioSource>();
    }

    public void PlaySound(string clip)
    {
        currentVolume = audioSource.volume;
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
            case "arrowMiss":
                audioSource.PlayOneShot(arrowMiss);
                break;
            case "fireball":
                audioSource.PlayOneShot(fireBall);
                break;

        }
        audioSource.volume = currentVolume;
    }
}
