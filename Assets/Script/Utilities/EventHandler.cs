using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public  static class EventHandler
{
    public static event Action<InventoryType, List<BagItemDetails>> UpDateBagUI;
    public static void CallUpdateBagUI(InventoryType inventoryType,List<BagItemDetails> list)
    {
        UpDateBagUI?.Invoke(inventoryType, list);
    }

    public static event Action BeforeSceneUnLoad;
    public static void CallBeforeSceneLoad()
    {
        BeforeSceneUnLoad?.Invoke();
    }

    public static event Action AfterSceneLoad;
    public static void CallAfterSceneLoad()
    {
        AfterSceneLoad?.Invoke();
    }

    public static event Action AfterSceneLoadMove;
    public static void CallAfterSceneLoadMove()
    {
        AfterSceneLoadMove?.Invoke();
    }

    //��������,�����ı�
    public static event Action Button_AddDay;
    public static void CallButton_AddDay()
    {
        Button_AddDay?.Invoke();
    }

    //ÿ��̶��޸���������
    public static event Action Button_ChangePlayerPorperty;
    public static void CallButton_ChangePlayerPorperty()
    {
        Button_ChangePlayerPorperty?.Invoke();
    }

    //���жԻ�UI���ž���
    public static event Action<DialogueData> ShowSentenceInDialUI;
    public static void CallShowSentenceInDialUI(DialogueData dialData)
    {
        ShowSentenceInDialUI?.Invoke(dialData);
    }

    public static event Action ShowIfDialoguePanelActive;
    public static void CallShowIfDialoguePanelActive()
    {
        ShowIfDialoguePanelActive?.Invoke();
    }


    /// <summary>
    /// ������������¼�
    /// </summary>
    public static event Action<string> LayOutToAddEvent;
    public static void CallLayOutToAddEvent(string TextContain)
    {
        LayOutToAddEvent?.Invoke(TextContain);
    }


    //���̵����
    public static event Action TellInventoryUIToOpenShelves;
    public static void CallTellInventoryUIToOpenShelves()
    {
        TellInventoryUIToOpenShelves?.Invoke();
    }

    //�ر��̵����
    public static event Action TellInventoryUIToCloseShelves;
    public static void CallTellInventoryUIToCloseShelves()
    {
        TellInventoryUIToCloseShelves?.Invoke();
    }

    //�̵��ʱ�鿴��Ʒ�� �ر�ó�׿�
    public static event Action TellHomeUIToCloseTradeList;
    public static void CallTellHomeUIToCloseTradeList()
    {
        TellHomeUIToCloseTradeList?.Invoke();
    }

    //��ܵ�ʱ�����UI��
    public static event Action<BattleState> ClosePanelAndButtonWhenFight;
    public static void CallClosePanelAndButtonWhenFight(BattleState battleState)
    {
        ClosePanelAndButtonWhenFight?.Invoke(battleState);
    }


    public static event Action TellInventoryUIToOpenAB;
    public static void CallTellInventoryUIToOpenAB()
    {
        TellInventoryUIToOpenAB?.Invoke();
    }

    public static event Action TellInventoryUIToCloseAB;
    public static void CallTellInventoryUIToCloseAB()
    {
        TellInventoryUIToCloseAB?.Invoke();
    }

    //������Ч
    public static event Action<SoundDetails> InitSoundEffect;
    public static void CallInitSoundEffect(SoundDetails soundDetails)
    {
        InitSoundEffect?.Invoke(soundDetails);
    }

    public static event Action<SoundName> PlaySoundEvent;
    public static void CallPlaySoundEvent(SoundName soundName)
    {
        PlaySoundEvent?.Invoke(soundName);
    }

    public static event Action ShowNewInfoBookSign;
    public static void CallShowNewInfoBookSign()
    {
        ShowNewInfoBookSign?.Invoke();
    }

    //���������ȸ���
    public static Action StartChurchLadyThing;
    public static void CallStartChurchLadyThing()
    {
        StartChurchLadyThing?.Invoke();
    }

    //��Ϸ�ж��ο�������Ϸ�������
    public static Action NewGameEmptyData;
    public static void CallNewGameEmptyData()
    {
        NewGameEmptyData?.Invoke();
    }

    //0������ ��Ҳ�����Ǹ�ʲô���ˣ��Ȳ�ɾ ����û��
    //�Ի���������
    public static event Action<DialogueData> DialogueController;
    public static void CallDialogueController(DialogueData dialdata)
    {
        DialogueController?.Invoke(dialdata);
    }






    //����ת��Button
    public static event Action Button_HomeToMap;
    public static void CallHomeToMap()
    {
        Button_HomeToMap?.Invoke();
    }

    public static event Action Button_MaptoHome;
    public static void CallButton_MaptoHome()
    {
        Button_MaptoHome?.Invoke();
    }

    public static event Action Button_MaptoPark;
    public static void CallButton_MaptoPark()
    {
        Button_MaptoPark?.Invoke();
    }

    public static event Action Button_MaptoRB;
    public static void CallButton_MaptoRB()
    {
        Button_MaptoRB?.Invoke();
    }

    public static event Action Button_ToIntroduceScene;
    public static void CallButton_ToIntroduceScene()
    {
        Button_ToIntroduceScene?.Invoke();
    }

    public static event Action Button_ToChurch;
    public static void CallButton_ToChurch()
    {
        Button_ToChurch?.Invoke();
    }

    public static event Action Button_ToWarY;
    public static void CallButton_ToWarY()
    {
        Button_ToWarY?.Invoke();
    }

    public static event Action Button_ToENDGame;
    public static void CallButton_ToENDGame()
    {
        Button_ToENDGame?.Invoke();
    }

    public static event Action Button_ToStart;
    public static void CallButton_ToStart()
    {
        Button_ToStart?.Invoke();
    }

    public static event Action TestingEventLay;
    public static void CallTesting()
    {
        TestingEventLay?.Invoke();
    }




     
}
