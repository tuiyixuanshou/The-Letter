using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FogChangeScene : MonoBehaviour
{
    public int index;


    //fog����������֮����¼�
    public void AfterButtonPressedEvent()
    {
        Debug.Log(index);
        //int index = this.transform.GetSiblingIndex();
        switch (index)
        {
            //��ʼ��Ϸ
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
        //NPC(�⾰NPC,����NPC,����NPC)���£����¼����£��������£���������,����״̬����,���Manager����,���CG����
        EventHandler.CallNewGameEmptyData();
        //EventHandler.CallButton_MaptoHome();
        //EventHandler.CallButton_MaptoHome();
        EventHandler.CallButton_ToIntroduceScene();
        //��������
       
    }
}
