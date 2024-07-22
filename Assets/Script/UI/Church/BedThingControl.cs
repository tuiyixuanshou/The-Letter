using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BedThingControl : MonoBehaviour
{
    //手动填写
    public int index;
    [Header("床体组件")]
    public SpriteRenderer Lianzi;
    public SpriteRenderer LyingPeople;

    [Header("组件样式")]
    public Sprite LianOpen;
    public Sprite LianClose;

    public void Init(ChurchBedDetails bedDetails)
    {
        //帘子开合
        if (bedDetails.isLianOpen[index])
        {
            Lianzi.sprite = LianOpen;
        }
        else
        {
            Lianzi.sprite = LianClose;
        }
        //人是否躺着
        if (bedDetails.isLyingPeople[index])
        {
            LyingPeople.enabled = true;
        }
        else
        {
            LyingPeople.enabled = false;
        }
    }
}
