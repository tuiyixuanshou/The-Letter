using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Button_MapToParkSafe : MonoBehaviour
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



    public void callButton_MapToPark()
    {
        EventHandler.CallInitSoundEffect(AudioManager.Instance.GetSoundDetailsfromName(SoundName.ButtonClick));
        if (PlayerProperty.Instance.PlayerAction >= ActionPoint)
        {
            if (!PlayerInventory.Instance.isReachPark)
            {
                PlayerInventory.Instance.isReachPark = true;
                ActionPointControl.Instance.PointHUDControl(PlayerProperty.Instance.PlayerAction - ActionPoint);
                EventHandler.CallButton_MaptoPark();
            }
            else
            {
                string mtext = "�����Ѿ�ȥ�����й㳡�ˡ���";
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
