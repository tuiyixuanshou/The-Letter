using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BattleSystem : Singleton<BattleSystem>
{
    [Header("�������ݿ�")]
    public EnemyList_SO enemyList_SO;

    [Header("����ˢ��")]
    public GameObject EnemyPrefab;
    public int ShowTimes;
    private int MaxShowTimes;
    public string ActiveSceneName;
    public int showUpTime;
    public Enemy enemy;
    public Player player;

    [Header("��Ϸʱ��")]
    public int gameSecond;
    public bool gameClockPause;
    private float tikTime;

    public bool isUpdateEnemy;

    private SceneMod sceneMod;
    //����
    public EnemyDetails GetEnemyDetailsFromID(int EnemyID)
    {
        return enemyList_SO.enemyDetailsList.Find(i => i.EnemyID == EnemyID);
    }

    protected override void Awake()
    {
        base.Awake();
        gameClockPause = true;
    }

    private void OnEnable()
    {
        EventHandler.AfterSceneLoad += OnAfterSceneLoad;
    }

    private void OnDisable()
    {
        EventHandler.AfterSceneLoad -= OnAfterSceneLoad;
    }

    private void OnAfterSceneLoad()
    {
        //reset time
        gameSecond = 0;
        tikTime = 0;
        showUpTime = 1000;
        ShowTimes = 0;
        ActiveSceneName = SceneManager.GetActiveScene().name;
        SceneSet();
    }


    public void SceneSet()
    {
        int daycount = DayManager.Instance.DayCount;
        if (daycount < 4)
        {
            sceneMod = SceneMod.Safe;
        }
        else if(daycount>=4 && daycount <6)
        {
            if(ActiveSceneName == "WarYield")
            {
                sceneMod = SceneMod.War;
            }
            else
            {
                sceneMod = SceneMod.Safe;
            }
        }
        else if(daycount>=6 && daycount<9)
        {
            sceneMod = SceneMod.War;
        }
        else if(daycount == 9)
        {
            if (ActiveSceneName != "WarYield")
            {
                sceneMod = SceneMod.War;
            }
            else
            {
                sceneMod = SceneMod.Epidemic;
            }
        }
        else
        {
            sceneMod = SceneMod.Epidemic;
        }
        
        switch (ActiveSceneName)
        {
            case "SafetyArea":
                gameClockPause = false;      //��Ҫ��ʱ����ʼ��ʱ
                if(sceneMod == SceneMod.Safe)
                {
                    MaxShowTimes = 4;            //���ˢ�´���
                    showUpTime = Random.Range(20, 40);                 //��һ������ˢ��ʱ��
                }
                else if(sceneMod == SceneMod.War)
                {
                    MaxShowTimes = 5;            //���ˢ�´���
                    showUpTime = Random.Range(10, 30);                 //��һ������ˢ��ʱ��
                }
                else if (sceneMod == SceneMod.Epidemic)
                {
                    MaxShowTimes = 6;            //���ˢ�´���
                    showUpTime = Random.Range(10, 20);                 //��һ������ˢ��ʱ��
                }
                player = GameObject.FindWithTag("Player").GetComponent<Player>();
                break;


            //case "ResearchBase":
            //    gameClockPause = false;      //��Ҫ��ʱ����ʼ��ʱ
            //    if (sceneMod == SceneMod.Safe)
            //    {
            //        MaxShowTimes = 4;            //���ˢ�´���
            //        showUpTime = Random.Range(20, 40);                 //��һ������ˢ��ʱ��
            //    }
            //    else if (sceneMod == SceneMod.War)
            //    {
            //        MaxShowTimes = 5;            //���ˢ�´���
            //        showUpTime = Random.Range(10, 30);                 //��һ������ˢ��ʱ��
            //    }
            //    else if (sceneMod == SceneMod.Epidemic)
            //    {
            //        MaxShowTimes = 6;            //���ˢ�´���
            //        showUpTime = Random.Range(10, 20);                 //��һ������ˢ��ʱ��
            //    }
            //    player = GameObject.FindWithTag("Player").GetComponent<Player>();
            //    break;
            case "WarYield":
                gameClockPause = false;      //��Ҫ��ʱ����ʼ��ʱ
                if (sceneMod == SceneMod.Safe)
                {
                    MaxShowTimes = 5;            //���ˢ�´���
                    showUpTime = Random.Range(20, 35);                 //��һ������ˢ��ʱ��
                }
                else if (sceneMod == SceneMod.War)
                {
                    MaxShowTimes = 6;            //���ˢ�´���
                    showUpTime = Random.Range(10, 25);                 //��һ������ˢ��ʱ��
                }
                else if (sceneMod == SceneMod.Epidemic)
                {
                    MaxShowTimes = 7;            //���ˢ�´���
                    showUpTime = Random.Range(10, 25);                 //��һ������ˢ��ʱ��
                }
                player = GameObject.FindWithTag("Player").GetComponent<Player>();
                break;
            default:
                //����Ҫ��ʱ
                gameClockPause = true;
                break;
        }
    }

    public void EnemyUpdate()
    {
        Debug.Log("do updateEnemy");
        int ID;
        switch (ActiveSceneName)
        {
            case "SafetyArea":
                if (sceneMod == SceneMod.Safe)
                {
                    showUpTime = Random.Range(20, 100);//����ˢ��ʱ��
                    int i = Random.Range(0, 2);       //������ɵ��˱��
                    if (i == 0) { ID = 10001; } else { ID = 10003; }
                }
                else if (sceneMod == SceneMod.War)
                {
                    showUpTime = Random.Range(20, 60);//����ˢ��ʱ��
                    int i = Random.Range(0, 3);
                    if (i == 0) { ID = 10001; } else if(i == 1) { ID = 10003; } else { ID = 10004; }
                }
                else if (sceneMod == SceneMod.Epidemic)
                {
                    showUpTime = Random.Range(10, 60);//����ˢ��ʱ��
                    int i = Random.Range(0, 5);
                    if (i == 0) { ID = 10001; } else if (i == 1) { ID = 10003; } else if (i == 2) { ID = 10004; } else if (i == 3) { ID = 10002; } else { ID = 10005; }
                }
                else
                {
                    ID = -1;
                }
                //ˢ�µ��˲�׼��ս��
                var newEnemy = Instantiate(EnemyPrefab);
                enemy = newEnemy.GetComponent<Enemy>();
                if(ID == -1) { }
                else { enemy.Init(ID, 0); }
                enemy.StartDoDialogue();
                player.InputDisable = true;
                break;
            //case "ResearchBase":
            //    if (sceneMod == SceneMod.Safe)
            //    {
            //        showUpTime = Random.Range(20, 100);//����ˢ��ʱ��
            //        int i = Random.Range(0, 2);       //������ɵ��˱��
            //        if (i == 0) { ID = 10001; } else { ID = 10003; }
            //    }
            //    else if (sceneMod == SceneMod.War)
            //    {
            //        showUpTime = Random.Range(20, 60);//����ˢ��ʱ��
            //        int i = Random.Range(0, 3);
            //        if (i == 0) { ID = 10001; } else if (i == 1) { ID = 10003; } else { ID = 10004; }
            //    }
            //    else if (sceneMod == SceneMod.Epidemic)
            //    {
            //        showUpTime = Random.Range(10, 60);//����ˢ��ʱ��
            //        int i = Random.Range(0, 5);
            //        if (i == 0) { ID = 10001; } else if (i == 1) { ID = 10003; } else if (i == 2) { ID = 10004; } else if (i == 3) { ID = 10002; } else { ID = 10005; }
            //    }
            //    else
            //    {
            //        ID = -1;
            //    }
            //    //ˢ�µ��˲�׼��ս��
            //    var newEnemy1 = Instantiate(EnemyPrefab);
            //    enemy = newEnemy1.GetComponent<Enemy>();
            //    if (ID == -1) { }
            //    else { enemy.Init(ID, 0); }
            //    enemy.StartDoDialogue();
            //    player.InputDisable = true;
            //    break;
            case "WarYield":
                if (sceneMod == SceneMod.Safe)
                {
                    showUpTime = Random.Range(20, 85);//����ˢ��ʱ��
                    int i = Random.Range(0, 2);       //������ɵ��˱��
                    if (i == 0) { ID = 10001; } else { ID = 10003; }
                }
                else if (sceneMod == SceneMod.War)
                {
                    showUpTime = Random.Range(20, 55);//����ˢ��ʱ��
                    int i = Random.Range(0, 3);
                    if (i == 0) { ID = 10001; } else if (i == 1) { ID = 10003; } else { ID = 10004; }
                }
                else if (sceneMod == SceneMod.Epidemic)
                {
                    showUpTime = Random.Range(10, 50);//����ˢ��ʱ��
                    int i = Random.Range(0, 5);
                    if (i == 0) { ID = 10001; } else if (i == 1) { ID = 10003; } else if (i == 2) { ID = 10004; } else if (i == 3) { ID = 10002; } else { ID = 10005; }
                }
                else
                {
                    ID = -1;
                }
                //ˢ�µ��˲�׼��ս��
                var newEnemy2 = Instantiate(EnemyPrefab);
                enemy = newEnemy2.GetComponent<Enemy>();
                if (ID == -1) { }
                else { enemy.Init(ID, 0); }
                enemy.StartDoDialogue();
                player.InputDisable = true;
                break;
            default:
                break;
        }
    }

    public void ReStartTime()
    {
        if (enemy != null)
            Destroy(enemy.gameObject);
        gameClockPause = false;
        TimeManager.Instance.gameClockPause = false;
        player.InputDisable = false;
       
    }

    private void Update()
    {
        if (!gameClockPause)
        {
            tikTime += Time.deltaTime;

            if (tikTime >= Settings.secondThreshold)
            {
                tikTime -= Settings.secondThreshold;
                UpdateGameTime();
            }
        }

        EnemyUpdateController();

        if (enemy != null)
        {
            if (enemy.StartTalk && (Input.GetKeyDown(KeyCode.Space) || Input.GetMouseButtonDown(0)) && !enemy.isTalking)
            {
                enemy.StartDoDialogue();
            }
        }
    }


    private void EnemyUpdateController()
    {
        if (gameSecond >= showUpTime && ShowTimes <= MaxShowTimes && !gameClockPause)
        {
            Debug.Log("do updateController");
            gameSecond = 0;
            foreach (var i in GameObject.FindGameObjectsWithTag("NPC"))
            {
                if (i.GetComponent<NPCDialogueController>().StartTalk)
                {
                    //�������
                    return;
                }
            }

            gameClockPause = true;
            TimeManager.Instance.gameClockPause = true;
            ShowTimes++;
            //��ʼˢ�µ���
            EnemyUpdate();

        }
    }

    private void UpdateGameTime()
    {
        gameSecond++;
    }

   
}
