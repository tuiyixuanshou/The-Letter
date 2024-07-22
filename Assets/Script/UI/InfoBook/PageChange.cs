using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PageChange : MonoBehaviour
{
    [Header("翻页组件")]
    public Image leftButtonPage_Appear;
    public Image leftTopCover_Disappear;
    public Image leftTopPage_Disappear;
    public Image leftNextCover;
    public Image leftNextPage;
    public Image leftPageShadow;

    public Image rightButtonPage_Appear;
    public Image rightTopCover_Disappear;
    public Image rightTopPage_Disappear;
    public Image rightNextCover;
    public Image rightNextPage;
    public Image rightPageShadow;

    [Header("每一页底图")]
    public Sprite[] sprites;

    [Header("重置底图")]
    public Sprite Left;
    public Sprite Right;

    //public GameObject[] spriteGameObjects;

    [Header("翻页时间")]
    public float turningTime;
    private bool isTurning;
    public int currentPages;
    private int maxPages;
    public Image ImagePrefab;
    public Image Info20114;

    private Coroutine turningLeftCoroutine;
    private Coroutine turningRightCoroutine;

    public List<List<int>> InfoIDListPageList = new();


    private void OnEnable()
    {
        BattleSystem.Instance.gameClockPause = true;
        TimeManager.Instance.gameClockPause = true;
        maxPages = sprites.Length;
        //新建infoidListPageList
        for (int i = 0; i < maxPages; i++)
        {
            List<int> InfoIDList = new();
            switch (i)
            {
                case 0:
                    InfoIDList.Add(20001); InfoIDList.Add(20002);InfoIDList.Add(20101);InfoIDList.Add(20104); InfoIDList.Add(20006);
                    InfoIDListPageList.Add(item: InfoIDList);
                    break;
                case 1:
                    InfoIDList.Add(20007); InfoIDList.Add(20008); InfoIDList.Add(20009); InfoIDList.Add(20003); InfoIDList.Add(20004);
                    InfoIDListPageList.Add(item: InfoIDList);
                    break;
                case 2:
                    InfoIDList.Add(20010); InfoIDList.Add(20011); InfoIDList.Add(20013); InfoIDList.Add(20105); InfoIDList.Add(20103);
                    InfoIDListPageList.Add(item: InfoIDList);
                    break;
                case 3:
                    InfoIDList.Add(20102); InfoIDList.Add(20111); InfoIDList.Add(20113); InfoIDList.Add(20106); InfoIDList.Add(20110);
                    InfoIDListPageList.Add(item: InfoIDList);
                    break;
                case 4:
                    InfoIDList.Add(20114); InfoIDList.Add(20108); InfoIDList.Add(20112); InfoIDList.Add(20015);
                    InfoIDListPageList.Add(item: InfoIDList);
                    break;
                case 5:
                    InfoIDList.Add(20109); InfoIDList.Add(20116); InfoIDList.Add(20115); InfoIDList.Add(20005);
                    InfoIDListPageList.Add(item: InfoIDList);
                    break;
                case 6:
                    InfoIDList.Add(20012); InfoIDList.Add(20014); InfoIDList.Add(20016); InfoIDList.Add(20017);
                    InfoIDListPageList.Add(item: InfoIDList);
                    break;
                default:
                    InfoIDListPageList.Add(item: InfoIDList);
                    break;
            }
        }
        ResetBookImage();
    }

    private void OnDisable()
    {
        string ActiveSceneName = SceneManager.GetActiveScene().name;
        if (ActiveSceneName == "SafetyArea" || ActiveSceneName == "ResearchBase" || ActiveSceneName == "WarYield")
        {
            BattleSystem.Instance.gameClockPause = false;
            TimeManager.Instance.gameClockPause = false;
        }
    }
    private void Start()
    {
        //maxPages = sprites.Length;
        ////新建infoidListPageList
        //for (int i = 0; i < maxPages; i++)
        //{
        //    List<int> InfoIDList = new();
        //    switch (i)
        //    {
        //        case 0:
        //            InfoIDList.Add(20001);
        //            InfoIDListPageList.Add(item: InfoIDList);
        //            break;
        //        case 1:
        //            InfoIDList.Add(20002);
        //            InfoIDListPageList.Add(item: InfoIDList);
        //            break;
        //        case 2:
        //            InfoIDList.Add(20101);
        //            InfoIDListPageList.Add(item: InfoIDList);
        //            break;
        //        case 3:
        //            InfoIDListPageList.Add(item: InfoIDList);
        //            break;
        //        case 4:
        //            InfoIDListPageList.Add(item: InfoIDList);
        //            break;
        //        default:
        //            InfoIDListPageList.Add(item: InfoIDList);
        //            break;
        //    }
        //}
        //ResetBookImage();
    }

    /// <summary>
    /// 左边翻向右边
    /// </summary>
    public void Turning_Left()
    {
        if(!isTurning && currentPages - 2 >= 0)
        {
            turningLeftCoroutine = StartCoroutine(TurningLeftPage());
        }
    }

    /// <summary>
    /// 右边翻向左边
    /// </summary>
    public void Turning_Right()
    {
        if (!isTurning && currentPages + 2 <= maxPages)
        {
            turningRightCoroutine = StartCoroutine(TurningRightPage());
        }
    }

    /// <summary>
    /// 翻页携程效果，右边翻向左边
    /// </summary>
    /// <returns></returns>
    IEnumerator TurningRightPage()
    {
        if (turningTime <= 0)
            StopCoroutine(turningRightCoroutine);

        isTurning = true;
        //sibing 0 是遮罩panel
        leftButtonPage_Appear.rectTransform.SetSiblingIndex(1);
        leftTopCover_Disappear.rectTransform.SetSiblingIndex(2);
        leftNextPage.rectTransform.SetSiblingIndex(3);

        Vector3 offsetAngle = new Vector3(0, 0, -90 / (turningTime * 100));
        float currentTime = 0;
        while (true)
        {
            yield return new WaitForSeconds(0.01f);
            currentTime += 0.01f;

            if (currentTime < turningTime)
            {
                rightTopCover_Disappear.rectTransform.eulerAngles -= offsetAngle;
                rightTopPage_Disappear.rectTransform.eulerAngles += offsetAngle;

                rightNextCover.rectTransform.eulerAngles -= offsetAngle;
                rightNextPage.rectTransform.eulerAngles -= offsetAngle;

                rightPageShadow.rectTransform.eulerAngles += offsetAngle;
            }
            else
            {
                break;
            }
        }
        currentPages += 2;
        //重新设置
        ResetBookImage();
        ResetPagesAttribute();
        isTurning = false;

        StopCoroutine(turningRightCoroutine);

    }

    /// <summary>
    /// 翻页携程效果，左边翻向右边
    /// </summary>
    /// <returns></returns>
    IEnumerator TurningLeftPage()
    {
        if (turningTime <= 0)
            StopCoroutine(turningLeftCoroutine);

        isTurning = true;
        rightButtonPage_Appear.rectTransform.SetSiblingIndex(1);
        rightTopCover_Disappear.rectTransform.SetSiblingIndex(2);
        rightNextPage.rectTransform.SetSiblingIndex(3);

        Vector3 offsetAngle = new Vector3(0, 0, -90 / (turningTime * 100));
        float currentTime = 0;
        while (true)
        {
            yield return new WaitForSeconds(0.01f);
            currentTime += 0.01f;

            if (currentTime < turningTime)
            {
                leftTopCover_Disappear.rectTransform.eulerAngles += offsetAngle;
                leftTopPage_Disappear.rectTransform.eulerAngles -= offsetAngle;

                leftNextCover.rectTransform.eulerAngles += offsetAngle;
                leftNextPage.rectTransform.eulerAngles += offsetAngle;

                leftPageShadow.rectTransform.eulerAngles -= offsetAngle;
            }
            else
            {
                break;
            }
        }
        currentPages -= 2;
        //重新设置
        ResetBookImage();
        ResetPagesAttribute();
        isTurning = false;

        StopCoroutine(turningLeftCoroutine);

    }

    /// <summary>
    /// 重置页面位置
    /// </summary>
    private void ResetPagesAttribute()
    {
        leftButtonPage_Appear.rectTransform.eulerAngles = Vector3.zero;
        leftTopCover_Disappear.rectTransform.eulerAngles = Vector3.zero;
        leftTopPage_Disappear.rectTransform.eulerAngles = Vector3.zero;
        leftNextCover.rectTransform.eulerAngles = new Vector3(0, 0, 90);
        leftNextPage.rectTransform.eulerAngles = new Vector3(0, 0, 180);
        leftPageShadow.rectTransform.eulerAngles = new Vector3(0, 0, 90);

        rightButtonPage_Appear.rectTransform.eulerAngles = Vector3.zero;
        rightTopCover_Disappear.rectTransform.eulerAngles = Vector3.zero;
        rightTopPage_Disappear.rectTransform.eulerAngles = Vector3.zero;
        rightNextCover.rectTransform.eulerAngles = new Vector3(0, 0, -90);
        rightNextPage.rectTransform.eulerAngles = new Vector3(0, 0, -180);
        rightPageShadow.rectTransform.eulerAngles = new Vector3(0, 0, -90);

        leftButtonPage_Appear.rectTransform.anchoredPosition = new Vector2(0, -leftButtonPage_Appear.rectTransform.sizeDelta.y * 0.5f); 
        leftTopCover_Disappear.rectTransform.anchoredPosition = new Vector2(0, -leftTopCover_Disappear.rectTransform.sizeDelta.y * 0.5f);
        leftTopPage_Disappear.rectTransform.anchoredPosition = new Vector2(leftTopPage_Disappear.rectTransform.sizeDelta.x, -leftTopCover_Disappear.rectTransform.sizeDelta.y * 0.5f);
        leftNextCover.rectTransform.anchoredPosition = new Vector2(0, -leftNextCover.rectTransform.sizeDelta.y * 0.25f);
        leftNextPage.rectTransform.anchoredPosition = new Vector2(-leftNextPage.rectTransform.sizeDelta.x * 0.5f, -leftNextPage.rectTransform.sizeDelta.y);
        leftPageShadow.rectTransform.anchoredPosition = new Vector2(leftNextPage.rectTransform.anchoredPosition.x, leftNextPage.rectTransform.anchoredPosition.y * 0.5f);


        rightButtonPage_Appear.rectTransform.anchoredPosition = new Vector2(0, -rightButtonPage_Appear.rectTransform.sizeDelta.y * 0.5f);
        rightTopCover_Disappear.rectTransform.anchoredPosition = new Vector2(0, -rightTopCover_Disappear.rectTransform.sizeDelta.y * 0.5f);
        rightTopPage_Disappear.rectTransform.anchoredPosition = new Vector2(-rightTopPage_Disappear.rectTransform.sizeDelta.x, -rightTopCover_Disappear.rectTransform.sizeDelta.y * 0.5f);
        rightNextCover.rectTransform.anchoredPosition = new Vector2(0,-rightNextCover.rectTransform.sizeDelta.y * 0.25f);
        rightNextPage.rectTransform.anchoredPosition = new Vector2(rightNextPage.rectTransform.sizeDelta.x * 0.5f, -rightNextPage.rectTransform.sizeDelta.y);
        rightPageShadow.rectTransform.anchoredPosition = new Vector2(rightNextPage.rectTransform.anchoredPosition.x, rightNextPage.rectTransform.anchoredPosition.y * 0.5f/*+ rightPageShadow.rectTransform.sizeDelta.x*/);

    }


    private void ResetBookImage()
    {
        if(currentPages-2>= 0)
        {
            leftButtonPage_Appear.sprite = sprites[currentPages - 2];
            ResetChild(leftButtonPage_Appear.transform, currentPages - 2);
            leftNextPage.sprite = sprites[currentPages - 1];
            ResetChild(leftNextPage.transform, currentPages - 1);
        }
        else
        {
            //zheliyouwenti
            leftButtonPage_Appear.sprite = Left;
            ResetChild(leftButtonPage_Appear.transform,-1);
            leftNextPage.sprite = null;
            ResetChild(leftNextPage.transform, -1);
        }
        leftTopPage_Disappear.sprite = sprites[currentPages];
        ResetChild(leftTopPage_Disappear.transform, currentPages);

        if (currentPages + 3 < maxPages)
        {
            rightButtonPage_Appear.sprite = sprites[currentPages + 3];
            ResetChild(rightButtonPage_Appear.transform, currentPages+3);
        }
        else
        {
            rightButtonPage_Appear.sprite = Right;
            ResetChild(rightButtonPage_Appear.transform,-1);
        }

        if (currentPages + 2 < maxPages)
        {
            rightNextPage.sprite = sprites[currentPages + 2];
            ResetChild(rightNextPage.transform, currentPages + 2);
        }
        else
        {
            rightNextPage.sprite = Right;
            ResetChild(rightNextPage.transform, -1);
        }

        if (currentPages + 1 < maxPages)
        {
            rightTopPage_Disappear.sprite = sprites[currentPages + 1];
            ResetChild(rightTopPage_Disappear.transform, currentPages + 1);
        }
        else
        {
            rightTopPage_Disappear.sprite = Right;
            ResetChild(rightTopPage_Disappear.transform, -1);
        }
    }


    private void ResetChild(Transform tran ,int curP)
    {
        //删除原有子物体
        foreach(var i in tran.GetComponentsInChildren<SingleInfoInBook>())
        {
            Destroy(i.gameObject);
        }

        if(curP != -1)
        {
            foreach (int infoID in InfoIDListPageList[curP])
            {
                if(infoID == 20114 || infoID == 20115)
                {
                    var newImage1 = Instantiate(Info20114, tran);
                    newImage1.rectTransform.anchoredPosition = new Vector2(InfoManager.Instance.GetInfoDetailsFromID(infoID).PosX,
                       InfoManager.Instance.GetInfoDetailsFromID(infoID).PosY);
                    newImage1.gameObject.GetComponent<SingleInfoInBook>().Init(infoID);
                }
                else
                {
                    var newImage = Instantiate(ImagePrefab, tran);
                    newImage.rectTransform.anchoredPosition = new Vector2(InfoManager.Instance.GetInfoDetailsFromID(infoID).PosX,
                        InfoManager.Instance.GetInfoDetailsFromID(infoID).PosY);
                    newImage.gameObject.GetComponent<SingleInfoInBook>().Init(infoID);
                }
              
                
            }
        }

    }

}
