using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BookCloseOpen : MonoBehaviour
{
    //关闭线索本canvas
    public void CloseInfoBook()
    {
        this.gameObject.SetActive(false);
    }

    //打开线索本canvas
    public void OpenInfoBook()
    {
        this.gameObject.SetActive(true);
    }

}
