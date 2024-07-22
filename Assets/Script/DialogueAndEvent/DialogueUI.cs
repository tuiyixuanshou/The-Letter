using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogueUI : MonoBehaviour
{
    public GameObject DialoguePanel;
    public Text DialogueText;
    public Image LeftPeople, RightPeople;
    public Text LeftName, RightName;
    public GameObject NextTip;
    public float textspeed = 0.01f;
    public bool isTalking = false;
    private DialogueData currentObj;
    private Coroutine DialRoutine;

    private void Awake()
    {
        NextTip.SetActive(false);
    }

    private void OnEnable()
    {
        EventHandler.ShowSentenceInDialUI += OnShowSentenceInDialUI;
    }

    private void OnDisable()
    {
        EventHandler.ShowSentenceInDialUI -= OnShowSentenceInDialUI;
    }

    private void OnShowSentenceInDialUI(DialogueData obj)
    {
        currentObj = obj;
        DialRoutine = StartCoroutine(ShowDialSentence(currentObj));
    }

    private IEnumerator ShowDialSentence(DialogueData dialData)
    {
        if (dialData != null)
        {
            dialData.isOver = false;

            DialoguePanel.SetActive(true);
            NextTip.SetActive(false);

            DialogueText.text = string.Empty;


            if(dialData.PeopleName != string.Empty)
            {
                //人物的话
                if (dialData.isLeft)
                {
                    LeftName.text = dialData.PeopleName;
                    RightName.text = string.Empty;
                    RightPeople.gameObject.SetActive(false);

                    if (dialData.PeopleImage != null)
                    {
                        LeftPeople.gameObject.SetActive(true);
                        LeftPeople.sprite = dialData.PeopleImage;
                        LeftPeople.SetNativeSize();

                    }
                    else
                    {
                        LeftPeople.gameObject.SetActive(false);
                    }
                      
                }
                else
                {
                    RightName.text = dialData.PeopleName;
                    LeftName.text = string.Empty;
                    LeftPeople.gameObject.SetActive(false);

                    if (dialData.PeopleImage != null)
                    {
                        RightPeople.gameObject.SetActive(true);
                        RightPeople.sprite = dialData.PeopleImage;
                        RightPeople.SetNativeSize();
                    }
                    else
                    {
                        RightPeople.gameObject.SetActive(false);
                    }
                   
                }
            }
            //旁白
            else
            {
                LeftPeople.gameObject.SetActive(false);
                RightPeople.gameObject.SetActive(false);
                RightName.text = string.Empty;
                LeftName.text = string.Empty;              
            }


            //字字显示
            for (int i = 0; i < dialData.DialogueText.Length; i++)
            {
                DialogueText.text += dialData.DialogueText[i];
                yield return new WaitForSeconds(textspeed);
                isTalking = true;
            }

            isTalking = false;
            dialData.isOver = true;
            //调用语句结束事件
            dialData.AfterSentenceFinish?.Invoke();

            //显示next内容
            if(dialData.isNeedPause && dialData.isOver)
            {
                NextTip.SetActive(true);
            }
        }
        else
        {
            DialoguePanel.SetActive(false);
            //dialData.AfterDialFinish?.Invoke();
            Debug.Log("关闭对话面板");
            yield break;
        }
    }

    private void Update()
    {
        if ((Input.GetKeyDown(KeyCode.Space) || Input.GetMouseButtonDown(0)) && isTalking)
        {
            isTalking = false;
            StopCoroutine(DialRoutine);
            DialogueText.text = string.Empty;
            DialogueText.text = currentObj.DialogueText;

            currentObj.isOver = true;
            //调用语句结束事件
            currentObj.AfterSentenceFinish?.Invoke();

            //显示next内容
            if (currentObj.isNeedPause && currentObj.isOver)
            {
                NextTip.SetActive(true);
            }
        }
    }
}
