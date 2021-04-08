using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Music : MonoBehaviour
{
    public static Music instance;
    AudioSource audioSrc;
    public AudioClip bgMusic1,bgMusic2,bgMusic3;


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
        audioSrc = GetComponent<AudioSource>();
        bgMusic1 = Resources.Load<AudioClip>("BGmusic1");
        bgMusic2 = Resources.Load<AudioClip>("BGmusic2");
        bgMusic3 = Resources.Load<AudioClip>("BGmusic3");
        audioSrc.loop = true;
        audioSrc.Play();
    }

    public void ChangeTrack(int track)
    {
        switch(track)
        {
            case 1:
                audioSrc.clip = bgMusic1;
                audioSrc.Play();
                break;
            case 2:
                audioSrc.clip = bgMusic2;
                audioSrc.Play();
                break;
            case 3:
                audioSrc.clip = bgMusic3;
                audioSrc.Play();
                break;
        }
    }
}
