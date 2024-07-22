using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

public class Enemy : MonoBehaviour
{
    [Header("基本信息")]
    public int EnemyID;
    public EnemyDetails enemyDetails;
    public SpriteRenderer EnemyspriteRenderer;
    private BoxCollider2D coll;

    [Header("对话信息")]
    //public List<DialogueData> currentDialList = new();
    public List<DialogueData> currentDialList;
    public int DialIndex;
    public bool StartTalk;
    public bool isTalking;
    //private Stack<DialogueData> DialStack = new();
    private Stack<DialogueData> DialStack;

    [Header("对战信息")]
    public int currentHP;

    private Image EnemyDialogueFadePanel;

    private bool isStartFightLater;
    private bool isEnemyTurn;



    private void Awake()
    {
        coll = GetComponent<BoxCollider2D>();
    }

    private void Start()
    {
        //if(EnemyID != 0)
        //{
        //    //Init(EnemyID,DialIndex);
        //}
    }

    public void Init(int ID,int dialIndex)
    {
        Debug.Log("do init");
        EnemyID = ID;
        enemyDetails = BattleSystem.Instance.GetEnemyDetailsFromID(ID);
        if (enemyDetails != null)
        {
            EnemyspriteRenderer.sprite = enemyDetails.EnemyFace;
        }
        currentHP = enemyDetails.MaxHP;

        //获得当前对话信息
        currentDialList = enemyDetails.dialogueElemList[dialIndex].DialListElem;
        BuildDialStack();

        //逃跑失败
        if (dialIndex == 3 || dialIndex == 2)
            isEnemyTurn = true;
        else
            isEnemyTurn = false;

        Vector2 newsize = new Vector2(EnemyspriteRenderer.sprite.bounds.size.x, EnemyspriteRenderer.sprite.bounds.size.y);
        coll.size = newsize;
        coll.offset = new Vector2(0, EnemyspriteRenderer.sprite.bounds.size.y);

        //EnemyDialogueFadePanel = GameObject.FindGameObjectWithTag("FadePanelforDial").GetComponent<Image>();
    }

    //构建对话堆栈
    private void BuildDialStack()
    {
        DialStack = new Stack<DialogueData>();
        for (int i = currentDialList.Count - 1; i > -1; i--)
        {
            //Debug.Log("do stack");
            currentDialList[i].isOver = false;
            DialStack.Push(currentDialList[i]);
        }
    }

    IEnumerator DoDialogue()
    {

        //StartCoroutine(DialFade(0.6f, 0.5f));
        StartTalk = true;
        isTalking = true;
        if (DialStack.TryPop(out DialogueData dialData))
        {
            if(dialData.isFight)
            {
                isStartFightLater = true;
            }
            EventHandler.CallShowSentenceInDialUI(dialData);
            yield return new WaitUntil(() => dialData.isOver);
            isTalking = false;
        }
        else
        {
            EventHandler.CallShowSentenceInDialUI(null);
            isTalking = false;
            StartTalk = false;
            if (isStartFightLater)
            {
                Debug.Log("开始打架");
                TheBattle.Instance.StartBattle(isEnemyTurn);


                //稍后变回来
                isStartFightLater = false;
            }
           // StartCoroutine(DialFade(0.0f, 0.2f));
            //enemyDetails.AfterDialFinish?.Invoke();
        }
    }

    //开始携程
    public void StartDoDialogue()
    {
        StartCoroutine(DoDialogue());
    }


    IEnumerator DialFade(float targetAlpha, float fadeDuration)
    {
            float speed = Mathf.Abs(EnemyDialogueFadePanel.color.a - targetAlpha) / fadeDuration;

            while (!Mathf.Approximately(EnemyDialogueFadePanel.color.a, targetAlpha))
            {
                float a = Mathf.MoveTowards(EnemyDialogueFadePanel.color.a, targetAlpha, speed * Time.deltaTime);
                EnemyDialogueFadePanel.color = new Color(EnemyDialogueFadePanel.color.r, EnemyDialogueFadePanel.color.b, EnemyDialogueFadePanel.color.g, a);
                yield return null;
            }

    }


    public int TakeDamage(int dmg,int defense)
    {
        
        currentHP -= (int)(dmg* defense*0.01f);
        if (currentHP <= 0)
        {
            return 0;
        }
        else
            return currentHP;

    }

    public void Heal(int amount)
    {
        currentHP += amount;
        if(currentHP > enemyDetails.MaxHP)
        {
            currentHP = enemyDetails.MaxHP;
        }
    }

}
