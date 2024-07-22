using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInventory : Singleton<PlayerInventory>
{
    [Header("教堂")]
    public bool isReachChurch;
    public bool isStayInChurchOutDoor;

    [Header("地图")]
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

    ////每日固定消耗变化值
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
    //            //第一天挨饿不扣血
    //            WealthLackDay++;
    //        }
    //        else if(WealthLackDay<3)
    //        {
    //            //第一阶段扣少许血
    //            WealthLackDay++;
    //            PlayerProperty.Instance.PlayerHealth = playerH - Settings.FirstPeriodHung + playerW;
    //        }
    //        else if(WealthLackDay >= 3)
    //        {
    //            //第二阶段继续扣血
    //            WealthLackDay++;
    //            PlayerProperty.Instance.PlayerHealth = playerH - Settings.SecondPeriodHung + playerW;
    //        }

    //    }

    //    PlayerProperty.Instance.PlayerAction = Settings.OrigPlayerAction;
    //    ActionPointControl.Instance.PointHUDControl(PlayerProperty.Instance.PlayerAction);

    //}

    ////游戏结束判断
    //private void GameEnd()
    //{
    //    //if()
    //}
}
