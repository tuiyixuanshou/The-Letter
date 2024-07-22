using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;

public class InventoryUI : MonoBehaviour,IPointerClickHandler
{
    [Header("��������")]
    public SlotUI[] BagSlots;

    [Header("�̵���ܸ���")]
    public SlotUI[] ShopSlots;

    [Header("װ������")]
    public SlotUI[] EquipMentSlots;

    [Header("��������")]
    public SlotUI[] WeaponSlots;

    [Header("��קͼƬ")]
    public Image DragItem;

    [Header("���� ���� սʱ�� װ��ҳ��")]
    public GameObject ShopShelves;
    public GameObject Bag;
    public GameObject WarPaperPanel;
    public GameObject EquipPanel;
    public Text EquipAttckNum;
    public Text EquipDefenseNum;

    [Header("��Ʒ��Ϣ��")]
    public GameObject ItemDetailPanel;
    public Image ItemImage;
    public Text ItemName;
    public Text ItemType;
    public Text ItemPrice;
    public Text ItemAttack;
    public Text ItemDefense;
    public Text ItemDescribe;
    public Text ItemPS;

    [Header("Button����")]
    public Button BagOpenButton;
    public Button EquipOpenButton;
    public Button WarPaperButton;

    [Header("������������")]
    public Animator BagAnimator;
    public Animator EquipAnimator;
    public Animator ShelvesAnimator;
    public Animator WarAnimator;
    public Animator WarBookAnimator;

    [Header("�����ر�ҳ��")]
    public GameObject BagCloseInHome;
    public GameObject BagClose;

    [Header("ActionBar����")]
    public Transform ActionBarOuter;
    public Transform ActionBarInBag;
    public Animator ActionBarOutAnimator;


    [Header("���ø�������Canvas����")]
    public GameObject BattleCanvas;
    public GameObject[] UIs;

    private void OnEnable()
    {
        EventHandler.UpDateBagUI += OnUpdateBagUI;
        EventHandler.TellInventoryUIToOpenShelves += OntTellInventoryUIToOpenShelves;
        EventHandler.TellInventoryUIToCloseShelves += OntTellInventoryUIToCloseShelves;
        EventHandler.AfterSceneLoad += GetActiveSceneNameAndSetActionBar;

        EventHandler.ClosePanelAndButtonWhenFight += ControllButtonWhenFight;

        EventHandler.TellInventoryUIToOpenAB += ActionBarOutBagFunc;
        EventHandler.TellInventoryUIToCloseAB += ActionBarInBagFunc;
    }

    private void OnDisable()
    {
        EventHandler.UpDateBagUI -= OnUpdateBagUI;
        EventHandler.TellInventoryUIToOpenShelves -= OntTellInventoryUIToOpenShelves;
        EventHandler.TellInventoryUIToCloseShelves -= OntTellInventoryUIToCloseShelves;
        EventHandler.AfterSceneLoad -= GetActiveSceneNameAndSetActionBar;

        EventHandler.ClosePanelAndButtonWhenFight -= ControllButtonWhenFight;

        EventHandler.TellInventoryUIToOpenAB -= ActionBarOutBagFunc;
        EventHandler.TellInventoryUIToCloseAB -= ActionBarInBagFunc;
    }


    void Start()
    {
        for(int i = 0; i < BagSlots.Length; i++)
        {
            BagSlots[i].index = i;
        }
        OnUpdateBagUI(InventoryType.BagInventory, InventoryManager.Instance.bagItemList_SO.bagItemList);

        for (int j = 0; j < ShopSlots.Length; j++)
        {
            ShopSlots[j].index = j;
        }
        //�̵�����ڳ�ʼʱ����򿪣����ó�ʼ��
        //�ڴ�����ʱ���ʼ��
        
        //����ActionBar��λ�ã����ڿ��ܻ���Ҫ��������
        GetActiveSceneNameAndSetActionBar();

        //����shop
        if (ShopShelves.activeInHierarchy)
        {
            OnUpdateBagUI(InventoryType.ShopInventory, InventoryManager.Instance.shopItemList_SO.bagItemList);
        }

        //equip��weapon���
        for (int k = 0; k < EquipMentSlots.Length; k++)
        {
            EquipMentSlots[k].index = k;
        }
        for (int l = 0; l < WeaponSlots.Length; l++)
        {
            WeaponSlots[l].index = l;
        }
        //���װ����һ��ʼ�ʹ򿪵Ļ�������һ��
        if (EquipPanel.activeInHierarchy)
        {
            OnUpdateBagUI(InventoryType.EquipInventory, InventoryManager.Instance.equipItemList_SO.bagItemList);
            OnUpdateBagUI(InventoryType.WeaponInventory, InventoryManager.Instance.weaponItemList_SO.bagItemList);
        }

        ItemDetailPanel.SetActive(false);
    }

   //�������Ӻ����ݿ�
   private void OnUpdateBagUI(InventoryType inventoryType,List<BagItemDetails> list)
    {
        switch (inventoryType)
        {
            case InventoryType.BagInventory:
                //���±���
                for(int i = 0; i < BagSlots.Length; i++)
                {
                    if (list[i].BagItemAmount > 0)
                    {
                        var item = InventoryManager.Instance.GetItemDetails(list[i].BagItemID);
                        BagSlots[i].UpdateSlot(item, list[i].BagItemAmount);
                    }
                    else BagSlots[i].UpdateEmptySlot();
                }
                break;

            case InventoryType.ShopInventory:
                //�����̵����
                for(int j = 0; j < ShopSlots.Length; j++)
                {
                    if (list[j].BagItemAmount > 0)
                    {
                        var item = InventoryManager.Instance.GetItemDetails(list[j].BagItemID);
                        ShopSlots[j].UpdateSlot(item, list[j].BagItemAmount);
                    }
                    else ShopSlots[j].UpdateEmptySlot();
                }
                break;

            case InventoryType.EquipInventory:
                //����װ����
                int aNum = 0;
                int dNum = 0;
                for(int k = 0; k < EquipMentSlots.Length; k++)
                {
                    if (list[k].BagItemID != 0)
                    {
                        //ֻ�в�ͬ����Ҫ����  ���Բ��Բ��� �������һ���Ĺ�����ˢ�´����Ļ�Ҳ�ᱻ���Ե�
                        //if (list[k].BagItemID != EquipMentSlots[k].itemDetails.ItemID)
                        //{
                        //    var item = InventoryManager.Instance.GetItemDetails(list[k].BagItemID);
                        //    EquipMentSlots[k].UpdateSlot(item, list[k].BagItemAmount);
                        //}
                        var item = InventoryManager.Instance.GetItemDetails(list[k].BagItemID);
                        EquipMentSlots[k].UpdateSlot(item, list[k].BagItemAmount);
                        aNum += EquipMentSlots[k].itemDetails.ItemEquipAttack;
                        dNum += EquipMentSlots[k].itemDetails.ItemEquipDefense;
                    }
                    else EquipMentSlots[k].UpdateEmptySlot();
                }

                EquipAttckNum.text = aNum.ToString();
                EquipDefenseNum.text = dNum.ToString();
                break;

            case InventoryType.WeaponInventory:
                //����������
                for(int l = 0; l < WeaponSlots.Length; l++)
                {
                    if (list[l].BagItemID != 0)
                    {
                        var item = InventoryManager.Instance.GetItemDetails(list[l].BagItemID);
                        WeaponSlots[l].UpdateSlot(item, list[l].BagItemAmount);
                    }
                    else WeaponSlots[l].UpdateEmptySlot();
                }
                break;

            default:
                break;
        }
    }

    //�߹�
    public void UpdateSlotHighLight(SlotType slotType,int index)
    {
        switch (slotType)
        {
            case SlotType.Bag:
                foreach (var slot in BagSlots)
                {
                    if (slot.isSelected && slot.index == index)
                    {
                        slot.slotHighLight.gameObject.SetActive(true);
                    }
                    else
                    {
                        slot.isSelected = false;
                        slot.slotHighLight.gameObject.SetActive(false);
                    }
                }
                break;
            case SlotType.Shop:
                foreach (var slot in ShopSlots)
                {
                    if (slot.isSelected && slot.index == index)
                    {
                        slot.slotHighLight.gameObject.SetActive(true);
                    }
                    else
                    {
                        slot.isSelected = false;
                        slot.slotHighLight.gameObject.SetActive(false);
                    }
                }
                break;
            case SlotType.Equipment:
                foreach (var slot in EquipMentSlots)
                {
                    if (slot.isSelected && slot.index == index)
                    {
                        slot.slotHighLight.gameObject.SetActive(true);
                    }
                    else
                    {
                        slot.isSelected = false;
                        slot.slotHighLight.gameObject.SetActive(false);
                    }
                }
                break;
            case SlotType.Weapon:
                foreach (var slot in WeaponSlots)
                {
                    if (slot.isSelected && slot.index == index)
                    {
                        slot.slotHighLight.gameObject.SetActive(true);
                    }
                    else
                    {
                        slot.isSelected = false;
                        slot.slotHighLight.gameObject.SetActive(false);
                    }
                }
                break;
            default:
                break;
        }
        
    }


    //���̵�ʱʹ�õĻ���
    private void OntTellInventoryUIToOpenShelves()
    {
        ShopShelves.gameObject.SetActive(true);
        //���û�����Ϣ,��������
        ShelvesAnimator.SetBool("shopIn", true);
        OnUpdateBagUI(InventoryType.ShopInventory, InventoryManager.Instance.shopItemList_SO.bagItemList);
        ShopShelves.transform.localScale = new Vector3(-1, 1, 1);
        ShopShelves.transform.Find("ShopShelves").localScale = new Vector3(-1, 1, 1);

        //ShopShelves.transform.position = new Vector3(1356, 456, 0);

        //װ����ť��������
        EquipOpenButton.GetComponent<MainCanvasEquipButton>().OnEquipButtonPressed();
        EquipOpenButton.interactable = false;
        EquipOpenButton.GetComponent<Image>().raycastTarget = false;

        //���ťӦ��Ҳ��������
        WarPaperButton.GetComponent<MainCanvasWarPaperButton>().OnWarParperButtonPressed();
        WarPaperButton.interactable = false;
        WarPaperButton.GetComponent<Image>().raycastTarget = false;
    }

    //���̵��йرջ���
    private void OntTellInventoryUIToCloseShelves()
    {
        ShelvesAnimator.SetBool("shopOut", true);

        //װ����ť������
        EquipOpenButton.GetComponent<MainCanvasEquipButton>().OnEquipClosed();
        EquipOpenButton.interactable = true;
        EquipOpenButton.GetComponent<Image>().raycastTarget = true;

        //���ť������
        WarPaperButton.GetComponent<MainCanvasWarPaperButton>().OnWarParperClosed();
        WarPaperButton.interactable = true;
        WarPaperButton.GetComponent<Image>().raycastTarget = true;
    }

    //������յ�������
    public void ActionBarInBagFunc()
    {
        foreach (var i in ActionBarOuter.GetComponentsInChildren<SlotUI>())
        {
            i.transform.SetParent(ActionBarInBag);
        }
        ActionBarOutAnimator.SetBool("ActionClose",true);
        ActionBarOutAnimator.SetBool("ActionOpen", false);
    }

    //������ŵ���Ļ��
    public void ActionBarOutBagFunc()
    {
        foreach (var i in ActionBarInBag.GetComponentsInChildren<SlotUI>())
        {
            i.transform.SetParent(ActionBarOuter);
        }
        ActionBarOutAnimator.SetBool("ActionClose", false);
        ActionBarOutAnimator.SetBool("ActionOpen", true);
    }



    //afterloadLand�����ť�Ŀ��� ��Ʒ����ť�Ŀ����͹ر�
    //���������ʱ�����������ݡ��ж��¼��Ƿ�Ի������
    public void GetActiveSceneNameAndSetActionBar()
    {
        string SceneName = SceneManager.GetActiveScene().name;
        switch (SceneName)
        {
            case "Home":
                ActionBarInBagFunc();

                //���ùر����
                BagClose.SetActive(false);
                BagCloseInHome.SetActive(true);

                //�ر�BattleCanvas
                BattleCanvas.SetActive(false);

                if (!UIs[0].activeInHierarchy)
                {
                    for (int i = 0; i < UIs.Length; i++)
                    {
                        UIs[i].SetActive(true);
                    }
                }
                break;

            
            case "Map":
                ActionBarInBagFunc();

                //���ùر����
                BagClose.SetActive(true);
                BagCloseInHome.SetActive(false);

                //�ر�BattleCanvas
                BattleCanvas.SetActive(false);
                break;

            //����ʱ��

            case "IntroduceScene":
                //�ر�BattleCanvas
                BattleCanvas.SetActive(false);
                ActionBarInBagFunc();
                for (int i = 0; i < UIs.Length; i++)
                {
                    UIs[i].SetActive(false);
                }
                break;

            case "EndScene":
                //�ر�BattleCanvas
                BattleCanvas.SetActive(false);
                ActionBarInBagFunc();
                for (int i = 0; i < UIs.Length; i++)
                {
                    UIs[i].SetActive(false);
                }
                break;

            default:
                ActionBarOutBagFunc();
                //���˱����Ķ����¼�
                //���ùر����
                BagClose.SetActive(true);
                BagCloseInHome.SetActive(false);
                //Debug.Log(BagAnimator.GetBool("BagOut"));
                //��BattleCanvas
                BattleCanvas.SetActive(true);
                break;
        }
    }

    //�򿪱���
    public void OpenBagButtonPressed()
    {
        
        if (SceneManager.GetActiveScene().name == "Home")
        {
            //��ó�׿�
            EventHandler.CallTellHomeUIToCloseTradeList();
            ShopShelves.SetActive(false);
            //��������������
            Bag.SetActive(true);
            //OntTellInventoryUIToOpenShelves();
            ShopShelves.SetActive(true);
            //���û�����Ϣ,��������
            ShelvesAnimator.SetBool("HomeIn", true);
            EventHandler.CallInitSoundEffect(AudioManager.Instance.GetSoundDetailsfromName(SoundName.PanelOut));
            ShopShelves.transform.localScale = new Vector3(1, 1, 1);
            ShopShelves.transform.Find("ShopShelves").localScale = new Vector3(1, 1, 1);

            OnUpdateBagUI(InventoryType.ShopInventory, InventoryManager.Instance.shopItemList_SO.bagItemList);
            
        }
        else
        {
            Bag.SetActive(true);
        }
        //�ر�װ����
        if (EquipPanel.activeInHierarchy)
        {
            EquipAnimator.SetBool("equipOut", true);
        }
        EquipOpenButton.GetComponent<MainCanvasEquipButton>().OnEquipClosed();
        EquipOpenButton.interactable = true;
        EquipOpenButton.GetComponent<Image>().raycastTarget = true;

        //��warParper
        if (WarPaperPanel.activeInHierarchy)
        {
            WarAnimator.SetBool("WarOut", true);
            WarBookAnimator.SetTrigger("BookOut");
        }
        WarPaperButton.interactable = true;
        WarPaperButton.GetComponent<MainCanvasWarPaperButton>().OnWarParperClosed();
        WarPaperButton.GetComponent<Image>().raycastTarget = true;



        BagAnimator.SetBool("BagIn", true);
        BagOpenButton.GetComponent<MainCanvasBagButton>().OnBagButtonPressed();
        BagOpenButton.GetComponent<Image>().raycastTarget = false;
        BagOpenButton.interactable = false;

        OnUpdateBagUI(InventoryType.BagInventory, InventoryManager.Instance.bagItemList_SO.bagItemList);
        
        //GetActiveSceneNameAndSetActionBar();
    }
    
    //��װ��
    public void OpenEquipButtonPressed()
    {

        if (Bag.activeInHierarchy == false)
        {
            Bag.SetActive(true);
            BagAnimator.SetBool("BagIn", true);
        }
        //GetActiveSceneNameAndSetActionBar();

        //�ػ���
        if (ShopShelves.activeInHierarchy == true)
        {
            ShelvesAnimator.SetBool("HomeOut", true);
        }

        //��warpaper
        if (WarPaperPanel.activeInHierarchy)
        {
            WarAnimator.SetBool("WarOut", true);
            WarBookAnimator.SetTrigger("BookOut");
            WarPaperButton.interactable = true;
            WarPaperButton.GetComponent<MainCanvasWarPaperButton>().OnWarParperClosed();
            WarPaperButton.GetComponent<Image>().raycastTarget = true;
        }

        EquipPanel.SetActive(true);
        EquipAnimator.SetBool("equipIn", true);
        EventHandler.CallInitSoundEffect(AudioManager.Instance.GetSoundDetailsfromName(SoundName.PanelOut));
        EquipOpenButton.GetComponent<MainCanvasEquipButton>().OnEquipButtonPressed();
        EquipOpenButton.interactable = false;
        EquipOpenButton.GetComponent<Image>().raycastTarget = false;

        BagOpenButton.GetComponent<MainCanvasBagButton>().OnBagClosed();
        BagOpenButton.interactable = true;
        BagOpenButton.GetComponent<Image>().raycastTarget = true;


        //�������ùر����
        BagClose.SetActive(false);
        BagCloseInHome.SetActive(true);

        OnUpdateBagUI(InventoryType.EquipInventory, InventoryManager.Instance.equipItemList_SO.bagItemList);
        OnUpdateBagUI(InventoryType.WeaponInventory, InventoryManager.Instance.weaponItemList_SO.bagItemList);
    }

    //��warParper
    public void OpenWarPaperButtonPressed()
    {
        //�ػ���
        if (ShopShelves.activeInHierarchy == true)
        {
            ShelvesAnimator.SetBool("HomeOut", true);
        }

        //�ر�װ����
        if (EquipPanel.activeInHierarchy)
        {
            EquipAnimator.SetBool("equipOut", true);
            EquipOpenButton.GetComponent<MainCanvasEquipButton>().OnEquipClosed();
            EquipOpenButton.interactable = true;
            EquipOpenButton.GetComponent<Image>().raycastTarget = true;
        }

        //�ر���
        if(Bag.activeInHierarchy == true)
        {
            BagAnimator.SetBool("BagOut", true);
            BagOpenButton.GetComponent<MainCanvasBagButton>().OnBagClosed();
            BagOpenButton.interactable = true;
            BagOpenButton.GetComponent<Image>().raycastTarget = true;
        }

        WarPaperButton.GetComponent<MainCanvasWarPaperButton>().OnWarParperButtonPressed();
        WarPaperButton.interactable = false;
        WarPaperButton.GetComponent<Image>().raycastTarget = false;

        WarPaperPanel.SetActive(true);
        //���Ž��붯��
        WarAnimator.SetBool("WarIn", true);
        WarBookAnimator.SetTrigger("BookIn");
        EventHandler.CallInitSoundEffect(AudioManager.Instance.GetSoundDetailsfromName(SoundName.PaperOut));


        //�ر�warParper�ĳ��������Լ���panel�Ϲ��ţ����ø���
    }



    //�����ڼҿ���ʱ����Ҫ����
    public void OnPointerClick(PointerEventData eventData)
    {
        Debug.Log(Bag.activeInHierarchy);
        Debug.Log(eventData.pointerCurrentRaycast.gameObject.name);
        //�رձ��� ���� װ��
        if (Bag.activeInHierarchy)
        {
            if (SceneManager.GetActiveScene().name == "Home")
            {
                if (eventData.pointerCurrentRaycast.gameObject.name == BagCloseInHome.name)
                {
                    //�ر�װ����
                    if (EquipPanel.activeInHierarchy)
                    {
                        EquipAnimator.SetBool("equipOut", true);
                    }

                    //�ػ���
                    if (ShopShelves.activeInHierarchy == true)
                    {
                        ShelvesAnimator.SetBool("HomeOut", true);
                    }

                    BagAnimator.SetBool("BagOut", true);
                    BagOpenButton.GetComponent<MainCanvasBagButton>().OnBagClosed();
                    BagOpenButton.interactable = true;
                    BagOpenButton.GetComponent<Image>().raycastTarget = true;
                    EquipOpenButton.GetComponent<MainCanvasEquipButton>().OnEquipClosed();
                    EquipOpenButton.interactable = true;
                    EquipOpenButton.GetComponent<Image>().raycastTarget = true;

                    EventHandler.CallInitSoundEffect(AudioManager.Instance.GetSoundDetailsfromName(SoundName.PanelOut));
                }
                
            }
            else
            {
                if(eventData.pointerCurrentRaycast.gameObject.name == BagClose.name || eventData.pointerCurrentRaycast.gameObject.name == BagCloseInHome.name)
                {
                    //Bag.SetActive(false);
                    if (ShopShelves.activeInHierarchy == true)
                    {
                        ShelvesAnimator.SetBool("HomeOut", true);
                    }

                    //�ر�װ����
                    if (EquipPanel.activeInHierarchy)
                    {
                        //EquipPanel.SetActive(false);
                        EquipAnimator.SetBool("equipOut", true);
                        //EquipOpenButton.gameObject.SetActive(true);
                    }

                    BagAnimator.SetBool("BagOut", true);
                    BagOpenButton.GetComponent<MainCanvasBagButton>().OnBagClosed();
                    BagOpenButton.interactable = true;
                    BagOpenButton.GetComponent<Image>().raycastTarget = true;
                    EquipOpenButton.GetComponent<MainCanvasEquipButton>().OnEquipClosed();
                    EquipOpenButton.interactable = true;
                    EquipOpenButton.GetComponent<Image>().raycastTarget = true;

                    EventHandler.CallInitSoundEffect(AudioManager.Instance.GetSoundDetailsfromName(SoundName.PanelOut));

                    //�������ùر����
                    BagClose.SetActive(true);
                    BagCloseInHome.SetActive(false);
                }
            }
           
        }
        //GetActiveSceneNameAndSetActionBar();

        //Debug.Log(WarPaperPanel.activeInHierarchy);
        ////�ر�սʱ��
        //if (eventData.pointerCurrentRaycast.gameObject.name != "WarPaperPanel")
        //{
        //    WarPaperPanel.SetActive(false);
        //    Debug.Log("click");
        //    //if(eventData.pointerCurrentRaycast.gameObject.name != "WarPaperPanel")
        //    //{
        //    //    WarPaperPanel.SetActive(false);
        //    //}
        //}
    }


    public void ControllButtonWhenFight(BattleState state)
    {
        if(state == BattleState.START)
        {
            ActionBarInBagFunc();
            if (Bag.activeInHierarchy) { BagAnimator.SetBool("BagOut", true); }
            if (EquipPanel.activeInHierarchy) { EquipAnimator.SetBool("equipOut", true); }
            if (WarPaperPanel.activeInHierarchy) { WarAnimator.SetBool("WarOut", true); WarBookAnimator.SetTrigger("BookOut"); }
            BagOpenButton.GetComponent<MainCanvasBagButton>().OnBagButtonPressed(); BagOpenButton.GetComponent<Image>().raycastTarget = false; BagOpenButton.interactable = false;
            EquipOpenButton.GetComponent<MainCanvasEquipButton>().OnEquipButtonPressed(); EquipOpenButton.interactable = false; EquipOpenButton.GetComponent<Image>().raycastTarget = false;
            WarPaperButton.GetComponent<MainCanvasWarPaperButton>().OnWarParperButtonPressed(); WarPaperButton.interactable = false; WarPaperButton.GetComponent<Image>().raycastTarget = false;
        }
        else if (state == BattleState.WON)
        {
            BagOpenButton.GetComponent<MainCanvasBagButton>().OnBagClosed(); BagOpenButton.interactable = true; BagOpenButton.GetComponent<Image>().raycastTarget = true;
            EquipOpenButton.GetComponent<MainCanvasEquipButton>().OnEquipClosed(); EquipOpenButton.interactable = true; EquipOpenButton.GetComponent<Image>().raycastTarget = true;
            WarPaperButton.GetComponent<MainCanvasWarPaperButton>().OnWarParperClosed(); WarPaperButton.interactable = true; WarPaperButton.GetComponent<Image>().raycastTarget = true;
        }

     }

    public void ItemDetailsPanelUpdate(ItemDetails details,Vector3 Pos)
    {
        float x, y;
        ItemImage.sprite = details.ItemIcon;
        ItemName.text = details.ItemName;
        if (details.canEquip)
        {
            ItemType.text = "װ��";
            ItemPS.text = "*������װ�������ʹ��";
            ItemAttack.text = details.ItemEquipAttack.ToString();
            ItemDefense.text = details.ItemEquipDefense.ToString();
        }
        else if (details.canWeapon)
        {
            ItemType.text = "����";
            ItemPS.text = "*һ����������Ʒ";
            ItemAttack.text = details.ItemWeaponAttack.ToString();
            ItemDefense.text = details.ItemWeaponDefense.ToString();
        }
        else
        {
            ItemType.text = "��ͨ��Ʒ";
            ItemAttack.text = 0.ToString();
            ItemDefense.text = 0.ToString();
            ItemPS.text = "*��ͨ��Ʒ�������п��ܻ�������Ҫ";
        }
        ItemPrice.text = details.itemPrice.ToString();
        ItemDescribe.text = details.ItemDescribe;

        ItemDetailPanel.SetActive(true);
        if (Pos.x > 1000)
        {
            x = Pos.x - 300;
        }
        else
        {
            x = Pos.x + 300;
        }
        if (Pos.y < 350)
        {
            y = Pos.y + 300;
        }
        else
        {
            y = Pos.y - 300;
        }
        ItemDetailPanel.transform.position = new Vector3(x, y, ItemDetailPanel.transform.position.z);
    }

    public void ItemDetailsPanelClose()
    {
        ItemDetailPanel.SetActive(false);
    }
}
