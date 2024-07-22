using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    private Rigidbody2D rb;
    public float speed;
    private Vector2 MoveInput;

    //item»¥¶¯
    private Item item;
    private bool ItemCanPick;

    //enemy»¥¶¯
    public Enemy enemy;



    private float inputX, inputY;
    private Animator anim;
    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = this.transform.GetChild(0).GetComponent<Animator>();
    }

    private void OnEnable()
    {
        EventHandler.BeforeSceneUnLoad += OnBeforeSceneUnLoad;
        EventHandler.AfterSceneLoad += OnAfterSceneLoad;
    }



    private void OnDisable()
    {
        EventHandler.BeforeSceneUnLoad -= OnBeforeSceneUnLoad;
        EventHandler.AfterSceneLoad -= OnAfterSceneLoad;
    }

    private void PlayerInput()
    {
        
        inputX = Input.GetAxisRaw("Horizontal");
        inputY = Input.GetAxisRaw("Vertical");

        if (inputX != 0 && inputY != 0)
        {
            inputX = 0.6f * inputX;
            inputY = 0.6f * inputY;
        }

        MoveInput = new Vector2(inputX, inputY);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        item = collision.gameObject.GetComponent<Item>();
        //Debug.Log(item);
        //Debug.Log(item.itemID);
        if (item != null)
        {
            
            ItemCanPick = true;
            item.transform.GetChild(1).gameObject.SetActive(ItemCanPick);
            
            //if (item.itemDetails.canPick && Input.GetKeyDown(KeyCode.Space))
            //{
            //    InventoryManager.Instance.AddBagItem(item, true, 1);
            //}
        }

        //enemy = collision.gameObject.GetComponent<Enemy>();
        //if(enemy != null)
        //{
        //    enemy.StartDoDialogue();
        //}

    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        Item item = collision.gameObject.GetComponent<Item>();
        ItemCanPick = false;
        if(item != null)
        {
            if (item.transform.GetChild(1) != null)
            {
                item.transform.GetChild(1).gameObject.SetActive(ItemCanPick);
            }
        }
        
    }

    private void MoveMent()
    {
        rb.MovePosition(rb.position + MoveInput * speed * Time.deltaTime);
    }

    private void TranslateAnim()
    {
        if (inputX == 0 && inputY == 0)
        {
            anim.SetBool("isFront", false);
            anim.SetBool("isBack", false);
        }
        else if (inputX != 0 && inputY == 0)
        {
            anim.SetBool("isFront", true);
            anim.SetBool("isBack", false);
        }
        else if (inputY > 0)
        {
            anim.SetBool("isBack", true);
            anim.SetBool("isFront", false);
        }
        else if (inputY < 0)
        {
            anim.SetBool("isFront", true);
            anim.SetBool("isBack", false);
        }

    }

    private void FixedUpdate()
    {
        PlayerInput();
        MoveMent();
    }

    private void Update()
    {
        if(item != null)
        {
            if (ItemCanPick && Input.GetKeyDown(KeyCode.Space))
            {
                InventoryManager.Instance.AddBagItem(item, true, 1);
            }
        }
        TranslateAnim();

        //if(enemy != null)
        //{
        //    if (enemy.StartTalk && (Input.GetKeyDown(KeyCode.Space) || Input.GetMouseButtonDown(0)) && !enemy.isTalking)
        //    {
        //        enemy.StartDoDialogue();
        //    }
        //}
        
    }

    private void OnBeforeSceneUnLoad()
    {
        this.transform.position = new Vector3(0, 0, 0);
        Debug.Log(this.transform.position);
    }

    private void OnAfterSceneLoad()
    {

    }


    public int TakeDamage(int dmg, int defense)
    {

        int currentHP = PlayerProperty.Instance.PlayerHealth;
        if (dmg - defense > 0)
        {
            currentHP -= (dmg - defense);
        }
        else if (dmg - defense <= 0)
        {

        }

        return currentHP;

    }

    public void Heal(int amount)
    {
        int currentHP = PlayerProperty.Instance.PlayerHealth;

        currentHP += amount;
        PlayerProperty.Instance.PlayerHealth = currentHP;

        if (PlayerProperty.Instance.PlayerHealth > Settings.OrigPlayerHealth)
        {
            PlayerProperty.Instance.PlayerHealth = Settings.OrigPlayerHealth;
        }
    }
}
