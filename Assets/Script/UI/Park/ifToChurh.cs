using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ifToChurh : MonoBehaviour
{
    public GameObject AskPanel;
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.transform.CompareTag("Player"))
        {
            TimeManager.Instance.gameClockPause = true;
            BattleSystem.Instance.gameClockPause = true;
            AskPanel.SetActive(true);
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        TimeManager.Instance.gameClockPause = false;
        BattleSystem.Instance.gameClockPause = false;
        AskPanel.SetActive(false);
    }

    public void OnStayButtonPressed()
    {
        EventHandler.CallPlaySoundEvent(SoundName.ButtonClick);
        TimeManager.Instance.gameClockPause = false;
        BattleSystem.Instance.gameClockPause = false;
        AskPanel.SetActive(false);
    }
}
