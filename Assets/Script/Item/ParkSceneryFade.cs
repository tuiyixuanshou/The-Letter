using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParkSceneryFade : MonoBehaviour
{
    public PolygonCollider2D coll;
    public SpriteRenderer spriteRenderer;
    public float FadeAlpha = 0.5f;

    private bool isStartFade;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        //Debug.Log("do this");
        if (collision.tag == "Player" /*&& !isStartFade*/)
        {
            //Debug.Log("do this");
            //isStartFade = true;
            StartCoroutine(SceneryItemFade(FadeAlpha, 0.5f));
        }
    }

    private IEnumerator SceneryItemFade(float targetAlpha, float FadeDuration)
    {
        float speed = Mathf.Abs(spriteRenderer.color.a - targetAlpha) / FadeDuration;

        while (!Mathf.Approximately(spriteRenderer.color.a, targetAlpha))
        {
            float a = Mathf.MoveTowards(spriteRenderer.color.a, targetAlpha, speed * Time.deltaTime);
            spriteRenderer.color = new Color(spriteRenderer.color.r, spriteRenderer.color.g, spriteRenderer.color.b, a);
            yield return null;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Player"/* && isStartFade*/)
        {
            //isStartFade = false;
            StartCoroutine(SceneryItemFade(1f, 0.2f));
        }
    }

}
