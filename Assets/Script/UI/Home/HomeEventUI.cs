using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using System;

public class HomeEventUI : MonoBehaviour
{
    [Header("�¼�ID")]
    public int HomeEventID;
    public string HomeEventName;
    public HomeEvent homeEvent;

    [Header("�¼�λ��")]
    public Vector3 RectPos;

    [Header("�Ի��������¼�")]
    public UnityEvent OnFinishDialogue;

    [Header("�Ի�����")]
    public List<DialogueData> dialogueDataList = new List<DialogueData>();


    public Image UIsymbol;

    //�Ƿ�ʼ����Ի�
    public bool StartTalk = false;

    //�Ƿ����ڲ���һ�仰
    private bool isTalking = false;

    //���Ƶ�һ�仰
    private bool hasStrat = false;

    //�Ի���ջ
    private Stack<DialogueData> dialogueStack = new Stack<DialogueData>();
    //��ջ�ں�Ԫ�ظ���
    private int StackCount;

    //���Ƶ�ǰ����
    //private int CurrentStackCount;


    private void OnEnable()
    {
        EventHandler.AfterSceneLoadMove += OnAfterSceneLoadMove;
    }

    private void OnDisable()
    {

        EventHandler.AfterSceneLoadMove -= OnAfterSceneLoadMove;
    }

    private void Start()
    {
        
        if(HomeEventID != 0)
        {
            Init(HomeEventID);
        }

        //�ӵ�0�俪ʼ
        //CurrentStackCount = 0;
        
    }

    //��ʼ��HomeEvent��ť
    public void Init(int ID)
    {
        HomeEventID = ID;
        homeEvent = HomeEventManager.Instance.GetHomeEventFromEventID(HomeEventID);
        HomeEventName = homeEvent.EventName;
        dialogueDataList = homeEvent.dialogueDatas;

        if (homeEvent.UISymbol != null)
        {
            UIsymbol.sprite = homeEvent.UISymbol;
            UIsymbol.SetNativeSize();
        }

        BuildDialogueStack();
        StackCount = dialogueStack.Count;

        RectPos = new Vector3(homeEvent.rectPos.x, homeEvent.rectPos.y, 0);

        this.GetComponent<RectTransform>().anchoredPosition = RectPos;
    }
    
    /// <summary>
    /// �����Ի���ջ
    /// </summary>
    private void BuildDialogueStack()
    {
        dialogueStack = new Stack<DialogueData>();
        for(int i = dialogueDataList.Count - 1; i > -1; i--)
        {
            dialogueDataList[i].isOver = false;
            dialogueStack.Push(dialogueDataList[i]);
            //Debug.Log("Push Dialogue" + i);
        }
    }
    
    //���ž������,������һ��
    private IEnumerator DoDialogue()
    {
        //�˾俪ʼ����
        isTalking = true;
        this.GetComponent<Image>().color = new Color(1, 1, 1, 0);

        //Ҫ�˶ζԻ������ ��Ϸ��ʼʱ�ͱ��
        HomeEventManager.Instance.MarkEventDone(HomeEventID);

        if (dialogueStack.TryPop(out DialogueData dialData))
        {
            //var dialData = dialogueStack.Pop();
            
            //Debug.Log("Popһ��");
            //CurrentStackCount++;

            //DialogueUIע��������¼�
            EventHandler.CallShowSentenceInDialUI(dialData);

            yield return new WaitUntil(() => dialData.isOver);

            //���Ž���
            isTalking = false;

        }
        else
        {
            //�Ի��������֮��
            EventHandler.CallShowSentenceInDialUI(null);

            StartTalk = false;
            isTalking = false;

            //�´��ٴζԻ�ʹ��
            //BuildDialogueStack();

            //���¼�������
            OnFinishDialogue?.Invoke();


            //�����¼�����
            if(HomeEventID == 5003|| HomeEventID == 5004)
            {
                GameObject.FindGameObjectWithTag("FadeCanvas").GetComponent<Canvas>().sortingOrder = 5;

                EventHandler.CallHomeToMap();
                //IEnumerator CloseHomeEventDialogue()
                //    {
                //        yield return new WaitUntil(() => GameObject.FindGameObjectWithTag("FadeCanvas").GetComponent<CanvasGroup>().alpha < 0.1f);
                //        Destroy(this.gameObject);
                //    }
                //    StartCoroutine(CloseHomeEventDialogue());
            }
            else
            {
                //����¼�����֮��Ϳ������������ť��
                Destroy(this.gameObject);
            }


          
        }

    }

    public void StartDoDialogue()
    {
        StartCoroutine(DoDialogue());
    }


    public void ButtonPressed()
    {
        if (HomeEventID == 5002)
        {
            Debug.Log("do 5002");
            GameObject.FindGameObjectWithTag("FadeCanvas").GetComponent<Canvas>().sortingOrder = 1;
            StartTalk = true;
            if(StartTalk && !isTalking)
            {
                StartCoroutine(StartSpecialEvent());
            }
            
        }
        else
        {
            StartTalk = true;
            if (StartTalk && isTalking == false)
            {
                StartCoroutine(DoDialogue());
            }
        }

        
       
    }


    IEnumerator StartSpecialEvent()
    {
        yield return StartCoroutine(TransitionFadeCanvas
                (GameObject.FindGameObjectWithTag("FadeCanvas").GetComponent<CanvasGroup>(), 1f, 1f));
        yield return StartCoroutine(DoDialogue());
    }


    private void Update()
    {
        if (StartTalk && (Input.GetKeyDown(KeyCode.Space)||Input.GetMouseButtonDown(0)) && isTalking == false)
        {
            StartCoroutine(DoDialogue());
        }
    }

    IEnumerator TransitionFadeCanvas(CanvasGroup fadeCanvasGroup, float targetAlpha, float transitionFadeDuration)
    {
        fadeCanvasGroup.blocksRaycasts = true;
        float speed = Mathf.Abs(fadeCanvasGroup.alpha - targetAlpha) / transitionFadeDuration;
        while (!Mathf.Approximately(fadeCanvasGroup.alpha, targetAlpha))
        {
            fadeCanvasGroup.alpha = Mathf.MoveTowards(fadeCanvasGroup.alpha, targetAlpha, speed * Time.deltaTime);
            yield return null;
        }
        fadeCanvasGroup.blocksRaycasts = false;
        yield return new WaitForSeconds(1.5f);
    }


    private void OnAfterSceneLoadMove()
    {
        if(HomeEventID == 5001 && DayManager.Instance.DayCount == 1)
        {
            ButtonPressed();
        }
    }
}
