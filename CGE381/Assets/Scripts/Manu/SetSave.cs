using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetSave : MonoBehaviour
{
    [SerializeField] List<GameObject> slodSave = new List<GameObject>();
    [SerializeField] List<Setname> _slodSave = new List<Setname>();
    [HideInInspector] public int indexSave = 0;
    public bool firstRead = true;
    [HideInInspector] public bool selectSave = true;
    private void OnEnable()
    {
        for (int i = 0; i < transform.childCount; i++)
        {
            slodSave.Add(transform.GetChild(i).gameObject);
            _slodSave.Add(slodSave[i].GetComponent<Setname>());
            _slodSave[i].setSave = this;
        }
        _slodSave[0].bg_select.SetActive(true);
    }

    private void OnDisable()
    {
        RestSlot();
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
        else if (Input.GetKeyDown(KeyCode.Z) && !selectSave)
        {
            selectSave = true;
            foreach (Setname s in _slodSave)
            {
                s.ResetSlot();
                s.selectSlot = false;
            }
        }

        if (Input.GetKeyDown(KeyCode.LeftArrow) && selectSave || Input.GetKeyDown(KeyCode.RightArrow) && selectSave)
        {
            indexSave = ArrowControl.aC.SetSlotSide(indexSave, transform.childCount - 1);
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
        _slodSave.Clear();
        slodSave.Clear();
    }
}
