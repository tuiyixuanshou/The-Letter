using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryEquip : MonoBehaviour
{
    public SlotStuff[] slotEquip;
    public Text EquipA;
    public Text EquipD;
    public int equipA;
    public int equipB;

    private void Start()
    {
        //for (int i = 0; i < slotEquip.Length; i++)
        //{
        //    slotEquip[i].index = i;
        //}
        //UpdateEquipLine(InventoryManager.Instance.equipItemList_SO.bagItemList);

    }
    private void OnEnable()
    {
        UpdateEquipText(InventoryManager.Instance.equipItemList_SO.bagItemList);
    }


    private void UpdateEquipText(List<BagItemDetails> list)
    {
        int aNum = 0;
        int dNum = 0;

        for(int i = 0; i < list.Count; i++)
        {
            if(list[i].BagItemID != 0)
            {
                var theItem = InventoryManager.Instance.GetItemDetails(list[i].BagItemID);
                aNum += theItem.ItemEquipAttack;
                dNum += theItem.ItemEquipDefense;
            }
        }
        equipA = aNum;
        equipB = dNum;
        EquipA.text = aNum.ToString();
        EquipD.text = dNum.ToString();
    }


    public void UpdateEquipLine(List<BagItemDetails> list)
    {
        for (int i = slotEquip.Length - 1; i > -1; i--)
        {
            int aNum = 0;
            int dNum = 0;
            if (i == slotEquip[i].index)
            {
                if (list[i].BagItemID != 0)
                {
                    var theItem = InventoryManager.Instance.GetItemDetails(list[i].BagItemID);
                    //slotEquip[i].UpdateSlotEquipDetails(theItem);
                    aNum += theItem.ItemEquipAttack;
                    dNum += theItem.ItemEquipDefense;
                }
                else
                {
                    slotEquip[i].UpdateEmptySlot();
                }


            }
            else
            {
                return;
            }
            equipA = aNum;
            equipB = dNum;
            EquipA.text = aNum.ToString();
            EquipD.text = dNum.ToString();
        }
    }

    public void UpdateSlotHighLight(int index)
    {
        foreach (var slot in slotEquip)
        {
            if (slot.isSelected && slot.index == index)
            {
                slot.HightLightImage.gameObject.SetActive(true);

            }
            else
            {
                slot.isSelected = false;
                slot.HightLightImage.gameObject.SetActive(false);
            }
        }
    }

}
