using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;
public class LightControl : MonoBehaviour
{
    public Light2D light2D;
    public int Index;

    [Header("µÆ¿ª±Õ")]
    public bool isPermanentOn;
    public bool isPermanentOff;
    public bool isOn;

    [Header("µÆÉÁ")]
    public bool isGlint;
    public float maxDuration;
    public float minDuration;
    private bool isGlinting;
    public float PSjiangeTime;
    public float LAjiangeTime;
    
    float intensitymax => Random.Range(0.95f, 01.22f);
    float intensitymin => Random.Range(0.4f, 0.6f);
    float curDuration => Random.Range(minDuration, maxDuration);

    private void Update()
    {
        //µÆÉÁË¸
        if (isGlint && !isGlinting)
        {
            isGlinting = true;
            //ÉÁË¸º¯Êý
            StartCoroutine(doGlint(maxDuration, minDuration));
        }

        //µÆ¿ª¹Ø
        if (!isPermanentOn)
        {
            if (isOn)
            {
                light2D.enabled = true;
            }
            else
            {
                light2D.enabled = false;
            }
        }
        else
        {
            light2D.enabled = true;
        }
        if (isPermanentOff)
        {
            light2D.enabled = false;
        }
    }

    IEnumerator doGlint(float maxD,float minD)
    {
        while (isGlint)
        {
            var tar1 = intensitymin;
            var tar2 = intensitymax;
            var curD1 = curDuration;
            var curD2 = curDuration;
            yield return LightGlint(tar1, curD1);
            yield return new WaitForSeconds(LAjiangeTime);

            yield return LightGlint(tar2, curD2);
            yield return new WaitForSeconds(PSjiangeTime);
        }

     }

    IEnumerator LightGlint(float tar, float curD)
    {
        float cur = light2D.intensity;
        while (!Mathf.Approximately(cur, tar))
        {
            cur = Mathf.MoveTowards(cur, tar, Mathf.Abs(cur - tar) / curD * Time.deltaTime);
            if (Mathf.Abs(cur - tar) < 0.01)
                cur = tar;
            light2D.intensity = cur;
            yield return null;
        }
       
    }
}
