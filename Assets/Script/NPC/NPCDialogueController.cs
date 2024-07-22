using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class NPCDialogueController : MonoBehaviour
{
    [Header("NPC������Ϣ")]
    public int NPCID;
    public SpriteRenderer npcFace;
    public GameObject NPCUI;
    public BoxCollider2D coll;
    public Animator NPCanim;
    public Rigidbody2D rb;
    public Sprite NPCUItanhao;
    private int AnimID;

    [Header("NPCͨ��Init��ȡ����Ϣ")]
    public string NPCName;
    public NPCDetails npcDetails;
    
    private List<DialogueData> NPCdialDataList = new List<DialogueData>();
    private List<DialogueData> NPCdialDataList1 = new List<DialogueData>();

    private Stack<DialogueData> NPCdialDataStack = new Stack<DialogueData>();
    private Stack<DialogueData> NPCdialDataStack1 = new Stack<DialogueData>();



    [Header("NPC�Ի���Ϣ")]
    //�Ƿ���Կ�ʼ��NPC�Ի�
    public bool CanStartTalk;
    //��NPC�Ի��Ĵ���
    public int TalkTimes;
    public bool StartTalk;

    //˵�������
    //public int currentTalkLine;
    //�Ƿ����ں�NPC�Ի�
    private bool isTalking;

    private Player player;

    [Header("�Ի��������¼�")]
    public UnityEvent OnFinishNPCDialogue;

    [Header("������")]
    private float leftpx;
    private float rightpx;
    public bool Faceleft = true;
    public float speed;
    private bool isMoving = true;

    private bool isInfo;
    public string SceneName;

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
        SceneName = SceneManager.GetActiveScene().name;
        //var ui = transform.GetChild(0);
        //transform.DetachChildren();

        //ui.SetParent(this.transform);
    }


    public void Init(int ID)
    {
        NPCID = ID;
        npcDetails = NPCManager.Instance.GetNPCDetailsFromID(NPCID);
        NPCName = npcDetails.NPCName;
        AnimID = npcDetails.AnimID;
        npcFace.sprite = npcDetails.NPCFace;

        Vector2 newsize = new Vector2(npcFace.sprite.bounds.size.x, npcFace.sprite.bounds.size.y/2);
        coll.size = newsize;
        coll.offset = new Vector2(0, npcFace.sprite.bounds.center.y/2);

        NPCUI.transform.localPosition = new Vector3(0, newsize.y * 2 + 0.5f);
        if (NPCID > 8100 && NPCID < 8200)
        {
            //������
            NPCUI.GetComponent<SpriteRenderer>().sprite = NPCUItanhao;
            isInfo = true;
            StopAnimAndShowWhights();
        }
        else if (npcDetails.UnNeedAnim)
        {
            isMoving = false; NPCanim.enabled = false;
        }
        this.transform.localPosition = npcDetails.NPCposition.ToVector3();

        //���ƶ�����
        NPCanim.SetFloat("NPCID", AnimID);
        leftpx = npcDetails.leftpx;
        rightpx = npcDetails.rightpx;
        speed = npcDetails.speed;

        NPCdialDataList = npcDetails.dialogueDatas;
        NPCdialDataList1 = npcDetails.dialogueDatas1;

        BuildNPCDialStack();
        BuildNPCDialStack1();

    }

    void Movement()
    {
        if (leftpx != rightpx)
        {
            if (Faceleft)
            {
                Vector2 Input = new Vector2(-1, 0);
                rb.MovePosition(rb.position + Input * speed * Time.deltaTime);
                if (transform.position.x < leftpx)
                {
                    transform.localScale = new Vector3(-1, 1, 1);
                    Faceleft = false;
                }
            }
            else
            {
                Vector2 Input = new Vector2(1, 0);
                rb.MovePosition(rb.position + Input * speed * Time.deltaTime);
                if (transform.position.x > rightpx)
                {
                    transform.localScale = new Vector3(1, 1, 1);
                    Faceleft = true;
                }
            }
        }
        
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
            player = collision.GetComponent<Player>();
            //NPCUI.SetActive(true);
            CanStartTalk = true;
            StopAnimAndShowWhights();
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if(collision.tag == "Player")
        {
            //NPCUI.SetActive(false);
            CanStartTalk = false;
            if (!isInfo)
            {
                ReStartAnimAndShowBlack();
            }
            //���Ի�����
            //currentTalkLine = 0;
        }
    }

    private void Update()
    {
        NPCUI.SetActive(CanStartTalk);
        if (CanStartTalk && !isTalking && Input.GetKeyDown(KeyCode.Space)&& !isInfo)
        {
            StartCoroutine(DoNPCDialogue());
        }
        else if(CanStartTalk && !isTalking && Input.GetKeyDown(KeyCode.F) && isInfo)
        {
            StartCoroutine(DoNPCDialogue());
        }
        if (isMoving)
        {
            Movement();
        }
    }

    private IEnumerator DoNPCDialogue()
    {

        isTalking = true;
        StartTalk = true;
        BattleSystem.Instance.gameClockPause = true;
        TimeManager.Instance.gameClockPause = true;
        player.InputDisable = true;
        //����Ļ�
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
                if (!isInfo)
                {
                    ReStartAnimAndShowBlack();
                }
                EventHandler.CallShowSentenceInDialUI(null);
                DayManager.Instance.NPCCurNum++;
                isTalking = false;
                CanStartTalk = false;
                StartTalk = false;
                if (SceneName != "Church")
                {
                    BattleSystem.Instance.gameClockPause = false;
                    TimeManager.Instance.gameClockPause = false;
                }
                player.InputDisable = false;
                TalkTimes++;
                
                //����װ��ջ
                BuildNPCDialStack();
                NPCManager.Instance.npcList_SO.NPCDetailsList.Find(i => i.NPCID == NPCID).isThisAppear = true;
                OnFinishNPCDialogue?.Invoke();
            }
            
        }
        else
        {
            //һЩ�����Ļ�
            if (NPCdialDataStack1.TryPop(out DialogueData result))
            {
                //currentTalkLine++;
                EventHandler.CallShowSentenceInDialUI(result);
                yield return new WaitUntil(() => result.isOver == true);

                isTalking = false;
            }
            else
            {
                if (!isInfo)
                {
                    ReStartAnimAndShowBlack();
                }
                if(SceneName != "Church")
                {
                    BattleSystem.Instance.gameClockPause = false;
                    TimeManager.Instance.gameClockPause = false;
                }
                player.InputDisable = false;
                EventHandler.CallShowSentenceInDialUI(null);
                isTalking = false;
                CanStartTalk = false;
                StartTalk = false;


                TalkTimes++;
                //����װ��ջ
                BuildNPCDialStack1();
            }
        }
    }


    public void StopAnimAndShowWhights()
    {
        isMoving = false;
        NPCanim.enabled = false;
        if (!isInfo)
        {
            npcFace.sprite = npcDetails.CanTalkFace;
        }
        else
        {
            npcFace.sprite = npcDetails.NPCFace; 
        }
        
    }

    public void ReStartAnimAndShowBlack()
    {
        npcFace.sprite = npcDetails.NPCFace;
        if (!npcDetails.UnNeedAnim)
        {
            isMoving = true;
            NPCanim.enabled = true;
            NPCanim.SetFloat("NPCID", NPCID);
            leftpx = npcDetails.leftpx;
            rightpx = npcDetails.rightpx;
            speed = npcDetails.speed;
        }
        else
        {
            isMoving = false;
            NPCanim.enabled = false;
        }
        
    }


    //ѡ����֮��������ŶԻ�֮��Ĳ���
    public void StartDoDialogue()
    {
        npcFace.sprite = npcDetails.CanTalkFace;
        //DayManager.Instance.NPCCurNum--;
        StartCoroutine(DoNPCDialogue());
    }
}
