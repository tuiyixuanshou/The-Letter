using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHurtOver : MonoBehaviour
{

    //�����¼�
    public void EnemyHurtIsOver()
    {
        this.gameObject.SetActive(false);
    }

}
