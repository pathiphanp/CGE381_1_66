using System.Security.Cryptography;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using TMPro;
using static PlayerInputActions;

enum Size
{
    SMALL, NORMAL
}
public class Player : MonoBehaviour, IPlayerActions, IUIActions, TakeDamage
{
    public static event Action CutSceneTrigger;

    [Header("SetGame")]
    [SerializeField] bool downMode;
    [SerializeField] bool sideMode;

    [Header("Status")]
    [SerializeField] public int hp;
    public GameObject showHp;
    public TMP_Text pointHp;
    bool immortal = false;
    Vector2 moveDirection;
    [Header("Point_Star")]
    public GameObject showStar;
    public TMP_Text pointStar;
    [HideInInspector] public int star;
    #region "Inventory"
    [Header("Inventory")]
    [SerializeField] GameObject inventory;
    [Header("Key")]
    [SerializeField] public GameObject keyItem;
    public int key;
    #endregion


    [Header("ChackIdel")]
    float waittime;
    #region //Move
    [Header("Body")]
    [SerializeField] GameObject body;
    [SerializeField] GameObject leg;
    [Header("Small")]
    [SerializeField] Size size;
    [SerializeField] float smallSize;
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
    #endregion

    [Header("CheckGround")]
    [SerializeField] GameObject pointCheckGround;
    [SerializeField] float radiusCheckGround;
    [SerializeField] LayerMask ground;
    [Header("Check Head Enemy")]
    [SerializeField] LayerMask dodamage;
    [Header("Platfrom")]
    [SerializeField] LayerMask platfrom;
    [SerializeField] GameObject _platfrom;
    [SerializeField] float distanceChrckPlatfrom;
    [Header("Set Gravity")]
    [SerializeField] float gravity;
    [HideInInspector] public Rigidbody2D rb;

    [Header("PlayerInputActions")]
    [Space][SerializeField] PlayerInputActions playerInputAction;
    [Header("Animation")]
    [SerializeField] Animator anim;
    [Header("Die")]
    public GameObject dieScenes;
    public BtnDataSystem[] btn;
    int indexDieManu = 0;
    bool die;
    [Header("Sprite")]
    SpriteRenderer _sprite;
    SpriteRenderer _sprite_Leg;

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
        _sprite = body.GetComponent<SpriteRenderer>();
        _sprite_Leg = leg.GetComponent<SpriteRenderer>();
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
    void SetUpDownMode(bool start)
    {
        if (downMode && start)
        {
            anim.SetBool("DownMode", downMode);
            downspeed = standard_down;
            rb.gravityScale = 1;

        }
        else
        {
            rb.gravityScale = 0;
            downspeed = 0;
        }

    }
    public void ControlPlayer()
    {
        //move
        if (drop && !downMode && rb.velocity.y == 0)
        {
            rb.velocity = Vector2.zero;
        }
        else if (sideMode)
        {
            rb.velocity = new Vector2(moveDirection.x * speedMove, rb.velocity.y);
        }
        else if (downMode)
        {
            rb.velocity = new Vector2(moveDirection.x * speedMove, downspeed);
        }
        if (moveDirection.x == 0)//Check Idel
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
            //Right
            if (downMode)
            {
                anim.SetBool("Side", true);
            }
            body.transform.rotation = Quaternion.Euler(0, 180, 0);
        }
        else if (inputVector.x > 0)
        {
            //Left
            if (downMode)
            {
                anim.SetBool("Side", true);
            }
            body.transform.rotation = Quaternion.Euler(0, 0, 0);
        }
        else
        {
            anim.SetBool("Side", false);
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
        if (context.started && !downMode && !drop)
        {
            Jump();
        }
    }
    void Jump()
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
    public void OnUp(InputAction.CallbackContext context)
    {
        downspeed = slow_down;
        if (context.canceled)
        {
            downspeed = standard_down;
        }
    }
    void OnDrawGizmos()
    {
        Debug.DrawRay(pointCheckGround.transform.position, Vector3.down * distanceChrckPlatfrom);
    }
    void checkGround()
    {
        Collider2D checkGround = Physics2D.OverlapCircle(pointCheckGround.transform.position, radiusCheckGround, ground);
        RaycastHit2D doda = Physics2D.Raycast(pointCheckGround.transform.position, Vector2.down, distanceChrckPlatfrom, dodamage);
        CheckPlatfrom();
        if (checkGround && rb.velocity.y <= 0)//On Ground
        {
            rb.gravityScale = 0;
            if (speedMove < walkspeed || speedMove < runspeed)//Change speed
            {
                speedMove = walkspeed;
                if (isRun)
                {
                    speedMove = runspeed;
                }
            }
            if (rb.velocity.y != 0)//Stop move
            {
                rb.velocity = Vector2.zero;
            }
            if (datacountJump == 0)//Reset JumpCount
            {
                datacountJump = countJump;
                onJump = false;
                anim.SetBool("Jump", false);
            }
        }
        else //Out Ground
        {

            if (rby > 7.9 && onJump)//Down to Floor
            {
                rb.gravityScale = speedDown;
            }
            else if (!die)
            {
                rb.gravityScale = gravity;

            }
        }
        if (doda)//Check destroy Enenmy
        {
            datacountJump = 1;
            Jump();
            Destroy(doda.collider.gameObject);
        }
    }
    void CheckPlatfrom()
    {
        RaycastHit2D plat = Physics2D.Raycast(pointCheckGround.transform.position, Vector2.down, distanceChrckPlatfrom, platfrom);
        if (plat)//CheckPlatfrom
        {
            if (_platfrom == null)
            {
                _platfrom = plat.collider.gameObject;
                if (_platfrom != null)
                {
                    AddChild add = _platfrom.GetComponent<AddChild>();
                    add.AddChild(this.gameObject);
                }
            }
        }
        else//Out off Platfrom
        {
            if (_platfrom != null)
            {
                OutPlatfrom();
            }
        }
    }
    public void OutPlatfrom()
    {
        DeleteChild delete = _platfrom.GetComponent<DeleteChild>();
        delete.DeleteChild();
        _platfrom = null;
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
        if (Gamemanager.Instance.cutScenesStartGame)
        {
            body.SetActive(false);
        }
        SetUpDownMode(false);
    }
    void PlayerMode()
    {
        playerInputAction.UI.Disable();
        playerInputAction.Player.Enable();
        body.SetActive(true);
        SetUpDownMode(true);
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
            other.GetComponent<GetStar>().Die();
            CheckGainAllStars.Instance.CheckStar();
            StartCoroutine(ShowStar());
        }
        if (other.tag == "Enemy" && !immortal)
        {
            TakeDamage(1);
            StartCoroutine(ShowHp());
            StartCoroutine(Immortal());
        }
        if (other.tag == "Key")
        {
            key++;
            keyItem.SetActive(true);
            other.GetComponent<Key>().Die();
        }
        if (other.tag == "Heal")
        {
            hp++;
            hp = Mathf.Clamp(hp, 0, 5);
            StartCoroutine(ShowHp());
            Destroy(other.gameObject);
        }
    }
    public void AliceChangeSize()
    {
        if (size == Size.NORMAL)
        {
            powerJump = 17;
            size = Size.SMALL;
            transform.localScale = new Vector3(0.35f, 0.35f, 0.35f);
        }
        else
        {
            powerJump = 22;
            size = Size.NORMAL;
            transform.localScale = new Vector3(1, 1, 1);
        }
    }
    IEnumerator ShowStar()
    {
        pointStar.text = star.ToString();
        showStar.SetActive(true);
        yield return new WaitForSeconds(2);
        showStar.SetActive(false);
    }
    IEnumerator ShowHp()
    {
        pointHp.text = hp.ToString();
        showHp.SetActive(true);
        yield return new WaitForSeconds(2);
        showHp.SetActive(false);
    }
    IEnumerator Immortal()
    {
        anim.Play("Hit");
        immortal = true;
        for (int i = 0; i < 30; i++)
        {
            if (_sprite.color.a == 0)
            {
                _sprite.color = new Color(1, 1, 1, 1);
                _sprite_Leg.color = new Color(1, 1, 1, 1);

            }
            else
            {
                _sprite.color = new Color(1, 1, 1, 0);
                _sprite_Leg.color = new Color(1, 1, 1, 0);
            }
            yield return new WaitForSeconds(0.1f);
        }
        _sprite_Leg.color = new Color(1, 1, 1, 1);
        _sprite.color = new Color(1, 1, 1, 1);
        immortal = false;
    }
}

