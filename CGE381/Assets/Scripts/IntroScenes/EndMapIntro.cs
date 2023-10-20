using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndMapIntro : MonoBehaviour
{
    [SerializeField] GameObject cameraCutScenes;
    void OnEnable()
    {

    }
    void OnDisable()
    {

    }
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            Gamemanager.ChangeUIMode();
            cameraCutScenes.SetActive(true);
        }
    }
}
