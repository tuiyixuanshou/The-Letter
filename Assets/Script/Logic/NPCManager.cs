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

        EmptyOriData();

        //游戏开始时加载初始数据
        //InitCustomerDayList();

    }

    private void EmptyOriData()
    {
        //重新设置对话和是否被卖出
        foreach (CustomerDetails i in customerList_SO.customerDetailsList)
        {
            i.isSellDone = false;
            foreach (DialogueListElement j in i.dialogueElemList)
            {
                j.isThisDialDone = false;
            }
        }

        //重新设置npc对话是否发生过
        foreach (NPCDetails i in npcList_SO.NPCDetailsList)
        {
            i.isThisAppear = false;
        }

    }

    private void OnEnable()
    {
        EventHandler.BeforeSceneUnLoad += OnBeforeSceneUnLoad;
        EventHandler.AfterSceneLoad += OnAfterSceneLoad;
        EventHandler.NewGameEmptyData += EmptyOriData;


    }

    private void OnDisable()
    {
        EventHandler.BeforeSceneUnLoad -= OnBeforeSceneUnLoad;
        EventHandler.AfterSceneLoad -= OnAfterSceneLoad;
        EventHandler.NewGameEmptyData -= EmptyOriData;
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

                    newcustomer00.NeedItemDictionary.Add(2101, 20);
                    newcustomer00.NeedItemDictionary.Add(2102, 20);
                    newcustomer00.NeedItemDictionary.Add(2201, 20);
                    newcustomer00.NeedItemDictionary.Add(2202, 20);
                    newcustomer00.NeedItemDictionary.Add(2301, 20);
                    newcustomer00.NeedItemDictionary.Add(2302, 20);
                    newcustomer00.NeedItemDictionary.Add(2303, 20);
                    newcustomer00.NeedItemDictionary.Add(2304, 20);


                    //添加伊璐玛事件
                    CustomerDayList[0].Add(newcustomer00);
                    //Debug.Log(CustomerDayList[0][0].DialogueIndex);
                    //Debug.Log(CustomerDayList[0][0].NeedItemDictionary[2303]);
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
                //第三个事件
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
                CustomerDayList[1] = new();

                //第二天 商超老板
                var newcustomer10 = GetCustomerDetailsFromID(1004);
                if (!newcustomer10.isSellDone)
                {
                    if (newcustomer10.NeedItemDictionary == null)
                    {
                        newcustomer10.NeedItemDictionary = new();
                    }
                    else
                    {
                        newcustomer10.NeedItemDictionary.Clear();
                    }
                    //设置彪哥商店对话
                    if (!newcustomer10.dialogueElemList[0].isThisDialDone)
                    {
                        newcustomer10.DialogueIndex = 0;
                    }
                    else
                    {
                        newcustomer10.DialogueIndex = -1;
                    }
                    newcustomer10.NeedItemDictionary.Add(1101, 40);
                    newcustomer10.NeedItemDictionary.Add(1102, 40);
                    newcustomer10.NeedItemDictionary.Add(1103, 40);
                    newcustomer10.NeedItemDictionary.Add(1104, 40);
                    newcustomer10.NeedItemDictionary.Add(1201, 40);
                    newcustomer10.NeedItemDictionary.Add(1202, 40);
                    newcustomer10.NeedItemDictionary.Add(1203, 40);
                    newcustomer10.NeedItemDictionary.Add(1301, 40);
                    //添加彪哥事件
                    CustomerDayList[1].Add(newcustomer10);
                }
                //杂货店老板
                var newcustomer11 = GetCustomerDetailsFromID(1005);
                if (!newcustomer11.isSellDone)
                {
                    if (newcustomer11.NeedItemDictionary == null)
                    {
                        newcustomer11.NeedItemDictionary = new();
                    }
                    else
                    {
                        newcustomer11.NeedItemDictionary.Clear();
                    }
                    if (!newcustomer11.dialogueElemList[0].isThisDialDone)
                    {
                        newcustomer11.DialogueIndex = 0;
                    }
                    else
                    {
                        newcustomer11.DialogueIndex = -1;
                    }
                    newcustomer11.NeedItemDictionary.Add(3101, 30);
                    newcustomer11.NeedItemDictionary.Add(3302, 30);
                    newcustomer11.NeedItemDictionary.Add(3304, 30);
                    newcustomer11.NeedItemDictionary.Add(3305, 30);
                    newcustomer11.NeedItemDictionary.Add(3306, 30);
                    newcustomer11.NeedItemDictionary.Add(4101, 30);
                    newcustomer11.NeedItemDictionary.Add(4102, 30);
                    newcustomer11.NeedItemDictionary.Add(4302, 30);
                    CustomerDayList[1].Add(newcustomer11);
                }
                var newcustomer12 = GetCustomerDetailsFromID(1006);
                if (!newcustomer12.isSellDone)
                {
                    if (newcustomer12.NeedItemDictionary == null)
                    {
                        newcustomer12.NeedItemDictionary = new();
                    }
                    else
                    {
                        newcustomer12.NeedItemDictionary.Clear();
                    }
                    newcustomer12.DialogueIndex = -1;

                    newcustomer12.NeedItemDictionary.Add(2101, 2);
                    newcustomer12.NeedItemDictionary.Add(2102, 1);
                    CustomerDayList[1].Add(newcustomer12);
                }

                TodayUnDeal = CustomerDayList[1].Count;
                break;
            case 3:
                CustomerDayList[2] = new();
                //凯子
                var newcustomer20 = GetCustomerDetailsFromID(1007);
                if (!newcustomer20.isSellDone)
                {
                    if (newcustomer20.NeedItemDictionary == null)
                    {
                        newcustomer20.NeedItemDictionary = new();
                    }
                    else
                    {
                        newcustomer20.NeedItemDictionary.Clear();
                    }
                    if (!newcustomer20.dialogueElemList[0].isThisDialDone)
                    {
                        newcustomer20.DialogueIndex = 0;
                    }
                    else
                    {
                        newcustomer20.DialogueIndex = -1;
                    }
                    newcustomer20.NeedItemDictionary.Add(2102, 5);
                    newcustomer20.NeedItemDictionary.Add(2103, 5);
                    newcustomer20.NeedItemDictionary.Add(2104, 5);
                    newcustomer20.NeedItemDictionary.Add(2202, 5);
                    newcustomer20.NeedItemDictionary.Add(2203, 5);
                    newcustomer20.NeedItemDictionary.Add(2204, 5);
                    newcustomer20.NeedItemDictionary.Add(2301, 5);
                    newcustomer20.NeedItemDictionary.Add(2302, 5);
                    CustomerDayList[2].Add(newcustomer20);
                }
                var newcustomer21 = GetCustomerDetailsFromID(1008);
                if (!newcustomer21.isSellDone)
                {
                    if (newcustomer21.NeedItemDictionary == null)
                    {
                        newcustomer21.NeedItemDictionary = new();
                    }
                    else
                    {
                        newcustomer21.NeedItemDictionary.Clear();
                    }
                    newcustomer21.DialogueIndex = -1;

                    newcustomer21.NeedItemDictionary.Add(3101, 30);
                    newcustomer21.NeedItemDictionary.Add(3305, 30);
                    newcustomer21.NeedItemDictionary.Add(3306, 30);
                    newcustomer21.NeedItemDictionary.Add(4102, 30);
                    newcustomer21.NeedItemDictionary.Add(4302, 30);
                    CustomerDayList[2].Add(newcustomer21);
                }
                var newcustomer22 = GetCustomerDetailsFromID(1009);
                if (!newcustomer22.isSellDone)
                {
                    if (newcustomer22.NeedItemDictionary == null)
                    {
                        newcustomer22.NeedItemDictionary = new();
                    }
                    else
                    {
                        newcustomer22.NeedItemDictionary.Clear();
                    }
                    newcustomer22.DialogueIndex = -1;

                    newcustomer22.NeedItemDictionary.Add(1101, 2);
                    newcustomer22.NeedItemDictionary.Add(1104, 1);
                    newcustomer22.NeedItemDictionary.Add(1201, 3);
                    CustomerDayList[2].Add(newcustomer22);
                }

                TodayUnDeal = CustomerDayList[2].Count;
                break;
            case 4:
                CustomerDayList[3] = new();
                var newcustomer30 = GetCustomerDetailsFromID(1010);
                if (!newcustomer30.isSellDone)
                {
                    if (newcustomer30.NeedItemDictionary == null)
                    {
                        newcustomer30.NeedItemDictionary = new();
                    }
                    else
                    {
                        newcustomer30.NeedItemDictionary.Clear();
                    }
                    newcustomer30.DialogueIndex = -1;

                    newcustomer30.NeedItemDictionary.Add(1101, 20);
                    newcustomer30.NeedItemDictionary.Add(1102, 20);
                    newcustomer30.NeedItemDictionary.Add(1103, 20);
                    newcustomer30.NeedItemDictionary.Add(1202, 20);
                    newcustomer30.NeedItemDictionary.Add(1203, 20);
                    newcustomer30.NeedItemDictionary.Add(1301, 20);
                    CustomerDayList[3].Add(newcustomer30);
                }
                var newcustomer31 = GetCustomerDetailsFromID(1011);
                if (!newcustomer31.isSellDone)
                {
                    if (newcustomer31.NeedItemDictionary == null)
                    {
                        newcustomer31.NeedItemDictionary = new();
                    }
                    else
                    {
                        newcustomer31.NeedItemDictionary.Clear();
                    }
                    newcustomer31.DialogueIndex = -1;

                    newcustomer31.NeedItemDictionary.Add(1201, 7);
                    CustomerDayList[3].Add(newcustomer31);
                }
                var newcustomer32 = GetCustomerDetailsFromID(1012);
                if (!newcustomer32.isSellDone)
                {
                    if (newcustomer32.NeedItemDictionary == null)
                    {
                        newcustomer32.NeedItemDictionary = new();
                    }
                    else
                    {
                        newcustomer32.NeedItemDictionary.Clear();
                    }
                    newcustomer32.DialogueIndex = -1;

                    newcustomer32.NeedItemDictionary.Add(1203, 1);
                    newcustomer32.NeedItemDictionary.Add(1202, 3);
                    newcustomer32.NeedItemDictionary.Add(1201, 2);
                    CustomerDayList[3].Add(newcustomer32);
                }

                TodayUnDeal = CustomerDayList[3].Count;
                break;
            case 5:
                CustomerDayList[4] = new();
                var newcustomer40 = GetCustomerDetailsFromID(1013);
                if (!newcustomer40.isSellDone)
                {
                    if (newcustomer40.NeedItemDictionary == null)
                    {
                        newcustomer40.NeedItemDictionary = new();
                    }
                    else
                    {
                        newcustomer40.NeedItemDictionary.Clear();
                    }
                   
                    if (!newcustomer40.dialogueElemList[0].isThisDialDone)
                    {
                        newcustomer40.DialogueIndex = 0;
                    }
                    else
                    {
                        newcustomer40.DialogueIndex = -1;
                    }
                    newcustomer40.NeedItemDictionary.Add(2104, 5);
                    newcustomer40.NeedItemDictionary.Add(2202, 5);
                    newcustomer40.NeedItemDictionary.Add(2203, 5);
                    newcustomer40.NeedItemDictionary.Add(2204, 5);
                    newcustomer40.NeedItemDictionary.Add(2301, 5);
                    newcustomer40.NeedItemDictionary.Add(2302, 5);
                    CustomerDayList[4].Add(newcustomer40);
                }
                var newcustomer41 = GetCustomerDetailsFromID(1014);
                if (!newcustomer41.isSellDone)
                {
                    if (newcustomer41.NeedItemDictionary == null)
                    {
                        newcustomer41.NeedItemDictionary = new();
                    }
                    else
                    {
                        newcustomer41.NeedItemDictionary.Clear();
                    }
                    newcustomer41.DialogueIndex = -1;

                    newcustomer41.NeedItemDictionary.Add(3304, 1);
                    CustomerDayList[4].Add(newcustomer41);
                }
               
                TodayUnDeal = CustomerDayList[4].Count;
                break;
            case 6:
                CustomerDayList[5] = new();
                var newcustomer50 = GetCustomerDetailsFromID(1015);
                if (!newcustomer50.isSellDone)
                {
                    if (newcustomer50.NeedItemDictionary == null)
                    {
                        newcustomer50.NeedItemDictionary = new();
                    }
                    else
                    {
                        newcustomer50.NeedItemDictionary.Clear();
                    }

                    if (!newcustomer50.dialogueElemList[0].isThisDialDone)
                    {
                        newcustomer50.DialogueIndex = 0;
                    }
                    else
                    {
                        newcustomer50.DialogueIndex = -1;
                    }
                    newcustomer50.NeedItemDictionary.Add(3101, 30);
                    newcustomer50.NeedItemDictionary.Add(3302, 30);
                    newcustomer50.NeedItemDictionary.Add(3304, 30);
                    newcustomer50.NeedItemDictionary.Add(3305, 30);
                    newcustomer50.NeedItemDictionary.Add(3306, 30);
                    newcustomer50.NeedItemDictionary.Add(4101, 30);
                    newcustomer50.NeedItemDictionary.Add(4102, 30);
                    newcustomer50.NeedItemDictionary.Add(4302, 30);
                    CustomerDayList[5].Add(newcustomer50);
                }
                var newcustomer51 = GetCustomerDetailsFromID(1016);
                if (!newcustomer51.isSellDone)
                {
                    if (newcustomer51.NeedItemDictionary == null)
                    {
                        newcustomer51.NeedItemDictionary = new();
                    }
                    else
                    {
                        newcustomer51.NeedItemDictionary.Clear();
                    }
                    newcustomer51.DialogueIndex = -1;

                    newcustomer51.NeedItemDictionary.Add(1101, 3);
                    newcustomer51.NeedItemDictionary.Add(1103, 1);
                    CustomerDayList[5].Add(newcustomer51);
                }
                var newcustomer52 = GetCustomerDetailsFromID(1017);
                if (!newcustomer52.isSellDone)
                {
                    if (newcustomer52.NeedItemDictionary == null)
                    {
                        newcustomer52.NeedItemDictionary = new();
                    }
                    else
                    {
                        newcustomer52.NeedItemDictionary.Clear();
                    }
                    newcustomer52.DialogueIndex = -1;

                    newcustomer52.NeedItemDictionary.Add(3302, 10);
                    newcustomer52.NeedItemDictionary.Add(4101, 4);
                    newcustomer52.NeedItemDictionary.Add(3306, 1);
                    newcustomer52.NeedItemDictionary.Add(3307, 1);
                    CustomerDayList[5].Add(newcustomer52);
                }
                TodayUnDeal = CustomerDayList[5].Count;
                break;
            case 7:
                CustomerDayList[6] = new();
                var newcustomer60 = GetCustomerDetailsFromID(1018);
                if (!newcustomer60.isSellDone)
                {
                    if (newcustomer60.NeedItemDictionary == null)
                    {
                        newcustomer60.NeedItemDictionary = new();
                    }
                    else
                    {
                        newcustomer60.NeedItemDictionary.Clear();
                    }
                    newcustomer60.DialogueIndex = -1;
                    newcustomer60.NeedItemDictionary.Add(2102, 5);
                    newcustomer60.NeedItemDictionary.Add(2103, 5);
                    newcustomer60.NeedItemDictionary.Add(2104, 5);
                    newcustomer60.NeedItemDictionary.Add(2202, 5);
                    newcustomer60.NeedItemDictionary.Add(2204, 5);
                    newcustomer60.NeedItemDictionary.Add(2301, 5);
                    CustomerDayList[6].Add(newcustomer60);
                }
                var newcustomer61 = GetCustomerDetailsFromID(1019);
                if (!newcustomer61.isSellDone)
                {
                    if (newcustomer61.NeedItemDictionary == null)
                    {
                        newcustomer61.NeedItemDictionary = new();
                    }
                    else
                    {
                        newcustomer61.NeedItemDictionary.Clear();
                    }
                    newcustomer61.DialogueIndex = -1;

                    newcustomer61.NeedItemDictionary.Add(2101, 3);
                    CustomerDayList[6].Add(newcustomer61);
                }
                TodayUnDeal = CustomerDayList[6].Count;
                break;
            case 8:
                CustomerDayList[7] = new();
                var newcustomer70 = GetCustomerDetailsFromID(1020);
                if (!newcustomer70.isSellDone)
                {
                    if (newcustomer70.NeedItemDictionary == null)
                    {
                        newcustomer70.NeedItemDictionary = new();
                    }
                    else
                    {
                        newcustomer70.NeedItemDictionary.Clear();
                    }
                    if (!newcustomer70.dialogueElemList[0].isThisDialDone)
                    {
                        newcustomer70.DialogueIndex = 0;
                    }
                    else
                    {
                        newcustomer70.DialogueIndex = -1;
                    }
                    newcustomer70.NeedItemDictionary.Add(3101, 30);
                    newcustomer70.NeedItemDictionary.Add(3305, 30);
                    newcustomer70.NeedItemDictionary.Add(3306, 30);
                    newcustomer70.NeedItemDictionary.Add(4101, 30);
                    CustomerDayList[7].Add(newcustomer70);
                }
                var newcustomer71 = GetCustomerDetailsFromID(1021);
                if (!newcustomer71.isSellDone)
                {
                    if (newcustomer71.NeedItemDictionary == null)
                    {
                        newcustomer71.NeedItemDictionary = new();
                    }
                    else
                    {
                        newcustomer71.NeedItemDictionary.Clear();
                    }
                    newcustomer71.DialogueIndex = -1;
                    newcustomer71.NeedItemDictionary.Add(2101, 20);
                    newcustomer71.NeedItemDictionary.Add(2102, 20);
                    newcustomer71.NeedItemDictionary.Add(2201, 20);
                    newcustomer71.NeedItemDictionary.Add(2202, 20);
                    newcustomer71.NeedItemDictionary.Add(2301, 20);
                    newcustomer71.NeedItemDictionary.Add(2302, 20);
                    newcustomer71.NeedItemDictionary.Add(2303, 20);
                    newcustomer71.NeedItemDictionary.Add(2304, 20);
                    CustomerDayList[7].Add(newcustomer71);
                }
                var newcustomer72 = GetCustomerDetailsFromID(1022);
                if (!newcustomer72.isSellDone)
                {
                    if (newcustomer72.NeedItemDictionary == null)
                    {
                        newcustomer72.NeedItemDictionary = new();
                    }
                    else
                    {
                        newcustomer72.NeedItemDictionary.Clear();
                    }
                    newcustomer72.DialogueIndex = -1;
                    newcustomer72.NeedItemDictionary.Add(1201, 5);
                    newcustomer72.NeedItemDictionary.Add(1104, 2);
                    CustomerDayList[7].Add(newcustomer72);
                }
                TodayUnDeal = CustomerDayList[7].Count;
                break;
            case 9:
                CustomerDayList[8] = new();
                var newcustomer80 = GetCustomerDetailsFromID(1023);
                if (!newcustomer80.isSellDone)
                {
                    if (newcustomer80.NeedItemDictionary == null)
                    {
                        newcustomer80.NeedItemDictionary = new();
                    }
                    else
                    {
                        newcustomer80.NeedItemDictionary.Clear();
                    }
                    if (!newcustomer80.dialogueElemList[0].isThisDialDone)
                    {
                        newcustomer80.DialogueIndex = 0;
                    }
                    else
                    {
                        newcustomer80.DialogueIndex = -1;
                    }
                    newcustomer80.NeedItemDictionary.Add(2101, 20);
                    newcustomer80.NeedItemDictionary.Add(2102, 20);
                    newcustomer80.NeedItemDictionary.Add(2201, 20);
                    newcustomer80.NeedItemDictionary.Add(2202, 20);
                    newcustomer80.NeedItemDictionary.Add(2301, 20);
                    newcustomer80.NeedItemDictionary.Add(2302, 20);
                    newcustomer80.NeedItemDictionary.Add(2303, 20);
                    newcustomer80.NeedItemDictionary.Add(2304, 20);
                    CustomerDayList[8].Add(newcustomer80);
                }
                var newcustomer81 = GetCustomerDetailsFromID(1024);
                if (!newcustomer81.isSellDone)
                {
                    if (newcustomer81.NeedItemDictionary == null)
                    {
                        newcustomer81.NeedItemDictionary = new();
                    }
                    else
                    {
                        newcustomer81.NeedItemDictionary.Clear();
                    }
                    if (!newcustomer81.dialogueElemList[0].isThisDialDone)
                    {
                        newcustomer81.DialogueIndex = 0;
                    }
                    else
                    {
                        newcustomer81.DialogueIndex = -1;
                    }
                    newcustomer81.NeedItemDictionary.Add(4304, 1);
                    newcustomer81.NeedItemDictionary.Add(4303, 1);
                    CustomerDayList[8].Add(newcustomer81);
                }
                TodayUnDeal = CustomerDayList[8].Count;
                break;
            case 10:
                CustomerDayList[9] = new();
                var newcustomer90 = GetCustomerDetailsFromID(1025);
                if (!newcustomer90.isSellDone)
                {
                    if (newcustomer90.NeedItemDictionary == null)
                    {
                        newcustomer90.NeedItemDictionary = new();
                    }
                    else
                    {
                        newcustomer90.NeedItemDictionary.Clear();
                    }
                    if (!newcustomer90.dialogueElemList[0].isThisDialDone)
                    {
                        newcustomer90.DialogueIndex = 0;
                    }
                    else
                    {
                        newcustomer90.DialogueIndex = -1;
                    }
                    newcustomer90.NeedItemDictionary.Add(1101, 40);
                    newcustomer90.NeedItemDictionary.Add(1102, 40);
                    newcustomer90.NeedItemDictionary.Add(1103, 40);
                    newcustomer90.NeedItemDictionary.Add(1104, 40);
                    newcustomer90.NeedItemDictionary.Add(1201, 40);
                    newcustomer90.NeedItemDictionary.Add(1202, 40);
                    newcustomer90.NeedItemDictionary.Add(1203, 40);
                    newcustomer90.NeedItemDictionary.Add(1301, 40);
                    CustomerDayList[9].Add(newcustomer90);
                }
                var newcustomer91 = GetCustomerDetailsFromID(1026);
                if (!newcustomer91.isSellDone)
                {
                    if (newcustomer91.NeedItemDictionary == null)
                    {
                        newcustomer91.NeedItemDictionary = new();
                    }
                    else
                    {
                        newcustomer91.NeedItemDictionary.Clear();
                    }
                    if (!newcustomer91.dialogueElemList[0].isThisDialDone)
                    {
                        newcustomer91.DialogueIndex = 0;
                    }
                    else
                    {
                        newcustomer91.DialogueIndex = -1;
                    }
                    newcustomer91.NeedItemDictionary.Add(3101, 30);
                    newcustomer91.NeedItemDictionary.Add(3302, 30);
                    newcustomer91.NeedItemDictionary.Add(3305, 30);
                    newcustomer91.NeedItemDictionary.Add(4102, 30);
                    newcustomer91.NeedItemDictionary.Add(4302, 30);
                    CustomerDayList[9].Add(newcustomer91);
                }
                TodayUnDeal = CustomerDayList[9].Count;
                break;
            case 11:
                CustomerDayList[10] = new();
                var newcustomer100 = GetCustomerDetailsFromID(1027);
                if (!newcustomer100.isSellDone)
                {
                    if (newcustomer100.NeedItemDictionary == null)
                    {
                        newcustomer100.NeedItemDictionary = new();
                    }
                    else
                    {
                        newcustomer100.NeedItemDictionary.Clear();
                    }
                    if (!newcustomer100.dialogueElemList[0].isThisDialDone)
                    {
                        newcustomer100.DialogueIndex = 0;
                    }
                    else
                    {
                        newcustomer100.DialogueIndex = -1;
                    }
                    newcustomer100.NeedItemDictionary.Add(2102, 5);
                    newcustomer100.NeedItemDictionary.Add(2103, 5);
                    newcustomer100.NeedItemDictionary.Add(2104, 5);
                    newcustomer100.NeedItemDictionary.Add(2202, 5);
                    newcustomer100.NeedItemDictionary.Add(2203, 5);
                    newcustomer100.NeedItemDictionary.Add(2204, 5);
                    newcustomer100.NeedItemDictionary.Add(2301, 5);
                    newcustomer100.NeedItemDictionary.Add(2302, 5);
                    CustomerDayList[10].Add(newcustomer100);
                }
                var newcustomer101 = GetCustomerDetailsFromID(1028);
                if (!newcustomer101.isSellDone)
                {
                    if (newcustomer101.NeedItemDictionary == null)
                    {
                        newcustomer101.NeedItemDictionary = new();
                    }
                    else
                    {
                        newcustomer101.NeedItemDictionary.Clear();
                    }
                    if (!newcustomer101.dialogueElemList[0].isThisDialDone)
                    {
                        newcustomer101.DialogueIndex = 0;
                    }
                    else
                    {
                        newcustomer101.DialogueIndex = -1;
                    }
                    newcustomer101.NeedItemDictionary.Add(2101, 60);
                    newcustomer101.NeedItemDictionary.Add(2102, 60);
                    newcustomer101.NeedItemDictionary.Add(2104, 60);
                    newcustomer101.NeedItemDictionary.Add(2203, 60);
                    newcustomer101.NeedItemDictionary.Add(2204, 60);
                    CustomerDayList[10].Add(newcustomer101);
                }
                TodayUnDeal = CustomerDayList[10].Count;
                break;
            case 12:
                CustomerDayList[11] = new();
                var newcustomer110 = GetCustomerDetailsFromID(1029);
                if (!newcustomer110.isSellDone)
                {
                    if (newcustomer110.NeedItemDictionary == null)
                    {
                        newcustomer110.NeedItemDictionary = new();
                    }
                    else
                    {
                        newcustomer110.NeedItemDictionary.Clear();
                    }
                    newcustomer110.DialogueIndex = -1;
                    newcustomer110.NeedItemDictionary.Add(1202, 3);
                    CustomerDayList[11].Add(newcustomer110);
                }
                var newcustomer111 = GetCustomerDetailsFromID(1030);
                if (!newcustomer111.isSellDone)
                {
                    if (newcustomer111.NeedItemDictionary == null)
                    {
                        newcustomer111.NeedItemDictionary = new();
                    }
                    else
                    {
                        newcustomer111.NeedItemDictionary.Clear();
                    }
                    if (!newcustomer111.dialogueElemList[0].isThisDialDone)
                    {
                        newcustomer111.DialogueIndex = 0;
                    }
                    else
                    {
                        newcustomer111.DialogueIndex = -1;
                    }
                    newcustomer111.NeedItemDictionary.Add(3305, 20);
                    newcustomer111.NeedItemDictionary.Add(3306, 20);
                    newcustomer111.NeedItemDictionary.Add(3307, 20);
                    newcustomer111.NeedItemDictionary.Add(4101, 20);
                    newcustomer111.NeedItemDictionary.Add(4302, 20);
                    CustomerDayList[11].Add(newcustomer111);
                }
                TodayUnDeal = CustomerDayList[11].Count;
                break;
            case 13:
                CustomerDayList[12] = new();
                var newcustomer120 = GetCustomerDetailsFromID(1031);
                if (!newcustomer120.isSellDone)
                {
                    if (newcustomer120.NeedItemDictionary == null)
                    {
                        newcustomer120.NeedItemDictionary = new();
                    }
                    else
                    {
                        newcustomer120.NeedItemDictionary.Clear();
                    }
                    newcustomer120.DialogueIndex = -1;
                    newcustomer120.NeedItemDictionary.Add(1101, 40);
                    newcustomer120.NeedItemDictionary.Add(1102, 40);
                    newcustomer120.NeedItemDictionary.Add(1103, 40);
                    newcustomer120.NeedItemDictionary.Add(1104, 40);
                    newcustomer120.NeedItemDictionary.Add(1201, 40);
                    newcustomer120.NeedItemDictionary.Add(1202, 40);
                    newcustomer120.NeedItemDictionary.Add(1203, 40);
                    newcustomer120.NeedItemDictionary.Add(1301, 40);
                    CustomerDayList[12].Add(newcustomer120);
                }
                var newcustomer121 = GetCustomerDetailsFromID(1032);
                if (!newcustomer121.isSellDone)
                {
                    if (newcustomer121.NeedItemDictionary == null)
                    {
                        newcustomer121.NeedItemDictionary = new();
                    }
                    else
                    {
                        newcustomer121.NeedItemDictionary.Clear();
                    }
                    if (!newcustomer121.dialogueElemList[0].isThisDialDone)
                    {
                        newcustomer121.DialogueIndex = 0;
                    }
                    else
                    {
                        newcustomer121.DialogueIndex = -1;
                    }
                    newcustomer121.NeedItemDictionary.Add(2101, 5);
                    CustomerDayList[12].Add(newcustomer121);
                }
                TodayUnDeal = CustomerDayList[12].Count;
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

    public bool CheckNPCDoneDialogue(int ID)
    {
        return npcList_SO.NPCDetailsList.Find(i => i.NPCID == ID).isThisAppear;
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
