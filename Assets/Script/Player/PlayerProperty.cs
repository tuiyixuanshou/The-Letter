using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerProperty : Singleton<PlayerProperty>
{
    [Header("������ֵ����")]
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

    //ÿ�չ̶����ı仯ֵ
    public void ChangePPtheNextDay()
    {
        int playerW = PlayerWealth;
        int playerH = PlayerHealth;

        if (!isBrokeUp)
        {
            //�����˵�����
            ConsumePanelControl consumePanelcontrol = GameObject.FindGameObjectWithTag("ConsumePanel").GetComponent<ConsumePanelControl>();
            consumePanelcontrol.YesterDayW.text = playerW.ToString();
            int daycount = DayManager.Instance.DayCount - 1;
            PlayerHealth = Settings.OrigPlayerHealth;
            consumePanelcontrol.WarmConsume.text = "-" + Warm[daycount].ToString();
            //����״̬
            if ( playerH >= 80)
            {
                PlayerWealth = playerW - Settings.NormalFoodConsume - Settings.NormalMedicalConsume - Warm[daycount];
                consumePanelcontrol.FoodConsume.text = "-" + Settings.NormalFoodConsume.ToString();
                consumePanelcontrol.MedicalConsume.text = "-" + Settings.NormalMedicalConsume.ToString();
                if (PlayerWealth < 0) isBrokeUp = true;
            }
            //����״̬
            else if(playerH < 80 && playerH >= 60)
            {
                PlayerWealth = playerW - Settings.HungerFoodConsume - Settings.HungerMedicalConsume - Warm[daycount];
                consumePanelcontrol.FoodConsume.text = "-" + Settings.HungerFoodConsume.ToString();
                consumePanelcontrol.MedicalConsume.text = "-" + Settings.HungerMedicalConsume.ToString();
                if (PlayerWealth < 0) isBrokeUp = true;
            }
            //����״̬
            else if(playerH < 60 && playerH >= 40)
            {
                PlayerWealth = playerW - Settings.WeakFoodConsume - Settings.WeakMedicalConsume - Warm[daycount];
                consumePanelcontrol.FoodConsume.text = "-" + Settings.WeakFoodConsume.ToString();
                consumePanelcontrol.MedicalConsume.text = "-" + Settings.WeakMedicalConsume.ToString();
                if (PlayerWealth < 0) isBrokeUp = true;
            }
            //����״̬
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
            //Debug.LogError("�Ʋ�δ���ȣ���Ϸ����");
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
        //        //��һ�찤������Ѫ
        //        WealthLackDay++;
        //    }
        //    else if (WealthLackDay < 3)
        //    {
        //        //��һ�׶ο�����Ѫ
        //        WealthLackDay++;
        //        PlayerProperty.Instance.PlayerHealth = playerH - Settings.FirstPeriodHung + playerW;
        //    }
        //    else if (WealthLackDay >= 3)
        //    {
        //        //�ڶ��׶μ�����Ѫ
        //        WealthLackDay++;
        //        PlayerProperty.Instance.PlayerHealth = playerH - Settings.SecondPeriodHung + playerW;
        //    }
        //}

    }

    //��Ϸ�����ж�
    private void GameEnd()
    {
        //if()
    }
}
