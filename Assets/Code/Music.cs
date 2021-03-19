using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Music : MonoBehaviour
{
    public AudioClip levelbg;
    AudioSource audioSrc;

    // Use this for initialization
    void Start()
    {

        audioSrc = GetComponent<AudioSource>();
        switch (GameManager.instance.gameState)
        {
            case GameManager.GameState.MainMenu:
                levelbg = Resources.Load<AudioClip>("BGmusic1");
                break;

            case GameManager.GameState.Level:
                levelbg = Resources.Load<AudioClip>("BGmusic2");
                break;
        }
        audioSrc.loop = true;
        audioSrc.clip = levelbg;
        audioSrc.Play();

    }
}
