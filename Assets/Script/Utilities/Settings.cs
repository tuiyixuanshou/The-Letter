using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Settings
{
    [Header("��������")]
    public const int OrigPlayerHealth = 100;
    public const int OrigPlayerSan = 100;
    public const int OrigPlayerWealth = 100;
    public const int OrigPlayerAttack = 30;
    public const int OrigPlayerAction = 3;

    [Header("ÿ�ո��¶�̬")]
    public const int DailyMinusWealth = 20;
    public const int FirstPeriodHung = 25;
    public const int SecondPeriodHung = 40;

    public const int NormalFoodConsume = 15;
    //public const int NormalWarmConsume = 5;
    public const int NormalMedicalConsume = 0;

    public const int HungerFoodConsume = 30;
    //public const int HungerWarmConsume = 5;
    public const int HungerMedicalConsume = 5;

    public const int WeakFoodConsume = 30;
    //public const int WeakWarmConsume = 10;
    public const int WeakMedicalConsume = 20;

    public const int DyingFoodConsume = 20;
    //public const int DyingWarmConsume = 20;
    public const int DyingMedicalConsume = 40;
    [Header("ʱ����ֵ��ԽСԽ��")]
    public const float secondThreshold = 1.0f;
    //public const float secondThreshold = 0.012f;
    public const int secondHold = 59;
    public const int minuteHold = 59;
}
