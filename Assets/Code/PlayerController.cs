using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public static PlayerController instance;

    public float moveSpeed;
    public float fireRate;
    public float dashSpeed = 8f;
    public float dashLength = .5f;
    public float dashCooldown = 1f;
    public float dashInvincibility = 0.5f;
    private float fireCount;
    private float activeMoveSpeed;
    private float dashCounter, dashCoolCounter;
    public bool isDashing = false;

    private Vector2 moveInput;

    public Rigidbody2D rb;

    public Transform bowArm;
    public Transform arrowSpawnPoint;

    private Camera mainCamera;
    public Animator anim;

    public GameObject arrow;

    public GameObject[] trailEffects;

    public SpriteRenderer bodySprite;



    void Awake()
    {
        instance = this;
    }
    
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        mainCamera = Camera.main;
        anim = GetComponent<Animator>();
        activeMoveSpeed = moveSpeed;
    }

    // Update is called once per frame
    void Update()
    {
        moveInput.x = Input.GetAxisRaw("Horizontal");
        moveInput.y = Input.GetAxisRaw("Vertical");

        moveInput.Normalize();

        rb.velocity = moveInput * activeMoveSpeed;


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

        if(Input.GetKeyDown(KeyCode.Space))
        {
            if(dashCoolCounter <= 0 && dashCounter <= 0)
            {
                activeMoveSpeed = dashSpeed;
                dashCounter = dashLength;
                isDashing = true;
                anim.SetTrigger("isDashing");

                for(int i=0;i<trailEffects.Length;i++)
                {
                    trailEffects[i].SetActive(true);
                }
            }

        }

        if(dashCounter > 0)
        {
            dashCounter -= Time.deltaTime;
            if(dashCounter <= 0)
            {
                activeMoveSpeed = moveSpeed;
                isDashing = false;
                dashCoolCounter = dashCooldown;
                for (int i = 0; i < trailEffects.Length; i++)
                {
                    trailEffects[i].SetActive(false);
                }
            }
        }

        if(dashCoolCounter > 0)
        {
            dashCoolCounter -= Time.deltaTime;
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
