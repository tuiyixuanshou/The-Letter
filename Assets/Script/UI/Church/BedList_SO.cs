using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "BedList",menuName = "Inventory/BedList")]

public class BedList_SO : ScriptableObject
{
    public List<ChurchBedDetails> churchBedDetails;
}
