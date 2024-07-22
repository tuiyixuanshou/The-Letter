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
    public Button EndButton;

    [Header("�̵�")]
    public GameObject ShopBackGroup;
    public GameObject ShopClosePanel;
    public Animator ShopAnimator;
    public GameObject DarkMarketEnter;

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
        int daycount = DayManager.Instance.DayCount;
        if(daycount < 14)
        {
            AskIntoNightButton.gameObject.SetActive(true);
            MapButton.interactable = true;
        }
        else
        {
            EndButton.gameObject.SetActive(true);
            AskIntoNightButton.gameObject.SetActive(false);
            MapButton.interactable = false;
        }
        NextDayButton.gameObject.SetActive(false);
        Shop.gameObject.SetActive(false);
        foreach (var i in FindObjectsOfType<HomeEventUI>())
        {
            Destroy(i.gameObject);
        }
        DarkMarketEnter.SetActive(true);
    }


    public void OpenShopButtonPressed()
    {
        ShopAnimator.SetBool("ShopIn", true);
        EventHandler.CallInitSoundEffect(AudioManager.Instance.GetSoundDetailsfromName(SoundName.ButtonClick));
        EventHandler.CallTellInventoryUIToOpenShelves();
        EventHandler.CallInitSoundEffect(AudioManager.Instance.GetSoundDetailsfromName(SoundName.PanelOut));
        //�ر������װ����ť,д����inventoryUI��
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
                CloseShopAndShelves();
            }
        }
    }

    //�ر��̵�ó�׿�ͻ���
    public void CloseShopAndShelves()
    {
        ShopAnimator.SetBool("ShopOut", true);
        EventHandler.CallTellInventoryUIToCloseShelves();
        Shop.gameObject.GetComponent<Button_Shop>().UpdateShopButton();
        EventHandler.CallInitSoundEffect(AudioManager.Instance.GetSoundDetailsfromName(SoundName.PanelOut));
    }


    //���ر��̵�ó�׿�
    private void OnCallTellHomeUIToCloseTradeList()
    {
        if(ShopBackGroup.activeInHierarchy == true)
        ShopAnimator.SetBool("ShopOut", true);
        EventHandler.CallInitSoundEffect(AudioManager.Instance.GetSoundDetailsfromName(SoundName.PanelOut));
    }
}
