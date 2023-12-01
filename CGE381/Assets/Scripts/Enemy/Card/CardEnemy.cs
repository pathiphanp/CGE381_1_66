using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardEnemy : MonoBehaviour
{
    [SerializeField] GameObject start;
    [SerializeField] GameObject end;
    [SerializeField] float targetY;

    float speed;
    [SerializeField] float duration;

    [SerializeField] float delayAtk;

    [SerializeField] bool canAtk = true;

    [SerializeField] GameObject weapon;

    bool atk = true;
    AudioSource sfxSource;

    void Start()
    {
        sfxSource = GetComponent<AudioSource>();
        canAtk = true;
        targetY = end.transform.localPosition.y;
    }
    void Update()
    {
        Atk();
    }
    void Atk()
    {
        speed += Time.deltaTime;
        float percencomplete = speed / duration;
        float moveY = Mathf.Lerp(weapon.transform.localPosition.y,
        targetY, percencomplete);
        weapon.transform.localPosition = new Vector3(weapon.transform.localPosition.x,
        moveY, weapon.transform.localPosition.z);
        if (weapon.transform.localPosition.y == targetY && canAtk)
        {
            canAtk = false;
            if (weapon.transform.localPosition.y == end.transform.localPosition.y)
            {
                targetY = start.transform.localPosition.y;
                speed = 0;
                canAtk = true;
            }
            else
            {
                StartCoroutine(DelayAtk());
            }
        }
    }
    IEnumerator DelayAtk()
    {
        yield return new WaitForSeconds(delayAtk);
        targetY = end.transform.localPosition.y;
        sfxSource.PlayOneShot(SoundManager.Instance.SearchSfx("CardAtk"));
        speed = 0;
        canAtk = true;
    }


}
