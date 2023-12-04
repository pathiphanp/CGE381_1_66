using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookFollow : MonoBehaviour
{
    [Header("Eye")]
    [SerializeField] GameObject eye;
    [SerializeField] float clampMinEye;
    [SerializeField] float clampMaxEye;
    [Header("Speed Look")]
    [SerializeField] float duration;
    float speed;

    [Header("Player Target")]
    [SerializeField] GameObject player;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        EyeControl();
    }
    void EyeControl()
    {
        if (player != null)
        {
            float target = player.transform.position.x;//Target Look
            //Eye Left look
            float _eye = Mathf.Lerp(eye.transform.localPosition.x, target, Time.deltaTime * duration);
            _eye = Mathf.Clamp(_eye, clampMinEye, clampMaxEye);
            eye.transform.localPosition = new Vector3(_eye, eye.transform.localPosition.y,
            eye.transform.localPosition.z);
        }

    }
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            player = other.gameObject;
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            player = null;
        }
    }
}
