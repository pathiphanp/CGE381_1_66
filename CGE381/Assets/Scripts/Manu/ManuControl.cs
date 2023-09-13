using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ManuControl : MonoBehaviour
{
    [SerializeField] GameObject bg_Start;
    [SerializeField] GameObject manu;
    [SerializeField] TMP_Text[] modeText;
    int indexBtnManu = 0;
    [SerializeField] BtnDataSystem[] btn;

    public SetSave setSave;
    private void OnEnable()
    {
        btn[0].manuBtn.animator.Play("Highlighted");
        foreach (BtnDataSystem b in btn)
        {
            b.manu.SetActive(false);
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        RestMaun();
    }

    // Update is called once per frame
    void Update()
    {
        SetManu();
        EnterToManu();
        if (UnityEngine.Input.GetKeyDown(KeyCode.Escape))
        {
            Application.Quit();
        }
    }

    void EnterToManu()
    {
        if (bg_Start.active)
        {
            if (Input.GetKeyDown(KeyCode.Return))
            {
                bg_Start.SetActive(false);
                manu.SetActive(true);
                foreach (TMP_Text m in modeText)
                {
                    m.text = ModeGame.EASY.ToString();
                }
            }
        }
        if (Input.GetKeyDown(KeyCode.X) && setSave.selectSave && !bg_Start.active)
        {
            loopManu();
            manu.SetActive(false);

        }
        else if (Input.GetKeyDown(KeyCode.Z) && setSave.selectSave)
        {
            RestMaun();
        }
    }

    void SetManu()
    {
        if (Input.GetKeyDown(KeyCode.UpArrow) && setSave.selectSave || Input.GetKeyDown(KeyCode.DownArrow) && setSave.selectSave)
        {
            indexBtnManu = ArrowControl.aC.SetSlotUpDown(indexBtnManu, btn.Length - 1);
            loopBtn();
        }
        if (btn[1].manu.active)
        {
            ChangeMode();
        }


    }

    public void ChangeMode()
    {
        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            foreach (TMP_Text m in modeText)
            {
                m.text = ModeGame.EASY.ToString();
            }
            Debug.Log(ModeGame.EASY.ToString());
        }
        else if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            Debug.Log(ModeGame.HARD.ToString());
            foreach (TMP_Text m in modeText)
            {
                m.text = ModeGame.HARD.ToString();
            }
        }
        if (Input.GetKeyDown(KeyCode.X))
        {
            RestMaun();
        }
    }

    void RestMaun()
    {
        bg_Start.SetActive(true);
        manu.SetActive(false);
        foreach (BtnDataSystem b in btn)
        {
            b.manu.SetActive(false);
        }
        indexBtnManu = 0;
    }

    void loopManu()
    {
        for (int i = 0; i < btn.Length; i++)
        {
            if (indexBtnManu == i)
            {
                btn[i].manu.SetActive(true);
            }
            else
            {
                btn[i].manu.SetActive(false);
            }
        }
        manu.SetActive(false);
    }

    void loopBtn()
    {
        for (int i = 0; i < btn.Length; i++)
        {
            if (indexBtnManu == i)
            {
                btn[i].manuBtn.animator.Play("Highlighted");
            }
            else
            {
                btn[i].manuBtn.animator.Play("Normal");
            }
        }
    }

}
