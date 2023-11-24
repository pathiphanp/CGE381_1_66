using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookFollow : MonoBehaviour
{
    [Header("Eye Left")]
    [SerializeField] GameObject eyeLeft;
    [SerializeField] float clampMinEyeLeft;
    [SerializeField] float clampMaxEyeLeft;
    [Header("Eye Right")]
    [SerializeField] GameObject eyeRight;
    [SerializeField] float clampMinEyeRight;
    [SerializeField] float clampMaxEyeRight;
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
            float _eyeLeft = Mathf.Lerp(eyeLeft.transform.localPosition.x, target, Time.deltaTime * duration);
            _eyeLeft = Mathf.Clamp(_eyeLeft, clampMinEyeLeft, clampMaxEyeLeft);
            eyeLeft.transform.localPosition = new Vector3(_eyeLeft, eyeLeft.transform.localPosition.y,
            eyeLeft.transform.localPosition.z);
            //Eye Right look
            float _eyeRight = Mathf.Lerp(eyeRight.transform.localPosition.x, target, Time.deltaTime * duration);
            _eyeRight = Mathf.Clamp(_eyeRight, clampMinEyeRight, clampMaxEyeRight);
            eyeRight.transform.localPosition = new Vector3(_eyeRight, eyeRight.transform.localPosition.y,
            eyeRight.transform.localPosition.z);
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
