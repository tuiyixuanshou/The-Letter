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
            //人物是否已进入楼梯区域
            isPlayerInStair = !isPlayerInStair;

            //人物不再移动
            Player.gameObject.GetComponent<Player>().InputDisable = isPlayerInStair;

            //播放电梯音效
            EventHandler.CallInitSoundEffect(AudioManager.Instance.GetSoundDetailsfromName(SoundName.liftOpen));
        }
    }

    private void Update()
    {
        //人物在楼梯内
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
        //展示图标

        isCanChoose = true;
    }

    IEnumerator PlayerSwitchFlat()
    {
        int daycount = DayManager.Instance.DayCount;
        //在一楼
        if (Player.position.y > -5 && Player.position.y < 0)
        {
            if (Input.GetKeyDown(KeyCode.W))
            {
                //人物出现在顶楼
                if((daycount >= 7 && NPCManager.Instance.CheckNPCDoneDialogue(8015)) || daycount >= 11)
                {
                    isCanChoose = false;
                    //上下箭头图标消失

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
                    string mtext = "电梯故障，好像上不去了";
                    MainCanvasUITip.Instance.UITipShowAndDisappear(mtext);
                }
            }
            else if (Input.GetKeyDown(KeyCode.S))
            {
                isCanChoose = false;
                //上下箭头图标消失

                //电梯开始动，播放电梯动的声音
                yield return PlayerMoveInLift(-11.31f);
                //播放电梯到达的声音
                EventHandler.CallInitSoundEffect(AudioManager.Instance.GetSoundDetailsfromName(SoundName.liftArrive));
                yield return new WaitForSeconds(AudioManager.Instance.GetSoundDetailsfromName(SoundName.liftArrive).audioClip.length);
                OutLift = true;
                //人物往外走
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
        //地下一楼 -3.6f
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
                    string mtext = "电梯好像坏了，不能再往下了。";
                    MainCanvasUITip.Instance.UITipShowAndDisappear(mtext);
                }
               
            }
        }
        //在地下二楼
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
        //在天台
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
