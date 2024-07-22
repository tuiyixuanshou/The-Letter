using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NeedUISubmitButton : MonoBehaviour
{
    public GameObject SubmitCheckPanel;
    private CustomerController customerController => GetComponentInParent<CustomerController>();

    private void Start()
    {
        Debug.Log(this.transform.root.name);
        SubmitCheckPanel = this.transform.root.Find("ShopPanelBackGround/SubmitCheckPanel").gameObject;
    }
    public void NeedUIButtonPressed()
    {

        if (customerController.totalPrice > 0)
        {
            EventHandler.CallInitSoundEffect(AudioManager.Instance.GetSoundDetailsfromName(SoundName.ButtonClick));
            SubmitCheckPanel.GetComponent<CanvasGroup>().alpha = 1;
            SubmitCheckPanel.GetComponent<CanvasGroup>().blocksRaycasts = true;
            TradeAndSubmit.Instance.UpdateEmptySubmitItem();
            foreach (var i in transform.parent.GetComponentsInChildren<NeedItemController>())
            {
                Debug.Log("do submit" + i.NeedItemIndex);

                TradeAndSubmit.Instance.LoadSubmitItem(i.NeedItemIndex, i.ItemID, i.PossessAmountInt, i.GiveAmount);
            }

            TradeAndSubmit.Instance.totalPrice = this.transform.parent.parent.GetComponent<CustomerController>().totalPrice;
        }
      
    }
}
