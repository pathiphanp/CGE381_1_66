using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class DoorChecks : MonoBehaviour
{
    Animator anim;
    [SerializeField] bool endMap;
    [SerializeField] GameObject cutScenes;
    Collider2D doorCollider;
    void Start()
    {
        anim = GetComponent<Animator>();
        doorCollider = GetComponent<Collider2D>();
    }
    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.collider.gameObject.tag == "Player")
        {
            Player player = other.collider.GetComponent<Player>();
            if (player.key > 0)
            {
                player.key--;
                player.keyItem.SetActive(false);
                if (endMap)
                {
                    SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
                }
                else
                {
                    doorCollider.enabled = false;
                    OpenDoor();
                }
            }
            else
            {
                cutScenes.SetActive(true);
                //CutScenes
            }
        }
    }
    void OpenDoor()
    {
        //playdoor open
        anim.Play("Door");
    }
}
