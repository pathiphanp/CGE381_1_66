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
    #region StarAndMap
    [HideInInspector] public int star;
    #endregion
    [SerializeField] public int hp;
    [SerializeField] float speed;
    Rigidbody2D rb;
    [Space][SerializeField] PlayerInputActions playerInputAction;

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
        //star = SaveManager.Instance.star[SaveManager.Instance.numSave];
        rb = this.gameObject.GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {

    }
    public void MovePlayer(InputAction.CallbackContext context)
    {

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
        Debug.Log("Move");
        Vector2 inputVector = context.ReadValue<Vector2>();
        rb.velocity = new Vector2(inputVector.x * speed, rb.velocity.y);
    }
}
