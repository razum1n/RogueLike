using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyFire : MonoBehaviour
{

    GameObject fireBall;

    public void SpawnFireBall()
    {
        GameObject fireBall = Pool.instance.Get("EnemyFire");
        if (fireBall != null)
        {
            fireBall.transform.position = transform.position;
            fireBall.transform.rotation = transform.rotation;
            fireBall.SetActive(true);
            fireBall.GetComponent<EnemyAttack>().GetDirection();
            AudioManager.instance.PlaySound("fireball");
        }
        this.gameObject.SetActive(false);
    }
}
