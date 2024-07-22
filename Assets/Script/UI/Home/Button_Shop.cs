using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Button_Shop : MonoBehaviour
{
    public GameObject UndealText;
    public Text ShopButtonText;
    public int UndealNum;

    private void OnEnable()
    {
        UpdateShopButton();
    }

    public void UpdateShopButton()
    {
        NPCManager.Instance.InitCustomerDayList();
        UndealNum = NPCManager.Instance.TodayUnDeal;
        if (UndealNum > 0)
        {
            UndealText.SetActive(true);
            UndealText.GetComponentInChildren<Text>().text = UndealNum.ToString(); 
            ShopButtonText.text = "今日待处理交易";
        }
        else
        {
            UndealText.SetActive(false);
            ShopButtonText.text = "今日交易已处理完毕";
        }

    }
}
