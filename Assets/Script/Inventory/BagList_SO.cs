using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName ="BagList",menuName = "Inventory/BagList")]
public class BagList_SO : ScriptableObject
{
    public List<BagItemDetails> bagItemList;
}
