using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BookCloseOpen : MonoBehaviour
{
    //�ر�������canvas
    public void CloseInfoBook()
    {
        this.gameObject.SetActive(false);
    }

    //��������canvas
    public void OpenInfoBook()
    {
        this.gameObject.SetActive(true);
    }

}
