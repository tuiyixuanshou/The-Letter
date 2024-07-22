using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class SlotUI : MonoBehaviour,IPointerClickHandler,IBeginDragHandler,IDragHandler,IEndDragHandler,IPointerEnterHandler,IPointerExitHandler
{
    [Header("�����ȡ")]
    [SerializeField] private Image slotImage;
    [SerializeField] private Text amountText;
    [SerializeField] private Button button;
    [SerializeField] private Image EquipTips;


    public Image slotHighLight;

    [Header("��������")]
    public SlotType slotType;

    [Header("������Ϣ")]
    public bool isSelected;
    public int index;

    [Header("��Ʒ��Ϣ")]
    public ItemDetails itemDetails;
    public int itemAmount;

    private InventoryUI inventoryUI => GetComponentInParent<InventoryUI>();

    private void Start()
    {
        isSelected = false;
        if(itemDetails.ItemID == 0)
        {
            UpdateEmptySlot();
        }

        slotHighLight.gameObject.SetActive(false);
    }

    //��slot����Ϊ�ո���
    public void UpdateEmptySlot()
    {
        if (isSelected)
        {
            isSelected = false;
        }

        itemDetails = new ItemDetails();
        itemAmount = 0;

        slotImage.enabled = false;
        amountText.text = string.Empty;
        button.interactable = false;
        EquipTips.enabled = false;
    }

    //���¸�����Ϣ
    public void UpdateSlot(ItemDetails item,int amount)
    {
        itemDetails = item;
        slotImage.enabled = true;
        slotImage.sprite = itemDetails.ItemIcon;
        itemAmount = amount;
        amountText.text = amount.ToString();
        button.interactable = true;

        if (item.canEquip)
        {
            EquipTips.enabled = true;
        }
        else
        {
            EquipTips.enabled = false;
        }
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (itemDetails.ItemID == 0)
            return;
        isSelected = !isSelected;
        inventoryUI.UpdateSlotHighLight(index);
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        if(itemAmount != 0)
        {
            inventoryUI.DragItem.enabled = true;
            inventoryUI.DragItem.sprite = slotImage.sprite;

            //inventoryUI.DragItem.SetNativeSize();
            isSelected = true;
            inventoryUI.UpdateSlotHighLight(index);

        }
    }

    public void OnDrag(PointerEventData eventData)
    {
        inventoryUI.DragItem.transform.position = Input.mousePosition;
        //Debug.Log(eventData.pointerCurrentRaycast.gameObject.name);
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        inventoryUI.DragItem.enabled = false;
        if(eventData.pointerCurrentRaycast.gameObject!= null)
        {
            if (eventData.pointerCurrentRaycast.gameObject.GetComponent<SlotUI>() == null)
                return;

            var targetSlot = eventData.pointerCurrentRaycast.gameObject.GetComponent<SlotUI>();
            Debug.Log(eventData.pointerCurrentRaycast.gameObject.GetComponent<SlotUI>().gameObject.name);

            int targetIndex = targetSlot.index;

            //1�ڱ��������н���λ��
            if (slotType == SlotType.Bag && targetSlot.slotType == SlotType.Bag)
            {
                InventoryManager.Instance.SwapItem(1,index, targetIndex);
                inventoryUI.UpdateSlotHighLight(-1);
            }
            //2����->�̵�
            if(slotType == SlotType.Bag && targetSlot.slotType == SlotType.Shop)
            {
                InventoryManager.Instance.SwapItem(2, index, targetIndex);
                inventoryUI.UpdateSlotHighLight(-1);
            }
            //3�̵�->����
            if(slotType == SlotType.Shop&&targetSlot.slotType == SlotType.Bag)
            {
                InventoryManager.Instance.SwapItem(3, index, targetIndex);
                inventoryUI.UpdateSlotHighLight(-1);
            }
            //4�̵�->�̵�
            if(slotType == SlotType.Shop && targetSlot.slotType == SlotType.Shop)
            {
                InventoryManager.Instance.SwapItem(4, index, targetIndex);
                inventoryUI.UpdateSlotHighLight(-1);
            }
            //5����->װ��
            if(slotType == SlotType.Bag && targetSlot.slotType == SlotType.Equipment)
            {
                InventoryManager.Instance.SwapItem(5, index, targetIndex);
                inventoryUI.UpdateSlotHighLight(-1);
            }
            //6װ��->����
            if(slotType == SlotType.Equipment && targetSlot.slotType == SlotType.Bag)
            {
                InventoryManager.Instance.SwapItem(6, index, targetIndex);
                inventoryUI.UpdateSlotHighLight(-1);
            }
            //7����->����
            if (slotType == SlotType.Bag && targetSlot.slotType == SlotType.Weapon)
            {
                InventoryManager.Instance.SwapItem(7, index, targetIndex);
                inventoryUI.UpdateSlotHighLight(-1);
            }
            //8����->����
            if (slotType == SlotType.Weapon && targetSlot.slotType == SlotType.Bag)
            {
                InventoryManager.Instance.SwapItem(8, index, targetIndex);
                inventoryUI.UpdateSlotHighLight(-1);
            }
        }
        else
        {
            return;
        }
        

    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        
    }

    public void OnPointerExit(PointerEventData eventData)
    {
       
    }
}
