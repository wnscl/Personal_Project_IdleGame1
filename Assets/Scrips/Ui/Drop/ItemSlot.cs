using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using Unity.VisualScripting;

public interface ISlot
{

}

public class ItemSlot : MonoBehaviour, IPointerEnterHandler, IDropHandler, IPointerExitHandler
{
    [SerializeField] private Image image;
    private RectTransform rect;
    [SerializeField] private Color previousColor;
    [SerializeField] private Color mouseOnColor = new Color(160f / 255f, 210f / 255f, 100f / 255f, 220f / 255f);

    [SerializeField] private ItemType slotType;
    [SerializeField] private bool isEquip;

    private void Awake()
    {
        previousColor = image.color;
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
        if (eventData.pointerDrag == null) return;

        DragItemSet(eventData.pointerDrag.GetComponent<IDragItem>());

        eventData.pointerDrag.transform.SetParent(transform);
        eventData.pointerDrag.GetComponent<RectTransform>().position = rect.position;

    }
    private void DragItemSet(IDragItem dragItem)
    {
        dragItem.CheckType(slotType);
        if (slotType != ItemType.Etc)
        {
            dragItem.UpdateBox(true);
        }
        else
        {
            dragItem.UpdateBox(false);
        }
    }
}
