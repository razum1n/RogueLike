using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public static PlayerController instance;

    public float moveSpeed;
    public float fireRate;
    public int playerScoreMultiplier;
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
    public Transform trailPosition;

    private Camera mainCamera;
    private Vector3 screenPoint;
    private Vector2 offset;
    private float angle;
    public Animator anim;

    public string arrowType;
    public int playerSpeedLevel;

    public SpriteRenderer bodySprite;
    public Dissolve dissolve;

    public bool canMove = true;
    public bool hasKey = false;

    public int currentRoomID;

    public enum ControlType { Keyboard, Controller };
    public ControlType inputType;

    void Awake()
    {
        instance = this;
    }
    
    // Start is called before the first frame update
    void Start()
    {
        arrowType = GameManager.instance.playerArrow;
        moveSpeed = GameManager.instance.playerSpeed;
        playerSpeedLevel = GameManager.instance.playerSpeedLevel;
        playerScoreMultiplier = 1;
        rb = GetComponent<Rigidbody2D>();
        mainCamera = Camera.main;
        anim = GetComponent<Animator>();
        activeMoveSpeed = moveSpeed;
        inputType = (ControlType)GameManager.instance.playerControlType;
        dissolve = GetComponent<Dissolve>();
    }

    // Update is called once per frame
    void Update()
    {
        if(canMove && !LevelManager.instance.isPaused)
        {
            HandleMovement();
            HandleShooting();
            HandleDashing();
        }

        if (playerSpeedLevel == 1)
            dashCooldown = 0.7f;
        else if (playerSpeedLevel == 2)
            dashCooldown = 0.5f;
        else if (playerSpeedLevel == 3)
            dashCooldown = 0.3f;

    }

    private void HandleShooting()
    {
        if (Input.GetButtonDown("Fire"))
        {
            GameObject arrow = Pool.instance.Get(arrowType);
            if (arrow != null)
            {
                arrow.transform.position = arrowSpawnPoint.position;
                arrow.transform.rotation = arrowSpawnPoint.rotation;
                arrow.SetActive(true);
                AudioManager.instance.PlaySound("arrow");
            }
            fireCount = fireRate;
        }

        if (Input.GetButton("Fire"))
        {
            fireCount -= Time.deltaTime;

            if (fireCount <= 0)
            {
                GameObject arrow = Pool.instance.Get(arrowType);
                if(arrow != null)
                {
                    arrow.transform.position = arrowSpawnPoint.position;
                    arrow.transform.rotation = arrowSpawnPoint.rotation;
                    arrow.SetActive(true);
                    AudioManager.instance.PlaySound("arrow");
                }
                fireCount = fireRate;
            }
        }
    }

    private void HandleDashing()
    {
        if (Input.GetButtonDown("Dash"))
        {
            if (dashCoolCounter <= 0 && dashCounter <= 0)
            {
                activeMoveSpeed = dashSpeed;
                dashCounter = dashLength;
                isDashing = true;
                anim.SetTrigger("isDashing");
                PlayerHealthController.instance.DashIFrames();
            }

        }

        if (dashCounter > 0)
        {
            dashCounter -= Time.deltaTime;
            if (dashCounter <= 0)
            {
                activeMoveSpeed = moveSpeed;
                isDashing = false;
                dashCoolCounter = dashCooldown;
            }
        }

        if (dashCoolCounter > 0)
        {
            dashCoolCounter -= Time.deltaTime;
        }
    }

    public void StopPlayer()
    {
        moveInput = Vector2.zero;
        rb.velocity = Vector2.zero;
    }

    private void HandleMovement()
    {
        moveInput.x = Input.GetAxisRaw("Horizontal");
        moveInput.y = Input.GetAxisRaw("Vertical");

        moveInput.Normalize();

        rb.velocity = moveInput * activeMoveSpeed;

        switch(inputType)
        {
            case ControlType.Controller:
                Vector3 controlAimPoint = new Vector3(Input.GetAxisRaw("RightHoriz"), Input.GetAxisRaw("RightVert"), 0);
                if (controlAimPoint.x < 0)
                {
                    transform.localScale = new Vector3(-1f, 1f, 1f);
                    trailPosition.localScale = new Vector3(-1f, 1f, 1f);
                    bowArm.localScale = new Vector3(-1f, -1f, 1f);
                }
                else if(controlAimPoint.x > 0.1f)
                {
                    transform.localScale = Vector3.one;
                    trailPosition.localScale = Vector3.one;
                    bowArm.localScale = Vector3.one;
                }
                angle = Mathf.Atan2(-controlAimPoint.y, controlAimPoint.x) * Mathf.Rad2Deg;
                bowArm.rotation = Quaternion.Euler(0, 0, angle);
                break;
            case ControlType.Keyboard:
                Vector3 mousePos = Input.mousePosition;
                screenPoint = mainCamera.WorldToScreenPoint(transform.localPosition);

                if (mousePos.x < screenPoint.x)
                {
                    transform.localScale = new Vector3(-1f, 1f, 1f);
                    trailPosition.localScale = new Vector3(-1f, 1f, 1f);
                    bowArm.localScale = new Vector3(-1f, -1f, 1f);
                }
                else
                {
                    transform.localScale = Vector3.one;
                    trailPosition.localScale = Vector3.one;
                    bowArm.localScale = Vector3.one;
                }
                offset = new Vector2((mousePos.x) - screenPoint.x, (mousePos.y) - screenPoint.y);
                angle = Mathf.Atan2(offset.y, offset.x) * Mathf.Rad2Deg;
                bowArm.rotation = Quaternion.Euler(0, 0, angle);
                break;
        }
       
        if (moveInput != Vector2.zero)
            anim.SetBool("isMoving", true);
        else
            anim.SetBool("isMoving", false);
    }
}
