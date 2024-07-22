using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Button_MapToWarYield : MonoBehaviour
{
    public int ActionPoint;
    public Image ButtonImage;

    public Sprite SafeMode;
    public Sprite WarMode;
    public Sprite EpdemicMode;

    private void Start()
    {
        int daycount = DayManager.Instance.DayCount;
        if (daycount < 4)
        {
            ButtonImage.sprite = SafeMode;
        }
        else if (daycount < 10)
        {
            ButtonImage.sprite = WarMode;
        }
        else
        {
            ButtonImage.sprite = EpdemicMode;
        }
    }



    public void callButton_MapToWarYield()
    {
        EventHandler.CallInitSoundEffect(AudioManager.Instance.GetSoundDetailsfromName(SoundName.ButtonClick));
        if (PlayerProperty.Instance.PlayerAction >= ActionPoint)
        {
            if (!PlayerInventory.Instance.isReachWY)
            {
                PlayerInventory.Instance.isReachWY = true;
                ActionPointControl.Instance.PointHUDControl(PlayerProperty.Instance.PlayerAction - ActionPoint);
                EventHandler.CallButton_ToWarY();
            }
            else
            {
                string mtext = "今天已经去过战乱荒地了……";
                MainCanvasUITip.Instance.UITipShowAndDisappear(mtext);
            }

        }
        else
        {
            //提示：行动点不足！
            string mtext = "行动点不足！";
            MainCanvasUITip.Instance.UITipShowAndDisappear(mtext);
        }

    }


}
