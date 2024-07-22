using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Button_MapToRB : MonoBehaviour
{
    public int ActionPoint;

    public Image ButtonImage;

    public Sprite SafeMode;
    public Sprite WarMode;
    public Sprite EpdemicMode;

    private void Start()
    {
        int daycount = DayManager.Instance.DayCount;
        if (daycount < 6)
        {
            ButtonImage.sprite = SafeMode;
        }
        else if (daycount < 9)
        {
            ButtonImage.sprite = WarMode;
        }
        else
        {
            ButtonImage.sprite = EpdemicMode;
        }
    }

    public void callButton_MapToRB()
    {
        EventHandler.CallInitSoundEffect(AudioManager.Instance.GetSoundDetailsfromName(SoundName.ButtonClick));
        if (PlayerProperty.Instance.PlayerAction >= ActionPoint)
        {
            if (!PlayerInventory.Instance.isReachRB)
            {
                PlayerInventory.Instance.isReachRB = true;
                ActionPointControl.Instance.PointHUDControl(PlayerProperty.Instance.PlayerAction - ActionPoint);
                EventHandler.CallButton_MaptoRB();
            }
            else
            {
                string mtext = "�����Ѿ�ȥ�����л����ˡ���";
                MainCanvasUITip.Instance.UITipShowAndDisappear(mtext);
            }

        }
        else
        {
            //��ʾ���ж��㲻�㣡
            string mtext = "�ж��㲻�㣡";
            MainCanvasUITip.Instance.UITipShowAndDisappear(mtext);
        }


    }
}
