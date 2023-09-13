using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TextEffect : MonoBehaviour
{
    [SerializeField] TMP_Text textTest;
    [SerializeField] string supText;
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
                StartCoroutine(ReadText());
            }
            else if (!read)
            {
                RestRead();
            }
        }
    }

    IEnumerator ReadText()
    {
        while (charC < supText.Length)
        {
            yield return new WaitForSeconds(delayRead);
            charC++;
            string text = supText.Substring(0, charC);

            //text += "<color=#00000000>" + supText.Substring(charC) + "</color>";
            textTest.text = text;
        }
        RestRead();
    }

    void RestRead()
    {
        textTest.text = "";
        read = true;
        charC = 0;
    }
}
