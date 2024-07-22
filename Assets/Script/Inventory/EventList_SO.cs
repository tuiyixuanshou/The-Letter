using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "EventList", menuName = "Inventory/EventList")]

public class EventList_SO : ScriptableObject
{
    public List<HomeEvent> HomeEventList;
}
