using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CallHomeFinishDial : Singleton<CallHomeFinishDial>
{

    //给prefab在UnityEvent里面呼叫调用EventHandler函数用的
    public void testingnow()
    {
        Debug.Log("now is testing");
        EventHandler.CallTesting();
        Debug.Log("Done");
    }

    /// <summary>
    /// 添加事项的代办事件
    /// </summary>
    /// <param name="TextContain"></param>
    public void callLayOutGroupAddEvent(string TextContain)
    {
        EventHandler.CallLayOutToAddEvent(TextContain);
    }

    //打开选择面板
    public void ShowSelectPanel()
    {
        InstantiateSelectButtons.Instance.ShowSelectPanel();
    }

    public void InstantiateSelectButton()
    {
        foreach(var i in FindObjectsOfType<CustomerController>())
        {
            if (i.isFadeShow)
            {
                InstantiateSelectButtons.Instance.instantiateShopButtons(i.CustomerID, i.DialogueIndex);
                break;
            } 
        }
    }

    public void ShopDialogueFinish()
    {
        foreach (var i in FindObjectsOfType<CustomerController>())
        {
            if (i.isFadeShow)
            {
                i.CloseShopDialogue();
                break;
            }
        }
    }


    public void HomeEventInstantiateSelectButton()
    {
        foreach(var i in FindObjectsOfType<HomeEventUI>())
        {
            if(i.HomeEventID == 5002)
            {
                InstantiateSelectButtons.Instance.InstantiateHomeEventButton(5002);
            }
            //else if(i.HomeEventID == 5005)
        }
    }

    public void HomeEventInstantiateOtherEventButton()
    {
        DayManager.Instance.instantiateHomeEventInMorning();
    }
    

    public void IfBattleInstantiateSelectButton()
    {
        //var getEnemy = GameObject.FindWithTag("Player").GetComponent<Player>().enemy;
        var getEnemy = BattleSystem.Instance.enemy;
        InstantiateSelectButtons.Instance.InstantiateIfBattleButton(getEnemy.enemyDetails);
    }

    public void ShowBattleToFight()
    {
        //TheBattle.Instance.StartBattle();
    }


    //屏幕抖动
    public void CMSHAKE()
    {
        CameraControl.Instance.CMShake();
    }

    public void InstantiateLuckyPadding()
    {
        InstantiateSelectButtons.Instance.InstantiateLuckPadding();
    }

    //对话中生成音效
    public void InitSoundEffectInDial(string soundName)
    {
        var name = (SoundName)System.Enum.Parse(typeof(SoundName), soundName);
        EventHandler.CallInitSoundEffect(AudioManager.Instance.GetSoundDetailsfromName(name));
    }

}
