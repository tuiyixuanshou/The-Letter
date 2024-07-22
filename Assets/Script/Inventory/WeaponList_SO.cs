using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "WeaponList", menuName = "Inventory/WeaponList")]

public class WeaponList_SO : ScriptableObject
{
    public List<WeaponDetails> WeaponDetailsList;
}
