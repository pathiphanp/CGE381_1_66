using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowControl : Singletons<ArrowControl>
{
    public int SetSlotSide(int index, int clampMax)
    {
        SoundManager.Instance.PlaySfx("ArrowMove");
        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            index--;
        }
        else if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            index++;
        }
        return index = Mathf.Clamp(index, 0, clampMax);
    }

    public int SetSlotUpDown(int index, int clampMax)
    {
        SoundManager.Instance.PlaySfx("ArrowMove");
        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            index--;
        }
        else if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            index++;
        }
        return index = Mathf.Clamp(index, 0, clampMax);
    }
}
