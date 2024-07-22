using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{
    private Rigidbody2D rb;
    public float speed;
    private Vector2 MoveInput;

    //item互动
    private Item item;
    private bool ItemCanPick;

    //enemy互动
    public Enemy enemy;

    public bool InputDisable;
    private bool OutLiftInRB;

    [Header("item交互")]
    public int GetNum; 

    private float inputX, inputY;
    private Animator anim;
    private bool isMoving;

    public string sceneName;

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

    private void PlayerInputFor()
    {
        
        inputX = Input.GetAxisRaw("Horizontal");
        inputY = Input.GetAxisRaw("Vertical");

        if (inputX != 0 && inputY != 0)
        {
            inputX = 0.6f * inputX;
            inputY = 0.6f * inputY;
        }

        if (Input.GetKey(KeyCode.LeftShift))
        {
            inputX = 2f * inputX;
            inputY = 2f * inputY;
        }

        MoveInput = new Vector2(inputX, inputY);

        isMoving = !(MoveInput == Vector2.zero);
    }

    private void PlayerInputTwo()
    {
        inputX = Input.GetAxisRaw("Horizontal");
        inputY = 0;

        if (Input.GetKey(KeyCode.LeftShift))
        {
            inputX = 2f * inputX;
            inputY = 2f * inputY;
        }
       
        MoveInput = new Vector2(inputX, inputY);
        isMoving = !(MoveInput == Vector2.zero);
    }

    private void PlayerMoveAfterLift()
    {
        inputX = -1;
        inputY = 0;
        MoveInput = new Vector2(inputX,inputY);
        isMoving = !(MoveInput == Vector2.zero);
        rb.MovePosition(rb.position + MoveInput * speed * Time.deltaTime);
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
        anim.SetBool("isMoving", isMoving);
        if (isMoving)
        {
            anim.SetFloat("inputX", inputX);
            anim.SetFloat("inputY", inputY);
        }
    }

    private void FixedUpdate()
    {
        if (!InputDisable)
        {
            if(sceneName == "ResearchBase")
            {
                PlayerInputTwo();
            }
            else
            {
                PlayerInputFor();
            }
            MoveMent();
        }
        else
        {
            isMoving = false;
            //出电梯门的情况
            if(sceneName == "ResearchBase")
            {
                if (StairsTrigger.Instance.OutLift)
                {
                    PlayerMoveAfterLift();
                }
            }
        }
        
    }

    private void Update()
    {
        if(item != null)
        {
           
            if (ItemCanPick && Input.GetKeyDown(KeyCode.F))
            {
                anim.SetTrigger("isPick");
                InventoryManager.Instance.AddBagItem(item, true, 1);
                GetNum++;
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
        //根据场景不同重置位置
        //if (isStayInChurchDoor)
        //{
        //    isStayInChurchDoor = false;
        //    this.transform.position = new Vector3(30.85f, 11.05f, 0f);
        //}
        //else
        //{
        //    this.transform.position = new Vector3(0, 0, 0);
        //}
        //Debug.Log(sceneName);
        //Debug.Log(this.transform.position);
    }

    private void OnAfterSceneLoad()
    {
        GetNum = 0;
        sceneName = SceneManager.GetActiveScene().name;
        if (PlayerInventory.Instance.isStayInChurchOutDoor)
        {
            this.transform.position = new Vector3(30.85f, 11.05f, 0f);
            PlayerInventory.Instance.isStayInChurchOutDoor = false;
        }
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
