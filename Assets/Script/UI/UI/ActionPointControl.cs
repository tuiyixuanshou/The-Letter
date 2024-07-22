using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ActionPointControl : Singleton<ActionPointControl>
{
    public Text RestPoint;
    public int currentPoint;
    public GameObject[] PointLights;

    private int maxPoint;

    private void Start()
    {
        currentPoint = PlayerProperty.Instance.PlayerAction;
        maxPoint = Settings.OrigPlayerAction;
        PointHUDControl(currentPoint);
    }

    public void PointHUDControl(int restAction)
    {

        currentPoint = restAction;
        PlayerProperty.Instance.PlayerAction = currentPoint;
        RestPoint.text = currentPoint.ToString();

        for (int i = 0; i < restAction; i++)
        {
            PointLights[i].SetActive(true);
        }
        for(int i = maxPoint-1; i> restAction-1; i--)
        {
            PointLights[i].SetActive(false);
        }
    }
}
