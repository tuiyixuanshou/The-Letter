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
        //如果同一时间开始会比NPCmanager的Awake先执行，不能一起打开执行！！不然会报错！！
        //但是如果后期有游戏进入加载的主页面的话应该就问题不大了
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
    /// 先清除已有的客人
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
    /// 根据list里面的内容刷新
    /// </summary>
    private void InstantiateCustomerInList()
    {
        //每次打开关闭都重新刷新一遍内置列表
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
