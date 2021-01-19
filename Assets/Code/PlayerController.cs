using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float moveSpeed;
    public float fireRate;
    private float fireCount;

    private Vector2 moveInput;

    public Rigidbody2D rb;

    public Transform bowArm;
    public Transform arrowSpawnPoint;

    private Camera mainCamera;
    public Animator anim;

    public GameObject arrow;
    
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        mainCamera = Camera.main;
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        moveInput.x = Input.GetAxisRaw("Horizontal");
        moveInput.y = Input.GetAxisRaw("Vertical");

        moveInput.Normalize();

        rb.velocity = moveInput * moveSpeed;


        Vector3 mousePos = Input.mousePosition;
        Vector3 screenPoint = mainCamera.WorldToScreenPoint(transform.localPosition);

        if (mousePos.x < screenPoint.x)
        {
            transform.localScale = new Vector3(-1f, 1f, 1f);
            bowArm.localScale = new Vector3(-1f, -1f, 1f);
        }
        else
        {
            transform.localScale = Vector3.one;
            bowArm.localScale = Vector3.one;
        }
            

        RotateBow(mousePos, screenPoint);

        if(Input.GetMouseButtonDown(0))
        {
            Instantiate(arrow, arrowSpawnPoint.position,arrowSpawnPoint.rotation);
            fireCount = fireRate;
        }

        if(Input.GetMouseButton(0))
        {
            fireCount -= Time.deltaTime;

            if(fireCount <= 0)
            {
                Instantiate(arrow, arrowSpawnPoint.position, arrowSpawnPoint.rotation);
                fireCount = fireRate;
            }
        }

        if (moveInput != Vector2.zero)
            anim.SetBool("isMoving", true);
        else
            anim.SetBool("isMoving", false);

    }

    //rotate bow arm
    private void RotateBow(Vector3 mousePos, Vector3 screenPoint)
    {
        Vector2 offset = new Vector2((mousePos.x) - screenPoint.x, (mousePos.y) - screenPoint.y);
        float angle = Mathf.Atan2(offset.y, offset.x) * Mathf.Rad2Deg;
        bowArm.rotation = Quaternion.Euler(0, 0, angle);
    }
}
