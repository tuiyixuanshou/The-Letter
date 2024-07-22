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
        EventHandler.CallPlaySoundEvent(SoundName.ShiXiangGet);
    }

    /// <summary>
    /// ����Ѿ���ɵ����������½ӿڴ�ʹ��
    /// </summary>
    public void callInfoCollectionSetInfo(int InfoID)
    {
        EventHandler.CallPlaySoundEvent(SoundName.ShiXiangGet);
        InfoManager.Instance.MarkInfoFind(InfoID);
        EventHandler.CallShowNewInfoBookSign();
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


    //���޸ģ�����ʹ�ô���Ĳ���
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

    //�ı�HomeEvent
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

    //�⾰NPC��ѡ����
    public void OutNpcInstantiateSelectButton(int NPCID)
    {
        InstantiateSelectButtons.Instance.InstantiateOutNPCButton(NPCID);
    }
    //6004���������,��ʱ6004�Ѿ������6005
    public void Give6004Item()
    {
        InstantiateSelectButtons.Instance.InstantiateRandomGiveThing(3,6005);
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

    //���ܳɹ�����������ʱ���ɾ������
    public void DestroyEnemyAfterRun()
    {
        EventHandler.CallShowSentenceInDialUI(null);
        BattleSystem.Instance.ReStartTime();
    }

    //�Ի���������Ч
    public void InitSoundEffectInDial(string soundName)
    {
        var name = (SoundName)System.Enum.Parse(typeof(SoundName), soundName);
        //EventHandler.CallInitSoundEffect(AudioManager.Instance.GetSoundDetailsfromName(name));
        EventHandler.CallPlaySoundEvent(name);
    }


    //�Ի���ת������
    public void ChangeBgm(string musicName)
    {
        AudioManager.Instance.ChangeBGMInDial(musicName);
    }

    //�Ի���ת�仵����



    //��ʱʹ��introdueceScene�ı�cg
    public void ChangeCG(GameObject NextCG)
    {
        NextCG.SetActive(true);
    }

    //��ʼ���ø����¼�
    public void StartChurchLadyEvent()
    {
        EventHandler.CallStartChurchLadyThing();
    }
    //��ұ��ȴ��͵�������
    public void TransformToPark()
    {
        PlayerInventory.Instance.isStayInChurchOutDoor = true;
        EventHandler.CallButton_MaptoPark();
    }

    //��Ϸ���ʱ���½�����Ϸ��ʼ����
    public void TransformToStart()
    {
        EventHandler.CallButton_ToStart();
    }

}
