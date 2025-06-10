using TMPro;
using UnityEngine.UI;
using UnityEngine;
using UnityEngine.EventSystems;
using System.Collections;


public class UpgadeButton : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    //public GameObject costPanel;
    //public TextMeshProUGUI costText;
    //public UpgadeDataSo upgadeData;


    //public void ViewCostPanel(bool open)
    //{
    //    costText.text = $"Cost : {upgadeData.Cost}";
    //    costPanel.SetActive(open);
    //}
    //public void OnClickUpgade(out UpgadeDataSo upgadeData)
    //{
    //    upgadeData = this.upgadeData;
    //}

    public void OnPointerEnter(PointerEventData eventData)
    {
        Debug.Log(eventData);
    }

    public void OnPointerExit(PointerEventData eventData)
    {

    }
}
