using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InstantiateSelectButtons : Singleton<InstantiateSelectButtons>
{
    public Transform LayOutParent;

    //还没做完预制件
    public GameObject SelectedButtonPrefab;

    public GameObject SelectPanel;

    public GameObject DialoguePanel;

    InventoryUI inventoryUI => FindObjectOfType<InventoryUI>();

    public void ShowSelectPanel()
    {
        SelectPanel.SetActive(true);
        DialoguePanel.SetActive(false);

    }



    public void instantiateShopButtons(int NPCID,int DialogueIndex)
    {
        //根据ID和对话匹配生成的按钮
        //1是商店客人的选择
        switch (NPCID)
        {
            case 1001:
                switch (DialogueIndex)
                {
                    case 0:
                        var newButton1001_0_1 = LayOutParent.GetChild(0).gameObject;
                        newButton1001_0_1.SetActive(true);
                        newButton1001_0_1.GetComponent<SelectedButton>().InitSelectButton("帮助他们，按60%价格收购",
                            1,1);
                        
                        var newButton1001_0_2 = LayOutParent.GetChild(1).gameObject;
                        newButton1001_0_2.SetActive(true);
                        newButton1001_0_2.GetComponent<SelectedButton>().InitSelectButton("我也没钱，按80%价格收购",
                           /* NPCManager.Instance.GetSingleDialogue(1001).dialogueElemList[2]*/2,1);

                        break;
                    default:
                        break;
                }
                break;
            case 1030:
                switch (DialogueIndex)
                {
                    case 0:
                        var newButton1030_0_1 = LayOutParent.GetChild(0).gameObject;
                        newButton1030_0_1.SetActive(true);
                        newButton1030_0_1.GetComponent<SelectedButton>().InitSelectButton("诚实守信",1, 1);
                        var newButton1030_0_2 = LayOutParent.GetChild(1).gameObject;
                        newButton1030_0_2.SetActive(true);
                        newButton1030_0_2.GetComponent<SelectedButton>().InitSelectButton("投机取巧",2, 1);
                        break;
                    default:
                        break;
                }
                break;
            default:
                break;
        }
    }


    public void InstantiateHomeEventButton(int HomeEventID)
    {
        //生成的新事件
        //2是家事件的选择
        switch (HomeEventID)
        {
            case 5002:
                var newButton5002_1 = LayOutParent.GetChild(0).gameObject;
                newButton5002_1.SetActive(true);
                newButton5002_1.GetComponent<SelectedButton>().InitSelectButtonHomeEvent("见义勇为，把他拖进家中"
                    ,5003,2);

                var newButton5002_2 = LayOutParent.GetChild(1).gameObject;
                newButton5002_2.SetActive(true);
                newButton5002_2.GetComponent<SelectedButton>().InitSelectButtonHomeEvent("置之不理，保全自身要紧"
                    , 5004, 2);
                break;

            case 5006:
                var newButton5002_3 = LayOutParent.GetChild(0).gameObject;
                newButton5002_3.SetActive(true);
                newButton5002_3.GetComponent<SelectedButton>().InitSelectButtonHomeEvent("进行搜身"
                    , 5007, 2);

                var newButton5002_4 = LayOutParent.GetChild(1).gameObject;
                newButton5002_4.SetActive(true);
                newButton5002_4.GetComponent<SelectedButton>().InitSelectButtonHomeEvent("不进行搜身"
                    , 5008, 2);
                break;
            case 5009:
                var newButton5009_1 = LayOutParent.GetChild(0).gameObject;
                newButton5009_1.SetActive(true);
                newButton5009_1.GetComponent<SelectedButton>().InitSelectButtonHomeEvent("拦住伊璐玛"
                    , 5010, 2);

                var newButton5009_2 = LayOutParent.GetChild(1).gameObject;
                newButton5009_2.SetActive(true);
                newButton5009_2.GetComponent<SelectedButton>().InitSelectButtonHomeEvent("保持沉默"
                    , 5011, 2);
                break;
            case 5013:
                var newButton5013_1 = LayOutParent.GetChild(0).gameObject;
                newButton5013_1.SetActive(true);
                if (HomeEventManager.Instance.CheckIfEventDone(5007))
                {
                    newButton5013_1.GetComponent<SelectedButton>().InitSelectButtonHomeEvent("进行搜身", 5014, 2);
                }
                else
                {
                    newButton5013_1.GetComponent<SelectedButton>().InitSelectButtonHomeEvent("进行搜身", 5015, 2);
                }
               
                var newButton5013_2 = LayOutParent.GetChild(1).gameObject;
                newButton5013_2.SetActive(true);
                newButton5013_2.GetComponent<SelectedButton>().InitSelectButtonHomeEvent("不进行搜身", 5008, 2);
                break;
            case 5020:
                var newButton5020_1 = LayOutParent.GetChild(0).gameObject; newButton5020_1.SetActive(true);
                newButton5020_1.GetComponent<SelectedButton>().InitSelectButtonHomeEvent("有钱不赚王八蛋", 5021, 2);
              
                var newButton5020_2 = LayOutParent.GetChild(1).gameObject;
                newButton5020_2.SetActive(true);
                newButton5020_2.GetComponent<SelectedButton>().InitSelectButtonHomeEvent("严词拒绝", 5022, 2);
                break;
            case 5024:
                var newButton5024_1 = LayOutParent.GetChild(0).gameObject; newButton5024_1.SetActive(true);
                newButton5024_1.GetComponent<SelectedButton>().InitSelectButtonHomeEvent("开" , 5025, 2);

                var newButton5024_2 = LayOutParent.GetChild(1).gameObject; newButton5024_2.SetActive(true);
                newButton5024_2.GetComponent<SelectedButton>().InitSelectButtonHomeEvent("不开" , 5025, 2);
                break;
            case 5027:
                var newButton5027_1 = LayOutParent.GetChild(0).gameObject; newButton5027_1.SetActive(true);
                newButton5027_1.GetComponent<SelectedButton>().InitSelectButtonHomeEvent("给", 5028, 2);

                var newButton5027_2 = LayOutParent.GetChild(1).gameObject; newButton5027_2.SetActive(true);
                newButton5027_2.GetComponent<SelectedButton>().InitSelectButtonHomeEvent("不给", 5029, 2);
                break;
            case 5033:
                var newButton5033_1 = LayOutParent.GetChild(0).gameObject; newButton5033_1.SetActive(true);
                newButton5033_1.GetComponent<SelectedButton>().InitSelectButtonHomeEvent("答应爱尔莎", 5034, 2);

                var newButton5033_2 = LayOutParent.GetChild(1).gameObject; newButton5033_2.SetActive(true);
                newButton5033_2.GetComponent<SelectedButton>().InitSelectButtonHomeEvent("拒绝爱尔莎", 5035, 2);
                break;
            case 5047:
                var newButton5047_1 = LayOutParent.GetChild(0).gameObject; newButton5047_1.SetActive(true);
                newButton5047_1.GetComponent<SelectedButton>().InitSelectButtonHomeEvent("答应爱尔莎", 5048, 2);
                var newButton5047_2 = LayOutParent.GetChild(1).gameObject; newButton5047_2.SetActive(true);
                newButton5047_2.GetComponent<SelectedButton>().InitSelectButtonHomeEvent("拒绝爱尔莎", 5049, 2);
                break;
            default:
                break;
        }
    }

    public void InstantiateOutNPCButton(int NPCID)
    {
        switch (NPCID)
        {
            case 6004:
                var newButton6004_1 = LayOutParent.GetChild(0).gameObject;
                newButton6004_1.SetActive(true);
                newButton6004_1.GetComponent<SelectedButton>().InitSelectButtonOutNPC("分享物资", 6005, 3);

                var newButton6004_2 = LayOutParent.GetChild(1).gameObject;
                newButton6004_2.SetActive(true);
                newButton6004_2.GetComponent<SelectedButton>().InitSelectButtonOutNPC("还是不舍", 6006, 3);

                break;

            case 9003:
                var newButton9003_1 = LayOutParent.GetChild(0).gameObject;
                newButton9003_1.SetActive(true);
                newButton9003_1.GetComponent<SelectedButton>().InitSelectButtonOutNPC("什么也不拿", 8198, 3);

                var newButton9003_2 = LayOutParent.GetChild(1).gameObject;
                newButton9003_2.SetActive(true);
                newButton9003_2.GetComponent<SelectedButton>().InitSelectButtonOutNPC("拿走一部分", 8197, 3);

                var newButton9003_3 = LayOutParent.GetChild(2).gameObject;
                newButton9003_3.SetActive(true);
                newButton9003_3.GetComponent<SelectedButton>().InitSelectButtonOutNPC("全部拿走", 8196, 3);
                break;
            case 8004:
                var newButton8004_1 = LayOutParent.GetChild(0).gameObject;
                newButton8004_1.SetActive(true);
                newButton8004_1.GetComponent<SelectedButton>().InitSelectButtonOutNPC("提供帮助", 8005, 3);

                var newButton8004_2 = LayOutParent.GetChild(1).gameObject;
                newButton8004_2.SetActive(true);
                newButton8004_2.GetComponent<SelectedButton>().InitSelectButtonOutNPC("自身难保", 8006, 3);
                break;
            case 8013:
                var newButton8013_1 = LayOutParent.GetChild(0).gameObject;
                newButton8013_1.SetActive(true);
                newButton8013_1.GetComponent<SelectedButton>().InitSelectButtonOutNPC("提供帮助", 8015, 3);

                var newButton8013_2 = LayOutParent.GetChild(1).gameObject;
                newButton8013_2.SetActive(true);
                newButton8013_2.GetComponent<SelectedButton>().InitSelectButtonOutNPC("不管不顾", 8016, 3);
                break;
            case 6021:
                var newButton6021_1 = LayOutParent.GetChild(0).gameObject;newButton6021_1.SetActive(true);
                newButton6021_1.GetComponent<SelectedButton>().InitSelectButtonOutNPC("善意提醒", 6022, 3);

                var newButton6021_2 = LayOutParent.GetChild(1).gameObject;newButton6021_2.SetActive(true);
                newButton6021_2.GetComponent<SelectedButton>().InitSelectButtonOutNPC("沉默不语", 6023, 3);

                break;
            case 9013:
                var newButton9013_1 = LayOutParent.GetChild(0).gameObject; newButton9013_1.SetActive(true);
                newButton9013_1.GetComponent<SelectedButton>().InitSelectButtonOutNPC("利用她", 9014, 3);

                var newButton9013_2 = LayOutParent.GetChild(1).gameObject; newButton9013_2.SetActive(true);
                newButton9013_2.GetComponent<SelectedButton>().InitSelectButtonOutNPC("不利用", 9015, 3);
                break;
            case 8120:
                var newButton8120_1 = LayOutParent.GetChild(0).gameObject; newButton8120_1.SetActive(true);
                newButton8120_1.GetComponent<SelectedButton>().InitSelectButtonOutNPC("尝试打开", 8121, 3);

                var newButton8120_2 = LayOutParent.GetChild(1).gameObject; newButton8120_2.SetActive(true);
                newButton8120_2.GetComponent<SelectedButton>().InitSelectButtonOutNPC("可能有危险", 8122, 3);
                break;
            case 6026:
                var newButton6026_1 = LayOutParent.GetChild(0).gameObject; newButton6026_1.SetActive(true);
                newButton6026_1.GetComponent<SelectedButton>().InitSelectButtonOutNPC("进行翻找（精神-10）", 6027, 3);

                var newButton6026_2 = LayOutParent.GetChild(1).gameObject; newButton6026_2.SetActive(true);
                newButton6026_2.GetComponent<SelectedButton>().InitSelectButtonOutNPC("壮胆拒绝（体力-10）", 6028, 3);
                break;
            case 8021:
                var newButton8021_1 = LayOutParent.GetChild(0).gameObject; newButton8021_1.SetActive(true);
                newButton8021_1.GetComponent<SelectedButton>().InitSelectButtonOutNPC("主动攻击", 8022, 3);

                var newButton8021_2 = LayOutParent.GetChild(1).gameObject; newButton8021_2.SetActive(true);
                newButton8021_2.GetComponent<SelectedButton>().InitSelectButtonOutNPC("被动防守", 8023, 3);
                break;
            case 8007:
                var newButton8007_1 = LayOutParent.GetChild(0).gameObject; newButton8007_1.SetActive(true);
                newButton8007_1.GetComponent<SelectedButton>().InitSelectButtonOutNPC("抢！", 8008, 3);
                var newButton8007_2 = LayOutParent.GetChild(1).gameObject; newButton8007_2.SetActive(true);
                newButton8007_2.GetComponent<SelectedButton>().InitSelectButtonOutNPC("忍忍", 8009, 3);
                break;
            case 6031:
                var newButton6031_1 = LayOutParent.GetChild(0).gameObject; newButton6031_1.SetActive(true);
                newButton6031_1.GetComponent<SelectedButton>().InitSelectButtonOutNPC("报仇！", 6032, 3);
                var newButton6031_2 = LayOutParent.GetChild(1).gameObject; newButton6031_2.SetActive(true);
                newButton6031_2.GetComponent<SelectedButton>().InitSelectButtonOutNPC("再忍忍", 6033, 3);
                break;

            default:
                break;
        }
    }

    public void InstantiateRandomGiveThing(int GiveOptionNum,int itemShortInitID)
    {
        int i = 0;
        int[] ID = { 0,0,0,0,0,0};int[] Amount = { 0, 0, 0, 0, 0, 0 };
        foreach(var item in InventoryManager.Instance.bagItemList_SO.bagItemList)
        {
            if (item.BagItemID != 0)
            {
                if (i < GiveOptionNum)
                {
                    ID[i] = item.BagItemID;
                    Amount[i] = item.BagItemAmount;
                    i++;
                }
                
            }
        }

        for(int j = 0; j < GiveOptionNum; j++)
        {
            if(ID[j] != 0)
            {
                var giveThing = LayOutParent.GetChild(j).gameObject;
                giveThing.SetActive(true);
                giveThing.GetComponent<SelectedButton>().InitSelectButtonGiveItem
                    ( InventoryManager.Instance.GetItemDetails(ID[j]).ItemName+ "-1",ID[j],Amount[j],6,itemShortInitID);
            }
            else
            {
               //物品不足
               var noItem = LayOutParent.GetChild(0).gameObject;
                noItem.SetActive(true);
                noItem.GetComponent<SelectedButton>().InitSelectButtonGiveItem("……囊中羞涩，根本不够分享", 0, 0, 6, itemShortInitID);
            }
        }
     
    }


    //战斗前询问是否战斗
    public void InstantiateIfBattleButton(EnemyDetails enemyDetails)
    {
        //战斗
        var newButton_1 = LayOutParent.GetChild(0).gameObject;
        newButton_1.SetActive(true);

        //交谈
        var newButton_2 = LayOutParent.GetChild(1).gameObject;
        newButton_2.SetActive(true);

        //逃跑
        var newButton_3 = LayOutParent.GetChild(2).gameObject;
        newButton_3.SetActive(true);


        if (enemyDetails.EnemyChooseType == 1)
        {
            //不可交谈
            //newButton_3.GetComponent<Button>().interactable = false;

            //0.是否进入战斗
            //1.准备战斗
            //2.无法交流
            //3.逃跑失败，继续战斗
            //4.逃跑成功，扣除血量

            switch(enemyDetails.EnemyID)
            {
                case 10001:
                    newButton_1.GetComponent<SelectedButton>().InitSelectButtonIfBattle("开始战斗", enemyDetails, 4,1,1);
                    newButton_2.GetComponent<SelectedButton>().InitSelectButtonIfBattle("尝试交流", enemyDetails, 4,2,2);
                    newButton_3.GetComponent<SelectedButton>().InitSelectButtonIfBattle("直接逃跑", enemyDetails, 4,3,3);
                    break;
                case 10004:
                    newButton_1.GetComponent<SelectedButton>().InitSelectButtonIfBattle("开始战斗", enemyDetails, 4, 1, 1);
                    newButton_2.GetComponent<SelectedButton>().InitSelectButtonIfBattle("尝试交流", enemyDetails, 4, 2, 2);
                    newButton_3.GetComponent<SelectedButton>().InitSelectButtonIfBattle("直接逃跑", enemyDetails, 4, 3, 3);
                    break;

                default:
                    //所有不可交流的野怪们
                    newButton_1.GetComponent<SelectedButton>().InitSelectButtonIfBattle("开始战斗", enemyDetails, 4, 1, 1);
                    newButton_2.GetComponent<SelectedButton>().InitSelectButtonIfBattle("尝试交流", enemyDetails, 4, 2, 2);
                    newButton_3.GetComponent<SelectedButton>().InitSelectButtonIfBattle("直接逃跑", enemyDetails, 4, 3, 3);
                    break;
            }
        }
        else if(enemyDetails.EnemyChooseType == 2)
        {
            //不可逃跑 不可交谈
            newButton_2.GetComponent<Button>().interactable = false;
            newButton_3.GetComponent<Button>().interactable = false;
        }
        else if(enemyDetails.EnemyChooseType == 3)
        {
            //都可
        }
        else
        {

        }
    }

    public void InstantiateLuckPadding()
    {
        Debug.Log("do lucky padding");
        var newButton_1 = LayOutParent.GetChild(0).gameObject;
        newButton_1.SetActive(true);
        newButton_1.GetComponent<SelectedButton>().InitSelectButtonInLuckPadding("向东走",5,0);

        var newButton_2 = LayOutParent.GetChild(1).gameObject;
        newButton_2.SetActive(true);
        newButton_2.GetComponent<SelectedButton>().InitSelectButtonInLuckPadding("向西走",5,1);

        var newButton_3 = LayOutParent.GetChild(2).gameObject;
        newButton_3.SetActive(true);
        newButton_3.GetComponent<SelectedButton>().InitSelectButtonInLuckPadding("向南走",5,2);

        var newButton_4 = LayOutParent.GetChild(3).gameObject;
        newButton_4.SetActive(true);
        newButton_4.GetComponent<SelectedButton>().InitSelectButtonInLuckPadding("留在原地",5,3);

    }
}
