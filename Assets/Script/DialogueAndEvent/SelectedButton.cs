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


    //�̵����ѡ��
    public void InitSelectButton(string buttontext, int DialIndex,int ButtonType)
    {
        ButtonText.text = buttontext;
        dialIndex = DialIndex;
        ButtonPressType = ButtonType;

    }

    //���¼�ѡ��
    public void InitSelectButtonHomeEvent(string buttontext,int newHomeEventID,int ButtonType)
    {
        ButtonText.text = buttontext;
        homeEventID = newHomeEventID;
        ButtonPressType = ButtonType;
    }

    //�⾰NPCѡ��

    //�Ƿ�Ҫ����ս����ѡ��
    //1.��ʼս�� 2.���Խ�̸ 3.ֱ������
    public void InitSelectButtonIfBattle(string buttontext,EnemyDetails enemyDetails,
        int ButtonType,int ifBattle,int DialIndex)
    {
        ButtonText.text = buttontext;
        TheenemyDetails = enemyDetails;
        dialIndex = DialIndex;
        ButtonPressType = ButtonType;
        ifBattleType = ifBattle;
    }

    //��ʼʱ������ѡ��
    public void InitSelectButtonInLuckPadding(string buttontext, int ButtonType,int DialIndex)
    {
        ButtonText.text = buttontext;
        ButtonPressType = ButtonType;
        dialIndex = DialIndex;
    }

    public void ButtonPressed()
    {
        //Type 1 ���̵���˵�ѡ��Ի�
        if(ButtonPressType == 1)
        {
            //���ŶԻ�
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
        //Type 2 ��HomeEvent��ѡ��Ի�
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
        //�Ƿ�ս��ѡ��
        else if(ButtonPressType == 4)
        {
            //enemy = GameObject.FindWithTag("Player").GetComponent<Player>().enemy;
            enemy = BattleSystem.Instance.enemy;
            //ս��                                 
            if (ifBattleType == 1)
            {
                if (dialIndex != -1)
                {  
                    enemy.Init(TheenemyDetails.EnemyID, dialIndex);
                    enemy.StartDoDialogue();
                }
            }

            //��̸
            else if(ifBattleType == 2)
            {
                if (dialIndex != -1)
                {
                    enemy.Init(TheenemyDetails.EnemyID, dialIndex);
                    enemy.StartDoDialogue();
                }
            }

            //����
            else if(ifBattleType == 3)
            {
                if (dialIndex != -1)
                {
                    int bloodCut = TheBattle.Instance.PaddingRunning();
                    if(bloodCut == -1)
                    {
                        //����ʧ��
                        enemy.Init(TheenemyDetails.EnemyID, 3);
                        enemy.StartDoDialogue();
                    }
                    else
                    {
                        //���ܳɹ�
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
