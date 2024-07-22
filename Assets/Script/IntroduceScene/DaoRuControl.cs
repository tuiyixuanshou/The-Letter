using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DaoRuControl : MonoBehaviour
{

    public Image Kaichangbai;
    public Text MoveText;
    private CanvasGroup fadeCanvasGroup;
    private float TextOriPos;
    public float TextTarPos;

    public float movespeed;
    public Image Panel1;

    [Header("开场画面信息")]
    public List<DialogueData> currentDialList;
    private Stack<DialogueData> DialStack = new();
    public List<DialogueListElement> dialogueListElement;
    private bool isTalking;
    private bool StartTalk;

    public void Start()
    {
        //DayManager.Instance.DayCount = -1;
        fadeCanvasGroup = GameObject.FindGameObjectWithTag("FadeCanvas").GetComponent<CanvasGroup>();
        TextOriPos = Kaichangbai.rectTransform.anchoredPosition.y;
        //新建对话堆栈
        if(currentDialList != null)
        {
            BuildDialStack(currentDialList);
        }

        StartCoroutine(StartGameDaoru());
    }



    IEnumerator StartGameDaoru()
    {
        yield return new WaitForSeconds(0.5f);

        yield return KaichangbaiStart();

        yield return TransitionFadeCanvas(1, 0.6f);

        Kaichangbai.gameObject.SetActive(false);
        Panel1.gameObject.SetActive(false);

        yield return TransitionFadeCanvas(0, 0.2f);

        yield return DoDialogue();

    }


    IEnumerator KaichangbaiStart()
    {
        Debug.Log("do start");
        float textCurPos = MoveText.rectTransform.anchoredPosition.y;
        while (!Mathf.Approximately(textCurPos, TextTarPos))
        {
            textCurPos = Mathf.MoveTowards(textCurPos, TextTarPos, movespeed * Time.deltaTime);
            MoveText.rectTransform.anchoredPosition = new Vector2(MoveText.rectTransform.anchoredPosition.x,
                textCurPos);
            yield return new WaitForSeconds(0.01f);
        }

        yield return new WaitForSeconds(1.5f);
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

    //爆炸后黑屏后 改变黑屏的sortingOrder 记得要改回来  尖叫
    public void FadeAfterExplode()
    {
        Debug.Log("Do explode");
        StartCoroutine(TransitionFadeCanvas(1, 0.6f));

        GameObject.FindGameObjectWithTag("FadeCanvas").GetComponent<Canvas>().sortingOrder = 1;
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
            // StartCoroutine(DialFade(0.0f, 0.2f));
            //enemyDetails.AfterDialFinish?.Invoke();
        }
    }


    private void Update()
    {
        if(StartTalk && (Input.GetKeyDown(KeyCode.Space) || Input.GetMouseButtonDown(0))&& !isTalking )
        {
            StartCoroutine(DoDialogue());
        }
    }

    public void StartDialAfterChoose(int dialIndex)
    {
        BuildDialStack(dialogueListElement[dialIndex].DialListElem);
        StartCoroutine(DoDialogue());
    }

    public void ResetFadeCanvasSort()
    {
        EventHandler.CallShowSentenceInDialUI(null);
        GameObject.FindGameObjectWithTag("FadeCanvas").GetComponent<Canvas>().sortingOrder = 5;
        EventHandler.CallButton_MaptoHome();
    }
}
