using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IfLeave : MonoBehaviour
{
    public GameObject AskPanel;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            AskPanel.SetActive(true);
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            AskPanel.SetActive(false);
        }
    }

}
