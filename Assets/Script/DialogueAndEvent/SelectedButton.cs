using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SelectedButton : MonoBehaviour
{
    public Text ButtonText;
    public DialogueListElement dialList;
    public int dialIndex;
    public int homeEventID;

    private EnemyDetails TheenemyDetails;


    private int ButtonPressType;
    private int ifBattleType;

    private Enemy enemy; 

    private void OnEnable()
    {
        ButtonText = transform.GetChild(0).GetComponent<Text>();
        ButtonText.text = string.Empty;
    }

    private void OnDisable()
    {
        
    }


    //商店客人选择
    public void InitSelectButton(string buttontext, int DialIndex,int ButtonType)
    {
        ButtonText.text = buttontext;
        dialIndex = DialIndex;
        ButtonPressType = ButtonType;

    }

    //家事件选择
    public void InitSelectButtonHomeEvent(string buttontext,int newHomeEventID,int ButtonType)
    {
        ButtonText.text = buttontext;
        homeEventID = newHomeEventID;
        ButtonPressType = ButtonType;
    }

    //外景NPC选择

    //是否要进行战斗的选择
    //1.开始战斗 2.尝试交谈 3.直接逃跑
    public void InitSelectButtonIfBattle(string buttontext,EnemyDetails enemyDetails,
        int ButtonType,int ifBattle,int DialIndex)
    {
        ButtonText.text = buttontext;
        TheenemyDetails = enemyDetails;
        dialIndex = DialIndex;
        ButtonPressType = ButtonType;
        ifBattleType = ifBattle;
    }

    //开始时的幸运选择
    public void InitSelectButtonInLuckPadding(string buttontext, int ButtonType,int DialIndex)
    {
        ButtonText.text = buttontext;
        ButtonPressType = ButtonType;
        dialIndex = DialIndex;
    }

    public void ButtonPressed()
    {
        //Type 1 是商店客人的选择对话
        if(ButtonPressType == 1)
        {
            //播放对话
            if (dialIndex != -1)
            {
                foreach (var i in FindObjectsOfType<CustomerController>())
                {
                    if (i.isFadeShow)
                    {
                        Debug.Log(i.isFadeShow);
                        i.Init(i.CustomerID, dialIndex);
                        i.StartFadeOnButton();
                    }
                }
            }
        }
        //Type 2 是HomeEvent的选择对话
        else if (ButtonPressType == 2)
        {
            foreach (var j in FindObjectsOfType<HomeEventUI>())
            {
                switch (j.HomeEventID)
                {
                    case 5001:



                        break;

                    case 5002:
                        j.Init(homeEventID);
                        j.StartTalk = true;
                        j.StartDoDialogue();
                        break;

                    default:
                        break;
                }
            }
        }
        else if(ButtonPressType == 3)
        {

        }
        //是否战斗选择
        else if(ButtonPressType == 4)
        {
            //enemy = GameObject.FindWithTag("Player").GetComponent<Player>().enemy;
            enemy = BattleSystem.Instance.enemy;
            //战斗                                 
            if (ifBattleType == 1)
            {
                if (dialIndex != -1)
                {  
                    enemy.Init(TheenemyDetails.EnemyID, dialIndex);
                    enemy.StartDoDialogue();
                }
            }

            //交谈
            else if(ifBattleType == 2)
            {
                if (dialIndex != -1)
                {
                    enemy.Init(TheenemyDetails.EnemyID, dialIndex);
                    enemy.StartDoDialogue();
                }
            }

            //逃跑
            else if(ifBattleType == 3)
            {
                if (dialIndex != -1)
                {
                    int bloodCut = TheBattle.Instance.PaddingRunning();
                    if(bloodCut == -1)
                    {
                        //逃跑失败
                        enemy.Init(TheenemyDetails.EnemyID, 3);
                        enemy.StartDoDialogue();
                    }
                    else
                    {
                        //逃跑成功
                        enemy.Init(TheenemyDetails.EnemyID, 4);
                        enemy.StartDoDialogue();
                        PlayerProperty.Instance.PlayerHealth -= bloodCut;
                    }
                    
                }
            }
            
           
        }
        else if (ButtonPressType == 5)
        {

            FindObjectOfType<DaoRuControl>().StartDialAfterChoose(dialIndex);

        }
    }


}
