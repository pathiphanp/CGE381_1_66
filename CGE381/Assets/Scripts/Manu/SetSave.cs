using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;


public class SetSave : MonoBehaviour
{
    [SerializeField] public int slotNum;
    [SerializeField] public SetSaveSlot setSaveSlot;
    [SerializeField] GameObject defaultname;
    [SerializeField] TMP_Text namePlayer;
    [SerializeField] SystemSlotName[] systemName;
    [SerializeField] public int indexName = 0;
    public GameObject bg_select;
    /*[HideInInspector]*/
    public bool selectSlot = false;
    bool slotEmty = true;

    private void OnEnable()
    {
        foreach (SystemSlotName s in systemName)
        {
            s.namePlayer = s.obNamePlayer.GetComponent<TMP_Text>();
        }
        namePlayer = defaultname.GetComponent<TMP_Text>();
    }

    // Start is called before the first frame update
    void Start()
    {
        if (SaveManager.Instance.namePlayer[slotNum] != "")
        {
            slotEmty = false;
            namePlayer.text = SaveManager.Instance.namePlayer[slotNum];
        }

    }
    // Update is called once per frame
    void Update()
    {
        if (selectSlot)
        {
            SelectSlot();
        }
    }
    void SelectSlot()
    {
        if (slotEmty)
        {
            defaultname.SetActive(false);
            foreach (SystemSlotName s in systemName)
            {
                s.obNamePlayer.SetActive(true);
            }
            SetName();
        }
        else
        {
            SaveManager.Instance.LoadGame(false, slotNum);
        }
    }
    void SetName()
    {
        if (Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.DownArrow))//SetName
        {
            systemName[indexName].setNamePlayer = (char)ArrowControl.Instance.SetSlotUpDown(systemName[indexName].setNamePlayer, 91);
            if (systemName[indexName].setNamePlayer == 91)
            {
                systemName[indexName].setNamePlayer = 'A';
            }
            else if (systemName[indexName].setNamePlayer == 64)
            {
                systemName[indexName].setNamePlayer = 'Z';
            }
        }
        if (Input.GetKeyDown(KeyCode.LeftArrow) || Input.GetKeyDown(KeyCode.RightArrow))//SelectNameSlot
        {
            indexName = ArrowControl.Instance.SetSlotSide(indexName, systemName.Length - 1);
            for (int i = 0; i < systemName.Length; i++)
            {
                if (indexName == i)
                {
                    systemName[i].arow.SetActive(true);
                }
                else if (indexName != i)
                {
                    systemName[i].arow.SetActive(false);
                }
            }
        }
        systemName[indexName].setNamePlayer = (Char)Mathf.Clamp(systemName[indexName].setNamePlayer, 'A', 'Z');
        systemName[indexName].namePlayer.text = systemName[indexName].setNamePlayer.ToString();//ChangeName
        StartCoroutine(SaveNameAndSlotSave());//NewGame

    }
    IEnumerator SaveNameAndSlotSave()
    {
        yield return new WaitForSeconds(0.5f);
        if (Input.GetKeyDown(KeyCode.X))
        {
            //Save and go to PlayGame(New Game)
            SaveManager.Instance.namePlayer[slotNum] = systemName[0].namePlayer.text + systemName[1].namePlayer.text + systemName[2].namePlayer.text;
            SaveManager.Instance.LoadGame(true, 0);
            SaveManager.Instance.numSave = slotNum;
        }
    }
    public void ResetSlot()
    {
        if (SaveManager.Instance.namePlayer[slotNum] == "")
        {
            foreach (SystemSlotName s in systemName)
            {
                Debug.Log("NameDe");
                s.setNamePlayer = 'A';
                s.namePlayer.text = "A";
                s.obNamePlayer.SetActive(false);
            }
            defaultname.SetActive(true);
        }
        selectSlot = false;
    }
}
