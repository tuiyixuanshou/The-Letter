using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "NPCList", menuName = "Inventory/NPCList")]
public class NPCList_SO : ScriptableObject
{
    public List<NPCDetails> NPCDetailsList;
}
