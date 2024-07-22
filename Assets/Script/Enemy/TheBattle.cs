using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TheBattle : Singleton<TheBattle>
{
    public BattleState state = BattleState.UNSTART;

    [Header("背景和特效信息")]
    public Image BlackBack;
    public Image RedEffectBack;
    public Image BlackEffectBack;
    public Image FatherDetailsImage;
    private CanvasGroup FatherGroup;
    public Image EnemyHurtImage;

    public float Movespeed;

    public Text BattleTip;

    private bool isFadeOver;
    private bool isRedOver;
    private bool isBlackOver;
    private bool isInfoOver;

    private bool isShakeOver;

    private float redOriPos;
    private float blackOriPos;
    public float redTarPos;
    public float blackTarPos;

    [Header("敌人信息")]
    public Image EnemyFace;
    public Text EnemyName;
    public Text EnemyCurHP;
    public Text EnemyMaxHP;

    public GameObject EnemyHurtRedEffect;
    public Text EnemyHurtNum;
    public Text PlayerHurtNum;

    public Image PlayerState;

    public Slider hpSlider;

    private Enemy enemy;
    private Player player;

    [Header("装备信息")]
    public GameObject EquipLine;



    [Header("按钮信息")]
    public Button AttackButton;
    public Button StuffButton;
    public Button RunButton;

    [Header("物品使用信息")]
    public InventoryStuff inventoryStuff;

    [Header("结束信息")]
    public GameObject BattleTipPanel;

    [Header("关闭其他页面")]
    public GameObject bag;
    public GameObject equip;
    public GameObject warpaper;

    private bool isDead;

    private int currentPlayerhp;
    private int currentEnemyhp;

    private void Start()
    {
        //StartBattle();
    }


    public void StartBattle(bool isEnemyTurn)
    {
        state = BattleState.START;
        bag.SetActive(false); equip.SetActive(false); warpaper.SetActive(false);
        if (!isEnemyTurn)
            BattleTip.text = "优先出击！";
        else
            BattleTip.text = "对方抢先……";
        FatherDetailsImage.gameObject.SetActive(true);
        RedEffectBack.gameObject.SetActive(true);
        BlackEffectBack.gameObject.SetActive(true);
        redOriPos = RedEffectBack.rectTransform.anchoredPosition.x;
        blackOriPos = BlackEffectBack.rectTransform.anchoredPosition.x;

        FatherGroup = FatherDetailsImage.gameObject.GetComponent<CanvasGroup>();

        //设置页面上的各个按钮部位
        //关掉ActionBar
        //关掉背包、简报等页面
        EventHandler.CallClosePanelAndButtonWhenFight();

        //背景先变黑
        StartCoroutine(BackFade(0.8f, 0.35f));
        //红色出现
        StartCoroutine(BackEffect(redTarPos));
        //黑色背景出现和装备栏出现
        StartCoroutine(BackEffect2(blackTarPos,1f,true));
        //敌人信息出现
        StartCoroutine(EnemyInfo());
        //出现时抖动
        StartCoroutine(EnemyInfoShake( FatherDetailsImage ,0.1f, 15f));
        //经过一秒之后开始战斗
        StartCoroutine(SetUpBattle(isEnemyTurn));
    }

    IEnumerator BackFade(float targetAlpha, float fadeDuration)
    {
        float speed = Mathf.Abs(BlackBack.color.a - targetAlpha) / fadeDuration;

        while (!Mathf.Approximately(BlackBack.color.a, targetAlpha))
        {
            float a = Mathf.MoveTowards(BlackBack.color.a, targetAlpha, speed * Time.deltaTime);
            BlackBack.color = new Color(BlackBack.color.r, BlackBack.color.b, BlackBack.color.g, a);
            yield return null;
        }
        isFadeOver = true;
    }

    //背景红特效
    IEnumerator BackEffect(float targetPos)
    {
        yield return new WaitUntil(() => isFadeOver);

        float redCrrentPos = RedEffectBack.rectTransform.rect.x;

        while (!Mathf.Approximately(redCrrentPos, targetPos))
        {
            redCrrentPos = Mathf.MoveTowards(redCrrentPos, targetPos, Movespeed*Time.deltaTime*60);
            RedEffectBack.rectTransform.anchoredPosition = new Vector2(redCrrentPos, RedEffectBack.rectTransform.anchoredPosition.y);
            yield return new WaitForSeconds(0.001f);
        }
        isRedOver = true;
    }

    //背景黑特效
    IEnumerator BackEffect2(float targetPos,float targeta,bool EquipLineTrue)
    {
        yield return new WaitUntil(() => isRedOver);

        float blackCurrentPos = BlackEffectBack.rectTransform.anchoredPosition.x;
        Debug.Log(blackCurrentPos);
        float a = BlackEffectBack.color.a;
        float dis = Mathf.Abs(targetPos - blackOriPos);
        float changeTimes = dis / Movespeed;
        float thedis = Mathf.Abs(targeta - a);
        float fadeChangeSpeed = thedis / changeTimes;
        while (!Mathf.Approximately(blackCurrentPos, targetPos))
        {
            blackCurrentPos = Mathf.MoveTowards(blackCurrentPos, targetPos, Movespeed*Time.deltaTime*50);
            BlackEffectBack.rectTransform.anchoredPosition = new Vector2(blackCurrentPos, BlackEffectBack.rectTransform.anchoredPosition.y);

            a = Mathf.MoveTowards(a, targeta, fadeChangeSpeed);
            BlackEffectBack.color = new Color(BlackEffectBack.color.r, BlackEffectBack.color.g, BlackEffectBack.color.b,
                a);
            yield return new WaitForSeconds(0.001f);
        }
        BlackEffectBack.color = new Color(BlackEffectBack.color.r, BlackEffectBack.color.g, BlackEffectBack.color.b,
                targeta);
        isBlackOver = true;
        EquipLine.SetActive(EquipLineTrue);
    }

    IEnumerator EnemyInfo()
    {
        yield return new WaitUntil(() => isBlackOver);
        FatherGroup.alpha = 0;
        float a = 0;
        FatherDetailsImage.rectTransform.localScale = new Vector3(0.9f, 0.9f, 1);

        //读取信息
        player = GameObject.FindWithTag("Player").GetComponent<Player>();
        //enemy = player.enemy;
        enemy = BattleSystem.Instance.enemy;

        EnemyDetails enemyDetails = enemy.enemyDetails;
        EnemyFace.sprite = enemyDetails.EnemyFaceInBattle;
        EnemyName.text = enemyDetails.EnemyName;
        EnemyCurHP.text = enemyDetails.MaxHP.ToString();
        EnemyMaxHP.text = enemyDetails.MaxHP.ToString();
        hpSlider.maxValue = enemyDetails.MaxHP;
        hpSlider.value = enemyDetails.MaxHP;

        float fatherScale = FatherDetailsImage.rectTransform.localScale.x;
        while (!Mathf.Approximately(FatherDetailsImage.rectTransform.localScale.x, 1))
        {
            fatherScale = Mathf.MoveTowards(fatherScale, 1, 0.01f);
            FatherDetailsImage.rectTransform.localScale = new Vector3(fatherScale, fatherScale, 1);
            if (FatherGroup.alpha < 1)
            {
                a += 0.12f;
                FatherGroup.alpha = a;
            }
            else
            {
                FatherGroup.alpha = 1;
            }
            yield return new WaitForSeconds(0.02f);
        }
        isInfoOver = true;
    }


    //shakeTime = 0.08f;//震动时间
    //shakeAmount =15f;//振幅
    IEnumerator EnemyInfoShake(Image ShakeImage,float shakeTime, float shakeAmount)
    {
       yield return new WaitUntil(() => isInfoOver);
        isShakeOver = false;
       Vector3 OriPos = ShakeImage.rectTransform.anchoredPosition;

        Debug.Log(OriPos);
        while (shakeTime > 0)
        {
            ShakeImage.rectTransform.anchoredPosition = OriPos + Random.insideUnitSphere * shakeAmount;

            shakeTime -= Time.deltaTime *2f;
            yield return new WaitForSeconds(0.01f);
        }
        ShakeImage.rectTransform.anchoredPosition = OriPos;

        isShakeOver = true;
    }


    //抖动和挠一下特效
    IEnumerator EnemyHurtEffect()
    {
        EnemyHurtRedEffect.SetActive(true);
        EnemyHurtNum.gameObject.SetActive(true);
        hpSlider.value = currentEnemyhp;
        StartCoroutine(EnemyNumChange());
        yield return EnemyInfoShake(FatherDetailsImage, 0.2f, 15f);
        //yield return new WaitForSeconds(2f);
        //EnemyHurtRedEffect.SetActive(false);
    }

    IEnumerator EnemyNumChange()
    {
        int hp = int.Parse(EnemyCurHP.text);
        while(hp != currentEnemyhp)
        {
            hp--;
            EnemyCurHP.text = hp.ToString();
            yield return new WaitForSeconds(0.05f);
        }

    }

    IEnumerator SetUpBattle(bool isenemyTurn)
    {
        yield return new WaitUntil(() => isInfoOver);
        //yield return new WaitForSeconds(0.01f);
        if (!isenemyTurn)
        {
            state = BattleState.PLAYERTURN;

            Playerturn();
        }
        else
        {
            BattleTip.text = "对方的回合";
            state = BattleState.ENEMYTURN;
            StartCoroutine(EnemyAttack());
        }
       
    }

    void Playerturn()
    {
        BattleTip.text = "你的回合";
        AttackButton.interactable = true;
        StuffButton.interactable = true;
        RunButton.interactable = true;
    }

    public void OnAttackButtonPressed()
    {
        if(state == BattleState.PLAYERTURN)
        {
            StartCoroutine(PlayerAttack());
        }
    }

    public void OnStuffUseButtonPressed()
    {
        if(state == BattleState.PLAYERTURN)
        {
            StartCoroutine(PlayerStuffUsing());
        }
    }

    public void OnRunButtonPressed()
    {

    }

    IEnumerator PlayerAttack()
    {
        int EquipA = EquipLine.GetComponent<InventoryEquip>().equipA;
        int thedefense = (int) (Random.Range(enemy.enemyDetails.defenseMin, enemy.enemyDetails.defenseMax));
        currentEnemyhp = enemy.TakeDamage(PlayerProperty.Instance.PlayerAttack+EquipA,thedefense);
        //EnemyHurtNum.gameObject.SetActive(true);
        EnemyHurtNum.text = string.Empty;
        EnemyHurtNum.text = "-" + (PlayerProperty.Instance.PlayerAttack + EquipA).ToString() + "×" + thedefense.ToString()+"%";
        yield return EnemyHurtEffect();

        if (currentEnemyhp<=0)
        {
            state = BattleState.WON;
            BattleTip.text = "战斗结束！";
            isFadeOver = false;
            isRedOver = false;
            isBlackOver = false;
            isInfoOver = false;
            EndWinBattle();
        }
        else
        {
            state = BattleState.ENEMYTURN;
            BattleTip.text = "对方的回合";
            StartCoroutine(EnemyAttack());
        }
    }

    IEnumerator PlayerStuffUsing()
    {
        yield return new WaitForSeconds(1f);
        //这里写使用物品对敌人的损耗


        if (isDead)
        {
            state = BattleState.WON;
            isFadeOver = false;
            isRedOver = false;
            isBlackOver = false;
            isInfoOver = false;
            EndWinBattle();
        }
        else
        {
            state = BattleState.ENEMYTURN;
            BattleTip.text = "对方的回合";
            StartCoroutine(EnemyAttack());
        }
    }


    IEnumerator EnemyAttack()
    {

        AttackButton.interactable = false;
        StuffButton.interactable = false;
        RunButton.interactable = false;

        yield return new WaitForSeconds(2f);
 
        int EquipD = EquipLine.GetComponent<InventoryEquip>().equipB;
        int dmg = (int)(Random.Range(enemy.enemyDetails.AttackMin, enemy.enemyDetails.AttackMax));

        currentPlayerhp = player.TakeDamage(dmg, EquipD);

        PlayerHurtNum.text = string.Empty;
        PlayerHurtNum.text = "-" + ((dmg - EquipD) > 0 ? (dmg - EquipD).ToString() : 0.ToString());

        yield return PlayerHurtEffect();

        if (currentPlayerhp<=0)
        {
            state = BattleState.LOST;
            //这里直接快进到整体游戏失败
        }
        else
        {
            state = BattleState.PLAYERTURN;
            Playerturn();
        }
    }


    IEnumerator PlayerHurtEffect()
    {
        //屏幕先抖动
        CameraControl.Instance.CMShake();
        yield return EnemyInfoShake(PlayerState, 0.2f, 15f);
        //显示扣血特效和扣血
        PlayerHurtNum.gameObject.SetActive(true);
        PlayerProperty.Instance.PlayerHealth = currentPlayerhp;

        yield return new WaitForSeconds(2f);
    }
    void EndWinBattle()
    {
        //出现一个对话框 展示成果，然后关闭战斗界面
        StartCoroutine(WinBattle());


    }

    IEnumerator WinBattle()
    {
        yield return new WaitForSeconds(1f);

        BattleTipPanel.transform.GetChild(0).GetComponentInChildren<Text>().text
           = "战斗结束，四周有东西掉落";
        BattleTipPanel.SetActive(true);

        //战斗界面全部关闭
        yield return new WaitForSeconds(1f);
        //出现确认按钮
        BattleTipPanel.transform.GetChild(1).gameObject.SetActive(true);
       
    }

   public void OnWinButtonPressed()
    {
        FatherGroup.alpha = 0;
        FatherDetailsImage.gameObject.SetActive(false);

        StartCoroutine(BackFade(0f, 0.8f));
        RedEffectBack.rectTransform.anchoredPosition = new Vector2(redOriPos, RedEffectBack.rectTransform.anchoredPosition.y);
        RedEffectBack.gameObject.SetActive(false);

        BlackEffectBack.rectTransform.anchoredPosition = new Vector2(blackOriPos, BlackEffectBack.rectTransform.anchoredPosition.y);
        BlackEffectBack.gameObject.SetActive(false);
        EquipLine.SetActive(false);
        ItemManager.Instance.CreatItemAfterBattle(player.transform.localPosition, enemy.enemyDetails.GetBagItems);
        BattleTipPanel.SetActive(false);
        //在battleSystem中删除
        //Destroy(enemy.gameObject);
        BattleSystem.Instance.ReStartTime();
    
    }

    //判定逃跑是否成功
    public int PaddingRunning()
    {
        //player = GameObject.FindWithTag("Player").GetComponent<Player>();
        //enemy = player.enemy;
        enemy = BattleSystem.Instance.enemy;
        var enemyDetails = enemy.enemyDetails;
        Debug.Log(enemyDetails);
        int s = (int)(170 - enemyDetails.AttackMin - (100 - enemyDetails.defenseMin) - enemyDetails.MaxHP);
        if (s < 0)
        {
            s = 0;
        }
        else if (s > 100)
        {
            s = 100;
        }

        int i = Random.Range(1, 100);

        if (i < s)
        {
            //逃跑成功,扣血
            int bloodCut = 80 - s;
            if (bloodCut < 0)
            {
                bloodCut = 0;
            }
            BattleSystem.Instance.ReStartTime();
            return bloodCut;

        }
        else
        {
            //逃跑失败
            return -1;
        }

        //float runPrecentage = (170 - enemyDetails.AttackMin+(100-enemyDetails.defenseMin) +enemyDetails.MaxHP)
        //逃跑概率p =【170 -（攻击最小值 + 防御最小值 + 血量）】/ 100。
        //当p值小于或等于0时，p = 0。
        //扣血量y = 80 - 100 * p。
    }


}
