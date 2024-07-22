using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class HomeUI : Singleton<HomeUI>,IPointerClickHandler
{
    [Header("家中的一些UI")]
    public Button NextDayButton;
    public Button AskIntoNightButton;
    public Button MapButton;
    public Button Shop;

    [Header("商店")]
    public GameObject ShopBackGroup;
    public GameObject ShopClosePanel;
    public Animator ShopAnimator;

    private void OnEnable()
    {
        //EventHandler.Button_AddDay += OnButtonAddDay;
        EventHandler.TellHomeUIToCloseTradeList += OnCallTellHomeUIToCloseTradeList;
      
    }


    private void OnDisable()
    {
        //EventHandler.Button_AddDay -= OnButtonAddDay;
        EventHandler.TellHomeUIToCloseTradeList -= OnCallTellHomeUIToCloseTradeList;
       
    }

    public void DoTheButton()
    {
        AskIntoNightButton.gameObject.SetActive(true);
        NextDayButton.gameObject.SetActive(false);
        MapButton.interactable = true;
        Shop.gameObject.SetActive(false);
    }


    public void OpenShopButtonPressed()
    {
        ShopAnimator.SetBool("ShopIn", true);
        EventHandler.CallTellInventoryUIToOpenShelves();

        //关闭事项和装备按钮
    }

    //弃用button
    public void CloseShopButtonPressed()
    {
        EventHandler.CallTellInventoryUIToCloseShelves();
    }

    //点击空白处关闭商店贸易框和货架
    public void OnPointerClick(PointerEventData eventData)
    {
        if (ShopBackGroup.activeInHierarchy)
        {

            Debug.Log(eventData.pointerCurrentRaycast.gameObject.name);
            if (eventData.pointerCurrentRaycast.gameObject.name == "ClosePanel")
            {
                Debug.Log("do close shop");
                //ShopBackGroup.SetActive(false);
                ShopAnimator.SetBool("ShopOut", true);
                EventHandler.CallTellInventoryUIToCloseShelves();
                Shop.gameObject.GetComponent<Button_Shop>().UpdateShopButton();
            }
        }
    }

    //仅关闭商店贸易框
    private void OnCallTellHomeUIToCloseTradeList()
    {
        if(ShopBackGroup.activeInHierarchy == true)
        ShopAnimator.SetBool("ShopOut", true);
    }
}
