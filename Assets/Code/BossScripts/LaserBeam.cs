﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserBeam : MonoBehaviour
{
    [Header("Laser pieces")]
    public GameObject laserStart;
    public GameObject laserMiddle;
    public GameObject laserEnd;

    public LayerMask ignoreLayer;
    private GameObject start;
    private Collider2D boxCollider;
    private GameObject middle;
    private GameObject end;
    public float timer;
    private bool timerActive = true;

    void Start()
    {
    }
    void Update()
    {
        // Create the laser start from the prefab
        if (start == null)
        {
            start = Instantiate(laserStart) as GameObject;
            start.transform.parent = this.transform;
            start.transform.localPosition = new Vector2(0f,1f);
            start.transform.rotation = this.transform.rotation;
        }

        // Laser middle
        if (middle == null)
        {
            middle = Instantiate(laserMiddle) as GameObject;
            middle.transform.parent = this.transform;
            middle.transform.localPosition = Vector2.zero;
            middle.transform.rotation = this.transform.rotation;
        }

        // Define an "infinite" size, not too big but enough to go off screen
        float maxLaserSize = 20f;
        float currentLaserSize = maxLaserSize;

        // Raycast at the right as our sprite has been design for that
        Vector2 laserDirection = this.transform.up;
        RaycastHit2D hit = Physics2D.Raycast(this.transform.position, laserDirection, maxLaserSize,ignoreLayer);

        if (hit.collider != null)
        {
            // We touched something!

            // -- Get the laser length
            currentLaserSize = Vector2.Distance(hit.point, this.transform.position);

            // -- Create the end sprite
            if (end == null)
            {
                end = Instantiate(laserEnd) as GameObject;
                end.transform.parent = this.transform;
                end.transform.localPosition = Vector2.zero;
                end.transform.rotation = this.transform.rotation;
            }
        }
        else
        {
            // Nothing hit
            // -- No more end
            if (end != null) Destroy(end);
        }

        // Place things
        // -- Gather some data
        float startSpriteHeigth = start.GetComponent<Renderer>().bounds.size.y;
        float endSpriteHeigth = 0f;
        if (end != null) endSpriteHeigth = end.GetComponent<Renderer>().bounds.size.y;

        // -- the middle is after start and, as it has a center pivot, have a size of half the laser (minus start and end)
        middle.transform.localScale = new Vector3(middle.transform.localScale.x,currentLaserSize, middle.transform.localScale.z);
        middle.transform.localPosition = new Vector2(0f,(currentLaserSize / 2f));

        // End?
        if (end != null)
        {
            end.transform.localPosition = new Vector2(0f,currentLaserSize);
        }

        if(timer > 0 && timerActive)
        {
            timer -= Time.deltaTime;
        }
        else if(timerActive)
        {
            ActivateCollider();
            timerActive = false;
        }
    }

    private void ActivateCollider()
    {
        boxCollider = middle.GetComponent<Collider2D>();
        boxCollider.enabled = !boxCollider.enabled;
    }
}
