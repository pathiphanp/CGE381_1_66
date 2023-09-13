using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TextEffect : MonoBehaviour
{
    [SerializeField] TMP_Text textTest;
    [SerializeField] string[] supText;
    int charC = 0;
    [SerializeField] float delayRead;
    [SerializeField] bool read = true;

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (read)
            {
                read = false;
                RestRead();
                StartCoroutine(ReadText(supText[0]));
            }
            else if (!read)
            {
                RestRead();
            }
        }
    }

    IEnumerator ReadText(string suptitle)
    {
        while (charC < suptitle.Length)
        {
            yield return new WaitForSeconds(delayRead);
            charC++;
            string text = suptitle.Substring(0, charC);
            textTest.text = text;
        }
        read = true;
    }

    void RestRead()
    {
        textTest.text = "";
        charC = 0;
    }
}
