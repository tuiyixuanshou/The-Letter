using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DMitemControl : MonoBehaviour
{
    [Header("商品信息")]
    public int itemID;
    public Image itemIcon;
    public Text itemName;
    public Text attack;
    public Text defense;
    public Text itemDescribe;
    public Text saveAmounttext;
    private int saveAmount;
    public Text priceAmounttext;
    private int priceAmount;
    private ItemDetails itemDetails;
    public Button buyButton;

    //初始化DMItem
    public void initDMitem(int ItemID)
    {
        //获得item信息
        itemID = ItemID;
        if(InventoryManager.Instance.GetItemDetails(itemID) != null)
        {
            itemDetails = InventoryManager.Instance.GetItemDetails(itemID);
        }
        else
        {
            itemDetails = InventoryManager.Instance.GetItemDetails(1101);
        }
        

        //刷新物品条
        itemIcon.sprite = itemDetails.ItemIcon;
        itemName.text = itemDetails.ItemName;
        if(itemDetails.canEquip)
        {
            attack.text = "攻击：＋" + itemDetails.ItemEquipAttack.ToString();
            defense.text = "防御：＋" + itemDetails.ItemEquipDefense.ToString();
        }
        else if (itemDetails.canWeapon)
        {
            attack.text = "攻击：＋" + itemDetails.ItemWeaponAttack.ToString();
            defense.text = "防御：＋" + itemDetails.ItemWeaponDefense.ToString();
        }
        else
        {
            attack.gameObject.SetActive(false);
            defense.gameObject.SetActive(false);
        }
        itemDescribe.text = itemDetails.ItemDescribe;

        saveAmount = Random.Range(1, 8);
        saveAmounttext.text = "库存：" + saveAmount.ToString();

        priceAmount = (int)itemDetails.itemPrice;
        priceAmounttext.text = priceAmount.ToString();

        //设置按钮
        SetBuyButtonInteractable();
    }


    public void BuyButtonPressed()
    {
        int index = InventoryManager.Instance.GetItemIndexInBag(itemID);
        //可以添加
        if (InventoryManager.Instance.CheckBagCapacity() || index != -1)
        {
            if (SetBuyButtonInteractable())
            {
                saveAmount--;
                saveAmounttext.text = "库存：" + saveAmount.ToString();
                PlayerProperty.Instance.PlayerWealth -= priceAmount;
                EventHandler.CallPlaySoundEvent(SoundName.MoneyChange);
                SetBuyButtonInteractable();
                InventoryManager.Instance.AddItemAtIndex(itemID, index, 1, false);
            }
            else
            {
                buyButton.interactable = false;
                string mtext = "资金不足……";
                MainCanvasUITip.Instance.UITipShowAndDisappear(mtext);
            }
            
        }
        else
        {
            string mtext = "背包已满！";
            MainCanvasUITip.Instance.UITipShowAndDisappear(mtext);
        }
    }

    //设置按钮
    private bool SetBuyButtonInteractable()
    {
        if (saveAmount > 0 && PlayerProperty.Instance.PlayerWealth >= priceAmount)
        {
            buyButton.interactable = true;
            return true;
        }
        else
        {
            buyButton.interactable = false;
            return false;
        }
    }
}
