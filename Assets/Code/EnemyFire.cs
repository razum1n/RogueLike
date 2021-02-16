using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyFire : MonoBehaviour
{

    GameObject fireBall;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SpawnFireBall()
    {
        GameObject fireBall = Pool.instance.Get("EnemyFire");
        if (fireBall != null)
        {
            fireBall.transform.position = transform.position;
            fireBall.transform.rotation = transform.rotation;
            fireBall.SetActive(true);
            fireBall.GetComponent<EnemyAttack>().GetDirection();
        }
        this.gameObject.SetActive(false);
    }
}
