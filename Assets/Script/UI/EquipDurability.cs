using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EquipDurability : MonoBehaviour
{
    public Slider hpSlider;
    public SlotUI theSlot;

    private void OnEnable()
    {
        UpdateDurability();
    }
    public void UpdateDurability()
    {
        if(theSlot.itemDetails.ItemID != 0)
        {
            hpSlider.gameObject.SetActive(true);
            if (theSlot.MaxUse != -1)
            {
                hpSlider.maxValue = theSlot.MaxUse;
                hpSlider.value = theSlot.itemAmount;
            }
            else
            {
                hpSlider.maxValue = 1;
                hpSlider.value = 1;
            }
        }
        else
        {
            hpSlider.gameObject.SetActive(false);
        }
        
        
    }
}
