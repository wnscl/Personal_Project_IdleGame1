using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

public class StoreUi : MonoBehaviour
{
    [SerializeField] private List<Rect> upgadeGroups = new List<Rect>();
    [SerializeField] private RectTransform[] interactiveAreas;

    [SerializeField] private GameObject costPanel;
    public TextMeshProUGUI costText;

    public bool isActive;
    public bool isMouseIn = false;


    //public void OnPointerEnter(PointerEventData eventData)
    //{
    //    Debug.Log(eventData);
    //}
}
//Vector2 mousePos = Input.mousePosition;
//foreach (Rect group in upgadeGroups)
//{
//    if (group.Contains(mousePos))
//    {
//        isMouseIn = true;
//        break;
//    }
//}