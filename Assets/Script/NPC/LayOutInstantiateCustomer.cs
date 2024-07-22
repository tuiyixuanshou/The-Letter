using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LayOutInstantiateCustomer : MonoBehaviour
{
    public GameObject TradeUIPrefab;

    public GameObject TradeUILayOut;

    public List<CustomerDetails> currentCustomerList = new List<CustomerDetails>();

    private void Awake()
    {
       
    }

    private void OnEnable()
    {
        //Debug.Log("CustomerOnEnable");
        //���ͬһʱ�俪ʼ���NPCmanager��Awake��ִ�У�����һ���ִ�У�����Ȼ�ᱨ����
        //���������������Ϸ������ص���ҳ��Ļ�Ӧ�þ����ⲻ����
        DeletOldCustomer();
        InstantiateCustomerInList();
    }

    private void OnDisable()
    {
        
    }

    private void Start()
    {
       
    }

    /// <summary>
    /// ��������еĿ���
    /// </summary>
    private void DeletOldCustomer()
    {
        Debug.Log("do delet");
        if(this.GetComponentsInChildren<CustomerController>().Length != 0)
        {
            foreach(var i in this.GetComponentsInChildren<CustomerController>())
            {
                Destroy(i.gameObject);
            }
        }
       
    }

    /// <summary>
    /// ����list���������ˢ��
    /// </summary>
    private void InstantiateCustomerInList()
    {
        //ÿ�δ򿪹رն�����ˢ��һ�������б�
        NPCManager.Instance.InitCustomerDayList();
        //Debug.Log(NPCManager.Instance.CustomerDayList);
        //Debug.Log(NPCManager.Instance.CustomerDayList[DayManager.Instance.DayCount - 1]);

        currentCustomerList = new List<CustomerDetails>();
        currentCustomerList = NPCManager.Instance.CustomerDayList[DayManager.Instance.DayCount - 1];

        foreach (var cusDetails in currentCustomerList)
        {
            Debug.Log("do instantiate");
            var newCus = Instantiate(TradeUIPrefab, TradeUILayOut.transform);
            newCus.gameObject.GetComponent<CustomerController>().Init(cusDetails.CustomerID, cusDetails.DialogueIndex);
        }
    }
}
