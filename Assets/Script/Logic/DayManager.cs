using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class DayManager : Singleton<DayManager>
{
    [Header("��ǰ����")]
    public int DayCount = 1;

    [Header("HomeEvent")]
    public Transform ParentCanvas;
    public Button HomeEventButtonPrefab;
    [SerializeField]private EventList_SO eventList_SO;

    [Header("NPC")]
    public Transform NpcParent;
    public GameObject NPC;

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
                Debug.Log("����");
                instantiateHomeEventInMorning();
                //����HomeEventд��InventoryUI�����ˣ����ø��ˣ���ʵ�о�д������Ҳһ�� InvenoryUI���滹�п��Ƶ�һĻ�͵ڶ�ĻUI������ ��дһ����
                break;

            default:
                break;
        }
    }

    /// <summary>
    /// HomeEventÿ��ҹ��ˢ��
    /// </summary>
    public void instantiateHomeEvent()
    {
        switch (DayCount)
        {
            case 1:
                break;
            case 2:
                //var newButtton = Instantiate(HomeEventButtonPrefab, ParentCanvas);
                //newButtton.GetComponent<HomeEventUI>().Init(5001);
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


    /// <summary>
    /// ÿ�հ���ˢ��
    /// </summary>
    public void instantiateHomeEventInMorning()
    {
        Debug.Log(DayCount);
        switch (DayCount)
        {
            case 1:
                //yang�����¼�

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
                //���ź�ż�����˺��¼� ���Ǽٳ��ŵİ�ť
                if(!eventList_SO.HomeEventList.Find(i => i.EventID == 5002).isEventOver)
                {
                    var newButtton2 = Instantiate(HomeEventButtonPrefab, ParentCanvas);
                    newButtton2.GetComponent<HomeEventUI>().Init(5002);
                }

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


    /// <summary>
    /// ��ȫ��NPC�¼�ÿ��ˢ��
    /// </summary>
    public void instantiateNPCEventInSafetyOne()
    {
        switch (DayCount)
        {
            case 1:
                var newNpc1_1 = Instantiate(NPC, NpcParent);
                newNpc1_1.GetComponent<NPCDialogueController>().Init(6001);

                var newNpc1_2 = Instantiate(NPC, NpcParent);
                newNpc1_2.GetComponent<NPCDialogueController>().Init(6002);

                var newNpc1_3 = Instantiate(NPC, NpcParent);
                newNpc1_3.GetComponent<NPCDialogueController>().Init(6003);

                var newNpc1_4 = Instantiate(NPC, NpcParent);
                newNpc1_4.GetComponent<NPCDialogueController>().Init(6101);

                var newNpc1_5 = Instantiate(NPC, NpcParent);
                newNpc1_5.GetComponent<NPCDialogueController>().Init(6102);

                var newNpc1_6 = Instantiate(NPC, NpcParent);
                newNpc1_6.GetComponent<NPCDialogueController>().Init(6103);

                var newNpc1_7 = Instantiate(NPC, NpcParent);
                newNpc1_7.GetComponent<NPCDialogueController>().Init(6104);

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
}
