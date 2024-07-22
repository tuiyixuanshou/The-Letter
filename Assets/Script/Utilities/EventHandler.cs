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


    public static event Action ClosePanelAndButtonWhenFight;
    public static void CallClosePanelAndButtonWhenFight()
    {
        ClosePanelAndButtonWhenFight?.Invoke();
    }


    //������Ч
    public static event Action<SoundDetails> InitSoundEffect;
    public static void CallInitSoundEffect(SoundDetails soundDetails)
    {
        InitSoundEffect?.Invoke(soundDetails);
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

    public static event Action Button_ToENDGame;
    public static void CallButton_ToENDGame()
    {
        Button_ToENDGame?.Invoke();
    }

    public static event Action TestingEventLay;
    public static void CallTesting()
    {
        TestingEventLay?.Invoke();
    }


     
}
