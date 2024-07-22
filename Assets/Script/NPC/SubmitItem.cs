using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SubmitItem : MonoBehaviour
{
    public int ItemID;
    public ItemDetails itemDetails;
    public Image ItemIcon;
    public Text ItemNameText;
    public Text PossessAmountText;
    public Text RestAmountText;
    public GameObject Jiantou;

    public int PossessAmount;
    public int GiveAmount;
    public int RestAmount;
    
    public void InitSubmitItem(int itemID,int possessamount,int giveamount)
    {
        if (giveamount != 0)
        {
            ItemID = itemID;
             itemDetails = InventoryManager.Instance.GetItemDetails(itemID);
            ItemIcon.gameObject.SetActive(true);
            ItemIcon.sprite = itemDetails.ItemIcon;
            ItemNameText.text = itemDetails.ItemName;

            PossessAmount = possessamount;
            PossessAmountText.text = PossessAmount.ToString();

            GiveAmount = giveamount;

            RestAmount = possessamount - giveamount;
            RestAmountText.text = RestAmount.ToString();
            Jiantou.SetActive(true);
        }
        else
        {
            ItemID = -1;
            ItemIcon.gameObject.SetActive(false);
            ItemNameText.text = string.Empty;
            PossessAmountText.text = string.Empty;
            RestAmountText.text = string.Empty;
            Jiantou.SetActive(false);
        }
    }

    public void DeletItemInBag()
    {
        InventoryManager.Instance.GetItemIndexInBagAndSetRestAmount(ItemID, RestAmount);

    }

}
