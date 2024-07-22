using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "ShopList", menuName = "Inventory/ShopList")]

public class ShopList_SO : ScriptableObject
{
    public List<ShopItemDetails> ShopItemDetailsList;
}
