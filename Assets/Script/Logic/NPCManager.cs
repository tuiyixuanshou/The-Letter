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
        //�������ִ��LayOutInstantiateCustomer��onEnable��
        //�����������Ϸһ���оʹ��̵�Ļ��ᱨ������Ҫ��ֹ��Ϸһ��ʼ�ͽ����̵꣡������
        base.Awake();
        if (CustomerDayList.Count == 0)
        {
            for (int i = 0; i < 14; i++)
            {
                List<CustomerDetails> currentCustomerList = new();

                CustomerDayList.Add(item: currentCustomerList);
            }
        }

        //�������öԻ����Ƿ�����
        foreach(CustomerDetails i in customerList_SO.customerDetailsList)
        {
            i.isSellDone = false;
            foreach(DialogueListElement j in i.dialogueElemList)
            {
                j.isThisDialDone = false;
            }
        }

       
        //��Ϸ��ʼʱ���س�ʼ����
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




    //ע�⣺һ���˲���һ��������Σ�����ͬһ���˵�dialogueIndex�ᱻ���һ�εĸ�ֵ����
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

                    //����������̵�Ի�
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


                    //���������¼�
                    CustomerDayList[0].Add(newcustomer00);
                    Debug.Log(CustomerDayList[0][0].DialogueIndex);
                    Debug.Log(CustomerDayList[0][0].NeedItemDictionary[2303]);
                }
                

                //�ڶ����¼�
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
                    //ɢ��û�жԻ�
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
                    //ɢ��û�жԻ�
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







    //��ʱû����
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
