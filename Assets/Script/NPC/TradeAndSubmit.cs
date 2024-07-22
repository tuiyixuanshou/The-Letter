using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TradeAndSubmit : Singleton<TradeAndSubmit>
{
    public SubmitItem[] Submititem;
    public Image YaoFanPanel;

    //��load����һ�𱻸�ֵ
    public int totalPrice;

    //ÿ�θ���SubmitItem
    public void UpdateEmptySubmitItem()
    {
        for(int i = 0; i < 8; i++)
        {
            Submititem[i].InitSubmitItem(-1, 0, 0);
        }
    }



    public void LoadSubmitItem(int itemIndex,int itemID,int possessamount,int giveamount)
    {
        Debug.Log("do initSubmitItem");
        Submititem[itemIndex].InitSubmitItem(itemID, possessamount, giveamount);

    }

    public void SubmitBePressed()
    {
        PlayerProperty.Instance.PlayerWealth += totalPrice;
        foreach(var i in Submititem)
        {
            i.DeletItemInBag();
        }

        foreach(var i in GameObject.FindObjectsOfType<CustomerController>())
        {
            if(i.isStartSell == true)
            {
                i.customerDetails.isSellDone = true;
                Destroy(i.gameObject);
                YaoFanPanel.color = new Color(1, 1, 1, 0);
                YaoFanPanel.raycastTarget = false;
            }
        }

    }
}
