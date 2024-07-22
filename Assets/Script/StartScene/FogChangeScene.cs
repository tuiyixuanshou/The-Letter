using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FogChangeScene : MonoBehaviour
{
    public int index;


    //fog动画播放完之后的事件
    public void AfterButtonPressedEvent()
    {
        Debug.Log(index);
        //int index = this.transform.GetSiblingIndex();
        switch (index)
        {
            //开始游戏
            case 0:
                PressStartGame();
                break;
            case 1:
                break;
            case 2:
                Application.Quit();
                break;
            default:
                break;
        }
    }


    private void PressStartGame()
    {
        DayManager.Instance.DayCount = 1;
        DayUI.Instance.UpdateTextCount();
        //NPC(外景NPC,线索NPC,黑市NPC)更新，家事件更新，背包更新，线索更新,人物状态更新,结局Manager更新,结局CG重置
        EventHandler.CallNewGameEmptyData();
        //EventHandler.CallButton_MaptoHome();
        //EventHandler.CallButton_MaptoHome();
        EventHandler.CallButton_ToIntroduceScene();
        //天数更新
       
    }
}
