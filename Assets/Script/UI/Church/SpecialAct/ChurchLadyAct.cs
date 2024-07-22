using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ChurchLadyAct : MonoBehaviour
{
    [Header("NPC���ֵ�����")]
    public int AppearDayCount;

    [Header("�Ի���Ϣ")]
    public List<DialogueData> NPCdialDataList = new();
    public List<DialogueData> NPCdialDataList1 = new();

    private Stack<DialogueData> NPCdialDataStack = new();
    private Stack<DialogueData> NPCdialDataStack1 = new();

    //�Ƿ���Կ�ʼ��NPC�Ի�
    public bool CanStartTalk;
    public bool StartTalk;
    //�Ƿ����ں�NPC�Ի�
    private bool isTalking;
    private Player player;


    [Header("��������")]
    public Animator anim;

    [Header("����������Ϣ")]
    public GameObject NPCUI;

    [Header("�Ի��������¼�")]
    public UnityEvent OnFinishNPCDialogue;

    public bool istrueEventTrigger;
    private bool isMoving = false;

    private void OnEnable()
    {
        EventHandler.StartChurchLadyThing += OnStartChurchLadyEvent;
    }

    private void OnDisable()
    {
        EventHandler.StartChurchLadyThing -= OnStartChurchLadyEvent;
    }

    private void Start()
    {
        istrueEventTrigger = false;
        //�Ƿ���ֵ�ԭ��
        if ((AppearDayCount == -1 || AppearDayCount == DayManager.Instance.DayCount) /*&& false*/)
        {

        }
        else
        {
            this.gameObject.SetActive(false);
        }

        BuildNPCDialStack();
        BuildNPCDialStack1();
    }

    private void BuildNPCDialStack()
    {
        NPCdialDataStack = new Stack<DialogueData>();
        for (int i = NPCdialDataList.Count - 1; i > -1; i--)
        {
            NPCdialDataList[i].isOver = false;
            NPCdialDataStack.Push(NPCdialDataList[i]);
        }
    }

    private void BuildNPCDialStack1()
    {
        NPCdialDataStack1 = new Stack<DialogueData>();
        for (int i = NPCdialDataList1.Count - 1; i > -1; i--)
        {
            NPCdialDataList1[i].isOver = false;
            NPCdialDataStack1.Push(NPCdialDataList1[i]);
            Debug.Log("push" + i);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            if (!isMoving)
            {
                player = collision.GetComponent<Player>();
                CanStartTalk = true;
                StopAnimAndShowWhights();
            }
           
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            if (!isMoving)
            {
                CanStartTalk = false;
                ReStartAnimAndShowBlack();
            }
        }
    }

    private IEnumerator NormalNPCDialogue()
    {
        isTalking = true;
        StartTalk = true;
        BattleSystem.Instance.gameClockPause = true;
        TimeManager.Instance.gameClockPause = true;
        player.InputDisable = true;
        istrueEventTrigger = false;

        if (NPCdialDataStack.TryPop(out DialogueData result))
        {
            EventHandler.CallShowSentenceInDialUI(result);
            yield return new WaitUntil(() => result.isOver == true);
            isTalking = false;
        }
        else
        {
            ReStartAnimAndShowBlack();
            EventHandler.CallShowSentenceInDialUI(null);
            isTalking = false;
            CanStartTalk = false;
            StartTalk = false;
            BattleSystem.Instance.gameClockPause = false;
            TimeManager.Instance.gameClockPause = false;
            player.InputDisable = false;
            BuildNPCDialStack();
        }
    }

    private IEnumerator EventNPCDialogue()
    {
        isTalking = true;
        StartTalk = true;
        player.InputDisable = true;
        Debug.Log(NPCdialDataStack1.Count);
        if (NPCdialDataStack1.TryPop(out DialogueData result))
        {
           // Debug.Log(result.DialogueText);
            EventHandler.CallShowSentenceInDialUI(result);
            yield return new WaitUntil(() => result.isOver == true);
            isTalking = false;
        }
        else
        {
            ReStartAnimAndShowBlack();
            BattleSystem.Instance.gameClockPause = false;
            TimeManager.Instance.gameClockPause = false;
            player.InputDisable = false;
            EventHandler.CallShowSentenceInDialUI(null);
            isTalking = false;
            CanStartTalk = false;
            StartTalk = false;
            isMoving = false;
            //�����뿪
            OnFinishNPCDialogue?.Invoke();

        }
    }

    //�����¼���Ϊ������־
    public void ChurchLadyAnimEvent()
    {
        istrueEventTrigger = true;
    }


    private void OnStartChurchLadyEvent()
    {
        StartCoroutine(StartChurchLadyEventDial());
    }


    IEnumerator StartChurchLadyEventDial()
    {
        isMoving = true;
        EventHandler.CallShowSentenceInDialUI(null);
        BattleSystem.Instance.gameClockPause = true;
        TimeManager.Instance.gameClockPause = true;
        player.InputDisable = true;
        anim.SetTrigger("EventTrigger");
        yield return new WaitUntil(() => istrueEventTrigger == true);
        CanStartTalk = true;
        StartCoroutine(EventNPCDialogue());
    }




    private void Update()
    {
        NPCUI.SetActive(CanStartTalk);
        if (CanStartTalk && !isTalking && Input.GetKeyDown(KeyCode.Space) && !istrueEventTrigger)
        {
            StartCoroutine(NormalNPCDialogue());
        }
        else if(CanStartTalk && !isTalking && Input.GetKeyDown(KeyCode.Space) && istrueEventTrigger)
        {
            Debug.Log("do this");
            StartCoroutine(EventNPCDialogue());
        }
    }


    public void StopAnimAndShowWhights()
    {
        anim.enabled = false;
    }

    public void ReStartAnimAndShowBlack()
    {
        anim.enabled = true;
    }

}
