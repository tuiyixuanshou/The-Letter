using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "InfoList", menuName = "Inventory/InfoList")]

public class InfoList_SO : ScriptableObject
{
    public List<InfoDetails> InfoDetailsList;
}
