using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class rotate : MonoBehaviour
{

    public float speed;
    public bool rotationActive = false;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(rotationActive)
            transform.Rotate(0,0, speed * Time.deltaTime, Space.Self);
    }
}
