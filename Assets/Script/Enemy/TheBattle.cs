using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TheBattle : Singleton<TheBattle>
{
    public BattleState state = BattleState.UNSTART;

    [Header("��������Ч��Ϣ")]
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

    //private bool isShakeOver;

    private float redOriPos;
    private float blackOriPos;
    public float redTarPos;
    public float blackTarPos;

    [Header("������Ϣ")]
    public Image EnemyFace;
    public Text EnemyName;
    public Text EnemyCurHP;
    public Text EnemyMaxHP;

    public GameObject EnemyHurtRedEffect;
    public GameObject EnemyHurt2;
    public Text EnemyHurtNum;
    public Text PlayerHurtNum;

    public Image PlayerState;

    public Slider hpSlider;

    private Enemy enemy;
    private Player player;

    [Header("װ����Ϣ")]
    public GameObject EquipLine;

    [Header("��ť��Ϣ")]
    public Button AttackButton;
    public Button StuffButton;
    public Button RunButton;

    [Header("��Ʒʹ����Ϣ")]
    public InventoryStuff inventoryStuff;

    [Header("������Ϣ")]
    public GameObject BattleTipPanel;

    [Header("�ر�����ҳ��")]
    public GameObject bag;
    public GameObject equip;
    public GameObject warpaper;

    private bool isDead;

    private int currentPlayerhp;
    private int currentEnemyhp;

    private bool isEquipOver;
    private void Start()
    {
        //StartBattle();
    }


    public void StartBattle(bool isEnemyTurn)
    {
        AudioManager.Instance.ChangeBGMInDial("FightBGM");
        state = BattleState.START;
        bag.SetActive(false); equip.SetActive(false); warpaper.SetActive(false);
        if (!isEnemyTurn)
            BattleTip.text = "���ȳ�����";
        else
            BattleTip.text = "�Է����ȡ���";
        FatherDetailsImage.gameObject.SetActive(true);
        RedEffectBack.gameObject.SetActive(true);
        BlackEffectBack.gameObject.SetActive(true);
        redOriPos = RedEffectBack.rectTransform.anchoredPosition.x;
        blackOriPos = BlackEffectBack.rectTransform.anchoredPosition.x;

        FatherGroup = FatherDetailsImage.gameObject.GetComponent<CanvasGroup>();

        //����ҳ���ϵĸ�����ť��λ
        //�ص�ActionBar
        //�ص��������򱨵�ҳ��
        EventHandler.CallClosePanelAndButtonWhenFight(state);

        //�����ȱ��
        StartCoroutine(BackFade(0.8f, 0.35f));
        //��ɫ����
        StartCoroutine(BackEffect(redTarPos));
        //��ɫ�������ֺ�װ��������
        StartCoroutine(BackEffect2(blackTarPos,1f,true));
        //������Ϣ����
        StartCoroutine(EnemyInfo());
        //����ʱ����
        StartCoroutine(EnemyInfoShake( FatherDetailsImage ,0.1f, 15f));
        //����һ��֮��ʼս��
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

    //��������Ч
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

    //��������Ч
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

        //��ȡ��Ϣ
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


    //shakeTime = 0.08f;//��ʱ��
    //shakeAmount =15f;//���
    IEnumerator EnemyInfoShake(Image ShakeImage,float shakeTime, float shakeAmount)
    {
       yield return new WaitUntil(() => isInfoOver);
        //isShakeOver = false;
       Vector3 OriPos = ShakeImage.rectTransform.anchoredPosition;

        Debug.Log(OriPos);
        while (shakeTime > 0)
        {
            ShakeImage.rectTransform.anchoredPosition = OriPos + Random.insideUnitSphere * shakeAmount;

            shakeTime -= Time.deltaTime *2f;
            yield return new WaitForSeconds(0.01f);
        }
        ShakeImage.rectTransform.anchoredPosition = OriPos;

        //isShakeOver = true;
    }


    //��������һ����Ч��buttontype:1���� 2:ʹ����Ʒ
    IEnumerator EnemyHurtEffect(int ButtonType)
    {
        EventHandler.CallInitSoundEffect(AudioManager.Instance.GetSoundDetailsfromName(SoundName.Ifight));
        if(ButtonType == 1) //������Ч
        {
            EnemyHurtRedEffect.SetActive(true);
        }
        else if(ButtonType == 2)//ʹ����Ʒ��Ч
        {
            EnemyHurt2.SetActive(true);
            
        }
        
        EnemyHurtNum.gameObject.SetActive(true);
        hpSlider.value = currentEnemyhp;
        StartCoroutine(EnemyNumChange());
        yield return EnemyInfoShake(FatherDetailsImage, 0.2f, 15f);

        if(ButtonType == 2)
        {
            inventoryStuff.UpdateStuffLine(InventoryManager.Instance.weaponItemList_SO.bagItemList);
            inventoryStuff.OnUsingButtonPress();
        }
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
            BattleTip.text = "�Է��Ļغ�";
            state = BattleState.ENEMYTURN;
            StartCoroutine(EnemyAttack());
        }
       
    }

    void Playerturn()
    {
        BattleTip.text = "��Ļغ�";
        AttackButton.interactable = true;
        StuffButton.interactable = true;
        RunButton.interactable = true;
    }

    public void OnAttackButtonPressed()
    {
        if(state == BattleState.PLAYERTURN)
        {
            EventHandler.CallInitSoundEffect(AudioManager.Instance.GetSoundDetailsfromName(SoundName.FightBt));
            StartCoroutine(PlayerAttack());
            //����װ�����
            var list = InventoryManager.Instance.equipItemList_SO.bagItemList;
            for (int i = 0; i < list.Count; i++)
            {
                if (list[i].BagItemID != 0)
                {
                    int a = list[i].BagItemAmount - 1;
                    if (a > 0)
                        InventoryManager.Instance.equipItemList_SO.bagItemList[i] = new() { BagItemID = list[i].BagItemID, BagItemAmount = a };
                    else if (a == 0)
                    {
                        isEquipOver = true;
                        InventoryManager.Instance.equipItemList_SO.bagItemList[i] = new() { BagItemID = 0, BagItemAmount = 0 };
                        EquipLine.GetComponent<InventoryEquip>().UpdateEquipText(InventoryManager.Instance.equipItemList_SO.bagItemList);
                    }
                }
            }
        }
    }

    //������stuffLine��ʹ�ð�ť
    public void OnStuffUseButtonPressed()
    {
        if(state == BattleState.PLAYERTURN)
        {
            EventHandler.CallInitSoundEffect(AudioManager.Instance.GetSoundDetailsfromName(SoundName.FightBt));
            StartCoroutine(PlayerStuffUsing());
        }
    }

    public void OnRunButtonPressed()
    {
        EventHandler.CallInitSoundEffect(AudioManager.Instance.GetSoundDetailsfromName(SoundName.FightBt));
    }

    IEnumerator PlayerAttack()
    {
        int EquipA = EquipLine.GetComponent<InventoryEquip>().equipA;
        int thedefense = (int) (Random.Range(enemy.enemyDetails.defenseMin, enemy.enemyDetails.defenseMax));
        currentEnemyhp = enemy.TakeDamage(PlayerProperty.Instance.PlayerAttack+EquipA,thedefense);
        //EnemyHurtNum.gameObject.SetActive(true);
        EnemyHurtNum.text = string.Empty;
        EnemyHurtNum.text = "-" + (PlayerProperty.Instance.PlayerAttack + EquipA).ToString() + "��" + thedefense.ToString()+"%";
        yield return EnemyHurtEffect(1);

        if (currentEnemyhp<=0)
        {
            state = BattleState.WON;
            BattleTip.text = "ս��������";
            isFadeOver = false;
            isRedOver = false;
            isBlackOver = false;
            isInfoOver = false;
            EndWinBattle();
        }
        else
        {
            state = BattleState.ENEMYTURN;
            BattleTip.text = "�Է��Ļغ�";
            StartCoroutine(EnemyAttack());
        }
    }

    IEnumerator PlayerStuffUsing()
    {
        //yield return new WaitForSeconds(1f);
        //����дʹ����Ʒ�Ե��˵����
        int UseWeaponIndex = inventoryStuff.HeightLightIndex;
        if(UseWeaponIndex != -1)
        {
            int weaponAttack = inventoryStuff.slotStuff[UseWeaponIndex].WeaponAttck;
            int thedefense = (int)(Random.Range(enemy.enemyDetails.defenseMin, enemy.enemyDetails.defenseMax));
            //����weapon_SO
            InventoryManager.Instance.ReduceWeaponInSO(UseWeaponIndex);

            currentEnemyhp = enemy.TakeDamage(PlayerProperty.Instance.PlayerAttack + weaponAttack, thedefense);
            EnemyHurtNum.text = string.Empty;
            EnemyHurtNum.text = "-" + (PlayerProperty.Instance.PlayerAttack + weaponAttack).ToString() + "��" + thedefense.ToString() + "%";
            yield return EnemyHurtEffect(2);
        }
        //�ر�stuffLine


        if (currentEnemyhp <= 0)
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
            BattleTip.text = "�Է��Ļغ�";
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
            EndManager.Instance.HealthNone = true;
            EventHandler.CallButton_ToENDGame();
            //����ֱ�ӿ����������Ϸʧ��
        }
        else
        {
            state = BattleState.PLAYERTURN;
            Playerturn();
        }
    }


    IEnumerator PlayerHurtEffect()
    {
        //��Ļ�ȶ���
        CameraControl.Instance.CMShake();
        EventHandler.CallInitSoundEffect(AudioManager.Instance.GetSoundDetailsfromName(SoundName.Efight));
        yield return EnemyInfoShake(PlayerState, 0.2f, 15f);
        //��ʾ��Ѫ��Ч�Ϳ�Ѫ
        PlayerHurtNum.gameObject.SetActive(true);
        PlayerProperty.Instance.PlayerHealth = currentPlayerhp;

        yield return new WaitForSeconds(2f);
    }
    void EndWinBattle()
    {
        //����һ���Ի��� չʾ�ɹ���Ȼ��ر�ս������
        StartCoroutine(WinBattle());


    }

    IEnumerator WinBattle()
    {
        EventHandler.CallUpdateBagUI(InventoryType.EquipInventory, InventoryManager.Instance.equipItemList_SO.bagItemList);
        EventHandler.CallUpdateBagUI(InventoryType.WeaponInventory, InventoryManager.Instance.weaponItemList_SO.bagItemList);
        yield return new WaitForSeconds(1f);
        if (isEquipOver)
        {
            BattleTipPanel.transform.GetChild(0).GetComponentInChildren<Text>().text
           = "ս�������������ж�������.��װ���������뼰ʱ������װ����";
            isEquipOver = false;
        }
        else
        {
            BattleTipPanel.transform.GetChild(0).GetComponentInChildren<Text>().text
         = "ս�������������ж�������";
            isEquipOver = false;
        }
        BattleTipPanel.SetActive(true);

        //ս������ȫ���ر�
        yield return new WaitForSeconds(1f);

        //����ȷ�ϰ�ť
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

        //����ҳ���ϵĸ�����ť��λ
        EventHandler.CallClosePanelAndButtonWhenFight(state);
        EventHandler.CallTellInventoryUIToOpenAB();
        AudioManager.Instance.ChangeBGMInDial("none");
        //��battleSystem��ɾ��
        //Destroy(enemy.gameObject);
        BattleSystem.Instance.ReStartTime();
        

    }

    //�ж������Ƿ�ɹ�
    public int PaddingRunning()
    {
        //player = GameObject.FindWithTag("Player").GetComponent<Player>();
        //enemy = player.enemy;
        enemy = BattleSystem.Instance.enemy;
        var enemyDetails = enemy.enemyDetails;
        Debug.Log(enemyDetails.EnemyName);
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
            //���ܳɹ�,��Ѫ
            int bloodCut = 80 - s;
            if (bloodCut < 0)
            {
                bloodCut = 0;
            }
            //���
            //BattleSystem.Instance.ReStartTime();
            return bloodCut;

        }
        else
        {
            //����ʧ��
            return -1;
        }

        //float runPrecentage = (170 - enemyDetails.AttackMin+(100-enemyDetails.defenseMin) +enemyDetails.MaxHP)
        //���ܸ���p =��170 -��������Сֵ + ������Сֵ + Ѫ������/ 100��
        //��pֵС�ڻ����0ʱ��p = 0��
        //��Ѫ��y = 80 - 100 * p��
    }


}
