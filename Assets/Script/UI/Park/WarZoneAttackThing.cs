using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WarZoneAttackThing : MonoBehaviour
{
    public int AttackNum;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            int currentPlayerhp =  collision.gameObject.GetComponent<Player>().TakeDamage(AttackNum, 0);

            CameraControl.Instance.CMShake();

            string mtext = "�ȱ������ѣ������е��ж��ˣ�Ѫ�����͡���";
            MainCanvasUITip.Instance.UITipShowAndDisappear(mtext);

            EventHandler.CallPlaySoundEvent(SoundName.Efight);

            PlayerProperty.Instance.PlayerHealth = currentPlayerhp;
        }
    }
}
