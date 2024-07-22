using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Button_MapToChurch : MonoBehaviour
{
    public int ActionPoint;
    public GameObject AskPanel;
    public void callButton_MapToChurch()
    {
        EventHandler.CallInitSoundEffect(AudioManager.Instance.GetSoundDetailsfromName(SoundName.ButtonClick));
        if (SceneManager.GetActiveScene().name == "SafetyArea" && PlayerInventory.Instance.isReachChurch == false)
        {
            PlayerInventory.Instance.isReachChurch = true;
            EventHandler.CallButton_ToChurch();
        }
        else if(PlayerInventory.Instance.isReachChurch == true)
        {
            AskPanel.SetActive(false);
            string mtext = "�����Ѿ�ȥ�������ˡ���";
            MainCanvasUITip.Instance.UITipShowAndDisappear(mtext);
        }
        //else if(SceneManager.GetActiveScene().name == "Map")
        //{
        //    if (PlayerProperty.Instance.PlayerAction >= ActionPoint)
        //    {
        //        ActionPointControl.Instance.PointHUDControl(PlayerProperty.Instance.PlayerAction - ActionPoint);
        //        EventHandler.CallButton_ToChurch();
        //    }
        //    else
        //    {
        //        //��ʾ���ж��㲻�㣡
        //    }
        //}

    }


}
