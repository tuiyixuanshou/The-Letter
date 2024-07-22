using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BattleSystem : Singleton<BattleSystem>
{
    [Header("敌人数据库")]
    public EnemyList_SO enemyList_SO;

    [Header("敌人刷新")]
    public GameObject EnemyPrefab;
    public int ShowTimes;
    private int MaxShowTimes;
    public string ActiveSceneName;
    public int showUpTime;
    public Enemy enemy;

    [Header("游戏时间")]
    public int gameSecond;
    public bool gameClockPause;
    private float tikTime;

    //查找
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

    //添加野怪的刷新机制，和TimeManager联动
    public void SceneSet()
    {
        switch (ActiveSceneName)
        {
            case "SafetyArea":
                gameClockPause = false;      //需要计时，开始计时
                MaxShowTimes = 4;            //最大刷新次数
                showUpTime = Random.Range(10, 15);                 //生成刷新时间
                break;

            default:
                //不需要计时
                gameClockPause = true;
                break;
        }
    }

    public void EnemyUpdate()
    {
        switch (ActiveSceneName)
        {
            case "SafetyArea":
                //重置刷新时间
                showUpTime = Random.Range(20, 40);

                //随机生成敌人编号

                //刷新敌人并准备战斗
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
                    //跳过这次
                    gameSecond = 0;
                    return;
                }
            }

            gameClockPause = true;
            gameSecond = 0;
            ShowTimes++;
            //开始刷新敌人
            EnemyUpdate();

        }
    }

    private void UpdateGameTime()
    {
        gameSecond++;
    }

   
}
