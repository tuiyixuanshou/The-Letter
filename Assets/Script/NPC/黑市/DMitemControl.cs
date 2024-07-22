using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DMitemControl : MonoBehaviour
{
    [Header("��Ʒ��Ϣ")]
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

    //��ʼ��DMItem
    public void initDMitem(int ItemID)
    {
        //���item��Ϣ
        itemID = ItemID;
        if(InventoryManager.Instance.GetItemDetails(itemID) != null)
        {
            itemDetails = InventoryManager.Instance.GetItemDetails(itemID);
        }
        else
        {
            itemDetails = InventoryManager.Instance.GetItemDetails(1101);
        }
        

        //ˢ����Ʒ��
        itemIcon.sprite = itemDetails.ItemIcon;
        itemName.text = itemDetails.ItemName;
        if(itemDetails.canEquip)
        {
            attack.text = "��������" + itemDetails.ItemEquipAttack.ToString();
            defense.text = "��������" + itemDetails.ItemEquipDefense.ToString();
        }
        else if (itemDetails.canWeapon)
        {
            attack.text = "��������" + itemDetails.ItemWeaponAttack.ToString();
            defense.text = "��������" + itemDetails.ItemWeaponDefense.ToString();
        }
        else
        {
            attack.gameObject.SetActive(false);
            defense.gameObject.SetActive(false);
        }
        itemDescribe.text = itemDetails.ItemDescribe;

        saveAmount = Random.Range(1, 8);
        saveAmounttext.text = "��棺" + saveAmount.ToString();

        priceAmount = (int)itemDetails.itemPrice;
        priceAmounttext.text = priceAmount.ToString();

        //���ð�ť
        SetBuyButtonInteractable();
    }


    public void BuyButtonPressed()
    {
        int index = InventoryManager.Instance.GetItemIndexInBag(itemID);
        //�������
        if (InventoryManager.Instance.CheckBagCapacity() || index != -1)
        {
            if (SetBuyButtonInteractable())
            {
                saveAmount--;
                saveAmounttext.text = "��棺" + saveAmount.ToString();
                PlayerProperty.Instance.PlayerWealth -= priceAmount;
                EventHandler.CallPlaySoundEvent(SoundName.MoneyChange);
                SetBuyButtonInteractable();
                InventoryManager.Instance.AddItemAtIndex(itemID, index, 1, false);
            }
            else
            {
                buyButton.interactable = false;
                string mtext = "�ʽ��㡭��";
                MainCanvasUITip.Instance.UITipShowAndDisappear(mtext);
            }
            
        }
        else
        {
            string mtext = "����������";
            MainCanvasUITip.Instance.UITipShowAndDisappear(mtext);
        }
    }

    //���ð�ť
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
