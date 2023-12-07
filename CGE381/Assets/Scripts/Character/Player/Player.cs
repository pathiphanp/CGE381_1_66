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
    public static event Action HealAll;

    [Header("SetGame")]
    [SerializeField] bool downMode;
    [SerializeField] bool sideMode;

    [Header("Status")]
    [SerializeField] public int hp;
    public GameObject showHp;
    public TMP_Text pointHp;
    [SerializeField] bool immortal = false;
    bool normalDie = true;
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
    [SerializeField] float powerJumpNormal;
    [SerializeField] float powerJumpSmall;
    float powerJump;
    [SerializeField] float speedDown;
    [SerializeField] float speedUp;
    [SerializeField] float countJump;
    [SerializeField] bool onJump = false;
    float rby = 0f;
    float datacountJump;
    int jumpMove;
    #endregion

    [Header("CheckGround")]
    [SerializeField] GameObject pointCheckGround;
    [SerializeField] float radiusCheckGround;
    [SerializeField] LayerMask ground;
    [Header("Check Head Enemy")]
    [SerializeField] LayerMask dodamage;
    bool candodamage = false;
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
        Player.HealAll += HealFull;
    }
    void OnDisable()
    {
        playerInputAction.Player.Disable();
        playerInputAction.UI.Disable();
        Gamemanager.PlayerUIMode -= UIMode;
        Gamemanager.PlayerMode -= PlayerMode;
        Player.HealAll -= HealFull;
        Save();
    }
    void OnApplicationQuit()
    {
        Save();
    }
    private void Awake()
    {
        playerInputAction = new PlayerInputActions();
        playerInputAction.Player.SetCallbacks(this);
        playerInputAction.UI.SetCallbacks(this);
        rb = GetComponent<Rigidbody2D>();
    }
    // Start is called before the first frame update
    void Start()
    {
        LoadSave();
        speedMove = walkspeed;
        powerJump = powerJumpNormal;
        datacountJump = countJump;
        RestartPlayer();
        rby = rb.velocity.y;
        if (body != null)
        {
            _sprite = body.GetComponent<SpriteRenderer>();
        }
        if (leg != null)
        {
            _sprite_Leg = leg.GetComponent<SpriteRenderer>();
        }
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
        if (!downMode && !die)
        {
            checkGround();
        }
        ControlPlayer();
        //Debug.Log(rb.velocity.y);
    }
    void Save()
    {
        SaveManager.Instance.star[SaveManager.Instance.numSave] = star;
        SaveManager.Instance.nameMap[SaveManager.Instance.numSave] = SceneManager.GetActiveScene().name;
        SaveManager.Instance.SaveGame(SaveManager.Instance.numSave);
    }
    void LoadSave()
    {
        star = SaveManager.Instance.star[SaveManager.Instance.numSave];
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
            jumpMove = Mathf.RoundToInt(rb.velocity.y);
            anim.SetInteger("JumpMove", jumpMove);
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
            SoundManager.Instance.PlaySfx("PlayerJump");
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
            if (rb.velocity.y < 0)
            {
                rb.velocity = new Vector2(rb.velocity.x, 0);
            }
            if (datacountJump == 0)//Reset JumpCount
            {
                datacountJump = countJump;
            }

            onJump = false;
            anim.SetBool("Jump", false);
        }
        else //Out Ground
        {
            if (onJump && datacountJump == 0)
            {
                rb.gravityScale = speedDown;
                if (rb.velocity.y <= 0)
                {
                    Dodamage();
                }
            }
            if ((rb.velocity.y <= 0 || rb.velocity.y > 0) && !onJump)//Down to Floor
            {
                Dodamage();
                if (datacountJump > 0)
                {
                    datacountJump--;
                }
                rb.gravityScale = gravity;
                if (!onJump)
                {
                    anim.Play("JumpDown");
                    anim.SetBool("Jump", true);
                }
            }
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
            }
            if (_platfrom != null)
            {
                AddChild add = _platfrom.GetComponent<AddChild>();
                add.AddChild(this.gameObject);
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
    void Dodamage()
    {
        RaycastHit2D dodamage = Physics2D.Raycast(pointCheckGround.transform.position,
        Vector2.down, distanceChrckPlatfrom, this.dodamage);
        if (dodamage && size == Size.NORMAL)//Check destroy Enenmy
        {
            datacountJump = 1;
            Jump();
            if (dodamage.collider.gameObject.GetComponent<ObjEnemy>() != null)
            {
                dodamage.collider.gameObject.GetComponent<ObjEnemy>().DelayDie();
            }
            else
            {
                Destroy(dodamage.collider.gameObject);
            }
        }
    }

    #endregion
    #region //UI    

    public void OnPause(InputAction.CallbackContext context)
    {
        /*if (context.started)
        {
            UIMode();
            Time.timeScale = 0f;
        }*/
    }
    public void OnResume(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            PlayerMode();
            inventory.SetActive(false);
            showHp.SetActive(false);
            showStar.SetActive(false);
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
    public void OnInventory(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            inventory.SetActive(true);
            showHp.SetActive(true);
            showStar.SetActive(true);
            UIMode();
            Time.timeScale = 0f;
        }
    }
    #endregion


    #region //ChangeMode
    void UIMode()
    {
        immortal = true;
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
        immortal = false;
        playerInputAction.UI.Disable();
        playerInputAction.Player.Enable();
        body.SetActive(true);
        SetUpDownMode(true);
    }
    #endregion
    public void OnDie(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            indexDieManu = ArrowControl.Instance.SetSlotUpDown(indexDieManu, btn.Length);
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
    public void TakeDamage(int damage, string dieReprot)
    {
        if (immortal == false)
        {
            hp -= damage;
        }
        if (hp <= 0)
        {
            UIMode();
            die = true;
            rb.velocity = Vector2.zero;
            rb.gravityScale = 0;
            if (dieReprot == "Water")
            {
                anim.Play("WaterDie");
            }
            else
            {
                DieMode();
            }
        }
    }

    public void DieMode()
    {
        dieScenes.SetActive(true);
        btn[0].manuBtn.animator.Play("Highlighted");
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Water")
        {
            TakeDamage(99, "Water");
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
            TakeDamage(1, "");
            StartCoroutine(ShowHp());
            EnemyHit();
        }
        if (other.tag == "Key")
        {
            key++;
            SoundManager.Instance.PlaySfx("StarDie");
            keyItem.SetActive(true);
            other.GetComponent<Key>().Die();
        }
        if (other.tag == "Heal")
        {
            Heal(1);
            Destroy(other.gameObject);
        }
        if (other.tag == "HealAll")
        {
            Heal(5);
            Destroy(other.gameObject);
        }
        if (other.tag == "BuffJump")
        {
            StartCoroutine(BuffJump());
            Destroy(other.gameObject);
        }
        if (other.tag == "Immortal")
        {
            StartCoroutine(Immortal_when_hit(60));
            Destroy(other.gameObject);
        }
        if (other.tag == "DodamageBoss")
        {
            BossClock bossClock = FindObjectOfType<BossClock>();
            Destroy(bossClock.head);
            Destroy(other.gameObject);
        }
        if (other.tag == "Bird")
        {
            other.GetComponent<Bird>().DropItem();
        }
    }
    void Heal(int heal)
    {
        hp += heal;
        hp = Mathf.Clamp(hp, 0, 5);
        StartCoroutine(ShowHp());
    }
    public void AliceChangeSize()
    {
        if (size == Size.NORMAL)
        {
            powerJump = powerJumpSmall;
            size = Size.SMALL;
            transform.localScale = new Vector3(0.4f, 0.4f, 0.4f);
            distanceChrckPlatfrom = 0.2f;
        }
        else
        {
            powerJump = powerJumpNormal;
            size = Size.NORMAL;
            transform.localScale = new Vector3(1, 1, 1);
            distanceChrckPlatfrom = 0.33f;
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
    void EnemyHit()
    {
        anim.Play("Hit");
        StartCoroutine(Immortal_when_hit(30));
    }

    IEnumerator Immortal_when_hit(int immrotaltime)
    {
        immortal = true;
        for (int i = 0; i < immrotaltime; i++)
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
    IEnumerator BuffJump()
    {
        powerJump = 40;
        yield return new WaitForSeconds(5);
        powerJump = powerJumpNormal;
    }
    public static void GainHealAll()
    {
        HealAll?.Invoke();
    }
    void HealFull()
    {
        hp = 5;
        StartCoroutine(ShowHp());
    }

}

