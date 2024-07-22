using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LianZiControl : MonoBehaviour
{
    public Animator anim;
    private bool isLianOpen;

    private void Start()
    {
        isLianOpen = false;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            if (!isLianOpen)
            {
                isLianOpen = true;
                anim.SetTrigger("LianOpen");
                EventHandler.CallPlaySoundEvent(SoundName.OpenChurchLian);
            }
        }
    }
}
