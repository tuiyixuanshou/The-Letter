using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class WarPaperControl : MonoBehaviour,IPointerClickHandler
{

    public GameObject WarPaperPanel;
    public Animator WarPaperAnimator;
    public Animator WarBookAnimator;
    public Button WarButton;
    public void OnPointerClick(PointerEventData eventData)
    {
        if (WarPaperPanel.activeInHierarchy)
        {
            Debug.Log(eventData.pointerCurrentRaycast.gameObject.name);
            if(eventData.pointerCurrentRaycast.gameObject.name == "WarPaperClosePanel")
            {
                //���Ž�������
                WarPaperAnimator.SetBool("WarOut", true);
                WarBookAnimator.SetTrigger("BookOut");
                //WarPaperPanel.gameObject.SetActive(false);
                WarButton.GetComponent<MainCanvasWarPaperButton>().OnWarParperClosed();
                WarButton.interactable = true;
                WarButton.GetComponent<Image>().raycastTarget = true;
                EventHandler.CallInitSoundEffect(AudioManager.Instance.GetSoundDetailsfromName(SoundName.PaperOut));
            }
        }


    }

}
