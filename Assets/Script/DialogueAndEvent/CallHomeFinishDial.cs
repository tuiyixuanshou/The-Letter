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
        EventHandler.CallPlaySoundEvent(SoundName.ShiXiangGet);
    }

    /// <summary>
    /// 标记已经完成的线索，留下接口待使用
    /// </summary>
    public void callInfoCollectionSetInfo(int InfoID)
    {
        EventHandler.CallPlaySoundEvent(SoundName.ShiXiangGet);
        InfoManager.Instance.MarkInfoFind(InfoID);
        EventHandler.CallShowNewInfoBookSign();
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


    //待修改：可以使用传入的参数
    public void HomeEventInstantiateSelectButton(int HEID)
    {
        InstantiateSelectButtons.Instance.InstantiateHomeEventButton(HEID);
        //foreach(var i in FindObjectsOfType<HomeEventUI>())
        //{
        //    if(i.HomeEventID == 5002)
        //    {
        //        InstantiateSelectButtons.Instance.InstantiateHomeEventButton(5002);
        //    }
        //    //else if(i.HomeEventID == 5005)
        //}
    }

    //改变HomeEvent
    public void ChangeHomeEvent(int HEID)
    {
        foreach (var j in FindObjectsOfType<HomeEventUI>())
        {
            if(j.StartTalk == true)
            {
                j.StartTalk = false;
                EventHandler.CallShowSentenceInDialUI(null);
                j.Init(HEID);
            }
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

    //外景NPC的选择题
    public void OutNpcInstantiateSelectButton(int NPCID)
    {
        InstantiateSelectButtons.Instance.InstantiateOutNPCButton(NPCID);
    }
    //6004随机给东西,此时6004已经变成了6005
    public void Give6004Item()
    {
        InstantiateSelectButtons.Instance.InstantiateRandomGiveThing(3,6005);
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

    //逃跑成功后用于重置时间和删除敌人
    public void DestroyEnemyAfterRun()
    {
        EventHandler.CallShowSentenceInDialUI(null);
        BattleSystem.Instance.ReStartTime();
    }

    //对话中生成音效
    public void InitSoundEffectInDial(string soundName)
    {
        var name = (SoundName)System.Enum.Parse(typeof(SoundName), soundName);
        //EventHandler.CallInitSoundEffect(AudioManager.Instance.GetSoundDetailsfromName(name));
        EventHandler.CallPlaySoundEvent(name);
    }


    //对话中转变音乐
    public void ChangeBgm(string musicName)
    {
        AudioManager.Instance.ChangeBGMInDial(musicName);
    }

    //对话中转变坏境音



    //暂时使用introdueceScene改变cg
    public void ChangeCG(GameObject NextCG)
    {
        NextCG.SetActive(true);
    }

    //开始教堂赶人事件
    public void StartChurchLadyEvent()
    {
        EventHandler.CallStartChurchLadyThing();
    }
    //玩家被迫传送到教堂外
    public void TransformToPark()
    {
        PlayerInventory.Instance.isStayInChurchOutDoor = true;
        EventHandler.CallButton_MaptoPark();
    }

    //游戏结局时重新进入游戏开始界面
    public void TransformToStart()
    {
        EventHandler.CallButton_ToStart();
    }

}
