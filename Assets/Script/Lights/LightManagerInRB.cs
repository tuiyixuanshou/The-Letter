using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightManagerInRB : MonoBehaviour
{
    public LightControl[] lightControlListInRB;

    private void Start()
    {
        SwichLight(false);
    }

    public void SwichLight(bool isOn)
    {
        int daycount = DayManager.Instance.DayCount;
        foreach (var lc in lightControlListInRB)
        {
            lc.isOn = isOn;
        }
        if(daycount < 3)
        {
            lightControlListInRB[9].isOn = false;
        }
    }

}
