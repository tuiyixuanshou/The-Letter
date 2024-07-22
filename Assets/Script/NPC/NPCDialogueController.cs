using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class NPCDialogueController : MonoBehaviour
{
    [Header("NPC基本信息")]
    public int NPCID;
    public SpriteRenderer npcFace;
    public GameObject NPCUI;
    public BoxCollider2D coll;

    [Header("NPC通过Init获取的信息")]
    public string NPCName;
    public NPCDetails npcDetails;
    
    private List<DialogueData> NPCdialDataList = new List<DialogueData>();
    private List<DialogueData> NPCdialDataList1 = new List<DialogueData>();

    private Stack<DialogueData> NPCdialDataStack = new Stack<DialogueData>();
    private Stack<DialogueData> NPCdialDataStack1 = new Stack<DialogueData>();



    [Header("NPC对话信息")]
    //是否可以开始和NPC对话
    public bool CanStartTalk;
    //和NPC对话的次数
    public int TalkTimes;
    public bool StartTalk;

    //说完的行数
    //public int currentTalkLine;
    //是否正在和NPC对话
    private bool isTalking;

    private int StackCount;
    private int StackCount1;

    [Header("对话后发生的事件")]
    public UnityEvent OnFinishNPCDialogue;

    private void Awake()
    {
        NPCUI.SetActive(false);
        CanStartTalk = false;
        isTalking = false;
        //currentTalkLine = 0;
    }

    private void Start()
    {
        if(NPCID!= 0)
        {
            Init(NPCID);
        }
    }


    public void Init(int ID)
    {
        NPCID = ID;
        npcDetails = NPCManager.Instance.GetNPCDetailsFromID(NPCID);
        NPCName = npcDetails.NPCName;

        npcFace.sprite = npcDetails.NPCFace;

        Vector2 newsize = new Vector2(npcFace.sprite.bounds.size.x, npcFace.sprite.bounds.size.y/2);
        coll.size = newsize;
        coll.offset = new Vector2(0, npcFace.sprite.bounds.center.y/2);

        this.transform.localPosition = npcDetails.NPCposition.ToVector3();

        NPCdialDataList = npcDetails.dialogueDatas;
        NPCdialDataList1 = npcDetails.dialogueDatas1;

        BuildNPCDialStack();
        BuildNPCDialStack1();
        StackCount = NPCdialDataStack.Count;
        StackCount1 = NPCdialDataStack1.Count;

    }


    private void BuildNPCDialStack()
    {
        NPCdialDataStack = new Stack<DialogueData>();
        for (int i = NPCdialDataList.Count - 1; i > -1; i--)
        {
            NPCdialDataList[i].isOver = false;
            NPCdialDataStack.Push(NPCdialDataList[i]);
            //Debug.Log("Push Dialogue" + i);
        }
    }

    private void BuildNPCDialStack1()
    {
        NPCdialDataStack1 = new Stack<DialogueData>();
        for (int i = NPCdialDataList1.Count - 1; i > -1; i--)
        {
            NPCdialDataList1[i].isOver = false;
            NPCdialDataStack1.Push(NPCdialDataList1[i]);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Player")
        {
            //NPCUI.SetActive(true);
            CanStartTalk = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if(collision.tag == "Player")
        {
            //NPCUI.SetActive(false);
            CanStartTalk = false;

            //将对话归零
            //currentTalkLine = 0;
        }
    }

    private void Update()
    {
        NPCUI.SetActive(CanStartTalk);
        if (CanStartTalk && !isTalking && Input.GetKeyDown(KeyCode.Space))
        {
            StartCoroutine(DoNPCDialogue());
        }
    }

    private IEnumerator DoNPCDialogue()
    {
        isTalking = true;
        StartTalk = true;
        //认真的话
        if(TalkTimes == 0)
        {
            if(NPCdialDataStack.TryPop(out DialogueData result))
            {
                //currentTalkLine++;
                //var dialData = NPCdialDataStack.Pop();
                EventHandler.CallShowSentenceInDialUI(result);
                yield return new WaitUntil(() => result.isOver == true);
                
                isTalking = false;
            }
            else
            {
                EventHandler.CallShowSentenceInDialUI(null);
                isTalking = false;
                CanStartTalk = false;
                StartTalk = false;
                TalkTimes++;
                
                //重新装填栈
                BuildNPCDialStack();

                OnFinishNPCDialogue?.Invoke();
            }
            
        }
        else
        {
            //一些其他的话
            if (NPCdialDataStack1.TryPop(out DialogueData result))
            {
                //currentTalkLine++;
                EventHandler.CallShowSentenceInDialUI(result);
                yield return new WaitUntil(() => result.isOver == true);

                isTalking = false;
            }
            else
            {
                EventHandler.CallShowSentenceInDialUI(null);
                isTalking = false;
                CanStartTalk = false;
                StartTalk = false;
                TalkTimes++;
                //重新装填栈
                BuildNPCDialStack1();
            }
        }
    }
}
