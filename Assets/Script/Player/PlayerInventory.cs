using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInventory : Singleton<PlayerInventory>
{
    [Header("����")]
    public bool isReachChurch;
    public bool isStayInChurchOutDoor;

    [Header("��ͼ")]
    public bool isReachPark;
    public bool isReachRB;
    public bool isReachWY;
    //public int WealthLackDay=0;

    private void OnEnable()
    {
        EventHandler.Button_AddDay += UpdateChurchReach;
    }

    private void OnDisable()
    {
        EventHandler.Button_AddDay -= UpdateChurchReach;
    }

    private void UpdateChurchReach()
    {
        isReachChurch = false;
        isReachPark = false;
        isReachRB = false;
        isReachWY = false;
    }

    ////ÿ�չ̶����ı仯ֵ
    //private void ChangePPtheNextDay()
    //{
    //    int playerW = PlayerProperty.Instance.PlayerWealth;
    //    int playerH = PlayerProperty.Instance.PlayerHealth;

    //    if (playerW > Settings.DailyMinusWealth-1)
    //    {
    //        WealthLackDay = 0;
    //        PlayerProperty.Instance.PlayerWealth = playerW - Settings.DailyMinusWealth;
    //        PlayerProperty.Instance.PlayerHealth = Settings.OrigPlayerHealth;
    //    }
    //    else if(playerW < Settings.DailyMinusWealth)
    //    {
    //        PlayerProperty.Instance.PlayerWealth = 0;
    //        if(WealthLackDay == 0)
    //        {
    //            //��һ�찤������Ѫ
    //            WealthLackDay++;
    //        }
    //        else if(WealthLackDay<3)
    //        {
    //            //��һ�׶ο�����Ѫ
    //            WealthLackDay++;
    //            PlayerProperty.Instance.PlayerHealth = playerH - Settings.FirstPeriodHung + playerW;
    //        }
    //        else if(WealthLackDay >= 3)
    //        {
    //            //�ڶ��׶μ�����Ѫ
    //            WealthLackDay++;
    //            PlayerProperty.Instance.PlayerHealth = playerH - Settings.SecondPeriodHung + playerW;
    //        }

    //    }

    //    PlayerProperty.Instance.PlayerAction = Settings.OrigPlayerAction;
    //    ActionPointControl.Instance.PointHUDControl(PlayerProperty.Instance.PlayerAction);

    //}

    ////��Ϸ�����ж�
    //private void GameEnd()
    //{
    //    //if()
    //}
}
