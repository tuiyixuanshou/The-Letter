using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class CustomerController : MonoBehaviour,IPointerClickHandler
{
    public int CustomerID;

    public int DialogueIndex;

    public Text CustomerName;

    public Text CustomerType;

    public Text CustomerText;

    public Image CustomerFace;

    public CustomerDetails customerDetails;

    public GameObject DialogueButtonPanel;

    public GameObject NeedItemPanel;

    public GameObject NeedSubmitButton;

    public Dictionary<int,int> NeedItemDict = new();

    public GameObject NeedItemPrefab;

    private Image ShopDialogueFadePanel;

    private bool isTalking;
    public  bool isFadeShow;
    public int totalPrice;
    public Text nowPrice;

    public bool isStartSell;

    public Button RefusedYesButton;

    //当前对话
    public List<DialogueData> currentDialList = new ();
    public Stack<DialogueData> DialStack = new ();

    //需要的物资

    [Header("要饭遮罩")]
    public Image YaofanFade;

    private void Start()
    {
        //if(CustomerID != 0)
        //{
        //    Init(CustomerID,DialogueIndex);
        //}
        YaofanFade = GameObject.FindGameObjectWithTag("YaoFan").GetComponent<Image>();
        YaofanFade.color = new Color(1, 1, 1, 0);
        YaofanFade.raycastTarget = false;
    }

    public void Init(int ID,int dialIndex)
    {
        //获得id和对话序列
        CustomerID = ID;
        DialogueIndex = dialIndex;

        //获得其他基础信息
        customerDetails = NPCManager.Instance.GetCustomerDetailsFromID(CustomerID);
        CustomerName.text = customerDetails.CustomerName;
        CustomerFace.sprite = customerDetails.CustomerFace;
        CustomerText.text = customerDetails.CustomerText;
        CustomerType.text = customerDetails.CustomerType.ToString();

        //获取当前对话
        if (DialogueIndex != -1)
        {
            currentDialList = customerDetails.dialogueElemList[DialogueIndex].DialListElem;
            BuildDialStack();
            ShopDialogueFadePanel = GameObject.FindGameObjectWithTag("FadeShopPanel").GetComponent<Image>();
            
        }
        else
        {
            DialogueButtonPanel.SetActive(false);
        }
        //获得所需货物信息
        //if (NeedItemDict != null)
        //{
        //    NeedItemDict.Clear();
        //}

        if (NeedItemDict == null)
        {
            NeedItemDict = new();
        }
        else
        {
            //NeedItemDict.Clear();不知道为啥这句一写就报错
        }
        //Debug.Log(customerDetails.NeedItemDictionary[2304]);
        NeedItemDict = customerDetails.NeedItemDictionary;
        nowPrice.text = totalPrice.ToString();
    }

    //构建对话堆栈
    private void BuildDialStack()
    {
        DialStack = new Stack<DialogueData>();
        for(int i = currentDialList.Count - 1; i > -1; i--)
        {
            currentDialList[i].isOver = false;
            DialStack.Push(currentDialList[i]);
        }
    }



    IEnumerator ShopDialFade(float targetAlpha, float fadeDuration)
    {
        if(!isFadeShow)
        {
            float speed = Mathf.Abs(ShopDialogueFadePanel.color.a - targetAlpha) / fadeDuration;

            while (!Mathf.Approximately(ShopDialogueFadePanel.color.a, targetAlpha))
            {
                float a = Mathf.MoveTowards(ShopDialogueFadePanel.color.a, targetAlpha, speed * Time.deltaTime);
                ShopDialogueFadePanel.color = new Color(ShopDialogueFadePanel.color.r, ShopDialogueFadePanel.color.b, ShopDialogueFadePanel.color.g, a);
                yield return null;
            }
            isFadeShow = true;
        }
        
        
    }

    /// <summary>
    /// 按钮点击打开对话渐黑层
    /// </summary>
    public void StartFadeOnButton()
    {
        EventHandler.CallInitSoundEffect(AudioManager.Instance.GetSoundDetailsfromName(SoundName.ButtonClick));
        ShopDialogueFadePanel.raycastTarget = true;
        StartCoroutine(ShopDialFade(0.6f, 0.5f));
        StartCoroutine(StartShopDialogue());
        //打开渐黑层之后标记这段对话结束
        NPCManager.Instance.MarkDoneDialogue(CustomerID, DialogueIndex);
    }

    IEnumerator StartShopDialogue()
    {
        yield return new WaitUntil(() => isFadeShow);

        isTalking = true;
        if(DialStack.TryPop(out DialogueData dialData))
        {
            EventHandler.CallShowSentenceInDialUI(dialData);
            yield return new WaitUntil(() => dialData.isOver);
            isTalking = false;
        }
        

    }

    public void CloseShopDialogue()
    {
        EventHandler.CallShowSentenceInDialUI(null);
        Debug.Log("StartCorroutine");
        isFadeShow = false;
        //StartCoroutine(ShopDialFade(0f, 0.3f));
        ShopDialogueFadePanel.color = new Color(ShopDialogueFadePanel.color.r, ShopDialogueFadePanel.color.b, ShopDialogueFadePanel.color.g, 0);
        ShopDialogueFadePanel.raycastTarget = false;
        isFadeShow = false;
        DialogueButtonPanel.SetActive(false);
        NPCManager.Instance.MarkDoneDialogue(CustomerID, DialogueIndex);
    }

    private void Update()
    {
        if(!isTalking&&(Input.GetKeyDown(KeyCode.Space) || Input.GetMouseButtonDown(0))&&isFadeShow)
        {
            StartCoroutine(StartShopDialogue());
        }
    }


    //点击打开需求面板
    public void OnPointerClick(PointerEventData eventData)
    {
        if(eventData.pointerCurrentRaycast.gameObject!= null)
        {
            //Debug.Log(eventData.pointerCurrentRaycast.gameObject.name);
            if (eventData.pointerCurrentRaycast.gameObject.CompareTag("TradeUI") )
            {
                //Debug.Log("click right thing");
                //this.transform.SetAsLastSibling();这个方法不适用于又vertical layout group的 会导致顺序改变
                if (!NeedItemPanel.activeInHierarchy)
                {
                    NeedItemPanel.SetActive(true);
                    EventHandler.CallInitSoundEffect(AudioManager.Instance.GetSoundDetailsfromName(SoundName.ButtonClick));
                    YaofanFade.color = new Color(1, 1, 1, 0.8f);
                    YaofanFade.raycastTarget = true;
                    InitNeedItemPanel();
                    isStartSell = true;

                }
                else
                {
                    //无遮罩时的关闭措施
                    //NeedItemPanel.SetActive(false);
                    //isStartSell = false;
                    //totalPrice = 0;
                    //nowPrice.text = totalPrice.ToString();

                    //有遮罩时
                    //NeedItemCloseButtonPressed();
                }
                
            }
        }
    }

    public void NeedItemCloseButtonPressed()
    {
        if (NeedItemPanel.activeInHierarchy)
        {
            EventHandler.CallInitSoundEffect(AudioManager.Instance.GetSoundDetailsfromName(SoundName.ButtonClick));
            NeedItemPanel.SetActive(false);
            isStartSell = false;
            totalPrice = 0;
            nowPrice.text = totalPrice.ToString();
            YaofanFade.color = new Color(1, 1, 1, 0);
            YaofanFade.raycastTarget = false;
        }
    }

    private void InitNeedItemPanel()
    {
        foreach(var i in NeedItemPanel.GetComponentsInChildren<Transform>())
        {
            if(i.GetComponent<VerticalLayoutGroup>()== null && i.GetComponent<LayoutElement>()== null)
            {
                Destroy(i.gameObject);
            }
        }

        Debug.Log("do dictbianli");
        int k = 0;
        foreach (var j in NeedItemDict)
        {
 
            var newNeedItem = Instantiate(NeedItemPrefab, NeedItemPanel.transform);
            newNeedItem.GetComponent<NeedItemController>().InitNeedItem(j.Key, j.Value,k);

            k++;
        }

        NeedSubmitButton.transform.SetAsLastSibling();
        Debug.Log("do setSubmitButtonParent");
    }


    public void UpdatePriceTextInNeedItemController()
    {
        nowPrice.text = totalPrice.ToString();
    }

    public void RefuseYesButtonPressed()
    {
        EventHandler.CallInitSoundEffect(AudioManager.Instance.GetSoundDetailsfromName(SoundName.ButtonClick));
        this.customerDetails.isSellDone = true;
        Destroy(this.gameObject);
    }
}
