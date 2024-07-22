using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Button_Shop : MonoBehaviour
{
    public GameObject UndealText;
    public Text ShopButtonText;
    public int UndealNum;
    public Image Yaofanzhezhao; 

    private void OnEnable()
    {
        UpdateShopButton();
    }
    public void ButtonPressedControlZHEZHAO()
    {
        Yaofanzhezhao.color = new Color(Yaofanzhezhao.color.r, Yaofanzhezhao.color.g, Yaofanzhezhao.color.b, 0);
        Yaofanzhezhao.raycastTarget = false;
    }

    public void UpdateShopButton()
    {
        NPCManager.Instance.InitCustomerDayList();
        UndealNum = NPCManager.Instance.TodayUnDeal;
        if (UndealNum > 0)
        {
            UndealText.SetActive(true);
            UndealText.GetComponentInChildren<Text>().text = UndealNum.ToString(); 
            ShopButtonText.text = "���մ�������";
        }
        else
        {
            UndealText.SetActive(false);
            ShopButtonText.text = "���ս����Ѵ������";
        }

    }
}
