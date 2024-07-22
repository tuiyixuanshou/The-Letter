using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Button_YesIntoNight : MonoBehaviour
{

    public void InstantiateNightEvent()
    {
        DayManager.Instance.instantiateHomeEvent();
    }
    
}
