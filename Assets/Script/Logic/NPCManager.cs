using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class NPCManager : Singleton<NPCManager>
{
    public NPCList_SO npcList_SO;

    public CustomerList_SO customerList_SO;

    public List<List<CustomerDetails>> CustomerDayList = new();

    public bool Day1_yiluma;

    public int TodayUnDeal;

    protected override void Awake()
    {
        //Debug.Log("NPCManagerAwake");
        //这里会先执行LayOutInstantiateCustomer的onEnable，
        //所以如果在游戏一运行就打开商店的话会报错，所以要防止游戏一开始就进入商店！！！！
        base.Awake();
        if (CustomerDayList.Count == 0)
        {
            for (int i = 0; i < 14; i++)
            {
                List<CustomerDetails> currentCustomerList = new();

                CustomerDayList.Add(item: currentCustomerList);
            }
        }

        //重新设置对话和是否被卖出
        foreach(CustomerDetails i in customerList_SO.customerDetailsList)
        {
            i.isSellDone = false;
            foreach(DialogueListElement j in i.dialogueElemList)
            {
                j.isThisDialDone = false;
            }
        }

       
        //游戏开始时加载初始数据
        //InitCustomerDayList();

    }

    private void OnEnable()
    {
        EventHandler.BeforeSceneUnLoad += OnBeforeSceneUnLoad;
        EventHandler.AfterSceneLoad += OnAfterSceneLoad;
        
    }

    private void OnDisable()
    {
        EventHandler.BeforeSceneUnLoad += OnBeforeSceneUnLoad;
        EventHandler.AfterSceneLoad += OnAfterSceneLoad;
    }


    private void Start()
    {
        
    }




    //注意：一个人不能一天出现两次，否则同一个人的dialogueIndex会被最后一次的赋值覆盖
    public void InitCustomerDayList()
    {
        switch (DayManager.Instance.DayCount)
        {
            case 1:
                CustomerDayList[0] = new();
               
                var newcustomer00 = GetCustomerDetailsFromID(1001);
                if (!newcustomer00.isSellDone)
                {
                    if (newcustomer00.NeedItemDictionary == null)
                    {
                        newcustomer00.NeedItemDictionary = new();
                    }
                    else
                    {
                        newcustomer00.NeedItemDictionary.Clear();
                    }

                    //设置伊璐玛商店对话
                    if (!newcustomer00.dialogueElemList[0].isThisDialDone)
                    {
                        newcustomer00.DialogueIndex = 0;
                    }
                    else
                    {
                        newcustomer00.DialogueIndex = -1;
                    }

                    newcustomer00.NeedItemDictionary.Add(2101, 999);
                    newcustomer00.NeedItemDictionary.Add(2102, 999);
                    newcustomer00.NeedItemDictionary.Add(2201, 999);
                    newcustomer00.NeedItemDictionary.Add(2202, 999);
                    newcustomer00.NeedItemDictionary.Add(2301, 999);
                    newcustomer00.NeedItemDictionary.Add(2302, 999);
                    newcustomer00.NeedItemDictionary.Add(2303, 998);
                    newcustomer00.NeedItemDictionary.Add(2304, 999);


                    //添加伊璐玛事件
                    CustomerDayList[0].Add(newcustomer00);
                    Debug.Log(CustomerDayList[0][0].DialogueIndex);
                    Debug.Log(CustomerDayList[0][0].NeedItemDictionary[2303]);
                }
                

                //第二个事件
                var newcustomer01 = GetCustomerDetailsFromID(1002);
                if (!newcustomer01.isSellDone)
                {
                    if (newcustomer01.NeedItemDictionary == null)
                    {
                        newcustomer01.NeedItemDictionary = new();
                    }
                    else
                    {
                        newcustomer01.NeedItemDictionary.Clear();
                    }
                    //散户没有对话
                    newcustomer01.DialogueIndex = -1;

                    newcustomer01.NeedItemDictionary.Add(1101,2);
                    CustomerDayList[0].Add(newcustomer01);
                }

                var newcustomer02 = GetCustomerDetailsFromID(1003);
                if (!newcustomer02.isSellDone)
                {
                    if (newcustomer02.NeedItemDictionary == null)
                    {
                        newcustomer02.NeedItemDictionary = new();
                    }
                    else
                    {
                        newcustomer02.NeedItemDictionary.Clear();
                    }
                    //散户没有对话
                    newcustomer02.DialogueIndex = -1;

                    newcustomer02.NeedItemDictionary.Add(2302, 1);
                    CustomerDayList[0].Add(newcustomer02);
                }

                TodayUnDeal = CustomerDayList[0].Count;

                break;
            case 2:
                
                break;
            case 3:
                break;
            case 4:
                break;
            case 5:
                break;
            case 6:
                break;
            case 7:
                break;
            case 8:
                break;
            case 9:
                break;
            case 10:
                break;
            case 11:
                break;
            case 12:
                break;
            case 13:
                break;
            case 14:
                break;
            default:
                break;
        }


    }

  
    public void MarkDoneDialogue(int CusID,int dialIndex)
    {
        customerList_SO.customerDetailsList.Find(i => i.CustomerID == CusID).dialogueElemList[dialIndex].isThisDialDone = true;
    }







    //暂时没用上
    public  CustomerDetails GetSingleDialogue(int NPCID)
    {
        return GetCustomerDetailsFromID(NPCID);
    }




    public NPCDetails GetNPCDetailsFromID(int NPCID)
    {
        return npcList_SO.NPCDetailsList.Find(i => i.NPCID == NPCID);
    }

    public CustomerDetails GetCustomerDetailsFromID(int cusID)
    {
        return customerList_SO.customerDetailsList.Find(i => i.CustomerID == cusID);
    }

    private void OnBeforeSceneUnLoad()
    {

    }

    private void OnAfterSceneLoad()
    {
        
    }

}
