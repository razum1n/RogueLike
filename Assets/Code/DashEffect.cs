using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class DashEffect : MonoBehaviour
{
    public float effectDelay;
    private float effectDelaySeconds;
    public GameObject trailEffect;

    // Start is called before the first frame update
    void Start()
    {
        effectDelaySeconds = effectDelay;
    }

    // Update is called once per frame
    void Update()
    {
        if(PlayerController.instance.isDashing)
        {
            if (effectDelaySeconds > 0)
            {
                effectDelaySeconds -= Time.deltaTime;
            }
            else
            {
                GameObject currentEffect = Instantiate(trailEffect, PlayerController.instance.trailPosition.position, PlayerController.instance.trailPosition.rotation);
                currentEffect.transform.localScale = PlayerController.instance.trailPosition.localScale;
                effectDelaySeconds = effectDelay;
            }
        }
    }
}
