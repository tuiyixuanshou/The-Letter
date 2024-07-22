using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "EquipList", menuName = "Inventory/EquipList")]

public class EquipList_SO : ScriptableObject
{
    public List<EquipMentDetails> EquipMentDetailsList;
}
