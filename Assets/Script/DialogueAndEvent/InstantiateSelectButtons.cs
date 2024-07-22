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
            default:
                break;
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
        var newButton_1 = LayOutParent.GetChild(0).gameObject;
        newButton_1.SetActive(true);
        newButton_1.GetComponent<SelectedButton>().InitSelectButtonInLuckPadding("向前走",5,0);

        var newButton_2 = LayOutParent.GetChild(1).gameObject;
        newButton_2.SetActive(true);
        newButton_2.GetComponent<SelectedButton>().InitSelectButtonInLuckPadding("向左走",5,1);

        var newButton_3 = LayOutParent.GetChild(2).gameObject;
        newButton_3.SetActive(true);
        newButton_3.GetComponent<SelectedButton>().InitSelectButtonInLuckPadding("向右走",5,2);

        var newButton_4 = LayOutParent.GetChild(3).gameObject;
        newButton_4.SetActive(true);
        newButton_4.GetComponent<SelectedButton>().InitSelectButtonInLuckPadding("留在原地",5,3);


    }
}
