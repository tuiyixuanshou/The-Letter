using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class HomeUI : Singleton<HomeUI>,IPointerClickHandler
{
    [Header("���е�һЩUI")]
    public Button NextDayButton;
    public Button AskIntoNightButton;
    public Button MapButton;
    public Button Shop;

    [Header("�̵�")]
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

        //�ر������װ����ť
    }

    //����button
    public void CloseShopButtonPressed()
    {
        EventHandler.CallTellInventoryUIToCloseShelves();
    }

    //����հ״��ر��̵�ó�׿�ͻ���
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

    //���ر��̵�ó�׿�
    private void OnCallTellHomeUIToCloseTradeList()
    {
        if(ShopBackGroup.activeInHierarchy == true)
        ShopAnimator.SetBool("ShopOut", true);
    }
}
