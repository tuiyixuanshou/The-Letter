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
    public int NPCID;
    private int ItemID, ItemAmount;

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
    public void InitSelectButtonOutNPC(string buttontext,int newNPCID, int ButtonType)
    {
        ButtonText.text = buttontext;
        NPCID = newNPCID;
        ButtonPressType = ButtonType;
    }

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
        //Type 2 ��HomeEvent��ѡ��Ի� ����Ҳ��Ҫ�޸�case5002�Ľ�������
        else if (ButtonPressType == 2)
        {
            foreach (var j in FindObjectsOfType<HomeEventUI>())
            {
                switch (j.HomeEventID)
                {
                    case 5002:
                        if (j.StartTalk)
                        {
                            j.Init(homeEventID);
                            j.StartTalk = true;
                            j.StartDoDialogue();
                        }
                        break;
                    case 5006:
                        if (j.StartTalk)
                        {
                            j.Init(homeEventID);
                            j.StartTalk = true;
                            j.StartDoDialogue();
                        }
                        break;
                    case 5009 :
                        if (j.StartTalk)
                        {
                            j.Init(homeEventID);
                            j.StartTalk = true;
                            j.StartDoDialogue();
                        }
                        break;
                    case 5012:
                        if (j.StartTalk)
                        {
                            j.Init(homeEventID);
                            j.StartTalk = true;
                            j.StartDoDialogue();
                        }
                        break;
                    case 5013:
                        if (j.StartTalk)
                        {
                            j.Init(homeEventID);j.StartTalk = true;j.StartDoDialogue();
                        }
                        break;
                    case 5020:
                        if (j.StartTalk)
                        {
                            j.Init(homeEventID); j.StartTalk = true; j.StartDoDialogue();
                        }
                        break;
                    case 5024:
                        if (j.StartTalk)
                        {   j.Init(homeEventID); j.StartTalk = true; j.StartDoDialogue(); }
                        break;
                    case 5027:
                        if (j.StartTalk)
                        { j.Init(homeEventID); j.StartTalk = true; j.StartDoDialogue(); }
                        break;
                    case 5033:
                        if (j.StartTalk)
                        { j.Init(homeEventID); j.StartTalk = true; j.StartDoDialogue(); }
                        break;
                    case 5047:
                        if (j.StartTalk)
                        { j.Init(homeEventID); j.StartTalk = true; j.StartDoDialogue(); }
                        break;
                    default:
                        break;
                }
            }
        }
        else if(ButtonPressType == 3)
        {
            foreach (var j in FindObjectsOfType<NPCDialogueController>())
            {
                switch (j.NPCID)
                {
                    case 6004:
                        if (j.StartTalk)
                        {
                            j.Init(NPCID);
                            j.StartTalk = true;
                            j.CanStartTalk = true;
                            j.StartDoDialogue();
                        }
                        break;
                    case 8199:
                        if (j.StartTalk)
                        {
                            j.Init(NPCID);
                            j.StartTalk = true;
                            j.CanStartTalk = true;
                            j.StartDoDialogue();
                        }
                        break;
                    case 8004:
                        if (j.StartTalk)
                        {
                            j.Init(NPCID);
                            j.StartTalk = true;
                            j.CanStartTalk = true;
                            j.StartDoDialogue();
                        }
                        break;
                    case 8013:
                        if (j.StartTalk)
                        {
                            j.Init(NPCID);
                            j.StartTalk = true;
                            j.CanStartTalk = true;
                            j.StartDoDialogue();
                        }
                        break;
                    case 6021:
                        if (j.StartTalk)
                        {
                            j.Init(NPCID);
                            j.StartTalk = true;
                            j.CanStartTalk = true;
                            j.StartDoDialogue();
                        }
                        break;
                    case 9013:
                        if (j.StartTalk)
                        {
                            j.Init(NPCID);
                            j.StartTalk = true;
                            j.CanStartTalk = true;
                            j.StartDoDialogue();
                        }
                        break;
                    case 8120:
                        if (j.StartTalk)
                        {
                            j.Init(NPCID);
                            j.StartTalk = true;
                            j.CanStartTalk = true;
                            j.StartDoDialogue();
                        }
                        break;
                    case 6026:
                        if (j.StartTalk)
                        {
                            j.Init(NPCID);
                            j.StartTalk = true;
                            j.CanStartTalk = true;
                            j.StartDoDialogue();
                        }
                        break;
                    case 8021:
                        if (j.StartTalk)
                        {
                            j.Init(NPCID);
                            j.StartTalk = true;
                            j.CanStartTalk = true;
                            j.StartDoDialogue();
                        }
                        break;
                    case 8007:
                        if (j.StartTalk)
                        {
                            j.Init(NPCID);
                            j.StartTalk = true;
                            j.CanStartTalk = true;
                            j.StartDoDialogue();
                        }
                        break;
                    case 6031:
                        if (j.StartTalk)
                        {
                            j.Init(NPCID);
                            j.StartTalk = true;
                            j.CanStartTalk = true;
                            j.StartDoDialogue();
                        }
                        break;

                    default:
                        break;
                }
            }
            
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
        //���ѡ�����
        else if(ButtonPressType == 6)
        {
            if(ItemID != 0)
            {
                Debug.Log(ItemID);
                InventoryManager.Instance.ReduceItemInBag(ItemID, ItemAmount - 1);
                foreach (var j in FindObjectsOfType<NPCDialogueController>())
                {
                    //����֮��
                    if (j.NPCID == NPCID)
                    {
                        j.Init(NPCID + 2);
                        j.StartTalk = true;
                        j.CanStartTalk = true;
                        j.StartDoDialogue();
                    }
                }
            }
            else
            {
                foreach (var j in FindObjectsOfType<NPCDialogueController>())
                {
                    if(j.NPCID == NPCID)
                    {
                        //û�����ȥ
                        j.Init(NPCID + 1);
                        j.StartTalk = true;
                        j.CanStartTalk = true;
                        j.StartDoDialogue();
                    }
                }
            }
        }
    }


    public void InitSelectButtonGiveItem(string buttontext, int itemID,int itemAmount, int ButtonType,int curNPCID)
    {
        ButtonText.text = buttontext;
        ItemID = itemID;
        ItemAmount = itemAmount;
        ButtonPressType = ButtonType;
        NPCID = curNPCID;
    }
}
