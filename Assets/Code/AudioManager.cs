using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{

    public static AudioManager instance;
    public AudioClip fireSound;

    AudioSource audioSource;


    void Awake()
    {
        if (instance == null)
            instance = this;
        else if (instance != this)
            Destroy(gameObject);
    }

    // Use this for initialization
    void Start()
    {

        fireSound = Resources.Load<AudioClip>("fireSound");

        audioSource = GetComponent<AudioSource>();
    }

    public void PlaySound(string clip)
    {
        switch (clip)
        {
            case "fire":
                audioSource.PlayOneShot(fireSound);
                break;
        }
    }
}
