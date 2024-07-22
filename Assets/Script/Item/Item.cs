using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour
{
    public int itemID;
    public  SpriteRenderer ItemspriteRenderer;
    public SpriteRenderer UIspriteRenderer;
    public ItemDetails itemDetails;
    private BoxCollider2D coll;

    private void Awake()
    {
        coll = GetComponent<BoxCollider2D>();
    }


    private void Start()
    {
        if(itemID != 0)
        {
            Init(itemID);
        }
        
    }

    public void Init(int ID)
    {
        itemID = ID;
        itemDetails = InventoryManager.Instance.GetItemDetails(itemID);
        if(itemDetails != null)
        {
            ItemspriteRenderer.sprite = itemDetails.ItemSprite !=null ? itemDetails.ItemSprite: itemDetails.ItemIcon;
        }

        Vector2 newsize = new Vector2(ItemspriteRenderer.sprite.bounds.size.x, ItemspriteRenderer.sprite.bounds.size.y);
        coll.size = newsize;
        coll.offset = new Vector2(0, ItemspriteRenderer.sprite.bounds.center.y);

        UIspriteRenderer.gameObject.SetActive(false);
    }
}
