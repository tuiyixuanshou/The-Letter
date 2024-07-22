using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerProperty : Singleton<PlayerProperty>
{
    [Header("人物数值属性")]
    public int PlayerHealth;
    public int PlayerSan;
    public int PlayerWealth;
    public int PlayerAttack;
    public int PlayerAction;

    public bool isBrokeUp;

    int[] Warm = { 20, 20, 30, 30,40,20,20,20,15,15,15,15,15,10 };
    

    void Start()
    {
        EmptyOriData();
    }

    private void EmptyOriData()
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
        EventHandler.NewGameEmptyData += EmptyOriData;
    }

    private void OnDisable()
    {
        EventHandler.Button_ChangePlayerPorperty -= ChangePPtheNextDay;
        EventHandler.NewGameEmptyData -= EmptyOriData;
    }

    //每日固定消耗变化值
    public void ChangePPtheNextDay()
    {
        int playerW = PlayerWealth;
        int playerH = PlayerHealth;

        if (!isBrokeUp)
        {
            //更新账单内容
            ConsumePanelControl consumePanelcontrol = GameObject.FindGameObjectWithTag("ConsumePanel").GetComponent<ConsumePanelControl>();
            consumePanelcontrol.YesterDayW.text = playerW.ToString();
            int daycount = DayManager.Instance.DayCount - 1;
            PlayerHealth = Settings.OrigPlayerHealth;
            consumePanelcontrol.WarmConsume.text = "-" + Warm[daycount].ToString();
            //健康状态
            if ( playerH >= 80)
            {
                PlayerWealth = playerW - Settings.NormalFoodConsume - Settings.NormalMedicalConsume - Warm[daycount];
                consumePanelcontrol.FoodConsume.text = "-" + Settings.NormalFoodConsume.ToString();
                consumePanelcontrol.MedicalConsume.text = "-" + Settings.NormalMedicalConsume.ToString();
                if (PlayerWealth < 0) isBrokeUp = true;
            }
            //饥饿状态
            else if(playerH < 80 && playerH >= 60)
            {
                PlayerWealth = playerW - Settings.HungerFoodConsume - Settings.HungerMedicalConsume - Warm[daycount];
                consumePanelcontrol.FoodConsume.text = "-" + Settings.HungerFoodConsume.ToString();
                consumePanelcontrol.MedicalConsume.text = "-" + Settings.HungerMedicalConsume.ToString();
                if (PlayerWealth < 0) isBrokeUp = true;
            }
            //虚弱状态
            else if(playerH < 60 && playerH >= 40)
            {
                PlayerWealth = playerW - Settings.WeakFoodConsume - Settings.WeakMedicalConsume - Warm[daycount];
                consumePanelcontrol.FoodConsume.text = "-" + Settings.WeakFoodConsume.ToString();
                consumePanelcontrol.MedicalConsume.text = "-" + Settings.WeakMedicalConsume.ToString();
                if (PlayerWealth < 0) isBrokeUp = true;
            }
            //濒死状态
            else
            {
                PlayerWealth = playerW - Settings.DyingFoodConsume - Settings.DyingMedicalConsume - Warm[daycount];
                consumePanelcontrol.FoodConsume.text = "-" + Settings.DyingFoodConsume.ToString();
                consumePanelcontrol.MedicalConsume.text = "-" + Settings.DyingMedicalConsume.ToString();
                if (PlayerWealth < 0) isBrokeUp = true;
            }
            consumePanelcontrol.TodayW.text = PlayerWealth.ToString();
            consumePanelcontrol.Health.text = PlayerHealth.ToString()+"/100";
            consumePanelcontrol.San.text = PlayerSan.ToString() + "/100";
            
        }
        else
        {
            //Debug.LogError("破产未拯救，游戏结束");
            EndManager.Instance.WealthNone = true;
            EventHandler.CallButton_ToENDGame();
        }
        PlayerAction = Settings.OrigPlayerAction;
        ActionPointControl.Instance.PointHUDControl(PlayerAction);
        //if (playerW > Settings.DailyMinusWealth - 1)
        //{
        //    WealthLackDay = 0;
        //    PlayerProperty.Instance.PlayerWealth = playerW - Settings.DailyMinusWealth;
        //    PlayerProperty.Instance.PlayerHealth = Settings.OrigPlayerHealth;
        //}
        //else if (playerW < Settings.DailyMinusWealth)
        //{
        //    PlayerProperty.Instance.PlayerWealth = 0;
        //    if (WealthLackDay == 0)
        //    {
        //        //第一天挨饿不扣血
        //        WealthLackDay++;
        //    }
        //    else if (WealthLackDay < 3)
        //    {
        //        //第一阶段扣少许血
        //        WealthLackDay++;
        //        PlayerProperty.Instance.PlayerHealth = playerH - Settings.FirstPeriodHung + playerW;
        //    }
        //    else if (WealthLackDay >= 3)
        //    {
        //        //第二阶段继续扣血
        //        WealthLackDay++;
        //        PlayerProperty.Instance.PlayerHealth = playerH - Settings.SecondPeriodHung + playerW;
        //    }
        //}

    }

    //游戏结束判断
    private void GameEnd()
    {
        //if()
    }
}
