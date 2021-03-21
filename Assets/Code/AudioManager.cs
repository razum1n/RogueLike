using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{

    public static AudioManager instance;
    public AudioClip uiSound, arrowSound, ogreSound;

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

        audioSource = GetComponent<AudioSource>();
    }

    public void PlaySound(string clip)
    {
        switch (clip)
        {
            case "uiClick":
                audioSource.PlayOneShot(uiSound);
                break;
            case "arrow":
                audioSource.PlayOneShot(arrowSound);
                break;
            case "ogreHit":
                audioSource.PlayOneShot(ogreSound);
                break;

        }
    }
}
