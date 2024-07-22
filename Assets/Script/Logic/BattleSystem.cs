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

    [Header("��Ϸʱ��")]
    public int gameSecond;
    public bool gameClockPause;
    private float tikTime;

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

        ShowTimes = 0;
        ActiveSceneName = SceneManager.GetActiveScene().name;
        SceneSet();
    }

    //���Ұ�ֵ�ˢ�»��ƣ���TimeManager����
    public void SceneSet()
    {
        switch (ActiveSceneName)
        {
            case "SafetyArea":
                gameClockPause = false;      //��Ҫ��ʱ����ʼ��ʱ
                MaxShowTimes = 4;            //���ˢ�´���
                showUpTime = Random.Range(10, 15);                 //����ˢ��ʱ��
                break;

            default:
                //����Ҫ��ʱ
                gameClockPause = true;
                break;
        }
    }

    public void EnemyUpdate()
    {
        switch (ActiveSceneName)
        {
            case "SafetyArea":
                //����ˢ��ʱ��
                showUpTime = Random.Range(20, 40);

                //������ɵ��˱��

                //ˢ�µ��˲�׼��ս��
                var newEnemy = Instantiate(EnemyPrefab);
                enemy = newEnemy.GetComponent<Enemy>();
                enemy.Init(10001, 0);
                enemy.StartDoDialogue();

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
        if (gameSecond >= showUpTime && ShowTimes <= MaxShowTimes)
        {
            foreach (var i in GameObject.FindGameObjectsWithTag("NPC"))
            {
                if (i.GetComponent<NPCDialogueController>().StartTalk)
                {
                    //�������
                    gameSecond = 0;
                    return;
                }
            }

            gameClockPause = true;
            gameSecond = 0;
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
