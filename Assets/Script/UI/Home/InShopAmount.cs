using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InShopAmount : MonoBehaviour
{
    private int NowCustomerAmount;
    public GameObject LayOutCustomer;
    public Text Amount;

    private void Update()
    {
        NowCustomerAmount = LayOutCustomer.GetComponentsInChildren<CustomerController>().Length;
        Amount.text = NowCustomerAmount.ToString();
    }
}
