using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StairsTrigger : Singleton<StairsTrigger>
{
    public Transform Player;

    public bool isPlayerInStair;
    private bool isUpDownIconShow = false;
    private bool isCanChoose = false;

    public bool OutLift;

    private void Start()
    {
        isPlayerInStair = false;
        OutLift = false;
        isUpDownIconShow = false;
        isCanChoose = false;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Player")&& !collision.isTrigger)
        {
            //�����Ƿ��ѽ���¥������
            isPlayerInStair = !isPlayerInStair;

            //���ﲻ���ƶ�
            Player.gameObject.GetComponent<Player>().InputDisable = isPlayerInStair;

            //���ŵ�����Ч
            EventHandler.CallInitSoundEffect(AudioManager.Instance.GetSoundDetailsfromName(SoundName.liftOpen));
        }
    }

    private void Update()
    {
        //������¥����
        //if (isPlayerInStair)
        //{
        //    StartCoroutine(PlayerSwitchFlat());
        //}

        if(!isUpDownIconShow && isPlayerInStair)
        {
            isUpDownIconShow = true;
            StartCoroutine(ShowUpDownIcon());

        }

        if(isCanChoose)
        {
            StartCoroutine(PlayerSwitchFlat());
        }

    }

    IEnumerator ShowUpDownIcon()
    {
        yield return new WaitForSeconds(AudioManager.Instance.GetSoundDetailsfromName(SoundName.liftOpen).audioClip.length);
        //չʾͼ��

        isCanChoose = true;
    }

    IEnumerator PlayerSwitchFlat()
    {
        int daycount = DayManager.Instance.DayCount;
        //��һ¥
        if (Player.position.y > -5 && Player.position.y < 0)
        {
            if (Input.GetKeyDown(KeyCode.W))
            {
                //��������ڶ�¥
                if((daycount >= 7 && NPCManager.Instance.CheckNPCDoneDialogue(8015)) || daycount >= 11)
                {
                    isCanChoose = false;
                    //���¼�ͷͼ����ʧ

                    yield return PlayerMoveInLift(6.8f);
                    EventHandler.CallInitSoundEffect(AudioManager.Instance.GetSoundDetailsfromName(SoundName.liftArrive));
                    yield return new WaitForSeconds(AudioManager.Instance.GetSoundDetailsfromName(SoundName.liftArrive).audioClip.length);
                    OutLift = true;
                    int i = 0;
                    while (i < 4)
                    { yield return new WaitForSeconds(0.2f);
                        i++;
                    }
                    OutLift = false;
                    isPlayerInStair = false;
                    isUpDownIconShow = false;
                    Player.gameObject.GetComponent<Player>().InputDisable = false;
                }
                else
                {
                    string mtext = "���ݹ��ϣ������ϲ�ȥ��";
                    MainCanvasUITip.Instance.UITipShowAndDisappear(mtext);
                }
            }
            else if (Input.GetKeyDown(KeyCode.S))
            {
                isCanChoose = false;
                //���¼�ͷͼ����ʧ

                //���ݿ�ʼ�������ŵ��ݶ�������
                yield return PlayerMoveInLift(-11.31f);
                //���ŵ��ݵ��������
                EventHandler.CallInitSoundEffect(AudioManager.Instance.GetSoundDetailsfromName(SoundName.liftArrive));
                yield return new WaitForSeconds(AudioManager.Instance.GetSoundDetailsfromName(SoundName.liftArrive).audioClip.length);
                OutLift = true;
                //����������
                int i = 0;
                while (i < 4)
                {
                    yield return new WaitForSeconds(0.2f);
                    i++;
                }
                OutLift = false;
                isPlayerInStair = false;
                isUpDownIconShow = false;
                Player.gameObject.GetComponent<Player>().InputDisable = false;
            }
        }
        //����һ¥ -3.6f
        else if (Player.position.y > -12 && Player.position.y < -10)
        {
            if (Input.GetKeyDown(KeyCode.W))
            {
                isCanChoose = false;
                yield return PlayerMoveInLift(-3.6f);
                EventHandler.CallInitSoundEffect(AudioManager.Instance.GetSoundDetailsfromName(SoundName.liftArrive));
                yield return new WaitForSeconds(AudioManager.Instance.GetSoundDetailsfromName(SoundName.liftArrive).audioClip.length);
                OutLift = true;
                int i = 0;
                while (i < 4)
                {
                    yield return new WaitForSeconds(0.2f);
                    i++;
                }
                OutLift = false;
                isPlayerInStair = false;
                isUpDownIconShow = false;
                Player.gameObject.GetComponent<Player>().InputDisable = false;
            }
            else if (Input.GetKeyDown(KeyCode.S))
            {
                if((daycount >= 6 && NPCManager.Instance.CheckNPCDoneDialogue(8015)) || daycount >= 11)
                {
                    isCanChoose = false;
                    yield return PlayerMoveInLift(-18f);
                    EventHandler.CallInitSoundEffect(AudioManager.Instance.GetSoundDetailsfromName(SoundName.liftArrive));
                    yield return new WaitForSeconds(AudioManager.Instance.GetSoundDetailsfromName(SoundName.liftArrive).audioClip.length);
                    OutLift = true;
                    int i = 0;
                    while (i < 4)
                    {
                        yield return new WaitForSeconds(0.2f);
                        i++;
                    }
                    OutLift = false;
                    isPlayerInStair = false;
                    isUpDownIconShow = false;
                    Player.gameObject.GetComponent<Player>().InputDisable = false;
                }
                else
                {
                    string mtext = "���ݺ����ˣ������������ˡ�";
                    MainCanvasUITip.Instance.UITipShowAndDisappear(mtext);
                }
               
            }
        }
        //�ڵ��¶�¥
        else if (Player.position.y > -20 && Player.position.y < -17)
        {
            if (Input.GetKeyDown(KeyCode.W))
            {
                isCanChoose = false;
                yield return PlayerMoveInLift(-11.31f);
                EventHandler.CallInitSoundEffect(AudioManager.Instance.GetSoundDetailsfromName(SoundName.liftArrive));
                yield return new WaitForSeconds(AudioManager.Instance.GetSoundDetailsfromName(SoundName.liftArrive).audioClip.length);
                OutLift = true;
                int i = 0;
                while (i < 4)
                {
                    yield return new WaitForSeconds(0.2f);
                    i++;
                }
                OutLift = false;
                isPlayerInStair = false;
                isUpDownIconShow = false;
                Player.gameObject.GetComponent<Player>().InputDisable = false;
            }
        }
        //����̨
        else if(Player.position.y > 5.5 && Player.position.y < 8)
        {
            if (Input.GetKeyDown(KeyCode.S))
            {
                isCanChoose = false;
                yield return PlayerMoveInLift(-3.6f);
                EventHandler.CallInitSoundEffect(AudioManager.Instance.GetSoundDetailsfromName(SoundName.liftArrive));
                yield return new WaitForSeconds(AudioManager.Instance.GetSoundDetailsfromName(SoundName.liftArrive).audioClip.length);
                OutLift = true;
                int i = 0;
                while (i < 4)
                {
                    yield return new WaitForSeconds(0.2f);
                    i++;
                }
                OutLift = false;
                isPlayerInStair = false;
                isUpDownIconShow = false;
                Player.gameObject.GetComponent<Player>().InputDisable = false;
            }
        }
        //else if (Player.position.y > -20 && Player.position.y < -17)
        //    Player.position = new Vector2(Player.position.x, -18f);
    }

    IEnumerator PlayerMoveInLift(float tarY)
    {
        var cur = Player.position.y;
        float speed = Mathf.Abs(cur - tarY) / 1.5f;
        StartCoroutine(PlayLoopLiftOperate(tarY));
        while (Mathf.Abs(cur - tarY) > 0.0003)
        {
            cur = Mathf.MoveTowards(cur, tarY, speed * Time.deltaTime);
            if (Mathf.Abs(cur - tarY) < 0.0003)
            {
                cur = tarY;
            }
            Player.position = new Vector2(Player.position.x, cur);
            
            yield return null;
        }
    }

    IEnumerator PlayLoopLiftOperate(float tarY)
    {
        while (!Mathf.Approximately(Player.position.y, tarY))
        {
            EventHandler.CallInitSoundEffect(AudioManager.Instance.GetSoundDetailsfromName(SoundName.liftOperate));
            yield return new WaitForSeconds(AudioManager.Instance.GetSoundDetailsfromName(SoundName.liftOperate).audioClip.length);
        }
       
    }
}
