using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Music : MonoBehaviour
{
    public static Music instance;
    AudioSource audioSrc;
    public AudioClip bgMusic1,bgMusic2,bgMusic3;
    private Animator anim;


    void Awake()
    {
        if (instance == null)
            instance = this;
        else if (instance != this)
            Destroy(gameObject);
        DontDestroyOnLoad(this.gameObject);
        audioSrc = GetComponent<AudioSource>();
        anim = GetComponent<Animator>();
        bgMusic1 = Resources.Load<AudioClip>("BGmusic1");
        bgMusic2 = Resources.Load<AudioClip>("BGmusic2");
        bgMusic3 = Resources.Load<AudioClip>("BGmusic3");
    }
    // Use this for initialization
    void Start()
    {
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

    public void TriggerTransition(string transition)
    {
        switch(transition)
        {
            case "FadeIn":
                anim.SetTrigger("fadeIn");
                break;
            case "FadeOut":
                anim.SetTrigger("fadeOut");
                break;
            case "LowerVolume":
                anim.SetTrigger("lowVolume");
                break;
            case "DefaultVolume":
                anim.SetTrigger("defVolume");
                break;
        }
    }
}
