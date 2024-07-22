using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Settings
{
    [Header("人物属性")]
    public const int OrigPlayerHealth = 100;
    public const int OrigPlayerSan = 100;
    public const int OrigPlayerWealth = 100;
    public const int OrigPlayerAttack = 30;
    public const int OrigPlayerAction = 3;

    [Header("每日更新动态")]
    public const int DailyMinusWealth = 20;
    public const int FirstPeriodHung = 25;
    public const int SecondPeriodHung = 40;

    [Header("时间阈值，越小越快")]
    public const float secondThreshold = 1.0f;
}
