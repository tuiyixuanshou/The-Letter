using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stair3Trigger : MonoBehaviour
{
    public Transform Player;
    public bool isDoorOpen;
    private CanvasGroup fadeCanvasGroup;


    private void Start()
    {
        isDoorOpen = NPCManager.Instance.CheckNPCDoneDialogue(8121);
        fadeCanvasGroup = GameObject.FindGameObjectWithTag("FadeCanvas").GetComponent<CanvasGroup>();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.transform.CompareTag("Player"))
        {
            if (isDoorOpen)
            {
                if(Player.position.y > -20 && Player.position.y < -17)  //在地下二楼
                {
                    StartCoroutine(MoveTo32(-27.68f));
                }
                else
                {
                    StartCoroutine(MoveTo32(-18f));
                }
                
            }
            else
            {
                string mtext = "这里被杂物堵住了……";
                MainCanvasUITip.Instance.UITipShowAndDisappear(mtext);
            }
        }
    }

    IEnumerator MoveTo32(float tarY)
    {
        fadeCanvasGroup.blocksRaycasts = true;
        Player.gameObject.GetComponent<Player>().InputDisable = true;
        yield return TransitionFadeCanvas(1, 0.2f);
        Player.position = new Vector2(-6.43f, tarY);
        yield return TransitionFadeCanvas(0, 0.2f);
        Player.gameObject.GetComponent<Player>().InputDisable = false;
        fadeCanvasGroup.blocksRaycasts = false;
    }


    IEnumerator TransitionFadeCanvas(float targetAlpha, float transitionFadeDuration)
    {
        float speed = Mathf.Abs(fadeCanvasGroup.alpha - targetAlpha) / transitionFadeDuration;
        while (!Mathf.Approximately(fadeCanvasGroup.alpha, targetAlpha))
        {
            fadeCanvasGroup.alpha = Mathf.MoveTowards(fadeCanvasGroup.alpha, targetAlpha, speed * Time.deltaTime);
            yield return null;
        }
        if (targetAlpha > 0.5f)
        {
            yield return new WaitForSeconds(0.5f);
        }
        
    }

   
}
