using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndManager : Singleton<EndManager>
{
    public bool HealthNone;
    public bool SanNone;
    public bool WealthNone;

    [Header("½á¾Ö")]
    public bool End1;
    public bool End3;
    public bool End2;
    public void resetEnd()
    {
        HealthNone = false;
        SanNone = false;
        WealthNone = false;
        End1 = false;
        End2 = false;
        End3 = false;
    }
    private void OnEnable()
    {
        EventHandler.NewGameEmptyData += EmptyOriData;
    }

    private void OnDisable()
    {
        EventHandler.NewGameEmptyData -= EmptyOriData;
    }

    private void EmptyOriData()
    {
        resetEnd();
    }

    private void Start()
    {
        resetEnd();
    }
}
