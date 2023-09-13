using System.Security.Cryptography;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using UnityEngine.Windows;
using static PlayerInputActions;


public class Player : MonoBehaviour, IPlayerActions
{
    [Header("Point_Star")]
    [HideInInspector] public int star;
    [Header("Status")]
    [SerializeField] public int hp;
    Vector2 moveDirection;
    [SerializeField] float speed;
    [SerializeField] float runspeed;
    [SerializeField] float walkspeed;
    [SerializeField] bool drop = false;
    [Header("Jump")]
    [SerializeField] float powerJump;
    float datapowerJump;
    [SerializeField] float countJump;
    float datacountJump;
    [Header("Physics")]
    [SerializeField] Transform pointCheckGround;
    [SerializeField] float radiusCheckGround;
    [SerializeField] LayerMask ground;
    Rigidbody2D rb;
    [Header("PlayerInputActions")]
    [Space][SerializeField] PlayerInputActions playerInputAction;
    [Header("Animation")]
    Animator anim;

    private void OnEnable()
    {
        playerInputAction.Player.Enable();
    }
    void OnDisable()
    {
        playerInputAction.Player.Disable();
    }
    void OnApplicationQuit()
    {

    }
    private void Awake()
    {
        playerInputAction = new PlayerInputActions();
        playerInputAction.Player.SetCallbacks(this);
    }
    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        //star = SaveManager.Instance.star[SaveManager.Instance.numSave];
        rb = this.gameObject.GetComponent<Rigidbody2D>();
        datacountJump = countJump;
        datapowerJump = powerJump;
        speed = walkspeed;
    }

    // Update is called once per frame
    void Update()
    {
        checkGround();
        ControlPlayer();
    }
    public void ControlPlayer()
    {
        //move
        if (!drop)
        {
            rb.velocity = new Vector2(moveDirection.x * speed, rb.velocity.y);
        }
        else
        {
            rb.velocity = Vector2.zero;
        }
    }

    void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.tag == "Warp")
        {
            SceneManager.LoadScene("GamePlay 2");
        }
    }
    void Save()
    {
        SaveManager.Instance.star[SaveManager.Instance.numSave] = star;
        SaveManager.Instance.nameMap[SaveManager.Instance.numSave] = SceneManager.GetActiveScene().name;
        SaveManager.Instance.SaveGame(SaveManager.Instance.numSave);
    }
    public void OnMovement(InputAction.CallbackContext context)
    {
        Vector2 inputVector = context.ReadValue<Vector2>();
        anim.SetFloat("Move", inputVector.x);
        if (inputVector.x < 0)
        {
            transform.localScale = new Vector3(-1, 1, 1);
        }
        else if (inputVector.x > 0)
        {
            transform.localScale = new Vector3(1, 1, 1);
        }
        moveDirection.x = inputVector.x;
    }
    public void OnRun(InputAction.CallbackContext context)
    {
        anim.SetBool("Run", true);
        speed = runspeed;
        if (context.canceled)
        {
            anim.SetBool("Run", false);
            speed = walkspeed;
        }
    }
    public void OnJump(InputAction.CallbackContext context)
    {
        if (datacountJump > 0)
        {
            datacountJump -= 1;
            rb.velocity = new Vector2(rb.velocity.x, rb.velocity.y + datapowerJump);
        }
    }
    public void OnDrop(InputAction.CallbackContext context)
    {
        drop = true;
        anim.SetBool("Drop", drop);
        if (context.canceled)
        {
            drop = false;
            anim.SetBool("Drop", drop);
        }
    }
    void checkGround()
    {
        if (Physics2D.OverlapCircle(pointCheckGround.position, radiusCheckGround, ground) && rb.velocity.y == 0)
        {
            datacountJump = countJump;
        }
    }
    public void OnPause(InputAction.CallbackContext context)
    {

    }


}
