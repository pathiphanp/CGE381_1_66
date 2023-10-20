using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TextEffect : MonoBehaviour
{
    [SerializeField] TMP_Text textTest;
    [SerializeField] DescriptionData[] supText;
    int charC = 0;
    [SerializeField] float delayRead;
    [SerializeField] bool read = true;
    int datanewline;
    [SerializeField] int newline;


    // Start is called before the first frame update
    void Start()
    {
        RestRead();
        StartCoroutine(ReadText(supText[0].description[0]));
    }

    // Update is called once per frame
    void Update()
    {
        /*if (Input.GetMouseButtonDown(0))
        {
            /*textTest.text = "";
            datanewline = newline;
            for (int i = 0; i < supText[0].description[0].Length; i++)
            {
                charC++;
                string text = supText[0].description[0].Substring(i, 1);
                if (i == datanewline)
                {
                    datanewline += newline;
                    textTest.text += "\n";
                }

                textTest.text += text;
            }
            if (read)
            {
                read = false;
            }
            else if (!read)
            {
                RestRead();
            }
        }*/
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
