using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CallHomeFinishDial : Singleton<CallHomeFinishDial>
{

    //��prefab��UnityEvent������е���EventHandler�����õ�
    public void testingnow()
    {
        Debug.Log("now is testing");
        EventHandler.CallTesting();
        Debug.Log("Done");
    }

    /// <summary>
    /// �������Ĵ����¼�
    /// </summary>
    /// <param name="TextContain"></param>
    public void callLayOutGroupAddEvent(string TextContain)
    {
        EventHandler.CallLayOutToAddEvent(TextContain);
    }

    //��ѡ�����
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


    //��Ļ����
    public void CMSHAKE()
    {
        CameraControl.Instance.CMShake();
    }

    public void InstantiateLuckyPadding()
    {
        InstantiateSelectButtons.Instance.InstantiateLuckPadding();
    }

    //�Ի���������Ч
    public void InitSoundEffectInDial(string soundName)
    {
        var name = (SoundName)System.Enum.Parse(typeof(SoundName), soundName);
        EventHandler.CallInitSoundEffect(AudioManager.Instance.GetSoundDetailsfromName(name));
    }

}
