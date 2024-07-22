using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BedManager : MonoBehaviour
{
    public BedList_SO bedList_SO;
    public List<BedThingControl> bedThingControls;

    private void OnEnable()
    {
        EventHandler.AfterSceneLoad += OnInitBeds;
    }

    private void OnDisable()
    {
        EventHandler.AfterSceneLoad -= OnInitBeds;
    }

    private void OnInitBeds()
    {
        int daycount = DayManager.Instance.DayCount;
        for(int i = 0; i < bedThingControls.Count; i++)
        {
            bedThingControls[i].Init(bedList_SO.churchBedDetails[daycount-1]);
        }
    }
}
