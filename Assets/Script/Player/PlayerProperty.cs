using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerProperty : Singleton<PlayerProperty>
{
    public int PlayerHealth;
    public int PlayerSan;
    public int PlayerWealth;
    public int PlayerAttack;
    public int PlayerAction;

    void Start()
    {
        PlayerHealth = Settings.OrigPlayerHealth;
        PlayerSan = Settings.OrigPlayerSan;
        PlayerWealth = Settings.OrigPlayerWealth;
        PlayerAttack = Settings.OrigPlayerAttack;
        PlayerAction = Settings.OrigPlayerAction;
    }


    public int WealthLackDay = 0;

    private void OnEnable()
    {
        EventHandler.Button_ChangePlayerPorperty += ChangePPtheNextDay;
    }

    private void OnDisable()
    {
        EventHandler.Button_ChangePlayerPorperty -= ChangePPtheNextDay;
    }

    //ÿ�չ̶����ı仯ֵ
    public void ChangePPtheNextDay()
    {
        int playerW = PlayerProperty.Instance.PlayerWealth;
        int playerH = PlayerProperty.Instance.PlayerHealth;

        if (playerW > Settings.DailyMinusWealth - 1)
        {
            WealthLackDay = 0;
            PlayerProperty.Instance.PlayerWealth = playerW - Settings.DailyMinusWealth;
            PlayerProperty.Instance.PlayerHealth = Settings.OrigPlayerHealth;
        }
        else if (playerW < Settings.DailyMinusWealth)
        {
            PlayerProperty.Instance.PlayerWealth = 0;
            if (WealthLackDay == 0)
            {
                //��һ�찤������Ѫ
                WealthLackDay++;
            }
            else if (WealthLackDay < 3)
            {
                //��һ�׶ο�����Ѫ
                WealthLackDay++;
                PlayerProperty.Instance.PlayerHealth = playerH - Settings.FirstPeriodHung + playerW;
            }
            else if (WealthLackDay >= 3)
            {
                //�ڶ��׶μ�����Ѫ
                WealthLackDay++;
                PlayerProperty.Instance.PlayerHealth = playerH - Settings.SecondPeriodHung + playerW;
            }

        }

        PlayerProperty.Instance.PlayerAction = Settings.OrigPlayerAction;
        ActionPointControl.Instance.PointHUDControl(PlayerProperty.Instance.PlayerAction);

    }

    //��Ϸ�����ж�
    private void GameEnd()
    {
        //if()
    }
}
