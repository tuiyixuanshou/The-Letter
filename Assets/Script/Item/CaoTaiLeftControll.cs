using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CaoTaiLeftControll : MonoBehaviour
{

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            this.GetComponentInParent<SpriteRenderer>().sortingOrder = 2;
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            this.GetComponentInParent<SpriteRenderer>().sortingOrder = 1;
        }
    }

    //private void OnCollisionExit2D(Collision2D collision)
    //{
    //    if (collision.gameObject.tag == "Player")
    //    {
    //        this.GetComponentInParent<SpriteRenderer>().sortingOrder = 1;
    //    }
    //}

    //private void OnTriggerEnter2D(Collider2D collision)
    //{
    //    Debug.Log("do this");
    //    if (collision.gameObject.tag == "Player")
    //    {
    //        this.GetComponentInParent<SpriteRenderer>().sortingOrder = 2;
    //    }
    //}

    //private void OnCollisionEnter(Collision collision)
    //{
    //    Debug.Log("do this");
    //    if (collision.gameObject.tag == "Player")
    //    {
    //        this.GetComponentInParent<SpriteRenderer>().sortingOrder = 2;
    //    }
    //}
}

