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
    [SerializeField] private Image WeaponTips;

    public Image slotHighLight;

    [Header("��������")]
    public SlotType slotType;

    [Header("������Ϣ")]
    public bool isSelected;
    public int index;

    [Header("��Ʒ��Ϣ")]
    public ItemDetails itemDetails;
    public int itemAmount;
    public bool CanEquip;
    public bool CanWeapon;
    public int MaxUse;
    //public int CurUse;

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
        WeaponTips.enabled = false;
        CanEquip = false;
        CanWeapon = false;
        MaxUse = 0;
        //CurUse = 0;
    }

    //���¸�����Ϣ
    public void UpdateSlot(ItemDetails item,int amount)
    {
        itemDetails = item;
        slotImage.enabled = true;
        slotImage.sprite = itemDetails.ItemIcon;
        amountText.text = amount.ToString();
        button.interactable = true;

        //װ���������
        CanEquip = item.canEquip;
        MaxUse = item.MaxEquipUse;

        CanWeapon = item.canWeapon;
        //�����װ�����ӣ�����itemAmount���洢����ʣ��ʹ�ô���
        itemAmount = amount;

        if (CanEquip)
        {
            EquipTips.enabled = true;
            WeaponTips.enabled = false;
            //CurUse = MaxUse;
        }
        else if (CanWeapon)
        {
            WeaponTips.enabled = true;
            EquipTips.enabled = false;
        }
        else
        {
            EquipTips.enabled = false;
            WeaponTips.enabled = false;
            //CurUse = -1;
        }
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (itemDetails.ItemID == 0)
            return;
        isSelected = !isSelected;
        inventoryUI.UpdateSlotHighLight(slotType,index);
        PlaySlotEffect(slotType);
        if (isSelected)
        {
            if (slotType == SlotType.Bag || slotType == SlotType.Shop)
            {
                inventoryUI.ItemDetailsPanelUpdate(itemDetails,Input.mousePosition);
            }
        }
        //Debug.Log(inventoryUI.ItemDetailPanel.transform.position);
        //Debug.Log(Input.mousePosition);
    }

    public void PlaySlotEffect(SlotType type)
    {
        if (type == SlotType.Bag || slotType == SlotType.Shop)
        {
            EventHandler.CallInitSoundEffect(AudioManager.Instance.GetSoundDetailsfromName(SoundName.SlotNor));
        }
        else
        {
            EventHandler.CallInitSoundEffect(AudioManager.Instance.GetSoundDetailsfromName(SoundName.SlotFig));
        }
    }


    public void OnBeginDrag(PointerEventData eventData)
    {
        if(itemAmount != 0)
        {
            inventoryUI.DragItem.enabled = true;
            inventoryUI.DragItem.sprite = slotImage.sprite;

            //inventoryUI.DragItem.SetNativeSize();
            isSelected = true;
            inventoryUI.UpdateSlotHighLight(slotType, index);

        }
    }

    public void OnDrag(PointerEventData eventData)
    {
        inventoryUI.DragItem.transform.position = Input.mousePosition;
        //Debug.Log(eventData.pointerCurrentRaycast.gameObject.name);
        if (eventData.pointerCurrentRaycast.gameObject != null)
        {
            if (eventData.pointerCurrentRaycast.gameObject.GetComponent<SlotUI>() != null)
            {
                var tarslot = eventData.pointerCurrentRaycast.gameObject.GetComponent<SlotUI>();
                //������װ����
                if (tarslot.slotType == SlotType.Equipment && tarslot.itemDetails.ItemID != 0 && slotType == SlotType.Bag)
                {
                    if (tarslot.MaxUse == -1 || tarslot.itemAmount == tarslot.MaxUse)
                    {
                        //ǹ�ȿ�������ʹ�õ���Ʒ ����ȫ����Ʒ
                        string mtext = "װ�������������Ƚ�װ��������Ʒ������������";
                        MainCanvasUITip.Instance.SetActiveUITip(mtext);
                    }
                    else
                    {
                        //ʹ�ù�����Ʒ
                        string mtext = "���滻װ���������޷����գ����滻��ֱ������";
                        MainCanvasUITip.Instance.SetActiveUITip(mtext);
                    }
                }
                //װ����������
                else if(tarslot.slotType == SlotType.Bag && tarslot.itemDetails.ItemID == 0 && slotType == SlotType.Equipment)
                {
                    if(MaxUse != -1 && MaxUse != itemAmount)
                    {
                        string mtext = "װ���������޷����գ���������װ��ֱ���滻";
                        MainCanvasUITip.Instance.SetActiveUITip(mtext);
                    }
                }
            }
            else
            {
                MainCanvasUITip.Instance.SetUnActiveUITip();
            }

        }
        else
        {
            MainCanvasUITip.Instance.SetUnActiveUITip();
        }
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        //�ر��ƶ���ʾ��
        MainCanvasUITip.Instance.SetUnActiveUITip();

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
                inventoryUI.UpdateSlotHighLight(slotType, index);
                inventoryUI.UpdateSlotHighLight(targetSlot.slotType, index);
                PlaySlotEffect(targetSlot.slotType);
            }
            //2����->�̵�
            if(slotType == SlotType.Bag && targetSlot.slotType == SlotType.Shop)
            {
                InventoryManager.Instance.SwapItem(2, index, targetIndex);
                inventoryUI.UpdateSlotHighLight(slotType, index);
                inventoryUI.UpdateSlotHighLight(targetSlot.slotType, index);
                PlaySlotEffect(targetSlot.slotType);
            }
            //3�̵�->����
            if(slotType == SlotType.Shop&&targetSlot.slotType == SlotType.Bag)
            {
                InventoryManager.Instance.SwapItem(3, index, targetIndex);
                inventoryUI.UpdateSlotHighLight(slotType, index);
                inventoryUI.UpdateSlotHighLight(targetSlot.slotType, index);
                PlaySlotEffect(targetSlot.slotType);
            }
            //4�̵�->�̵�
            if(slotType == SlotType.Shop && targetSlot.slotType == SlotType.Shop)
            {
                InventoryManager.Instance.SwapItem(4, index, targetIndex);
                inventoryUI.UpdateSlotHighLight(slotType, index);
                inventoryUI.UpdateSlotHighLight(targetSlot.slotType, index);
                PlaySlotEffect(targetSlot.slotType);
            }
            //5����->װ��
            if(slotType == SlotType.Bag && targetSlot.slotType == SlotType.Equipment)
            {
                InventoryManager.Instance.SwapItem(5, index, targetIndex);
                inventoryUI.UpdateSlotHighLight(slotType, index);
                inventoryUI.UpdateSlotHighLight(targetSlot.slotType, index);
                PlaySlotEffect(targetSlot.slotType);
                targetSlot.gameObject.GetComponent<EquipDurability>().UpdateDurability();
            }
            //6װ��->����
            if(slotType == SlotType.Equipment && targetSlot.slotType == SlotType.Bag)
            {
                InventoryManager.Instance.SwapItem(6, index, targetIndex);
                inventoryUI.UpdateSlotHighLight(slotType, index);
                inventoryUI.UpdateSlotHighLight(targetSlot.slotType, index);
                PlaySlotEffect(targetSlot.slotType);
                this.GetComponent<EquipDurability>().UpdateDurability();
            }
            //7����->����
            if (slotType == SlotType.Bag && targetSlot.slotType == SlotType.Weapon)
            {
                InventoryManager.Instance.SwapItem(7, index, targetIndex);
                inventoryUI.UpdateSlotHighLight(slotType, index);
                inventoryUI.UpdateSlotHighLight(targetSlot.slotType, index);
                PlaySlotEffect(targetSlot.slotType);
            }
            //8����->����
            if (slotType == SlotType.Weapon && targetSlot.slotType == SlotType.Bag)
            {
                InventoryManager.Instance.SwapItem(8, index, targetIndex);
                inventoryUI.UpdateSlotHighLight(slotType, index);
                inventoryUI.UpdateSlotHighLight(targetSlot.slotType, index);
                PlaySlotEffect(targetSlot.slotType);
            }
        }
        else
        {
            return;
        }
        

    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (isSelected)
        {
            if(slotType == SlotType.Bag || slotType == SlotType.Shop)
            {
                inventoryUI.ItemDetailsPanelUpdate(itemDetails,Input.mousePosition);
            }
        }
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        inventoryUI.ItemDetailsPanelClose();
    }
}
