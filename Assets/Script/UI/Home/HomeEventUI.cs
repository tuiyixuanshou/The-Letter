using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using System;

public class HomeEventUI : MonoBehaviour
{
    [Header("事件ID")]
    public int HomeEventID;
    public string HomeEventName;
    public HomeEvent homeEvent;

    [Header("事件位置")]
    public Vector3 RectPos;

    [Header("对话后发生的事件")]
    public UnityEvent OnFinishDialogue;

    [Header("对话内容")]
    public List<DialogueData> dialogueDataList = new List<DialogueData>();


    public Image UIsymbol;

    //是否开始进入对话
    public bool StartTalk = false;

    //是否正在播放一句话
    private bool isTalking = false;

    //控制第一句话
    private bool hasStrat = false;

    //对话堆栈
    private Stack<DialogueData> dialogueStack = new Stack<DialogueData>();
    //堆栈内含元素个数
    private int StackCount;

    //控制当前数量
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

        //从第0句开始
        //CurrentStackCount = 0;
        
    }

    //初始化HomeEvent按钮
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
    /// 构建对话堆栈
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
    
    //播放句子入口,仅播放一句
    private IEnumerator DoDialogue()
    {
        //此句开始播放
        isTalking = true;
        this.GetComponent<Image>().color = new Color(1, 1, 1, 0);

        //要此段对话已完成 游戏开始时就标记
        HomeEventManager.Instance.MarkEventDone(HomeEventID);

        if (dialogueStack.TryPop(out DialogueData dialData))
        {
            //var dialData = dialogueStack.Pop();
            
            //Debug.Log("Pop一次");
            //CurrentStackCount++;

            //DialogueUI注册了这个事件
            EventHandler.CallShowSentenceInDialUI(dialData);

            yield return new WaitUntil(() => dialData.isOver);

            //播放结束
            isTalking = false;

        }
        else
        {
            //对话播放完毕之后
            EventHandler.CallShowSentenceInDialUI(null);

            StartTalk = false;
            isTalking = false;

            //下次再次对话使用
            //BuildDialogueStack();

            //新事件的生成
            OnFinishDialogue?.Invoke();


            //特殊事件重置
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
                //相关事件生成之后就可以消除这个按钮了
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
