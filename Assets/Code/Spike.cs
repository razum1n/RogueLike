using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spike : MonoBehaviour
{
    public Animator anim;
    public float waitTime;
    private float defaultWaitTime;
    public bool startingAnim;

    void Start()
    {
        anim = GetComponent<Animator>();
        defaultWaitTime = waitTime;
    }

    void Update()
    {

        if (waitTime <= 0 && startingAnim)
        {
            anim.SetTrigger("trigger");
            waitTime = defaultWaitTime;
            startingAnim = false;
        }

        if(startingAnim)
        {
            waitTime -= Time.deltaTime;
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        anim.SetTrigger("trigger");
        if (other.tag == "Player")
            PlayerHealthController.instance.DamagePlayer();
    }
}
