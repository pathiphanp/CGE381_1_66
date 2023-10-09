using System.Security.Cryptography;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using TMPro;
using static PlayerInputActions;


public class Player : MonoBehaviour, IPlayerActions, IUIActions, TakeDamage
{
    public static event Action CutSceneTrigger;

    private const int V = 1;
    [Header("SetGame")]
    [SerializeField] bool downMode;
    [SerializeField] bool sideMode;

    [Header("Point_Star")]
    public GameObject showStar;
    public TMP_Text pointStar;
    [HideInInspector] public int star;

    [Header("Status")]
    [SerializeField] public int hp;
    public GameObject showHp;
    public TMP_Text pointHp;



    Vector2 moveDirection;
    [Header("Inventory")]
    [SerializeField] GameObject inventory;

    [Header("ChackIdel")]
    float waittime;
    #region //Move
    [Header("Body")]
    [SerializeField] GameObject body;
    [Header("Move")]
    float speedMove;
    [Header("MoveDownMode")]
    [SerializeField] float boostpeed_down;
    [SerializeField] float standard_down;
    [SerializeField] float slow_down;
    [SerializeField] float downspeed;

    [Header("Walk")]
    [SerializeField] float walkspeed;
    [Header("Run")]
    [SerializeField] float runspeed;
    bool isRun = false;
    #endregion
    [Header("Drop")]
    [SerializeField] bool drop = false;
    [Header("Jump")]
    [SerializeField] float powerJump;
    [SerializeField] float speedDown;
    [SerializeField] float speedUp;
    [SerializeField] float countJump;
    bool onJump = false;
    float rby = 0f;
    float datacountJump;
    [Header("CheckGround")]
    [SerializeField] GameObject pointCheckGround;
    [SerializeField] float radiusCheckGround;
    [SerializeField] LayerMask ground;
    [SerializeField] float gravity;

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

    /////////////////////////
    /////////////////////////
    private void OnEnable()
    {
        playerInputAction.Player.Enable();
        Gamemanager.PlayerUIMode += UIMode;
        Gamemanager.PlayerMode += PlayerMode;
    }
    void OnDisable()
    {
        playerInputAction.Player.Disable();
        playerInputAction.UI.Disable();
        Gamemanager.PlayerUIMode -= UIMode;
        Gamemanager.PlayerMode -= PlayerMode;
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
        speedMove = walkspeed;
        RestartPlayer();
        rby = rb.velocity.y;
        SetUpDownMode();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {

        }
        else if (Input.GetMouseButtonDown(1))
        {

        }
        if (!downMode)
        {
            checkGround();
        }
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
    void SetUpDownMode()
    {
        if (downMode)
        {
            anim.SetBool("DownMode", downMode);
            downspeed = standard_down;
        }
        else
        {
            downspeed = 0;
        }

    }
    public void ControlPlayer()
    {
        //move
        if (drop && !downMode)
        {
            rb.velocity = Vector2.zero;
        }
        else if (sideMode)
        {
            rb.velocity = new Vector2(moveDirection.x * speedMove, rb.velocity.y);
        }
        else
        {
            rb.velocity = new Vector2(moveDirection.x * speedMove, downspeed);
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
            body.transform.localScale = new Vector3(-1, 1, 1);
        }
        else if (inputVector.x > 0)
        {
            body.transform.localScale = new Vector3(1, 1, 1);
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
        if (downMode)
        {
            DownSpeedControl();
        }
        else
        {
            speedMove = runspeed;
        }
        if (context.canceled)
        {
            isRun = false;
            anim.SetBool("Run", isRun);
            speedMove = walkspeed;
            downspeed = standard_down;
        }
    }

    public void OnDrop(InputAction.CallbackContext context)
    {
        drop = true;
        anim.SetBool("Drop", drop);
        if (downMode)
        {
            DownSpeedControl();
        }
        if (context.canceled)
        {
            drop = false;
            anim.SetBool("Drop", drop);
            downspeed = standard_down;
        }
    }

    void DownSpeedControl()
    {
        downspeed = boostpeed_down;
    }
    public void OnJump(InputAction.CallbackContext context)
    {
        if (context.started && !downMode)
        {
            if (datacountJump > 0)
            {
                rby = 0;
                rby = Mathf.Lerp(rby, powerJump, Time.deltaTime * speedUp);
                datacountJump -= 1;
                rb.velocity = new Vector2(rb.velocity.x, rby);
                onJump = true;
                anim.SetBool("Jump", true);
            }
        }
    }
    public void OnUp(InputAction.CallbackContext context)
    {
        downspeed = slow_down;
        if (context.canceled)
        {
            downspeed = standard_down;
        }
    }
    void checkGround()
    {
        if (Physics2D.OverlapCircle(pointCheckGround.transform.position, radiusCheckGround, ground) && rb.velocity.y <= 0)
        {
            rb.gravityScale = 0;
            if (speedMove < walkspeed || speedMove < runspeed)
            {
                speedMove = walkspeed;
                if (isRun)
                {
                    speedMove = runspeed;
                }
            }
            if (rb.velocity.y != 0)
            {
                rb.velocity = Vector2.zero;
            }
            if (datacountJump == 0)
            {
                datacountJump = countJump;
                onJump = false;
                anim.SetBool("Jump", false);
            }
        }
        else
        {
            if (rby > 7.9 && onJump)
            {
                rb.gravityScale = speedDown;
            }
            else if (!die)
            {
                rb.gravityScale = gravity;
            }
        }
    }
    public void OnInventory(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            inventory.SetActive(true);
            UIMode();
            Time.timeScale = 0f;
        }
    }
    public void OnPause(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            UIMode();
            Time.timeScale = 0f;
        }
    }
    #endregion
    #region //UI
    public void OnResume(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            PlayerMode();
            inventory.SetActive(false);
            Time.timeScale = 1f;
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
        playerInputAction.UI.Disable();
        playerInputAction.Player.Enable();
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
            Debug.Log("die");
            rb.velocity = Vector2.zero;
            UIMode();
        }
    }

    public void DieMode()
    {
        die = true;
        dieScenes.SetActive(true);
        btn[0].manuBtn.animator.Play("Highlighted");
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Water")
        {
            TakeDamage(99);
            anim.Play("WaterDie");
        }
        if (other.tag == "Star")
        {
            star++;
            pointStar.text = star.ToString();
            StartCoroutine(ShowStar());
            Destroy(other.gameObject);
        }
        if (other.tag == "Enemy")
        {
            TakeDamage(1);
            pointHp.text = hp.ToString();
            StartCoroutine(ShowHp());
            Destroy(other.gameObject);
        }

    }

    IEnumerator ShowStar()
    {
        showStar.SetActive(true);
        yield return new WaitForSeconds(2);
        showStar.SetActive(false);
    }
    IEnumerator ShowHp()
    {
        showHp.SetActive(true);
        yield return new WaitForSeconds(2);
        showHp.SetActive(false);
    }


}

