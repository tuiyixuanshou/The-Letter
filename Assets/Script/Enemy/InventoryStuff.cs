using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryStuff : MonoBehaviour
{
    public SlotStuff[] slotStuff;
    public int SelectIndex;


    //被选中的序号
    public int HeightLightIndex;

    public Button UsingButton;


    private void Start()
    {
        HeightLightIndex = -1;
        for (int i = 0; i < slotStuff.Length; i++)
        {
            slotStuff[i].index = i;
        }
        UpdateStuffLine(InventoryManager.Instance.weaponItemList_SO.bagItemList);
        usingButtonControl(HeightLightIndex);

    }
    private void OnEnable()
    {
        UpdateStuffLine(InventoryManager.Instance.weaponItemList_SO.bagItemList);
    }

    public void UpdateStuffLine(List<BagItemDetails> list)
    {
        for(int i = slotStuff.Length-1; i > -1; i--)
        {
            if(i == slotStuff[i].index)
            {
                if(list[i].BagItemID != 0)
                {
                    var theItem = InventoryManager.Instance.GetItemDetails(list[i].BagItemID);
                    slotStuff[i].UpdateSlotDetails(theItem,list[i].BagItemAmount);
                }
                else
                {
                    slotStuff[i].UpdateEmptySlot();
                }
            }
            else
            {
                return;
            }
        }
    }

    public int UpdateSlotHighLight(int index)
    {
        int isHLindex = -1;
        foreach (var slot in slotStuff)
        {
            if (slot.isSelected && slot.index == index)
            {
                slot.HightLightImage.gameObject.SetActive(true);
                isHLindex = index;
            }
            else
            {
                slot.isSelected = false;
                slot.HightLightImage.gameObject.SetActive(false);
            }
        }

        return isHLindex;
    }

    public void usingButtonControl(int HLindex)
    {
        //HLindex是点亮的位置
        SelectIndex = HLindex;
        if(HLindex == -1)
        {
            UsingButton.interactable = false;
        }
        else
        {
            UsingButton.interactable = true;
        }
    }

    public void OnUsingButtonPress()
    {
        //点击使用后，高光为-1，关闭stuffLine
        HeightLightIndex = -1;
        UpdateSlotHighLight(HeightLightIndex);
        usingButtonControl(HeightLightIndex);
        this.gameObject.SetActive(false);
    }
}
