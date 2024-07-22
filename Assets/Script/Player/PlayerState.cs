using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerState : MonoBehaviour
{
    public Text MoneyNum;
    public float changeSpeed;

    public Image HealthLine;
    public Text HealthNum;
    private float oriheightLine;
    private bool isHealthyLineStartChange;

    public Image SanLine;
    public Text SanNum;
    private float oriSanLine;
    private bool isSanLineStartChange;


    void Start()
    {
        oriheightLine = HealthLine.rectTransform.rect.width;
        oriSanLine = SanLine.rectTransform.rect.width;
        HealthNum.text = Settings.OrigPlayerHealth.ToString();
        SanNum.text = Settings.OrigPlayerSan.ToString();

    }


    void Update()
    {
        MoneyNum.text = PlayerProperty.Instance.PlayerWealth.ToString();
        UpdateHealthLine();
        UpdateSanLine();
    }

    void UpdateHealthLine()
    {
        int currentHealth = PlayerProperty.Instance.PlayerHealth;
        float LineRate = (float)currentHealth / Settings.OrigPlayerHealth;
        float currentHealthyLine = HealthLine.rectTransform.rect.width;

        if (!Mathf.Approximately(oriheightLine*LineRate, currentHealthyLine) && !isHealthyLineStartChange)
        {
            isHealthyLineStartChange = true;
            float targetHealthNum = PlayerProperty.Instance.PlayerHealth;
            float dis = Mathf.Abs(oriheightLine * LineRate - currentHealthyLine);
            //Ñ­»·´ÎÊý
            float changeTimes = dis / changeSpeed;
            float numDis = Mathf.Abs(targetHealthNum - float.Parse(HealthNum.text));
            float numchangespeed = numDis / changeTimes;
            StartCoroutine(LineChange(LineRate,targetHealthNum,numchangespeed));
        }
    }

    void UpdateSanLine()
    {
        int currentSan = PlayerProperty.Instance.PlayerSan;
        float LineRate = (float)currentSan / Settings.OrigPlayerSan;
        float currentSanLine = SanLine.rectTransform.rect.width;

        if(!Mathf.Approximately(oriSanLine*LineRate,currentSanLine)&& !isSanLineStartChange)
        {
            isSanLineStartChange = true;
            float targetSanNum = PlayerProperty.Instance.PlayerSan;
            float dis = Mathf.Abs(oriSanLine*LineRate - currentSanLine);
            float changeTimes = dis / changeSpeed;
            float numDis = Mathf.Abs(targetSanNum - float.Parse(SanNum.text));
            float numChangeSpeed = numDis / changeTimes;
            StartCoroutine(SanLineChange(LineRate,targetSanNum,numChangeSpeed));
        }
    }

    private IEnumerator LineChange(float lineRate,float targetNum,float numChangeSpeed)
    {
        float currentLine = HealthLine.rectTransform.rect.width;
        float currentHealthNum = int.Parse(HealthNum.text);

        while (!Mathf.Approximately(currentLine, oriheightLine * lineRate))
        {
            currentLine = Mathf.MoveTowards(currentLine, oriheightLine * lineRate, changeSpeed);
            currentHealthNum = Mathf.MoveTowards(currentHealthNum, targetNum, numChangeSpeed);
            HealthNum.text = ((int)(currentHealthNum)).ToString();
            HealthLine.rectTransform.sizeDelta = new Vector2(currentLine, HealthLine.rectTransform.rect.height);
            yield return new WaitForSeconds(0.05f);

        }
        isHealthyLineStartChange = false;
    }

    private IEnumerator SanLineChange(float lineRate, float targetNum, float numChangeSpeed)
    {
        float currentLine = SanLine.rectTransform.rect.width;
        float currentSanNum = int.Parse(SanNum.text);
        while (!Mathf.Approximately(currentLine, oriSanLine * lineRate))
        {
            currentLine = Mathf.MoveTowards(currentLine, oriSanLine * lineRate, changeSpeed);
            currentSanNum = Mathf.MoveTowards(currentSanNum, targetNum, numChangeSpeed);
            SanNum.text = ((int)(currentSanNum)).ToString();
            SanLine.rectTransform.sizeDelta = new Vector2(currentLine, SanLine.rectTransform.rect.height);
            yield return new WaitForSeconds(0.05f);

        }
        isSanLineStartChange = false;
    }
}

