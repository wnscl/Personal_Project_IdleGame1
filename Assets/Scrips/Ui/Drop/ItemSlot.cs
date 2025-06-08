using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using Unity.VisualScripting;

public class ItemSlot : MonoBehaviour, IPointerEnterHandler, IDropHandler, IPointerExitHandler
{
    public Image image;
    public RectTransform rect;
    public Color previousColor = new Color(0, 0, 0, 220f / 255f);
    public Color mouseOnColor = new Color(160f / 255f, 210f / 255f, 100f / 255f, 220f / 255f);

    private void Awake()
    {
        rect = GetComponent<RectTransform>();
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (image != null)
        {
            image.color = mouseOnColor;
        }
    }
    public void OnPointerExit(PointerEventData eventData)
    {
        if (image != null)
        {
            image.color = previousColor;
        }
    }
    public void OnDrop(PointerEventData eventData) //EndDrag보다 먼저 호출됨
    {
        if (eventData.pointerDrag != null)
        {
            eventData.pointerDrag.transform.SetParent(transform);
            eventData.pointerDrag.GetComponent<RectTransform>().position = rect.position;
        }

    }
}
