using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NeedItemController : MonoBehaviour
{
    public int ItemID;
    public Text ItemName;
    public Text NeedAmount;
    public Text PossessAmount;
    public Text GiveAmountText;

    public Button PlusButton;
    public Button MinuButton;
    //要给的物品数量
    public int GiveAmount;
    //从背包里面查找拥有的数量
    public int PossessAmountInt;
    private ItemDetails itemDetails;

    public int NeedItemIndex;

    public float itemPrice;

    public void InitNeedItem(int itemId,int itemAmount,int needItemIndex)
    {
        ItemID = itemId;
        GiveAmount = 0;
        itemDetails = InventoryManager.Instance.GetItemDetails(ItemID);
        ItemName.text = itemDetails.ItemName;
        NeedAmount.text = itemAmount.ToString();
        GiveAmountText.text = GiveAmount.ToString();
        CheckPossessAmountInBag();

        MinuButton.interactable = false;
        if(PossessAmountInt == 0)
        {
            PlusButton.interactable = false;
        }
        NeedItemIndex = needItemIndex;
        itemPrice = itemDetails.itemPrice;
    }

    public void PlusButtonPressed()
    {
       
        GiveAmount++;
        GiveAmountText.text = GiveAmount.ToString();
        calculatePrice((int)itemPrice);
        if (GiveAmount >= PossessAmountInt)
        {
            PlusButton.interactable = false;
        }
        if(GiveAmount <= PossessAmountInt)
        {
            MinuButton.interactable = true;
        }

    }

    public void MinusButtonPressed()
    {
        GiveAmount--;
        GiveAmountText.text = GiveAmount.ToString();
        calculatePrice((int)-itemPrice);
        if (GiveAmount == 0)
        {
            MinuButton.interactable = false;
        }
        if (GiveAmount >= 0)
        {
            PlusButton.interactable = true;
        }
    }

    public void CheckPossessAmountInBag()
    {
        PossessAmountInt= InventoryManager.Instance.GetItemAmountInBag(ItemID);
        PossessAmount.text = PossessAmountInt.ToString();
    }

    public void SubmitButtonPressed()
    {
    }


    public void calculatePrice(int newPriceChange)
    {
        int currentPrice = this.transform.parent.parent.gameObject.GetComponent<CustomerController>().totalPrice;
        this.transform.parent.parent.gameObject.GetComponent<CustomerController>().totalPrice = currentPrice + newPriceChange;
        this.transform.parent.parent.gameObject.GetComponent<CustomerController>().UpdatePriceTextInNeedItemController();
    }
}
