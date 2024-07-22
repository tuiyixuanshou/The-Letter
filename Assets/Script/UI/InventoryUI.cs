using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;

public class InventoryUI : MonoBehaviour,IPointerClickHandler
{
    [Header("背包格子")]
    public SlotUI[] BagSlots;

    [Header("商店货架格子")]
    public SlotUI[] ShopSlots;

    [Header("装备格子")]
    public SlotUI[] EquipMentSlots;

    [Header("武器格子")]
    public SlotUI[] WeaponSlots;

    [Header("拖拽图片")]
    public Image DragItem;

    [Header("货架 背包 战时简报 装备页面")]
    public GameObject ShopShelves;
    public GameObject Bag;
    public GameObject WarPaperPanel;
    public GameObject EquipPanel;
    public Text EquipAttckNum;
    public Text EquipDefenseNum;

    [Header("物品信息栏")]
    public GameObject ItemDetailPanel;
    public Image ItemImage;
    public Text ItemName;
    public Text ItemType;
    public Text ItemPrice;
    public Text ItemAttack;
    public Text ItemDefense;
    public Text ItemDescribe;
    public Text ItemPS;

    [Header("Button设置")]
    public Button BagOpenButton;
    public Button EquipOpenButton;
    public Button WarPaperButton;

    [Header("背包动画设置")]
    public Animator BagAnimator;
    public Animator EquipAnimator;
    public Animator ShelvesAnimator;
    public Animator WarAnimator;
    public Animator WarBookAnimator;

    [Header("背包关闭页面")]
    public GameObject BagCloseInHome;
    public GameObject BagClose;

    [Header("ActionBar设置")]
    public Transform ActionBarOuter;
    public Transform ActionBarInBag;
    public Animator ActionBarOutAnimator;


    [Header("设置各个场景Canvas开关")]
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
        //商店货架在初始时不会打开，不用初始化
        //在打开面板的时候初始化
        
        //设置ActionBar的位置，后期可能还需要更改内容
        GetActiveSceneNameAndSetActionBar();

        //更新shop
        if (ShopShelves.activeInHierarchy)
        {
            OnUpdateBagUI(InventoryType.ShopInventory, InventoryManager.Instance.shopItemList_SO.bagItemList);
        }

        //equip和weapon编号
        for (int k = 0; k < EquipMentSlots.Length; k++)
        {
            EquipMentSlots[k].index = k;
        }
        for (int l = 0; l < WeaponSlots.Length; l++)
        {
            WeaponSlots[l].index = l;
        }
        //如果装备栏一开始就打开的话，更新一下
        if (EquipPanel.activeInHierarchy)
        {
            OnUpdateBagUI(InventoryType.EquipInventory, InventoryManager.Instance.equipItemList_SO.bagItemList);
            OnUpdateBagUI(InventoryType.WeaponInventory, InventoryManager.Instance.weaponItemList_SO.bagItemList);
        }

        ItemDetailPanel.SetActive(false);
    }

   //关联格子和数据库
   private void OnUpdateBagUI(InventoryType inventoryType,List<BagItemDetails> list)
    {
        switch (inventoryType)
        {
            case InventoryType.BagInventory:
                //更新背包
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
                //更新商店货架
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
                //更新装备库
                int aNum = 0;
                int dNum = 0;
                for(int k = 0; k < EquipMentSlots.Length; k++)
                {
                    if (list[k].BagItemID != 0)
                    {
                        //只有不同才需要更新  不对不对不对 如果他用一样的工具来刷新次数的话也会被忽略掉
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
                //更新武器库
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

    //高光
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


    //打开商店时使用的货架
    private void OntTellInventoryUIToOpenShelves()
    {
        ShopShelves.gameObject.SetActive(true);
        //设置货架信息,开启动画
        ShelvesAnimator.SetBool("shopIn", true);
        OnUpdateBagUI(InventoryType.ShopInventory, InventoryManager.Instance.shopItemList_SO.bagItemList);
        ShopShelves.transform.localScale = new Vector3(-1, 1, 1);
        ShopShelves.transform.Find("ShopShelves").localScale = new Vector3(-1, 1, 1);

        //ShopShelves.transform.position = new Vector3(1356, 456, 0);

        //装备按钮不能用了
        EquipOpenButton.GetComponent<MainCanvasEquipButton>().OnEquipButtonPressed();
        EquipOpenButton.interactable = false;
        EquipOpenButton.GetComponent<Image>().raycastTarget = false;

        //事项按钮应该也不能用了
        WarPaperButton.GetComponent<MainCanvasWarPaperButton>().OnWarParperButtonPressed();
        WarPaperButton.interactable = false;
        WarPaperButton.GetComponent<Image>().raycastTarget = false;
    }

    //在商店中关闭货架
    private void OntTellInventoryUIToCloseShelves()
    {
        ShelvesAnimator.SetBool("shopOut", true);

        //装备按钮可用了
        EquipOpenButton.GetComponent<MainCanvasEquipButton>().OnEquipClosed();
        EquipOpenButton.interactable = true;
        EquipOpenButton.GetComponent<Image>().raycastTarget = true;

        //事项按钮可用了
        WarPaperButton.GetComponent<MainCanvasWarPaperButton>().OnWarParperClosed();
        WarPaperButton.interactable = true;
        WarPaperButton.GetComponent<Image>().raycastTarget = true;
    }

    //便捷栏收到背包中
    public void ActionBarInBagFunc()
    {
        foreach (var i in ActionBarOuter.GetComponentsInChildren<SlotUI>())
        {
            i.transform.SetParent(ActionBarInBag);
        }
        ActionBarOutAnimator.SetBool("ActionClose",true);
        ActionBarOutAnimator.SetBool("ActionOpen", false);
    }

    //便捷栏放到屏幕上
    public void ActionBarOutBagFunc()
    {
        foreach (var i in ActionBarInBag.GetComponentsInChildren<SlotUI>())
        {
            i.transform.SetParent(ActionBarOuter);
        }
        ActionBarOutAnimator.SetBool("ActionClose", false);
        ActionBarOutAnimator.SetBool("ActionOpen", true);
    }



    //afterloadLand和事项按钮的开启 物品栏按钮的开启和关闭
    //相比起命名时增加了新内容。判断事件是否对话完存在
    public void GetActiveSceneNameAndSetActionBar()
    {
        string SceneName = SceneManager.GetActiveScene().name;
        switch (SceneName)
        {
            case "Home":
                ActionBarInBagFunc();

                //设置关闭面板
                BagClose.SetActive(false);
                BagCloseInHome.SetActive(true);

                //关闭BattleCanvas
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

                //设置关闭面板
                BagClose.SetActive(true);
                BagCloseInHome.SetActive(false);

                //关闭BattleCanvas
                BattleCanvas.SetActive(false);
                break;

            //其他时候

            case "IntroduceScene":
                //关闭BattleCanvas
                BattleCanvas.SetActive(false);
                ActionBarInBagFunc();
                for (int i = 0; i < UIs.Length; i++)
                {
                    UIs[i].SetActive(false);
                }
                break;

            case "EndScene":
                //关闭BattleCanvas
                BattleCanvas.SetActive(false);
                ActionBarInBagFunc();
                for (int i = 0; i < UIs.Length; i++)
                {
                    UIs[i].SetActive(false);
                }
                break;

            default:
                ActionBarOutBagFunc();
                //给了背包的动画事件
                //设置关闭面板
                BagClose.SetActive(true);
                BagCloseInHome.SetActive(false);
                //Debug.Log(BagAnimator.GetBool("BagOut"));
                //打开BattleCanvas
                BattleCanvas.SetActive(true);
                break;
        }
    }

    //打开背包
    public void OpenBagButtonPressed()
    {
        
        if (SceneManager.GetActiveScene().name == "Home")
        {
            //关贸易框
            EventHandler.CallTellHomeUIToCloseTradeList();
            ShopShelves.SetActive(false);
            //开背包，开货架
            Bag.SetActive(true);
            //OntTellInventoryUIToOpenShelves();
            ShopShelves.SetActive(true);
            //设置货架信息,开启动画
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
        //关闭装备框
        if (EquipPanel.activeInHierarchy)
        {
            EquipAnimator.SetBool("equipOut", true);
        }
        EquipOpenButton.GetComponent<MainCanvasEquipButton>().OnEquipClosed();
        EquipOpenButton.interactable = true;
        EquipOpenButton.GetComponent<Image>().raycastTarget = true;

        //关warParper
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
    
    //打开装备
    public void OpenEquipButtonPressed()
    {

        if (Bag.activeInHierarchy == false)
        {
            Bag.SetActive(true);
            BagAnimator.SetBool("BagIn", true);
        }
        //GetActiveSceneNameAndSetActionBar();

        //关货架
        if (ShopShelves.activeInHierarchy == true)
        {
            ShelvesAnimator.SetBool("HomeOut", true);
        }

        //关warpaper
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


        //重新设置关闭面板
        BagClose.SetActive(false);
        BagCloseInHome.SetActive(true);

        OnUpdateBagUI(InventoryType.EquipInventory, InventoryManager.Instance.equipItemList_SO.bagItemList);
        OnUpdateBagUI(InventoryType.WeaponInventory, InventoryManager.Instance.weaponItemList_SO.bagItemList);
    }

    //打开warParper
    public void OpenWarPaperButtonPressed()
    {
        //关货架
        if (ShopShelves.activeInHierarchy == true)
        {
            ShelvesAnimator.SetBool("HomeOut", true);
        }

        //关闭装备框
        if (EquipPanel.activeInHierarchy)
        {
            EquipAnimator.SetBool("equipOut", true);
            EquipOpenButton.GetComponent<MainCanvasEquipButton>().OnEquipClosed();
            EquipOpenButton.interactable = true;
            EquipOpenButton.GetComponent<Image>().raycastTarget = true;
        }

        //关背包
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
        //播放进入动画
        WarAnimator.SetBool("WarIn", true);
        WarBookAnimator.SetTrigger("BookIn");
        EventHandler.CallInitSoundEffect(AudioManager.Instance.GetSoundDetailsfromName(SoundName.PaperOut));


        //关闭warParper的程序在他自己的panel上挂着，懒得改了
    }



    //背包在家开的时候需要调整
    public void OnPointerClick(PointerEventData eventData)
    {
        Debug.Log(Bag.activeInHierarchy);
        Debug.Log(eventData.pointerCurrentRaycast.gameObject.name);
        //关闭背包 货架 装备
        if (Bag.activeInHierarchy)
        {
            if (SceneManager.GetActiveScene().name == "Home")
            {
                if (eventData.pointerCurrentRaycast.gameObject.name == BagCloseInHome.name)
                {
                    //关闭装备框
                    if (EquipPanel.activeInHierarchy)
                    {
                        EquipAnimator.SetBool("equipOut", true);
                    }

                    //关货架
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

                    //关闭装备框
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

                    //重新设置关闭面板
                    BagClose.SetActive(true);
                    BagCloseInHome.SetActive(false);
                }
            }
           
        }
        //GetActiveSceneNameAndSetActionBar();

        //Debug.Log(WarPaperPanel.activeInHierarchy);
        ////关闭战时简报
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
            ItemType.text = "装备";
            ItemPS.text = "*可用于装备，多次使用";
            ItemAttack.text = details.ItemEquipAttack.ToString();
            ItemDefense.text = details.ItemEquipDefense.ToString();
        }
        else if (details.canWeapon)
        {
            ItemType.text = "武器";
            ItemPS.text = "*一次性消耗物品";
            ItemAttack.text = details.ItemWeaponAttack.ToString();
            ItemDefense.text = details.ItemWeaponDefense.ToString();
        }
        else
        {
            ItemType.text = "普通物品";
            ItemAttack.text = 0.ToString();
            ItemDefense.text = 0.ToString();
            ItemPS.text = "*普通物品，店铺中可能会有人需要";
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
