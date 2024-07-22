using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryStuff : MonoBehaviour
{
    public SlotStuff[] slotStuff;
    public int SelectIndex;


    //��ѡ�е����
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
        //HLindex�ǵ�����λ��
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
        //���ʹ�ú󣬸߹�Ϊ-1���ر�stuffLine
        HeightLightIndex = -1;
        UpdateSlotHighLight(HeightLightIndex);
        usingButtonControl(HeightLightIndex);
        this.gameObject.SetActive(false);
    }
}
