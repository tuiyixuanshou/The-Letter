using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BedThingControl : MonoBehaviour
{
    //�ֶ���д
    public int index;
    [Header("�������")]
    public SpriteRenderer Lianzi;
    public SpriteRenderer LyingPeople;

    [Header("�����ʽ")]
    public Sprite LianOpen;
    public Sprite LianClose;

    public void Init(ChurchBedDetails bedDetails)
    {
        //���ӿ���
        if (bedDetails.isLianOpen[index])
        {
            Lianzi.sprite = LianOpen;
        }
        else
        {
            Lianzi.sprite = LianClose;
        }
        //���Ƿ�����
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
