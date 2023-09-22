using System.Security.Cryptography;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using static PlayerInputActions;


public class Player : MonoBehaviour, IPlayerActions, IUIActions, TakeDamage
{
    public static event Action CutSceneTrigger;

    [Header("Point_Star")]
    [HideInInspector] public int star;
    [Header("Status")]
    [SerializeField] public int hp;
    Vector2 moveDirection;
    [Header("ChackIdel")]
    float waittime;
    [Header("Walk")]
    [SerializeField] float speed;
    [SerializeField] float walkspeed;
    [Header("Run")]
    [SerializeField] float runspeed;
    bool isRun = false;
    [Header("Drop")]
    [SerializeField] bool drop = false;

    [Header("Jump")]
    [SerializeField] float powerJump;
    float datapowerJump;
    [SerializeField] float countJump;
    bool onJump = false;
    float datacountJump;
    [Header("CheckGround")]
    [SerializeField] Transform pointCheckGround;
    [SerializeField] float radiusCheckGround;
    [SerializeField] LayerMask ground;
    public Rigidbody2D rb;
    [Header("PlayerInputActions")]
    [Space][SerializeField] PlayerInputActions playerInputAction;
    [Header("Animation")]
    [SerializeField] Animator anim;
    [Header("Die")]
    public GameObject dieScenes;
    public BtnDataSystem[] btn;
    int indexDieManu = 0;
    bool die;
    private void OnEnable()
    {
        playerInputAction.Player.Enable();
        ControlCutScenes.CutSceneStart += UIMode;
        SpawnCutScenes.EndCutScene += PlayerMode;
    }
    void OnDisable()
    {
        playerInputAction.Player.Disable();
        playerInputAction.UI.Disable();
        ControlCutScenes.CutSceneStart -= UIMode;
        SpawnCutScenes.EndCutScene -= PlayerMode;
    }
    void OnApplicationQuit()
    {
        //Save();
    }
    private void Awake()
    {
        playerInputAction = new PlayerInputActions();
        playerInputAction.Player.SetCallbacks(this);
        playerInputAction.UI.SetCallbacks(this);
    }
    // Start is called before the first frame update
    void Start()
    {
        //star = SaveManager.Instance.star[SaveManager.Instance.numSave];
        rb = GetComponent<Rigidbody2D>();
        datacountJump = countJump;
        datapowerJump = powerJump;
        speed = walkspeed;
        RestartPlayer();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            TakeDamage(65);
        }
        checkGround();
        ControlPlayer();
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
    #region //GamePlay
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
        if (moveDirection.x == 0)
        {
            waittime -= Time.deltaTime;
            if (waittime <= 0)
            {
                anim.SetBool("NotMove", true);
            }
        }
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
        if (context.performed)
        {
            anim.SetBool("NotMove", false);
            waittime = 0.05f;
        }
    }
    public void OnRun(InputAction.CallbackContext context)
    {
        isRun = true;
        anim.SetBool("Run", isRun);
        speed = runspeed;
        if (context.canceled)
        {
            isRun = false;
            anim.SetBool("Run", isRun);
            speed = walkspeed;
        }
    }

    public void OnDrop(InputAction.CallbackContext context)
    {
        if (!onJump)
        {
            drop = true;
            anim.SetBool("Drop", drop);
            if (context.canceled)
            {
                drop = false;
                anim.SetBool("Drop", drop);
            }
        }
    }

    public void OnJump(InputAction.CallbackContext context)
    {
        if (datacountJump > 0)
        {
            datacountJump -= 1;
            rb.velocity = new Vector2(rb.velocity.x, rb.velocity.y + datapowerJump);
            onJump = true;
            anim.SetBool("Jump", true);
        }
        if (rb.velocity.y != 0)
        {
            rb.gravityScale = 3;
        }
    }
    void checkGround()
    {
        if (Physics2D.OverlapCircle(pointCheckGround.position, radiusCheckGround, ground) && rb.velocity.y == 0 && datacountJump == 0)
        {
            datacountJump = countJump;
            onJump = false;
            anim.SetBool("Jump", false);
            rb.gravityScale = 2;
        }
    }
    public void OnInventory(InputAction.CallbackContext context)
    {

    }
    public void OnPause(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            UIMode();
        }
    }
    #endregion
    #region //UI
    public void OnResume(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            PlayerMode();
        }
    }

    public void OnCutSceneSkip(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            if (CutSceneTrigger != null)
            {
                Player.CutSceneTrigger();
            }
            if (die == true)
            {
                if (indexDieManu == 0)
                {
                    SceneManager.LoadScene(SceneManager.GetActiveScene().name);
                }
                else if (indexDieManu == 1)
                {
                    SceneManager.LoadScene("Start and Manu");
                }
            }
        }
    }

    public static void SkipCutScene()
    {
        CutSceneTrigger?.Invoke();
    }
    #endregion

    void UIMode()
    {
        playerInputAction.Player.Disable();
        playerInputAction.UI.Enable();
    }
    void PlayerMode()
    {
        playerInputAction.Player.Enable();
        playerInputAction.UI.Disable();
    }

    public void OnDie(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            indexDieManu = ArrowControl.aC.SetSlotUpDown(indexDieManu, btn.Length);
            for (int i = 0; i < btn.Length; i++)
            {
                if (indexDieManu == i)
                {
                    btn[i].manuBtn.animator.Play("Highlighted");

                }
                else
                {
                    btn[i].manuBtn.animator.Play("Normal");
                }
            }

        }
    }

    void RestartPlayer()
    {
        hp = 5;
        die = false;
        dieScenes.SetActive(false);
    }
    public void TakeDamage(int damage)
    {
        hp -= damage;
        if (hp <= 0)
        {
            UIMode();
            die = true;
            dieScenes.SetActive(true);
            btn[0].manuBtn.animator.Play("Highlighted");
        }
    }
}
