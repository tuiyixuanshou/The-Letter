using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PermaInter_turnOn : MonoBehaviour
{
    public GameObject LightManagerRB;
    public GameObject describeImage;
    private bool CanAct;
    private bool isAllLightOn;

    private void Start()
    {
        describeImage.SetActive(false);
        CanAct = false;
        isAllLightOn = false;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            describeImage.SetActive(true);
            CanAct = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        describeImage.SetActive(false);
        CanAct = false;
    }

    private void Update()
    {
        if (CanAct && Input.GetKeyDown(KeyCode.F))
        {
            isAllLightOn = !isAllLightOn;
            EventHandler.CallInitSoundEffect(AudioManager.Instance.GetSoundDetailsfromName(SoundName.TurnOnOff));
            StartCoroutine(SwichtLight());
        }
    }

    IEnumerator SwichtLight()
    {
        if (isAllLightOn)
        {
            //播放电流的声音
            EventHandler.CallInitSoundEffect(AudioManager.Instance.GetSoundDetailsfromName(SoundName.TurnOnElect));
            yield return new WaitForSeconds(AudioManager.Instance.GetSoundDetailsfromName(SoundName.TurnOnElect).audioClip.length);
            LightManagerRB.GetComponent<LightManagerInRB>().SwichLight(isAllLightOn);
        }
        else
        {
            LightManagerRB.GetComponent<LightManagerInRB>().SwichLight(isAllLightOn);
        }
    }
}
