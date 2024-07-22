using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "CustomerList", menuName = "Inventory/CustomerList")]

public class CustomerList_SO : ScriptableObject
{
    public List<CustomerDetails> customerDetailsList;
}
