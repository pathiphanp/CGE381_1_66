using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TagEnemy : MonoBehaviour
{
    public GameObject icon;
    public GameObject clampicon;
    public GameObject enemy;
    public float speed;
    public float timeDuration;
    public bool onTag;
    public GameObject clampXLeft;
    public GameObject clampXRight;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        IconMove();
    }
    void IconMove()
    {
        if (onTag)
        {
            if (icon.transform.position.x != enemy.transform.position.x)
            {
                speed += Time.deltaTime;
                float precentcomplete = speed / timeDuration;
                float _xDirection = Mathf.Lerp(icon.transform.position.x, enemy.transform.position.x, precentcomplete);
                _xDirection = Mathf.Clamp(_xDirection, clampXLeft.transform.position.x, clampXRight.transform.position.x);
                icon.transform.position = new Vector3(_xDirection, clampicon.transform.position.y, icon.transform.position.z);
            }
        }

    }

    public void SetTag(GameObject enemy)
    {
        this.enemy = enemy;
        icon.SetActive(true);
        speed = 0;
        onTag = true;
    }
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Enemy")
        {
            icon.SetActive(false);
            onTag = false;
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.tag == "Enemy")
        {
            Destroy(other.gameObject);
        }
    }
}
