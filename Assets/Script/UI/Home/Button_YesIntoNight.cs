using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Button_YesIntoNight : MonoBehaviour
{
    //夜晚生成事件的按钮
    public void InstantiateNightEvent()
    {
        EventHandler.CallInitSoundEffect(AudioManager.Instance.GetSoundDetailsfromName(SoundName.ButtonClick));
        foreach (var i in FindObjectsOfType<HomeEventUI>())
        {
            Destroy(i.gameObject);
        }
        DayManager.Instance.instantiateHomeEvent();
    }
    
}
