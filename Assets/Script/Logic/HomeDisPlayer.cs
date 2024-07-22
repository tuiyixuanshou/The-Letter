using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HomeDisPlayer : Singleton<HomeDisPlayer>
{
    public Sprite HomeMorning;
    public Sprite HomeEvening;
    public Sprite HomeNight;
    public SpriteRenderer spriteRenderer;
   
    // Start is called before the first frame update
    void Start()
    {
        //Debug.Log(GameObject.FindGameObjectWithTag("Player").name);
        //GameObject Player = GameObject.FindGameObjectWithTag("Player");
        //if (Player != null)
        //{
        //    Player.SetActive(false);
        //}

        //Player.transform.position = new Vector3(0, 0, 0);
    }

    public void changeMorningSprite()
    {
        spriteRenderer.sprite = HomeMorning;
    }

    public void changeEveningSprite()
    {
        spriteRenderer.sprite = HomeEvening;
    }

    public void changeNightSprite()
    {
        spriteRenderer.sprite = HomeNight;
    }
}
