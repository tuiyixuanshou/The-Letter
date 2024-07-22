using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class SlotStuff : MonoBehaviour, IPointerClickHandler
{
    public Image StuffImage;
    public Button button;
    public Image HightLightImage;

    public bool isSelected;
    public int index;

    public ItemDetails item;
    public int itemAmount;
    public Text Amount;

    public SlotType slottype;

    private InventoryStuff inventoryStuff => GetComponentInParent<InventoryStuff>();

    private InventoryEquip inventoryEquip => GetComponentInParent<InventoryEquip>();

    private void Start()
    {
        isSelected = false;
        if (item.ItemID == 0)
        {
            UpdateEmptySlot();
        }

        HightLightImage.gameObject.SetActive(false);
    }

    public void UpdateEmptySlot()
    {
        if (isSelected)
        {
            isSelected = false;
        }

        item = new();
        StuffImage.enabled = false;
        button.interactable = false;

        if (slottype == SlotType.Weapon)
        {
            Amount.text = string.Empty;
            itemAmount = 0;
        }
    }

    public void UpdateSlotDetails(ItemDetails itemDetails, int amount)
    {
        item = itemDetails;
        StuffImage.enabled = true;
        StuffImage.sprite = itemDetails.ItemIcon;
        button.interactable = true;

        if (slottype == SlotType.Weapon)
        {
            itemAmount = amount;
            Amount.text = amount.ToString();
        }
    }

    public void UpdateSlotEquipDetails(ItemDetails itemDetails)
    {
        item = itemDetails;
        StuffImage.enabled = true;
        StuffImage.sprite = itemDetails.ItemIcon;
        button.interactable = true;
    }


    public void OnPointerClick(PointerEventData eventData)
    {
        if (item.ItemID == 0)
            return;
        if(slottype == SlotType.Weapon)
        {
            Debug.Log("do click");
            isSelected = !isSelected;
            int HLIndex = inventoryStuff.UpdateSlotHighLight(index);
            inventoryStuff.HeightLightIndex = HLIndex;
            inventoryStuff.usingButtonControl(HLIndex);
        }
        else if(slottype == SlotType.Equipment)
        {
            isSelected = !isSelected;
            inventoryEquip.UpdateSlotHighLight(index);
        }
        
    }
}
