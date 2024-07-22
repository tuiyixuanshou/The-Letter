using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class EndingControl : MonoBehaviour
{
    private CanvasGroup fadeCanvasGroup;
    private List<DialogueData> currentDialList = new();
    private Stack<DialogueData> DialStack = new();

    public List<DialogueListElement> dialListElemList;
    [Header("对话后发生的事件")]
    public UnityEvent OnFinishNPCDialogue;

    private bool isTalking;
    private bool StartTalk;

    public GameObject End1;
    public GameObject End2;


    private void OnEnable()
    {
        EventHandler.NewGameEmptyData += EmptyOriData;
    }

    private void OnDisable()
    {
        EventHandler.NewGameEmptyData -= EmptyOriData;
    }

    private void EmptyOriData()
    {
        foreach(var i in End1.GetComponentsInChildren<Transform>())
        {
            i.gameObject.SetActive(false);
        }
        foreach (var i in End2.GetComponentsInChildren<Transform>())
        {
            i.gameObject.SetActive(false);
        }
        End1.SetActive(false);End2.SetActive(false);
    }

    private void Start()
    {
        fadeCanvasGroup = GameObject.FindGameObjectWithTag("FadeCanvas").GetComponent<CanvasGroup>();
        PaddingEnd();
        StartCoroutine(StartEndingControl());
    }

    public void PaddingEnd()
    {
        if(EndManager.Instance.HealthNone)
        {
            currentDialList = dialListElemList[0].DialListElem;   //体力为零
        }
        else if (EndManager.Instance.SanNone)
        {
            currentDialList = dialListElemList[1].DialListElem; //精神为零
        }
        else if (EndManager.Instance.WealthNone)
        {
            currentDialList = dialListElemList[2].DialListElem;//破产
        }
        else if(EndManager.Instance.End1)
        {
            currentDialList = dialListElemList[3].DialListElem; //结局1
        }
        else if (EndManager.Instance.End2)
        {
            currentDialList = dialListElemList[4].DialListElem; //结局2
        }
        else if (EndManager.Instance.End3)
        {
            currentDialList = dialListElemList[5].DialListElem; //结局2
        }
    }

    private void BuildDialStack(List<DialogueData> currentDialList)
    {
        DialStack = new Stack<DialogueData>();
        for (int i = currentDialList.Count - 1; i > -1; i--)
        {
            currentDialList[i].isOver = false;
            DialStack.Push(currentDialList[i]);
        }
    }

    IEnumerator StartEndingControl()
    {
        BuildDialStack(currentDialList);
        yield return new WaitForSeconds(0.2f);
        yield return DoDialogue();
    }

    IEnumerator DoDialogue()
    {
        StartTalk = true;
        isTalking = true;
        if (DialStack.TryPop(out DialogueData dialData))
        {
            EventHandler.CallShowSentenceInDialUI(dialData);
            yield return new WaitUntil(() => dialData.isOver);
            isTalking = false;
        }
        else
        {
            EventHandler.CallShowSentenceInDialUI(null);
            isTalking = false;
            StartTalk = false;
            OnFinishNPCDialogue?.Invoke();
        }
    }

    private void Update()
    {

        if (StartTalk && (Input.GetKeyDown(KeyCode.Space) || Input.GetMouseButtonDown(0)) && !isTalking)
        {
            StartCoroutine(DoDialogue());
        }
    }

    public void FadeAfterExplode()
    {
        StartCoroutine(TransitionFadeCanvas(1, 0.6f));

        GameObject.FindGameObjectWithTag("FadeCanvas").GetComponent<Canvas>().sortingOrder = 1;
    }

    public void ResetFade()
    {
        StartCoroutine(TransitionFadeCanvas(0, 0.4f));
    }

    IEnumerator TransitionFadeCanvas(float targetAlpha, float transitionFadeDuration)
    {
        fadeCanvasGroup.blocksRaycasts = true;
        float speed = Mathf.Abs(fadeCanvasGroup.alpha - targetAlpha) / transitionFadeDuration;

        while (!Mathf.Approximately(fadeCanvasGroup.alpha, targetAlpha))
        {
            fadeCanvasGroup.alpha = Mathf.MoveTowards(fadeCanvasGroup.alpha, targetAlpha, speed * Time.deltaTime);
            //yield return new WaitForSeconds(0.01f);
            yield return null;
        }
        fadeCanvasGroup.blocksRaycasts = false;

        yield return new WaitForSeconds(1.5f);

    }
}
