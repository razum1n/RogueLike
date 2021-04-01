using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossController : MonoBehaviour
{

    public GameObject laserBeam;

    public int laserNumber;
    private GameObject currentLaser;
    // Start is called before the first frame update
    void Start()
    {
        LaserAttack();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void LaserAttack()
    {
        for(int i=0;i<laserNumber;i++)
        {
            currentLaser = Instantiate(laserBeam, transform.position, transform.rotation * Quaternion.Euler(0f, 0f, 90f * i));
            currentLaser.GetComponent<rotate>().rotationActive = true;
        }
    }
}
