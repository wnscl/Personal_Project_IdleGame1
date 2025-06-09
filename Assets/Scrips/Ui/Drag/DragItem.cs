using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using Unity.VisualScripting;
using TMPro;

public interface IDragItem
{
    ItemType SendMyType();
    void CheckType(ItemType slotType);
    void UpdateBox(bool open);
}

public class DragItem : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler, IDragItem
{
    private Transform Inventory;
    private Transform previousSlot;
    private RectTransform rect;
    // 왜 UI는 RectTransform으로 움직여야 할까? 
    // UI 해상도 대응	RectTransform은 Canvas Scaler 등에 따라 자동으로 보정됨
    //드래그 위치 일치	eventData.position은 스크린 좌표 → 이걸 쓰려면 RectTransform의 position이 더 정확
    //transform.position 쓰면	드래그 시 위치 튐 / 안 맞음 / Z값 문제 발생 가능
    [SerializeField] private Image icon;
    [SerializeField] private GameObject box;
    [SerializeField] private TextMeshProUGUI boxText;
    [SerializeField] private ItemDataSo itemData;
    [SerializeField] private CanvasGroup itemGroup;

    private bool canGetIn = true;

    private void Awake()
    {
        Inventory = FindObjectOfType<UiInventory>().gameObject.transform;
        rect = GetComponent<RectTransform>();
        itemGroup = GetComponent<CanvasGroup>();
        icon = GetComponent<Image>();

        icon.sprite = itemData.icon;
        box.SetActive(false);
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        InitItem(true);
    }
    public void OnDrag(PointerEventData eventData)
    {
        rect.position = eventData.position; //자신의 rect포지션을 드래그 중 계속 갱신
    }
    public void OnEndDrag(PointerEventData eventData)
    {
        //drop보다 후에 호출됨
        InitItem(false);
    }

    private void InitItem(bool isDragStart)
    {
        if (isDragStart)
        {
            canGetIn = true;
            previousSlot = transform.parent; //자신을 가지고 있던 슬롯을 저장 
            itemGroup.blocksRaycasts = false; //자신이 eventsystem이 쏘는 ray를 막지 않게함
            transform.SetParent(Inventory);
            UpdateBox(false);
        }
        else
        {
            ItemSlot slot = transform.parent.GetComponent<ItemSlot>();
            //drop보다 후에 enddrag가 호출 됨으로 드래그가 끝난 후 올바른 슬롯에 놓였다면 slot은 null이 아닐 것이다.
            if (slot == null || !canGetIn) //올바른 위치에 놓이지 않았음으로 원래의 슬롯으로 돌린다.
            {
                transform.SetParent(previousSlot); 
                rect.position = previousSlot.position;
            }
            itemGroup.blocksRaycasts = true; //다시 eventsystem이 쏘는 ray를 자신이 맞을 수 있게 함
        }
    }
    public ItemType SendMyType()
    {
        return itemData.itemType;
    }
    public void CheckType(ItemType slotType)
    {
        if (slotType == itemData.itemType || slotType == ItemType.Etc) return;

        canGetIn = false;
    }
    public void UpdateBox(bool open)
    {
        switch (itemData.itemType)
        {
            case ItemType.Etc:
                boxText.text = itemData.itemCount;
                break;

            default:
                boxText.text = "E";
                break;
        }
        box.SetActive(open);
    }

}