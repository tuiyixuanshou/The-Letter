using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Button_IntoNight : MonoBehaviour
{
    public GameObject EventUnDonePanel;
    public CanvasGroup AskIntoNight;
    public void Button_IfToNight()
    {
        EventHandler.CallInitSoundEffect(AudioManager.Instance.GetSoundDetailsfromName(SoundName.ButtonClick));
        foreach (var i in FindObjectsOfType<HomeEventUI>())
        {
            if (!HomeEventManager.Instance.CheckIfEventDone(i.HomeEventID)&&i.HomeEventID != 5002 && i.HomeEventID != 5026)
            {
                //不出门也可以
                EventUnDonePanel.SetActive(true);
                return;
            }
        }
        AskIntoNight.alpha = 1;
        AskIntoNight.blocksRaycasts = true;
        //删除内容在yesintonight里面
        //foreach (var i in FindObjectsOfType<HomeEventUI>())
        //{
        //    Destroy(i.gameObject);
        //}
    }

    public void Button_Fake()
    {
        EndManager.Instance.HealthNone = true;
        EventHandler.CallButton_ToENDGame();
    }
}
