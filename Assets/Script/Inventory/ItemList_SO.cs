using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName ="ItemList",menuName ="Inventory/ItemList")]
public class ItemList_SO : ScriptableObject
{
    public List<ItemDetails> itemDetailsList;
}
