using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetSaveSlot : MonoBehaviour
{
    [SerializeField] List<GameObject> slodSave = new List<GameObject>();
    [SerializeField] List<SetSave> _slodSave = new List<SetSave>();
    [HideInInspector] public int indexSave = 0;
    public bool firstRead = true;
    [HideInInspector] public bool selectSave = true;
    [SerializeField] bool resetAllSave;
    private void OnEnable()
    {
        RestSlot();
    }

    private void OnDisable()
    {

    }
    void Start()
    {

    }
    private void Update()
    {
        SelectSave();
    }

    void SelectSave()
    {
        if (Input.GetKeyDown(KeyCode.X) && selectSave)
        {
            selectSave = false;
            _slodSave[indexSave].selectSlot = true;
        }
        if (Input.GetKeyDown(KeyCode.Z) && !selectSave)
        {
            selectSave = true;
            foreach (SetSave s in _slodSave)
            {
                s.ResetSlot();
                s.bg_select.SetActive(false);
            }
        }

        if (Input.GetKeyDown(KeyCode.LeftArrow) && selectSave || Input.GetKeyDown(KeyCode.RightArrow) && selectSave)
        {
            indexSave = ArrowControl.Instance.SetSlotSide(indexSave, transform.childCount - 1);
            for (int i = 0; i < _slodSave.Count; i++)
            {
                if (indexSave == i)
                {
                    _slodSave[i].bg_select.SetActive(true);
                }
                else
                {
                    _slodSave[i].bg_select.SetActive(false);
                }
            }
        }
    }

    void RestSlot()
    {
        if (slodSave.Count == 0)
        {
            for (int i = 0; i < transform.childCount; i++)
            {
                slodSave.Add(transform.GetChild(i).gameObject);
                _slodSave.Add(slodSave[i].GetComponent<SetSave>());
                _slodSave[i].setSaveSlot = this;
            }
        }
        foreach (SetSave s in _slodSave)
        {
            s.ResetSlot();
            s.bg_select.SetActive(false);
        }
        indexSave = 0;
        _slodSave[0].bg_select.SetActive(true);
    }
}
