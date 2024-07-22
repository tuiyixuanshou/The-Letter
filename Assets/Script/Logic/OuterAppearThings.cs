using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class OuterAppearThings : MonoBehaviour
{
    public GameObject ParkSafe;
    public GameObject ParkWar;
    public GameObject ParkEpidemic;

    public GameObject CSafe;
    public GameObject CWar;
    public GameObject CEpidemic;

    public GameObject RBSafe;
    public GameObject RBWar;
    public GameObject RBEpidemic;

    public GameObject WSafe;
    public GameObject WWar;
    public GameObject WEpidemic;

    private SceneMod mod;

    public GameObject HurtPanel;

    private void OnEnable()
    {
        EventHandler.AfterSceneLoad += OnAfterSceneLoad;
        EventHandler.AfterSceneLoadMove += OnAfterSceneLoadMove;

    }

    private void OnDisable()
    {
        EventHandler.AfterSceneLoad -= OnAfterSceneLoad;
        EventHandler.AfterSceneLoadMove -= OnAfterSceneLoadMove;
    }

    private void OnAfterSceneLoad()
    {
        string activename = SceneManager.GetActiveScene().name;
        int daycount = DayManager.Instance.DayCount;
        if(activename == "SafetyArea")
        {
            if (daycount < 6)
            {
                ParkSafe.SetActive(true);
                ParkWar.SetActive(false);
                ParkEpidemic.SetActive(false);
                mod = SceneMod.Safe;
            }
            else if (daycount >= 6 && daycount < 9)
            {
                ParkSafe.SetActive(false);
                ParkWar.SetActive(true);
                ParkEpidemic.SetActive(false);
                mod = SceneMod.War;
            }
            else
            {
                ParkSafe.SetActive(false);
                ParkWar.SetActive(true);
                ParkEpidemic.SetActive(true);
                mod = SceneMod.Epidemic;
            }
        }
        else if(activename == "Church")
        {
            if (daycount < 6)
            {
                CSafe.SetActive(true);
                CWar.SetActive(false);
                CEpidemic.SetActive(false);
                mod = SceneMod.Church;
            }
            else if (daycount >= 6 && daycount < 9)
            {
                CSafe.SetActive(false);
                CWar.SetActive(true);
                CEpidemic.SetActive(false);
                mod = SceneMod.Church;
            }
            else
            {
                CSafe.SetActive(false);
                CWar.SetActive(true);
                CEpidemic.SetActive(true);
                mod = SceneMod.Church;
            }
        }
        else if (activename == "ResearchBase")
        {
            if (daycount < 6)
            {
                RBSafe.SetActive(true);
                RBWar.SetActive(false);
                //RBEpidemic.SetActive(false);
                mod = SceneMod.Safe;
            }
            else if (daycount >= 6 && daycount < 9)
            {
                RBSafe.SetActive(false);
                RBWar.SetActive(true);
                //RBEpidemic.SetActive(false);
                mod = SceneMod.War;
            }
            else
            {
                RBSafe.SetActive(false);
                RBWar.SetActive(true);
                mod = SceneMod.Epidemic;
            }
          
        }
        else if (activename == "WarYield")
        {
            if (daycount < 4)
            {
                WSafe.SetActive(true);
                WWar.SetActive(false);
                //WEpidemic.SetActive(false);
                mod = SceneMod.Safe;
            }
            else if (daycount >= 4 && daycount< 10)
            {
               WSafe.SetActive(false);
                WWar.SetActive(true);
                //WEpidemic.SetActive(false);
                mod = SceneMod.War;
            }
            else if (daycount >= 10)
            {
                WSafe.SetActive(false);
                WWar.SetActive(true);
                //WEpidemic.SetActive(false);
                mod = SceneMod.Epidemic;
            }

        }

    }

    private void OnAfterSceneLoadMove()
    {
        //战区的内容
        if(mod == SceneMod.War)
        {
            HurtPanel.SetActive(true);
            StartCoroutine(HurtPanelFuncWar());
        }
        else if(mod == SceneMod.Epidemic)
        {
            HurtPanel.SetActive(true);
            StartCoroutine(HurtPanelFuncEpid());
        }
    }

    IEnumerator HurtPanelFuncWar()
    {
        int i = Random.Range(1, 5);
        switch (i)
        {
            case 1:
                HurtPanel.transform.GetChild(1).GetComponent<Text>().text = "今天很幸运，没有受到任何伤害";
                break;
            case 2:
                HurtPanel.transform.GetChild(1).GetComponent<Text>().text = "你不幸被子弹打中了腿部，血量下降10，精神下降3";
                DialPlayerPChange.Instance.PlayerCutHealth(-10);
                DialPlayerPChange.Instance.PlayerCutSan(-3);
                break;
            case 3:
                HurtPanel.transform.GetChild(1).GetComponent<Text>().text = "你不幸被子弹打中腹部，血量下降40，精神下降5";
                DialPlayerPChange.Instance.PlayerCutHealth(-40);
                DialPlayerPChange.Instance.PlayerCutSan(-5);
                break;
            case 4:
                HurtPanel.transform.GetChild(1).GetComponent<Text>().text = "你不幸被炮弹擦伤，血量下降20，精神下降2";
                DialPlayerPChange.Instance.PlayerCutHealth(-20);
                DialPlayerPChange.Instance.PlayerCutSan(-2);
                break;
            default:
                HurtPanel.SetActive(false);
                break;
        }

        yield return new WaitForSeconds(3f);
        HurtPanel.SetActive(false);
    }

    IEnumerator HurtPanelFuncEpid()
    {
        int i = Random.Range(1, 4);
        switch (i)
        {
            case 1:
                HurtPanel.transform.GetChild(1).GetComponent<Text>().text = "没有受到任何伤害，但精神下降10";
                DialPlayerPChange.Instance.PlayerCutSan(-10);
                break;
            case 2:
                HurtPanel.transform.GetChild(1).GetComponent<Text>().text = "你不幸被子弹打中了腿部，血量下降10，精神下降5";
                DialPlayerPChange.Instance.PlayerCutHealth(-10);
                DialPlayerPChange.Instance.PlayerCutSan(-5);
                break;
            case 3:
                HurtPanel.transform.GetChild(1).GetComponent<Text>().text = "你不幸被子弹打中腹部，血量下降40，精神下降10";
                DialPlayerPChange.Instance.PlayerCutHealth(-40);
                DialPlayerPChange.Instance.PlayerCutSan(-10);
                break;
            default:
                HurtPanel.SetActive(false);
                break;
        }

        yield return new WaitForSeconds(3f);
        HurtPanel.SetActive(false);
    }
}
