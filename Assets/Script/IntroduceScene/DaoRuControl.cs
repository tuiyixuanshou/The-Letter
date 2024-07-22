using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Playables;
using UnityEngine.Timeline;

public class DaoRuControl : MonoBehaviour
{

    //public Image Kaichangbai;
    //public Text MoveText;

    //private float TextOriPos;
    //public float TextTarPos;

    //public float movespeed;
    //public Image Panel1;
    [Header("导入视频和动画控制")]
    public PlayableDirector director;
    //public GameObject audioSource;
    public TimelineAsset[] A;
    public float countTime = 0f;
    public GameObject video;
    public GameObject KaichangManhua;
    int index = 0;


    private CanvasGroup fadeCanvasGroup;
    [Header("开场画面信息")]
    public List<DialogueData> currentDialList;
    private Stack<DialogueData> DialStack = new();
    public List<DialogueListElement> dialogueListElement;
    private bool isTalking;
    private bool StartTalk;

    public void Start()
    {
        fadeCanvasGroup = GameObject.FindGameObjectWithTag("FadeCanvas").GetComponent<CanvasGroup>();
        //新建对话堆栈
        if(currentDialList != null)
        {
            BuildDialStack(currentDialList);
        }

        //StartCoroutine(StartGameDaoru());
    }



    IEnumerator StartGameDaoru()
    {
        yield return new WaitForSeconds(0.5f);

        //yield return KaichangbaiStart();

        yield return TransitionFadeCanvas(1, 0.6f);

        //Kaichangbai.gameObject.SetActive(false);
        //Panel1.gameObject.SetActive(false);

        //修改BGM
        AudioManager.Instance.ChangeBGMInDial("CalmQuickMusic3");

        yield return TransitionFadeCanvas(0, 0.5f);

        yield return DoDialogue();

    }


    //IEnumerator KaichangbaiStart()
    //{
    //    Debug.Log("do start");
    //    float textCurPos = MoveText.rectTransform.anchoredPosition.y;
    //    while (!Mathf.Approximately(textCurPos, TextTarPos))
    //    {
    //        textCurPos = Mathf.MoveTowards(textCurPos, TextTarPos, movespeed * Time.deltaTime);
    //        MoveText.rectTransform.anchoredPosition = new Vector2(MoveText.rectTransform.anchoredPosition.x,
    //            textCurPos);
    //        yield return new WaitForSeconds(0.01f);
    //    }

    //    yield return new WaitForSeconds(1.5f);
    //}

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


    public void ResetFade()
    {
        StartCoroutine(TransitionFadeCanvas(0, 0.4f));
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
        if (countTime <= 63.05f)
        {
            CountTime();
        }

        if (KaichangManhua.activeInHierarchy)
        {
            ChangeTimeLine();
        }
        
        if (StartTalk && (Input.GetKeyDown(KeyCode.Space) || Input.GetMouseButtonDown(0))&& !isTalking )
        {
            StartCoroutine(DoDialogue());
        }
    }

    public void ChangeTimeLine()
    {
        if (Input.GetMouseButtonDown(0) && countTime > 63f)
        {
            if (index < A.Length)
            {
                if (director.state != PlayState.Playing)
                {
                    director.Play(A[index++]);
                }
            }
            else
            {
                if(director.state != PlayState.Playing)
                {
                    StartCoroutine(AfterKaichangbai());
                }
                
            }
        }

    }
    public void CountTime()
    {
        countTime += Time.deltaTime;
        if (countTime >= 63f)
        {
            video.SetActive(false);
            AudioManager.Instance.ChangeBGMInDial("KaichangMusic");
            //audioSource.SetActive(true);
        }

    }

    IEnumerator AfterKaichangbai()
    {
        yield return TransitionFadeCanvas(1, 0.3f);
        KaichangManhua.SetActive(false);
        //修改BGM
        AudioManager.Instance.ChangeBGMInDial("CalmQuickMusic3");

        yield return TransitionFadeCanvas(0, 0.25f);

        yield return DoDialogue();
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
