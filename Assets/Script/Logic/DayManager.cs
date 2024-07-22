using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class DayManager : Singleton<DayManager>
{
    [Header("当前天数")]
    public int DayCount = 1;

    [Header("HomeEvent")]
    public Transform ParentCanvas;
    public Button HomeEventButtonPrefab;
    [SerializeField] private EventList_SO eventList_SO;
    [SerializeField] private NPCList_SO npcList_SO;

    [Header("NPC")]
    public Transform NpcParent;
    public GameObject NPC;
    public int NPCNum;
    public int NPCCurNum;

    private void OnEnable()
    {
        EventHandler.BeforeSceneUnLoad += OnBeforeSceneUnLoad;
        EventHandler.AfterSceneLoad += OnAfterSceneLoad;
    }

    private void OnDisable()
    {
        EventHandler.BeforeSceneUnLoad -= OnBeforeSceneUnLoad;
        EventHandler.AfterSceneLoad -= OnAfterSceneLoad;
    }

    private void Start()
    {
        if (GameObject.FindGameObjectWithTag("HomeEventParent") != null)
        {
            ParentCanvas = GameObject.FindGameObjectWithTag("HomeEventParent").GetComponent<Transform>();
        }
        
        if(GameObject.FindGameObjectWithTag("NPCParent")!= null)
        {
            NpcParent = GameObject.FindGameObjectWithTag("NPCParent").GetComponent<Transform>();
        }
        //instantiateHomeEventInMorning();
    }

    private void OnBeforeSceneUnLoad()
    {

    }

    private void OnAfterSceneLoad()
    {
        if (GameObject.FindGameObjectWithTag("HomeEventParent") != null)
        {
            ParentCanvas = GameObject.FindGameObjectWithTag("HomeEventParent").GetComponent<Transform>();
        }

        if (GameObject.FindGameObjectWithTag("NPCParent") != null)
        {
            NpcParent = GameObject.FindGameObjectWithTag("NPCParent").GetComponent<Transform>();
        }

        var currentSceneName = SceneManager.GetActiveScene().name;
        switch (currentSceneName)
        {
            case "SafetyArea":
                instantiateNPCEventInSafetyOne();
                break;

            case "Home":
                Debug.Log("更新");
                instantiateHomeEventInMorning();
                //更新HomeEvent写到InventoryUI里面了，懒得改了，其实感觉写在这里也一样 InvenoryUI里面还有控制第一幕和第二幕UI的内容 都写一起了
                break;

            case "ResearchBase":
                instantiateNPCEventInResearchBase();
                break;

            case "Church":
                instantiateNPCEventInChurch();
                break;
            case "WarYield":
                instantiateNPCEventInWarYield();
                break;

            default:
                break;
        }
    }

    /// <summary>
    /// HomeEvent每日夜晚刷新
    /// </summary>
    public void instantiateHomeEvent()
    {
        switch (DayCount)
        {
            case 1:
                var newButtton1_1 = Instantiate(HomeEventButtonPrefab, ParentCanvas);
                newButtton1_1.GetComponent<HomeEventUI>().Init(5102);
                break;
            case 2:
                if(eventList_SO.HomeEventList.Find(i => i.EventID == 5003).isEventOver)
                {
                    var newButtton2_1 = Instantiate(HomeEventButtonPrefab, ParentCanvas);
                    newButtton2_1.GetComponent<HomeEventUI>().Init(5006);
                }

                break;
            case 3:
                var newButtton3_1 = Instantiate(HomeEventButtonPrefab, ParentCanvas);
                newButtton3_1.GetComponent<HomeEventUI>().Init(5104);

                //流浪汉二次搜身
                if(eventList_SO.HomeEventList.Find(i => i.EventID == 5010).isEventOver)
                {
                    var newButtton3_2 = Instantiate(HomeEventButtonPrefab, ParentCanvas);
                    newButtton3_2.GetComponent<HomeEventUI>().Init(5013);
                }

                var newButtton3_3 = Instantiate(HomeEventButtonPrefab, ParentCanvas);
                newButtton3_3.GetComponent<HomeEventUI>().Init(5020);
                break;
            case 4:
                if (eventList_SO.HomeEventList.Find(i => i.EventID == 5010).isEventOver)
                {
                    var newButtton4_2 = Instantiate(HomeEventButtonPrefab, ParentCanvas);
                    newButtton4_2.GetComponent<HomeEventUI>().Init(5023);
                }
                var newButtton4_1 = Instantiate(HomeEventButtonPrefab, ParentCanvas);
                newButtton4_1.GetComponent<HomeEventUI>().Init(5024);

                break;
            case 5:
                var newButtton5_1 = Instantiate(HomeEventButtonPrefab, ParentCanvas);  //夜里广播
                newButtton5_1.GetComponent<HomeEventUI>().Init(5103);
               
                break;
            case 6:
                break;
            case 7:
                break;
            case 8:
                var newButtton8_1 = Instantiate(HomeEventButtonPrefab, ParentCanvas);  //夜里广播
                newButtton8_1.GetComponent<HomeEventUI>().Init(5032);
                if(npcList_SO.NPCDetailsList.Find(i=>i.NPCID == 9014).isThisAppear)
                {
                    var newButtton8_2 = Instantiate(HomeEventButtonPrefab, ParentCanvas);
                    newButtton8_2.GetComponent<HomeEventUI>().Init(5033);
                }
                else
                {
                    var newButtton8_2 = Instantiate(HomeEventButtonPrefab, ParentCanvas);
                    newButtton8_2.GetComponent<HomeEventUI>().Init(5036);
                }
                break;
            case 9:
                var newButtton9_1 = Instantiate(HomeEventButtonPrefab, ParentCanvas);  //夜里广播
                newButtton9_1.GetComponent<HomeEventUI>().Init(5040);
                var newButtton9_2 = Instantiate(HomeEventButtonPrefab, ParentCanvas);
                if (eventList_SO.HomeEventList.Find(i=>i.EventID == 5034).isEventOver)
                {
                    newButtton9_2.GetComponent<HomeEventUI>().Init(5038);
                }
                else
                {
                    newButtton9_2.GetComponent<HomeEventUI>().Init(5039);
                }
                break;
            case 10:
                if (eventList_SO.HomeEventList.Find(i => i.EventID == 5034).isEventOver)  //建立盟约并且给爱尔莎东西
                {
                    var newButtton10_1 = Instantiate(HomeEventButtonPrefab, ParentCanvas);  //夜里事件
                    newButtton10_1.GetComponent<HomeEventUI>().Init(5041);
                }
                break;
            case 11:
                break;
            case 12:
                break;
            case 13:
                if(eventList_SO.HomeEventList.Find(i=>i.EventID == 5041).isEventOver)
                {
                    var newButtton13_1 = Instantiate(HomeEventButtonPrefab, ParentCanvas);  //夜里事件
                    newButtton13_1.GetComponent<HomeEventUI>().Init(5047);
                }
                break;
            case 14:
                break;
            default:
                break;
        }
    }


    /// <summary>
    /// 每日白天刷新
    /// </summary>
    public void instantiateHomeEventInMorning()
    {
        Debug.Log(DayCount);
        switch (DayCount)
        {
            case 1:
                //yang敲门事件

                if(!eventList_SO.HomeEventList.Find(i=>i.EventID == 5001).isEventOver)
                {
                    var newButtton = Instantiate(HomeEventButtonPrefab, ParentCanvas);
                    newButtton.GetComponent<HomeEventUI>().Init(5001);
                }

                else if (eventList_SO.HomeEventList.Find(i => i.EventID == 5001).isEventOver && 
                    !eventList_SO.HomeEventList.Find(i =>i.EventID == 5101).isEventOver)
                {
                    var newButton = Instantiate(HomeEventButtonPrefab, ParentCanvas);
                    newButton.GetComponent<HomeEventUI>().Init(5101);
                }
                break;


            case 2:
                //出门后偶遇流浪汉事件 这是假出门的按钮
                if(!eventList_SO.HomeEventList.Find(i => i.EventID == 5002).isEventOver)
                {
                    var newButtton2 = Instantiate(HomeEventButtonPrefab, ParentCanvas);
                    newButtton2.GetComponent<HomeEventUI>().Init(5002);
                }
                else if (HomeEventManager.Instance.CheckIfEventDone(5003))
                {
                    var newButtton2_1 = Instantiate(HomeEventButtonPrefab, ParentCanvas);
                    newButtton2_1.GetComponent<HomeEventUI>().Init(5005);
                }
                break;
            case 3:
                //ylm和流浪汉拉扯
                if(eventList_SO.HomeEventList.Find(i => i.EventID == 5003).isEventOver && !eventList_SO.HomeEventList.Find(i => i.EventID == 5009).isEventOver)
                {
                    var newButtton3_1 = Instantiate(HomeEventButtonPrefab, ParentCanvas);
                    newButtton3_1.GetComponent<HomeEventUI>().Init(5009);
                }
                if (eventList_SO.HomeEventList.Find(i => i.EventID == 5004).isEventOver && !eventList_SO.HomeEventList.Find(i => i.EventID == 5012).isEventOver)
                {
                    var newButtton3_2 = Instantiate(HomeEventButtonPrefab, ParentCanvas);
                    newButtton3_2.GetComponent<HomeEventUI>().Init(5012);
                }
                //留下流浪汉在房间中
                if(eventList_SO.HomeEventList.Find(i => i.EventID == 5010).isEventOver)
                {
                    var newButtton3_3 = Instantiate(HomeEventButtonPrefab, ParentCanvas);
                    newButtton3_3.GetComponent<HomeEventUI>().Init(5110);
                }
                break;
            case 4:
                //第四天白天，流浪汉依旧在房间中
                if (eventList_SO.HomeEventList.Find(i => i.EventID == 5010).isEventOver)
                {
                    var newButtton3_3 = Instantiate(HomeEventButtonPrefab, ParentCanvas);
                    newButtton3_3.GetComponent<HomeEventUI>().Init(5110);
                }
                break;
            case 5:
                var newButtton5_2 = Instantiate(HomeEventButtonPrefab, ParentCanvas);  //出门触发
                newButtton5_2.GetComponent<HomeEventUI>().Init(5026);
                break;
            case 6:
                var newButtton6_1 = Instantiate(HomeEventButtonPrefab, ParentCanvas);  //一大早就触发
                newButtton6_1.GetComponent<HomeEventUI>().Init(5027);
                break;
            case 7:
                if(npcList_SO.NPCDetailsList.Find(i => i.NPCID == 8111).isThisAppear)
                {
                    var newButtton7_1 = Instantiate(HomeEventButtonPrefab, ParentCanvas);
                    newButtton7_1.GetComponent<HomeEventUI>().Init(5030);
                }
                break;
            case 8:
                if (npcList_SO.NPCDetailsList.Find(i => i.NPCID == 8111).isThisAppear)
                {
                    var newButtton8_1 = Instantiate(HomeEventButtonPrefab, ParentCanvas);
                    newButtton8_1.GetComponent<HomeEventUI>().Init(5030);
                }
                break;
            case 9:
                if (npcList_SO.NPCDetailsList.Find(i => i.NPCID == 8111).isThisAppear)
                {
                    var newButtton9_1 = Instantiate(HomeEventButtonPrefab, ParentCanvas);
                    newButtton9_1.GetComponent<HomeEventUI>().Init(5030);
                }
                break;
            case 10:
                if (npcList_SO.NPCDetailsList.Find(i => i.NPCID == 8111).isThisAppear && !npcList_SO.NPCDetailsList.Find(i => i.NPCID == 5030).isThisAppear)
                {
                    var newButtton10_1 = Instantiate(HomeEventButtonPrefab, ParentCanvas);
                    newButtton10_1.GetComponent<HomeEventUI>().Init(5030);
                }
                break;
            case 11:
                break;
            case 12:
                var newButtton12_1 = Instantiate(HomeEventButtonPrefab, ParentCanvas); //一大早就触发
                newButtton12_1.GetComponent<HomeEventUI>().Init(5042);
                break;
            case 13:
                if(eventList_SO.HomeEventList.Find(i=>i.EventID == 5041).isEventOver)  //有相框
                {
                    var newButtton13_1 = Instantiate(HomeEventButtonPrefab, ParentCanvas);
                    newButtton13_1.GetComponent<HomeEventUI>().Init(5046);
                }
                break;
            case 14:
                //结局判定
                if(eventList_SO.HomeEventList.Find(i=>i.EventID == 5047).isEventOver)
                {
                    //判定线索
                    if (InfoManager.Instance.CheckKeyInfos())
                    {
                        EndManager.Instance.End1 = true;//举报成功结局 结局1
                    }
                    else
                    {
                        EndManager.Instance.End3 = true;//举报失败结局 结局3
                    }
                }
                else
                {
                    EndManager.Instance.End2 = true;//混混噩噩结局2
                }
                break;
            default:
                break;
        }
    }


    /// <summary>
    /// 安全区NPC事件每日刷新
    /// </summary>
    public void instantiateNPCEventInSafetyOne()
    {
        NPCCurNum = 0;
        switch (DayCount)
        {
            case 1:
                var newNpc1_1 = Instantiate(NPC, NpcParent);
                newNpc1_1.GetComponent<NPCDialogueController>().Init(6001);  //老人

                var newNpc1_2 = Instantiate(NPC, NpcParent);
                newNpc1_2.GetComponent<NPCDialogueController>().Init(6002); //军事迷

                var newNpc1_3 = Instantiate(NPC, NpcParent);
                newNpc1_3.GetComponent<NPCDialogueController>().Init(6003); //新手教学

                var newNpc1_4 = Instantiate(NPC, NpcParent);
                newNpc1_4.GetComponent<NPCDialogueController>().Init(6004);

                //人工统计今日的事项信息？
                NPCNum = 4;
                break;
            case 2:
                if (!npcList_SO.NPCDetailsList.Find(i => i.NPCID == 6001).isThisAppear)
                {
                    var newNpc1_5 = Instantiate(NPC, NpcParent);
                    newNpc1_5.GetComponent<NPCDialogueController>().Init(6001);
                }
                if (!npcList_SO.NPCDetailsList.Find(i => i.NPCID == 6002).isThisAppear)
                {
                    var newNpc1_5 = Instantiate(NPC, NpcParent);
                    newNpc1_5.GetComponent<NPCDialogueController>().Init(6002);
                }
                var newNpc2_1 = Instantiate(NPC, NpcParent);
                newNpc2_1.GetComponent<NPCDialogueController>().Init(6009);
                var newNpc2_2 = Instantiate(NPC, NpcParent);
                newNpc2_2.GetComponent<NPCDialogueController>().Init(6010);
                var newNpc2_3 = Instantiate(NPC, NpcParent);
                newNpc2_3.GetComponent<NPCDialogueController>().Init(6008);
                NPCNum = 3;
                break;
            case 3:
                if (!npcList_SO.NPCDetailsList.Find(i => i.NPCID == 6001).isThisAppear)
                {
                    var newNpc1_5 = Instantiate(NPC, NpcParent);
                    newNpc1_5.GetComponent<NPCDialogueController>().Init(6001);
                }
                if (!npcList_SO.NPCDetailsList.Find(i => i.NPCID == 6001).isThisAppear)
                {
                    var newNpc1_5 = Instantiate(NPC, NpcParent);
                    newNpc1_5.GetComponent<NPCDialogueController>().Init(6001);
                }
                if (!npcList_SO.NPCDetailsList.Find(i => i.NPCID == 6002).isThisAppear)
                {
                    var newNpc1_5 = Instantiate(NPC, NpcParent);
                    newNpc1_5.GetComponent<NPCDialogueController>().Init(6002);
                }
                if (!npcList_SO.NPCDetailsList.Find(i => i.NPCID == 6008).isThisAppear)
                {
                    var newNpc2_5 = Instantiate(NPC, NpcParent);
                    newNpc2_5.GetComponent<NPCDialogueController>().Init(6008);
                }

                if (npcList_SO.NPCDetailsList.Find(i => i.NPCID == 8001).isThisAppear && !npcList_SO.NPCDetailsList.Find(i => i.NPCID == 6012).isThisAppear)
                {
                    var newNpc3_1 = Instantiate(NPC, NpcParent);
                    newNpc3_1.GetComponent<NPCDialogueController>().Init(6012);
                }
                var newNpc3_2 = Instantiate(NPC, NpcParent);
                newNpc3_2.GetComponent<NPCDialogueController>().Init(6013);

                var newNpc3_3 = Instantiate(NPC, NpcParent);
                newNpc3_3.GetComponent<NPCDialogueController>().Init(6014);
                NPCNum = 3;
                break;
            case 4:
                if (!npcList_SO.NPCDetailsList.Find(i => i.NPCID == 6001).isThisAppear)
                {
                    var newNpc1_5 = Instantiate(NPC, NpcParent);
                    newNpc1_5.GetComponent<NPCDialogueController>().Init(6001);
                }
                if (!npcList_SO.NPCDetailsList.Find(i => i.NPCID == 6002).isThisAppear)
                {
                    var newNpc1_5 = Instantiate(NPC, NpcParent);
                    newNpc1_5.GetComponent<NPCDialogueController>().Init(6002);
                }
                var newNpc4_1 = Instantiate(NPC, NpcParent);
                newNpc4_1.GetComponent<NPCDialogueController>().Init(6015);

                var newNpc4_2 = Instantiate(NPC, NpcParent);
                newNpc4_2.GetComponent<NPCDialogueController>().Init(6016);
                NPCNum = 2;
                break;
            case 5:
                if (!npcList_SO.NPCDetailsList.Find(i => i.NPCID == 6001).isThisAppear)
                {
                    var newNpc1_5 = Instantiate(NPC, NpcParent);
                    newNpc1_5.GetComponent<NPCDialogueController>().Init(6001);
                }
                if (npcList_SO.NPCDetailsList.Find(i => i.NPCID == 8001).isThisAppear && !npcList_SO.NPCDetailsList.Find(i => i.NPCID == 6012).isThisAppear)
                {
                    var newNpc3_1 = Instantiate(NPC, NpcParent);
                    newNpc3_1.GetComponent<NPCDialogueController>().Init(6012);
                }
                if (!npcList_SO.NPCDetailsList.Find(i => i.NPCID == 6002).isThisAppear)
                {
                    var newNpc1_5 = Instantiate(NPC, NpcParent);
                    newNpc1_5.GetComponent<NPCDialogueController>().Init(6002);
                }
                var newNpc5_1 = Instantiate(NPC, NpcParent);
                newNpc5_1.GetComponent<NPCDialogueController>().Init(6017);

                var newNpc5_2 = Instantiate(NPC, NpcParent);
                newNpc5_2.GetComponent<NPCDialogueController>().Init(6018);

                var newNpc5_3 = Instantiate(NPC, NpcParent);
                newNpc5_3.GetComponent<NPCDialogueController>().Init(6019);
                break;
            case 6:
                if (!npcList_SO.NPCDetailsList.Find(i => i.NPCID == 6001).isThisAppear)
                {
                    var newNpc1_5 = Instantiate(NPC, NpcParent);
                    newNpc1_5.GetComponent<NPCDialogueController>().Init(6001);
                }
                if (npcList_SO.NPCDetailsList.Find(i => i.NPCID == 8001).isThisAppear && !npcList_SO.NPCDetailsList.Find(i => i.NPCID == 6012).isThisAppear)
                {
                    var newNpc3_1 = Instantiate(NPC, NpcParent);
                    newNpc3_1.GetComponent<NPCDialogueController>().Init(6012);
                }
                var newNpc6_1 = Instantiate(NPC, NpcParent);
                newNpc6_1.GetComponent<NPCDialogueController>().Init(6020);

                var newNpc6_2 = Instantiate(NPC, NpcParent);
                newNpc6_2.GetComponent<NPCDialogueController>().Init(6021);

                var newNpc6_3 = Instantiate(NPC, NpcParent);
                newNpc6_3.GetComponent<NPCDialogueController>().Init(9011);
                break;
            case 7:
                if (!npcList_SO.NPCDetailsList.Find(i => i.NPCID == 6001).isThisAppear)
                {
                    var newNpc1_5 = Instantiate(NPC, NpcParent);
                    newNpc1_5.GetComponent<NPCDialogueController>().Init(6001);
                }
                if (npcList_SO.NPCDetailsList.Find(i => i.NPCID == 8001).isThisAppear && !npcList_SO.NPCDetailsList.Find(i => i.NPCID == 6012).isThisAppear)
                {
                    var newNpc3_1 = Instantiate(NPC, NpcParent);
                    newNpc3_1.GetComponent<NPCDialogueController>().Init(6012);
                }
                var newNpc7_1 = Instantiate(NPC, NpcParent);
                newNpc7_1.GetComponent<NPCDialogueController>().Init(6024);
                var newNpc7_2 = Instantiate(NPC, NpcParent);
                newNpc7_2.GetComponent<NPCDialogueController>().Init(6025);
                break;
            case 8:
                if (!npcList_SO.NPCDetailsList.Find(i => i.NPCID == 6001).isThisAppear)
                {
                    var newNpc1_5 = Instantiate(NPC, NpcParent);
                    newNpc1_5.GetComponent<NPCDialogueController>().Init(6001);
                }
                if (npcList_SO.NPCDetailsList.Find(i => i.NPCID == 8001).isThisAppear && !npcList_SO.NPCDetailsList.Find(i => i.NPCID == 6012).isThisAppear)
                {
                    var newNpc3_1 = Instantiate(NPC, NpcParent);
                    newNpc3_1.GetComponent<NPCDialogueController>().Init(6012);
                }
                var newNpc8_1 = Instantiate(NPC, NpcParent);
                newNpc8_1.GetComponent<NPCDialogueController>().Init(6029);
                var newNpc8_2 = Instantiate(NPC, NpcParent);
                newNpc8_2.GetComponent<NPCDialogueController>().Init(6026);
                break;
            case 9:
                if (!npcList_SO.NPCDetailsList.Find(i => i.NPCID == 6001).isThisAppear)
                {
                    var newNpc1_5 = Instantiate(NPC, NpcParent);
                    newNpc1_5.GetComponent<NPCDialogueController>().Init(6001);
                }
                if (npcList_SO.NPCDetailsList.Find(i => i.NPCID == 8001).isThisAppear && !npcList_SO.NPCDetailsList.Find(i => i.NPCID == 6012).isThisAppear)
                {
                    var newNpc3_1 = Instantiate(NPC, NpcParent);
                    newNpc3_1.GetComponent<NPCDialogueController>().Init(6012);
                }
                if (!npcList_SO.NPCDetailsList.Find(i => i.NPCID == 6026).isThisAppear)
                {
                    var newNpc8_3 = Instantiate(NPC, NpcParent);
                    newNpc8_3.GetComponent<NPCDialogueController>().Init(6026);
                }
                var newNpc9_1 = Instantiate(NPC, NpcParent);
                newNpc9_1.GetComponent<NPCDialogueController>().Init(6030);
                break;
            case 10:
                if (!npcList_SO.NPCDetailsList.Find(i => i.NPCID == 6001).isThisAppear)
                {
                    var newNpc1_5 = Instantiate(NPC, NpcParent);
                    newNpc1_5.GetComponent<NPCDialogueController>().Init(6001);
                }
                if (npcList_SO.NPCDetailsList.Find(i => i.NPCID == 8001).isThisAppear && !npcList_SO.NPCDetailsList.Find(i => i.NPCID == 6012).isThisAppear)
                {
                    var newNpc3_1 = Instantiate(NPC, NpcParent);
                    newNpc3_1.GetComponent<NPCDialogueController>().Init(6012);
                }
                if (!npcList_SO.NPCDetailsList.Find(i => i.NPCID == 6026).isThisAppear)
                {
                    var newNpc8_3 = Instantiate(NPC, NpcParent);
                    newNpc8_3.GetComponent<NPCDialogueController>().Init(6026);
                }
                else
                {
                    var newNpc10_1 = Instantiate(NPC, NpcParent);
                    newNpc10_1.GetComponent<NPCDialogueController>().Init(6031);
                }
                
                var newNpc10_2 = Instantiate(NPC, NpcParent);
                newNpc10_2.GetComponent<NPCDialogueController>().Init(6034);
                break;
            case 11:
                if (!npcList_SO.NPCDetailsList.Find(i => i.NPCID == 6001).isThisAppear)
                {
                    var newNpc1_5 = Instantiate(NPC, NpcParent);
                    newNpc1_5.GetComponent<NPCDialogueController>().Init(6001);
                }
                if (npcList_SO.NPCDetailsList.Find(i => i.NPCID == 6026).isThisAppear && !npcList_SO.NPCDetailsList.Find(i => i.NPCID == 6031).isThisAppear)
                {
                    var newNpc10_1 = Instantiate(NPC, NpcParent);
                    newNpc10_1.GetComponent<NPCDialogueController>().Init(6031);
                }
                if (npcList_SO.NPCDetailsList.Find(i => i.NPCID == 8001).isThisAppear && !npcList_SO.NPCDetailsList.Find(i => i.NPCID == 6012).isThisAppear)
                {
                    var newNpc3_1 = Instantiate(NPC, NpcParent);
                    newNpc3_1.GetComponent<NPCDialogueController>().Init(6012);
                }
                if (!npcList_SO.NPCDetailsList.Find(i => i.NPCID == 6034).isThisAppear)
                {
                    var newNpc10_3 = Instantiate(NPC, NpcParent);
                    newNpc10_3.GetComponent<NPCDialogueController>().Init(6034);
                }
                var newNpc11_1 = Instantiate(NPC, NpcParent);
                newNpc11_1.GetComponent<NPCDialogueController>().Init(6035);
                break;
            case 12:
                if (!npcList_SO.NPCDetailsList.Find(i => i.NPCID == 6001).isThisAppear)
                {
                    var newNpc1_5 = Instantiate(NPC, NpcParent);
                    newNpc1_5.GetComponent<NPCDialogueController>().Init(6001);
                }
                if (npcList_SO.NPCDetailsList.Find(i => i.NPCID == 6026).isThisAppear && !npcList_SO.NPCDetailsList.Find(i => i.NPCID == 6031).isThisAppear)
                {
                    var newNpc10_1 = Instantiate(NPC, NpcParent);
                    newNpc10_1.GetComponent<NPCDialogueController>().Init(6031);
                }
                if (npcList_SO.NPCDetailsList.Find(i => i.NPCID == 8001).isThisAppear && !npcList_SO.NPCDetailsList.Find(i => i.NPCID == 6012).isThisAppear)
                {
                    var newNpc3_1 = Instantiate(NPC, NpcParent);
                    newNpc3_1.GetComponent<NPCDialogueController>().Init(6012);
                }
                if (!npcList_SO.NPCDetailsList.Find(i => i.NPCID == 6034).isThisAppear)
                {
                    var newNpc10_3 = Instantiate(NPC, NpcParent);
                    newNpc10_3.GetComponent<NPCDialogueController>().Init(6034);
                }
                var newNpc12_1 = Instantiate(NPC, NpcParent);
                newNpc12_1.GetComponent<NPCDialogueController>().Init(6036);
                break;
            case 13:
                if (!npcList_SO.NPCDetailsList.Find(i => i.NPCID == 6001).isThisAppear)
                {
                    var newNpc1_5 = Instantiate(NPC, NpcParent);
                    newNpc1_5.GetComponent<NPCDialogueController>().Init(6001);
                }
                if (npcList_SO.NPCDetailsList.Find(i => i.NPCID == 6026).isThisAppear && !npcList_SO.NPCDetailsList.Find(i => i.NPCID == 6031).isThisAppear)
                {
                    var newNpc10_1 = Instantiate(NPC, NpcParent);
                    newNpc10_1.GetComponent<NPCDialogueController>().Init(6031);
                }
                if (npcList_SO.NPCDetailsList.Find(i => i.NPCID == 8001).isThisAppear && !npcList_SO.NPCDetailsList.Find(i => i.NPCID == 6012).isThisAppear)
                {
                    var newNpc3_1 = Instantiate(NPC, NpcParent);
                    newNpc3_1.GetComponent<NPCDialogueController>().Init(6012);
                }
                var newNpc13_1 = Instantiate(NPC, NpcParent);
                newNpc13_1.GetComponent<NPCDialogueController>().Init(6037);
                break;
            case 14:
                break;
            default:
                break;
        }
    }

    /// <summary>
    /// 科研基地每周npc刷新
    /// </summary>
    public void instantiateNPCEventInResearchBase()
    {
        switch (DayCount)
        {
            case 1:
                var newNpc1_1 = Instantiate(NPC, NpcParent);
                newNpc1_1.GetComponent<NPCDialogueController>().Init(8001);

                var newNpc1_2 = Instantiate(NPC, NpcParent);
                newNpc1_2.GetComponent<NPCDialogueController>().Init(8002);

                var newNpc1_3 = Instantiate(NPC, NpcParent);
                newNpc1_3.GetComponent<NPCDialogueController>().Init(8101);

                var newNpc1_4 = Instantiate(NPC, NpcParent);
                newNpc1_4.GetComponent<NPCDialogueController>().Init(8116);

                var newNpc1_5 = Instantiate(NPC, NpcParent);
                newNpc1_5.GetComponent<NPCDialogueController>().Init(8103);

                //人工统计今日的事项信息？
                NPCNum = 5;
                break;
            case 2:
                if (!npcList_SO.NPCDetailsList.Find(i => i.NPCID == 8001).isThisAppear)
                {
                    var newNpc1_6 = Instantiate(NPC, NpcParent);
                    newNpc1_6.GetComponent<NPCDialogueController>().Init(8001);
                }
                if (!npcList_SO.NPCDetailsList.Find(i => i.NPCID == 8103).isThisAppear)
                {
                    var newNpc1_6 = Instantiate(NPC, NpcParent);
                    newNpc1_6.GetComponent<NPCDialogueController>().Init(8103);
                }
                if (!npcList_SO.NPCDetailsList.Find(i => i.NPCID == 8101).isThisAppear)
                {
                    var newNpc1_7 = Instantiate(NPC, NpcParent);
                    newNpc1_7.GetComponent<NPCDialogueController>().Init(8101);
                }
                if (!npcList_SO.NPCDetailsList.Find(i => i.NPCID == 8116).isThisAppear)
                {
                    var newNpc1_8 = Instantiate(NPC, NpcParent);
                    newNpc1_8.GetComponent<NPCDialogueController>().Init(8116);
                }

                var newNpc2_1 = Instantiate(NPC, NpcParent);
                newNpc2_1.GetComponent<NPCDialogueController>().Init(8003);

                var newNpc2_2 = Instantiate(NPC, NpcParent);
                newNpc2_2.GetComponent<NPCDialogueController>().Init(8004);
                NPCNum = 2;
                break;
            case 3:
                if (!npcList_SO.NPCDetailsList.Find(i => i.NPCID == 8001).isThisAppear)
                {
                    var newNpc1_6 = Instantiate(NPC, NpcParent);
                    newNpc1_6.GetComponent<NPCDialogueController>().Init(8001);
                }
                if (!npcList_SO.NPCDetailsList.Find(i => i.NPCID == 8103).isThisAppear)
                {
                    var newNpc1_6 = Instantiate(NPC, NpcParent);
                    newNpc1_6.GetComponent<NPCDialogueController>().Init(8103);
                }
                if (!npcList_SO.NPCDetailsList.Find(i => i.NPCID == 8101).isThisAppear)
                {
                    var newNpc1_7 = Instantiate(NPC, NpcParent);
                    newNpc1_7.GetComponent<NPCDialogueController>().Init(8101);
                }
                if (!npcList_SO.NPCDetailsList.Find(i => i.NPCID == 8004).isThisAppear)
                {
                    var newNpc2_3 = Instantiate(NPC, NpcParent);
                    newNpc2_3.GetComponent<NPCDialogueController>().Init(8004);
                }
                var newNpc3_1 = Instantiate(NPC, NpcParent);
                newNpc3_1.GetComponent<NPCDialogueController>().Init(8104);

                var newNpc3_2 = Instantiate(NPC, NpcParent);
                newNpc3_2.GetComponent<NPCDialogueController>().Init(8012);

                var newNpc3_3 = Instantiate(NPC, NpcParent);
                newNpc3_3.GetComponent<NPCDialogueController>().Init(8014);
                NPCNum = 3;
                break;
            case 4:
                if (!npcList_SO.NPCDetailsList.Find(i => i.NPCID == 8103).isThisAppear)
                {
                    var newNpc1_6 = Instantiate(NPC, NpcParent);
                    newNpc1_6.GetComponent<NPCDialogueController>().Init(8103);
                }
                if (!npcList_SO.NPCDetailsList.Find(i => i.NPCID == 8101).isThisAppear)
                {
                    var newNpc1_7 = Instantiate(NPC, NpcParent);
                    newNpc1_7.GetComponent<NPCDialogueController>().Init(8101);
                }
                if (!npcList_SO.NPCDetailsList.Find(i => i.NPCID == 8004).isThisAppear)
                {
                    var newNpc2_3 = Instantiate(NPC, NpcParent);
                    newNpc2_3.GetComponent<NPCDialogueController>().Init(8004);
                }
                if (!npcList_SO.NPCDetailsList.Find(i => i.NPCID == 8104).isThisAppear)
                {
                    var newNpc3_4 = Instantiate(NPC, NpcParent);
                    newNpc3_4.GetComponent<NPCDialogueController>().Init(8104);
                }
                var newNpc4_1 = Instantiate(NPC, NpcParent);
                newNpc4_1.GetComponent<NPCDialogueController>().Init(8013);
                NPCNum = 1;
                break;
            case 5:
                if (!npcList_SO.NPCDetailsList.Find(i => i.NPCID == 8103).isThisAppear)
                {
                    var newNpc1_6 = Instantiate(NPC, NpcParent);
                    newNpc1_6.GetComponent<NPCDialogueController>().Init(8103);
                }
                if (!npcList_SO.NPCDetailsList.Find(i => i.NPCID == 8101).isThisAppear)
                {
                    var newNpc1_7 = Instantiate(NPC, NpcParent);
                    newNpc1_7.GetComponent<NPCDialogueController>().Init(8101);
                }
                if (!npcList_SO.NPCDetailsList.Find(i => i.NPCID == 8104).isThisAppear)
                {
                    var newNpc3_4 = Instantiate(NPC, NpcParent);
                    newNpc3_4.GetComponent<NPCDialogueController>().Init(8104);
                }
                if (!npcList_SO.NPCDetailsList.Find(i => i.NPCID == 8013).isThisAppear)
                {
                    var newNpc4_2 = Instantiate(NPC, NpcParent);
                    newNpc4_2.GetComponent<NPCDialogueController>().Init(8013);
                }
                var newNpc5_1 = Instantiate(NPC, NpcParent);
                newNpc5_1.GetComponent<NPCDialogueController>().Init(8105);

                var newNpc5_2 = Instantiate(NPC, NpcParent);
                newNpc5_2.GetComponent<NPCDialogueController>().Init(8017);

                break;
            case 6:
                if (npcList_SO.NPCDetailsList.Find(i => i.NPCID == 8105).isThisAppear)
                {
                    var newNpc6_2 = Instantiate(NPC, NpcParent);
                    newNpc6_2.GetComponent<NPCDialogueController>().Init(8111);
                }
                else
                {
                    var newNpc5_3 = Instantiate(NPC, NpcParent);
                    newNpc5_3.GetComponent<NPCDialogueController>().Init(8105);
                    var newNpc6_3 = Instantiate(NPC, NpcParent);
                    newNpc6_3.GetComponent<NPCDialogueController>().Init(8019);
                }
                if (!npcList_SO.NPCDetailsList.Find(i => i.NPCID == 8101).isThisAppear)
                {
                    var newNpc1_7 = Instantiate(NPC, NpcParent);
                    newNpc1_7.GetComponent<NPCDialogueController>().Init(8101);
                }
                if (!npcList_SO.NPCDetailsList.Find(i => i.NPCID == 8104).isThisAppear)
                {
                    var newNpc3_4 = Instantiate(NPC, NpcParent);
                    newNpc3_4.GetComponent<NPCDialogueController>().Init(8104);
                }


                var newNpc6_1 = Instantiate(NPC, NpcParent);
                newNpc6_1.GetComponent<NPCDialogueController>().Init(8018);

                var newNpc6_4 = Instantiate(NPC, NpcParent);
                newNpc6_4.GetComponent<NPCDialogueController>().Init(8102);
                break;
            case 7:
                if (npcList_SO.NPCDetailsList.Find(i => i.NPCID == 8105).isThisAppear && !npcList_SO.NPCDetailsList.Find(i => i.NPCID == 8111).isThisAppear)
                {
                    var newNpc6_2 = Instantiate(NPC, NpcParent);
                    newNpc6_2.GetComponent<NPCDialogueController>().Init(8111);
                }
                else if(!npcList_SO.NPCDetailsList.Find(i => i.NPCID == 8105).isThisAppear)
                {
                    var newNpc5_3 = Instantiate(NPC, NpcParent);
                    newNpc5_3.GetComponent<NPCDialogueController>().Init(8105);
                    var newNpc6_3 = Instantiate(NPC, NpcParent);
                    newNpc6_3.GetComponent<NPCDialogueController>().Init(8019);
                }
               

                if (!npcList_SO.NPCDetailsList.Find(i => i.NPCID == 8101).isThisAppear)
                {
                    var newNpc1_7 = Instantiate(NPC, NpcParent);
                    newNpc1_7.GetComponent<NPCDialogueController>().Init(8101);
                }
                if (!npcList_SO.NPCDetailsList.Find(i => i.NPCID == 8104).isThisAppear)
                {
                    var newNpc3_4 = Instantiate(NPC, NpcParent);
                    newNpc3_4.GetComponent<NPCDialogueController>().Init(8104);
                }
                if (!npcList_SO.NPCDetailsList.Find(i => i.NPCID == 8102).isThisAppear)
                {
                    var newNpc6_5 = Instantiate(NPC, NpcParent);
                    newNpc6_5.GetComponent<NPCDialogueController>().Init(8102);
                }
                var newNpc7_1 = Instantiate(NPC, NpcParent);
                newNpc7_1.GetComponent<NPCDialogueController>().Init(8113);
                var newNpc7_2 = Instantiate(NPC, NpcParent);
                newNpc7_2.GetComponent<NPCDialogueController>().Init(8106);
                var newNpc7_3 = Instantiate(NPC, NpcParent);
                newNpc7_3.GetComponent<NPCDialogueController>().Init(8120);
                break;
            case 8:
                if (npcList_SO.NPCDetailsList.Find(i => i.NPCID == 8105).isThisAppear && !npcList_SO.NPCDetailsList.Find(i => i.NPCID == 8111).isThisAppear)
                {
                    var newNpc6_2 = Instantiate(NPC, NpcParent);
                    newNpc6_2.GetComponent<NPCDialogueController>().Init(8111);
                }
                else if (!npcList_SO.NPCDetailsList.Find(i => i.NPCID == 8105).isThisAppear)
                {
                    var newNpc5_3 = Instantiate(NPC, NpcParent);
                    newNpc5_3.GetComponent<NPCDialogueController>().Init(8105);
                    var newNpc6_3 = Instantiate(NPC, NpcParent);
                    newNpc6_3.GetComponent<NPCDialogueController>().Init(8019);
                }
                if (!npcList_SO.NPCDetailsList.Find(i => i.NPCID == 8101).isThisAppear)
                {
                    var newNpc1_7 = Instantiate(NPC, NpcParent);
                    newNpc1_7.GetComponent<NPCDialogueController>().Init(8101);
                }
                if (!npcList_SO.NPCDetailsList.Find(i => i.NPCID == 8104).isThisAppear)
                {
                    var newNpc3_4 = Instantiate(NPC, NpcParent);
                    newNpc3_4.GetComponent<NPCDialogueController>().Init(8104);
                }
                if (!npcList_SO.NPCDetailsList.Find(i => i.NPCID == 8102).isThisAppear)
                {
                    var newNpc6_5 = Instantiate(NPC, NpcParent);
                    newNpc6_5.GetComponent<NPCDialogueController>().Init(8102);
                }
                if (!npcList_SO.NPCDetailsList.Find(i => i.NPCID == 8113).isThisAppear)
                {
                    var newNpc7_4 = Instantiate(NPC, NpcParent);
                    newNpc7_4.GetComponent<NPCDialogueController>().Init(8113);
                }
                if (!npcList_SO.NPCDetailsList.Find(i => i.NPCID == 8106).isThisAppear)
                {
                    var newNpc7_5 = Instantiate(NPC, NpcParent);
                    newNpc7_5.GetComponent<NPCDialogueController>().Init(8106);
                }
                if (!npcList_SO.NPCDetailsList.Find(i => i.NPCID == 8120).isThisAppear)
                {
                    var newNpc7_5 = Instantiate(NPC, NpcParent);
                    newNpc7_5.GetComponent<NPCDialogueController>().Init(8120);
                }

                var newNpc8_1 = Instantiate(NPC, NpcParent);
                newNpc8_1.GetComponent<NPCDialogueController>().Init(8021);

                var newNpc8_2 = Instantiate(NPC, NpcParent);
                newNpc8_2.GetComponent<NPCDialogueController>().Init(8110);
                var newNpc8_3 = Instantiate(NPC, NpcParent);
                newNpc8_3.GetComponent<NPCDialogueController>().Init(8114);
                break;
            case 9:
                if (npcList_SO.NPCDetailsList.Find(i => i.NPCID == 8105).isThisAppear && !npcList_SO.NPCDetailsList.Find(i => i.NPCID == 8111).isThisAppear)
                {
                    var newNpc6_2 = Instantiate(NPC, NpcParent);
                    newNpc6_2.GetComponent<NPCDialogueController>().Init(8111);
                }
                else if (!npcList_SO.NPCDetailsList.Find(i => i.NPCID == 8105).isThisAppear)
                {
                    var newNpc5_3 = Instantiate(NPC, NpcParent);
                    newNpc5_3.GetComponent<NPCDialogueController>().Init(8105);
                    var newNpc6_3 = Instantiate(NPC, NpcParent);
                    newNpc6_3.GetComponent<NPCDialogueController>().Init(8019);
                }
                if (!npcList_SO.NPCDetailsList.Find(i => i.NPCID == 8104).isThisAppear)
                {
                    var newNpc3_4 = Instantiate(NPC, NpcParent);
                    newNpc3_4.GetComponent<NPCDialogueController>().Init(8104);
                }
                if (!npcList_SO.NPCDetailsList.Find(i => i.NPCID == 8113).isThisAppear)
                {
                    var newNpc7_4 = Instantiate(NPC, NpcParent);
                    newNpc7_4.GetComponent<NPCDialogueController>().Init(8113);
                }
                if (!npcList_SO.NPCDetailsList.Find(i => i.NPCID == 8106).isThisAppear)
                {
                    var newNpc7_5 = Instantiate(NPC, NpcParent);
                    newNpc7_5.GetComponent<NPCDialogueController>().Init(8106);
                }
                if (!npcList_SO.NPCDetailsList.Find(i => i.NPCID == 8120).isThisAppear)
                {
                    var newNpc7_5 = Instantiate(NPC, NpcParent);
                    newNpc7_5.GetComponent<NPCDialogueController>().Init(8120);
                }
                if (!npcList_SO.NPCDetailsList.Find(i => i.NPCID == 8110).isThisAppear)
                {
                    var newNpc8_4 = Instantiate(NPC, NpcParent);
                    newNpc8_4.GetComponent<NPCDialogueController>().Init(8110);
                }
                if (!npcList_SO.NPCDetailsList.Find(i => i.NPCID == 8114).isThisAppear)
                {
                    var newNpc8_5 = Instantiate(NPC, NpcParent);
                    newNpc8_5.GetComponent<NPCDialogueController>().Init(8114);
                }
                var newNpc9_1 = Instantiate(NPC, NpcParent);
                newNpc9_1.GetComponent<NPCDialogueController>().Init(8007);
                var newNpc9_2 = Instantiate(NPC, NpcParent);
                newNpc9_2.GetComponent<NPCDialogueController>().Init(8024);
                var newNpc9_3 = Instantiate(NPC, NpcParent);
                newNpc9_3.GetComponent<NPCDialogueController>().Init(8025);
                if (eventList_SO.HomeEventList.Find(i => i.EventID == 5030).isEventOver)
                {
                    var newNpc9_4 = Instantiate(NPC, NpcParent);
                    newNpc9_4.GetComponent<NPCDialogueController>().Init(8112);
                }
                break;
            case 10:
                if (!npcList_SO.NPCDetailsList.Find(i => i.NPCID == 8104).isThisAppear)
                {
                    var newNpc3_4 = Instantiate(NPC, NpcParent);
                    newNpc3_4.GetComponent<NPCDialogueController>().Init(8104);
                }
                if (!npcList_SO.NPCDetailsList.Find(i => i.NPCID == 8113).isThisAppear)
                {
                    var newNpc7_4 = Instantiate(NPC, NpcParent);
                    newNpc7_4.GetComponent<NPCDialogueController>().Init(8113);
                }
                if (!npcList_SO.NPCDetailsList.Find(i => i.NPCID == 8106).isThisAppear)
                {
                    var newNpc7_5 = Instantiate(NPC, NpcParent);
                    newNpc7_5.GetComponent<NPCDialogueController>().Init(8106);
                }
                if (!npcList_SO.NPCDetailsList.Find(i => i.NPCID == 8120).isThisAppear)
                {
                    var newNpc7_5 = Instantiate(NPC, NpcParent);
                    newNpc7_5.GetComponent<NPCDialogueController>().Init(8120);
                }
                if (!npcList_SO.NPCDetailsList.Find(i => i.NPCID == 8110).isThisAppear)
                {
                    var newNpc8_4 = Instantiate(NPC, NpcParent);
                    newNpc8_4.GetComponent<NPCDialogueController>().Init(8110);
                }
                if (!npcList_SO.NPCDetailsList.Find(i => i.NPCID == 8114).isThisAppear)
                {
                    var newNpc8_5 = Instantiate(NPC, NpcParent);
                    newNpc8_5.GetComponent<NPCDialogueController>().Init(8114);
                }
                if (eventList_SO.HomeEventList.Find(i => i.EventID == 5030).isEventOver && !npcList_SO.NPCDetailsList.Find(i => i.NPCID == 8112).isThisAppear)
                {
                    var newNpc9_4 = Instantiate(NPC, NpcParent);
                    newNpc9_4.GetComponent<NPCDialogueController>().Init(8112);
                }
                var newNpc10_1 = Instantiate(NPC, NpcParent);
                newNpc10_1.GetComponent<NPCDialogueController>().Init(8026);
                var newNpc10_2 = Instantiate(NPC, NpcParent);
                newNpc10_2.GetComponent<NPCDialogueController>().Init(8109);
                break;
            case 11:
                if (!npcList_SO.NPCDetailsList.Find(i => i.NPCID == 8106).isThisAppear)
                {
                    var newNpc7_5 = Instantiate(NPC, NpcParent);
                    newNpc7_5.GetComponent<NPCDialogueController>().Init(8106);
                }
                if (!npcList_SO.NPCDetailsList.Find(i => i.NPCID == 8120).isThisAppear)
                {
                    var newNpc7_5 = Instantiate(NPC, NpcParent);
                    newNpc7_5.GetComponent<NPCDialogueController>().Init(8120);
                }
                if (!npcList_SO.NPCDetailsList.Find(i => i.NPCID == 8104).isThisAppear)
                {
                    var newNpc3_4 = Instantiate(NPC, NpcParent);
                    newNpc3_4.GetComponent<NPCDialogueController>().Init(8104);
                }
                if (!npcList_SO.NPCDetailsList.Find(i => i.NPCID == 8113).isThisAppear)
                {
                    var newNpc7_4 = Instantiate(NPC, NpcParent);
                    newNpc7_4.GetComponent<NPCDialogueController>().Init(8113);
                }
                if (!npcList_SO.NPCDetailsList.Find(i => i.NPCID == 8110).isThisAppear)
                {
                    var newNpc8_4 = Instantiate(NPC, NpcParent);
                    newNpc8_4.GetComponent<NPCDialogueController>().Init(8110);
                }
                if (!npcList_SO.NPCDetailsList.Find(i => i.NPCID == 8114).isThisAppear)
                {
                    var newNpc8_5 = Instantiate(NPC, NpcParent);
                    newNpc8_5.GetComponent<NPCDialogueController>().Init(8114);
                }
                if (eventList_SO.HomeEventList.Find(i => i.EventID == 5030).isEventOver && !npcList_SO.NPCDetailsList.Find(i => i.NPCID == 8112).isThisAppear)
                {
                    var newNpc9_4 = Instantiate(NPC, NpcParent);
                    newNpc9_4.GetComponent<NPCDialogueController>().Init(8112);
                }
                if (!npcList_SO.NPCDetailsList.Find(i => i.NPCID == 8109).isThisAppear)
                {
                    var newNpc8_5 = Instantiate(NPC, NpcParent);
                    newNpc8_5.GetComponent<NPCDialogueController>().Init(8109);
                }
                var newNpc11_1 = Instantiate(NPC, NpcParent);
                newNpc11_1.GetComponent<NPCDialogueController>().Init(8027);
                var newNpc11_2 = Instantiate(NPC, NpcParent);
                newNpc11_2.GetComponent<NPCDialogueController>().Init(8028);
                var newNpc11_3 = Instantiate(NPC, NpcParent);
                newNpc11_3.GetComponent<NPCDialogueController>().Init(8115);
                break;
            case 12:
                if (!npcList_SO.NPCDetailsList.Find(i => i.NPCID == 8104).isThisAppear)
                {
                    var newNpc3_4 = Instantiate(NPC, NpcParent);
                    newNpc3_4.GetComponent<NPCDialogueController>().Init(8104);
                }
                if (!npcList_SO.NPCDetailsList.Find(i => i.NPCID == 8113).isThisAppear)
                {
                    var newNpc7_4 = Instantiate(NPC, NpcParent);
                    newNpc7_4.GetComponent<NPCDialogueController>().Init(8113);
                }
                if (!npcList_SO.NPCDetailsList.Find(i => i.NPCID == 8110).isThisAppear)
                {
                    var newNpc8_4 = Instantiate(NPC, NpcParent);
                    newNpc8_4.GetComponent<NPCDialogueController>().Init(8110);
                }
                if (!npcList_SO.NPCDetailsList.Find(i => i.NPCID == 8114).isThisAppear)
                {
                    var newNpc8_5 = Instantiate(NPC, NpcParent);
                    newNpc8_5.GetComponent<NPCDialogueController>().Init(8114);
                }
                if (!npcList_SO.NPCDetailsList.Find(i => i.NPCID == 8115).isThisAppear)
                {
                    var newNpc11_4 = Instantiate(NPC, NpcParent);
                    newNpc11_4.GetComponent<NPCDialogueController>().Init(8115);
                }
                var newNpc12_1 = Instantiate(NPC, NpcParent);
                newNpc12_1.GetComponent<NPCDialogueController>().Init(8029);
                break;
            case 13:
                if (!npcList_SO.NPCDetailsList.Find(i => i.NPCID == 8104).isThisAppear)
                {
                    var newNpc3_4 = Instantiate(NPC, NpcParent);
                    newNpc3_4.GetComponent<NPCDialogueController>().Init(8104);
                }
                if (!npcList_SO.NPCDetailsList.Find(i => i.NPCID == 8113).isThisAppear)
                {
                    var newNpc7_4 = Instantiate(NPC, NpcParent);
                    newNpc7_4.GetComponent<NPCDialogueController>().Init(8113);
                }
                if (!npcList_SO.NPCDetailsList.Find(i => i.NPCID == 8110).isThisAppear)
                {
                    var newNpc8_4 = Instantiate(NPC, NpcParent);
                    newNpc8_4.GetComponent<NPCDialogueController>().Init(8110);
                }
                if (!npcList_SO.NPCDetailsList.Find(i => i.NPCID == 8114).isThisAppear)
                {
                    var newNpc8_5 = Instantiate(NPC, NpcParent);
                    newNpc8_5.GetComponent<NPCDialogueController>().Init(8114);
                }
                break;
            case 14:
                break;
            default:
                break;
        }
    }

    public void instantiateNPCEventInChurch()
    {
        switch (DayCount)
        {
            case 1:
                var newNpc1_1 = Instantiate(NPC, NpcParent);
                newNpc1_1.GetComponent<NPCDialogueController>().Init(9001);
                var newNpc1_2 = Instantiate(NPC, NpcParent);
                newNpc1_2.GetComponent<NPCDialogueController>().Init(9002);
                var newNpc1_3 = Instantiate(NPC, NpcParent);
                newNpc1_3.GetComponent<NPCDialogueController>().Init(9003);
                //人工统计今日的事项信息？
                break;
            case 2:
                var newNpc2_1 = Instantiate(NPC, NpcParent);
                newNpc2_1.GetComponent<NPCDialogueController>().Init(8199);

                var newNpc2_2 = Instantiate(NPC, NpcParent);
                newNpc2_2.GetComponent<NPCDialogueController>().Init(9004);
                break;
            case 3:
                var newNpc3_1 = Instantiate(NPC, NpcParent);
                newNpc3_1.GetComponent<NPCDialogueController>().Init(9006);
                var newNpc3_2 = Instantiate(NPC, NpcParent);
                newNpc3_2.GetComponent<NPCDialogueController>().Init(9007);
                break;
            case 4:
                var newNpc4_1 = Instantiate(NPC, NpcParent);
                newNpc4_1.GetComponent<NPCDialogueController>().Init(9008);
                break;
            case 5:
                var newNpc5_1 = Instantiate(NPC, NpcParent);
                newNpc5_1.GetComponent<NPCDialogueController>().Init(9009);

                if (eventList_SO.HomeEventList.Find(i => i.EventID == 5023).isEventOver)
                {
                    var newNpc5_2 = Instantiate(NPC, NpcParent);
                    newNpc5_2.GetComponent<NPCDialogueController>().Init(9010);
                }
                break;
            case 6:
                var newNpc6_1 = Instantiate(NPC, NpcParent);
                newNpc6_1.GetComponent<NPCDialogueController>().Init(9012);
                break;
            case 7:
                var newNpc7_1 = Instantiate(NPC, NpcParent);
                newNpc7_1.GetComponent<NPCDialogueController>().Init(9013);

                var newNpc7_2 = Instantiate(NPC, NpcParent);
                newNpc7_2.GetComponent<NPCDialogueController>().Init(9114);
                break;
            case 8:
                var newNpc8_1 = Instantiate(NPC, NpcParent);
                newNpc8_1.GetComponent<NPCDialogueController>().Init(9115);
                break;
            case 9:
                var newNpc9_1 = Instantiate(NPC, NpcParent);
                newNpc9_1.GetComponent<NPCDialogueController>().Init(9016);
                break;
            case 10:
                var newNpc10_1 = Instantiate(NPC, NpcParent);
                newNpc10_1.GetComponent<NPCDialogueController>().Init(9018);
                var newNpc10_2 = Instantiate(NPC, NpcParent);
                newNpc10_2.GetComponent<NPCDialogueController>().Init(9017);
                break;
            case 11:
                var newNpc11_1 = Instantiate(NPC, NpcParent);
                newNpc11_1.GetComponent<NPCDialogueController>().Init(9019);
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
    public void instantiateNPCEventInWarYield()
    {
        switch (DayCount)
        {
            case 2:
                var newNpc2_1 = Instantiate(NPC, NpcParent);
                newNpc2_1.GetComponent<NPCDialogueController>().Init(7004);

                var newNpc2_2 = Instantiate(NPC, NpcParent);
                newNpc2_2.GetComponent<NPCDialogueController>().Init(7005);
                break;
            case 3:
                if (!npcList_SO.NPCDetailsList.Find(i => i.NPCID == 7004).isThisAppear)
                {
                    var newNpc2_3 = Instantiate(NPC, NpcParent);
                    newNpc2_3.GetComponent<NPCDialogueController>().Init(7004);
                }
                var newNpc3_1 = Instantiate(NPC, NpcParent);
                newNpc3_1.GetComponent<NPCDialogueController>().Init(7009);
                var newNpc3_2 = Instantiate(NPC, NpcParent);
                newNpc3_2.GetComponent<NPCDialogueController>().Init(7015);
                break;
            case 4:
                if (!npcList_SO.NPCDetailsList.Find(i => i.NPCID == 7004).isThisAppear)
                {
                    var newNpc2_3 = Instantiate(NPC, NpcParent);
                    newNpc2_3.GetComponent<NPCDialogueController>().Init(7004);
                }
                var newNpc4_1 = Instantiate(NPC, NpcParent);
                newNpc4_1.GetComponent<NPCDialogueController>().Init(7014);
                var newNpc4_2 = Instantiate(NPC, NpcParent);
                newNpc4_2.GetComponent<NPCDialogueController>().Init(7016);
                var newNpc4_3 = Instantiate(NPC, NpcParent);
                newNpc4_3.GetComponent<NPCDialogueController>().Init(7017);
                break;
            case 5:
                if (!npcList_SO.NPCDetailsList.Find(i => i.NPCID == 7004).isThisAppear)
                {
                    var newNpc2_3 = Instantiate(NPC, NpcParent);
                    newNpc2_3.GetComponent<NPCDialogueController>().Init(7004);
                }
                if (!npcList_SO.NPCDetailsList.Find(i => i.NPCID == 7017).isThisAppear)
                {
                    var newNpc2_3 = Instantiate(NPC, NpcParent);
                    newNpc2_3.GetComponent<NPCDialogueController>().Init(7017);
                }
                var newNpc5_1 = Instantiate(NPC, NpcParent);
                newNpc5_1.GetComponent<NPCDialogueController>().Init(7018);
                break;
            case 6:
                if (!npcList_SO.NPCDetailsList.Find(i => i.NPCID == 7004).isThisAppear)
                {
                    var newNpc2_3 = Instantiate(NPC, NpcParent);
                    newNpc2_3.GetComponent<NPCDialogueController>().Init(7004);
                }
                var newNpc6_1 = Instantiate(NPC, NpcParent);
                newNpc6_1.GetComponent<NPCDialogueController>().Init(7023);
                var newNpc6_2 = Instantiate(NPC, NpcParent);
                newNpc6_2.GetComponent<NPCDialogueController>().Init(7025);
                break;
            case 7:
                if (!npcList_SO.NPCDetailsList.Find(i => i.NPCID == 7004).isThisAppear)
                {
                    var newNpc2_3 = Instantiate(NPC, NpcParent);
                    newNpc2_3.GetComponent<NPCDialogueController>().Init(7004);
                }
                if (!npcList_SO.NPCDetailsList.Find(i => i.NPCID == 7023).isThisAppear)
                {
                    var newNpc2_3 = Instantiate(NPC, NpcParent);
                    newNpc2_3.GetComponent<NPCDialogueController>().Init(7023);
                }
                var newNpc7_1 = Instantiate(NPC, NpcParent);
                newNpc7_1.GetComponent<NPCDialogueController>().Init(7026);
                break;
            case 8:
                if (!npcList_SO.NPCDetailsList.Find(i => i.NPCID == 7023).isThisAppear)
                {
                    var newNpc2_3 = Instantiate(NPC, NpcParent);
                    newNpc2_3.GetComponent<NPCDialogueController>().Init(7023);
                }
                if (!npcList_SO.NPCDetailsList.Find(i => i.NPCID == 7026).isThisAppear)
                {
                    var newNpc2_3 = Instantiate(NPC, NpcParent);
                    newNpc2_3.GetComponent<NPCDialogueController>().Init(7026);
                }
                var newNpc8_1 = Instantiate(NPC, NpcParent);
                newNpc8_1.GetComponent<NPCDialogueController>().Init(7024);
                break;
            case 9:
                if (!npcList_SO.NPCDetailsList.Find(i => i.NPCID == 7026).isThisAppear)
                {
                    var newNpc2_3 = Instantiate(NPC, NpcParent);
                    newNpc2_3.GetComponent<NPCDialogueController>().Init(7026);
                }
                var newNpc9_1 = Instantiate(NPC, NpcParent);
                newNpc9_1.GetComponent<NPCDialogueController>().Init(7030);
                var newNpc9_2 = Instantiate(NPC, NpcParent);
                newNpc9_2.GetComponent<NPCDialogueController>().Init(7031);
                break;
            case 10:
                if (!npcList_SO.NPCDetailsList.Find(i => i.NPCID == 7026).isThisAppear)
                {
                    var newNpc2_3 = Instantiate(NPC, NpcParent);
                    newNpc2_3.GetComponent<NPCDialogueController>().Init(7026);
                }
                var newNpc10_1 = Instantiate(NPC, NpcParent);
                newNpc10_1.GetComponent<NPCDialogueController>().Init(7032);
                var newNpc10_2 = Instantiate(NPC, NpcParent);
                newNpc10_2.GetComponent<NPCDialogueController>().Init(7033);
                break;
            case 11:
                if (eventList_SO.HomeEventList.Find(i => i.EventID == 5041).isEventOver)
                {
                    var newNpc11_1 = Instantiate(NPC, NpcParent);
                    newNpc11_1.GetComponent<NPCDialogueController>().Init(7034);
                }
               
                break;
            case 12:
                if (eventList_SO.HomeEventList.Find(i => i.EventID == 5041).isEventOver && !npcList_SO.NPCDetailsList.Find(i => i.NPCID == 7034).isThisAppear)
                {
                    var newNpc11_1 = Instantiate(NPC, NpcParent);
                    newNpc11_1.GetComponent<NPCDialogueController>().Init(7034);
                }
                var newNpc12_1 = Instantiate(NPC, NpcParent);
                newNpc12_1.GetComponent<NPCDialogueController>().Init(7035);
                break;
            case 13:
                if (eventList_SO.HomeEventList.Find(i => i.EventID == 5041).isEventOver && !npcList_SO.NPCDetailsList.Find(i => i.NPCID == 7034).isThisAppear)
                {
                    var newNpc11_1 = Instantiate(NPC, NpcParent);
                    newNpc11_1.GetComponent<NPCDialogueController>().Init(7034);
                }
                var newNpc13_1 = Instantiate(NPC, NpcParent);
                newNpc13_1.GetComponent<NPCDialogueController>().Init(7038);
                break;
            case 14:
                break;
            default:
                break;
        }
    }
}
