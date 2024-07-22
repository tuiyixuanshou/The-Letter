using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DMItemManger : Singleton<DMItemManger>
{
    public List<int> EquipOrWeaponPool;
    List<List<int>> EWDayList = new();

    private void Start()
    {
        for (int i = 0; i < 14; i++)
        {
            //List<int> curEWList = new();
            //EWDayList.Add(item:)
            //List<CustomerDetails> currentCustomerList = new();

            //CustomerDayList.Add(item: currentCustomerList);
        }
    }

}
