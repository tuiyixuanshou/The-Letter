using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Button_ConsumePanel : MonoBehaviour
{
    public void Button_Continue()
    {

        EventHandler.CallInitSoundEffect(AudioManager.Instance.GetSoundDetailsfromName(SoundName.ButtonClick));
        if (PlayerProperty.Instance.PlayerHealth <= 0)
        {
            EndManager.Instance.HealthNone = true;
            EventHandler.CallButton_ToENDGame();
        }
        StartHomeEvent();
    }

    //public void Button_Fake()
    //{
    //    EventHandler.CallButton_ToENDGame();
    //}

    public void StartHomeEvent()
    {
        Debug.Log(DayManager.Instance.DayCount);
        int dayCount = DayManager.Instance.DayCount;
        if (dayCount == 3)
        {
            Debug.Log("do this");
            foreach(var i in FindObjectsOfType<HomeEventUI>())
            {
                Debug.Log("do this1");
                if (i.HomeEventID == 5009)
                {
                    Debug.Log("do this2");
                    i.ButtonPressed();
                }
                else if(i.HomeEventID == 5012)
                {
                    i.ButtonPressed();
                }
            }
        }
        else if (dayCount == 6)
        {
            foreach (var i in FindObjectsOfType<HomeEventUI>())
            {
                if (i.HomeEventID == 5027)
                {
                    i.ButtonPressed();
                }
              
            }
        }
        else if (dayCount == 12)
        {
            foreach (var i in FindObjectsOfType<HomeEventUI>())
            {
                if (i.HomeEventID == 5042)
                {
                    i.ButtonPressed();
                }

            }
        }
        //第一个demo包：暂时更新到第四天
        //else if(dayCount == 6)
        //{
        //    EventHandler.CallButton_ToENDGame();
        //}
    }

}
