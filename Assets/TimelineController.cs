using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.Timeline;


public class TimelineController : MonoBehaviour
{
    public PlayableDirector director;
    public GameObject audioSource;
    private bool isAlreadyPlay=false;
    public TimelineAsset[] A;
    public float countTime = 0f;
    public GameObject video;
    int index = 0;

    public GameObject KaichangManhua;

    void Update()
    {
        ChangeTimeLine();
        if (countTime <= 65f)
        {
            CountTime();
        }
    }

    public void ChangeTimeLine()
    {
        if(Input.GetMouseButtonDown(0)&&countTime>63f)
        {
            if(index<A.Length)
            {
                if(director.state!=PlayState.Playing)
                {
                    director.Play(A[index++]);
                }
            }
            else
            {
                KaichangManhua.SetActive(false);
            }
        }
        
    }
    public void CountTime()
    {
        countTime += Time.deltaTime;
        if (countTime > 63f)
        {
            video.SetActive(false);
            audioSource.SetActive(true);
        }
       
    }
    public void DestroyVideo()
    {
        if(countTime>63f)
        {
            video.SetActive(false);
            audioSource.SetActive(true);
        }
    }
}
