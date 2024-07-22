using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WarPaperText : MonoBehaviour
{
    public Text PaperText;
    [TextArea]
    public List<string> everyDayText; 

    private void OnEnable()
    {
        UpdateWarPaperText();
    }

    private void OnDisable()
    {

    }

    private void UpdateWarPaperText()
    {
        int daycount = DayManager.Instance.DayCount;
        if (daycount <= everyDayText.Count)
        {
            PaperText.text = everyDayText[daycount-1];
        }
    }
}
