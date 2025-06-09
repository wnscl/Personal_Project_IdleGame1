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
    // �� UI�� RectTransform���� �������� �ұ�? 
    // UI �ػ� ����	RectTransform�� Canvas Scaler � ���� �ڵ����� ������
    //�巡�� ��ġ ��ġ	eventData.position�� ��ũ�� ��ǥ �� �̰� ������ RectTransform�� position�� �� ��Ȯ
    //transform.position ����	�巡�� �� ��ġ Ʀ / �� ���� / Z�� ���� �߻� ����
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
        rect.position = eventData.position; //�ڽ��� rect�������� �巡�� �� ��� ����
    }
    public void OnEndDrag(PointerEventData eventData)
    {
        //drop���� �Ŀ� ȣ���
        InitItem(false);
    }

    private void InitItem(bool isDragStart)
    {
        if (isDragStart)
        {
            canGetIn = true;
            previousSlot = transform.parent; //�ڽ��� ������ �ִ� ������ ���� 
            itemGroup.blocksRaycasts = false; //�ڽ��� eventsystem�� ��� ray�� ���� �ʰ���
            transform.SetParent(Inventory);
            UpdateBox(false);
        }
        else
        {
            ItemSlot slot = transform.parent.GetComponent<ItemSlot>();
            //drop���� �Ŀ� enddrag�� ȣ�� ������ �巡�װ� ���� �� �ùٸ� ���Կ� �����ٸ� slot�� null�� �ƴ� ���̴�.
            if (slot == null || !canGetIn) //�ùٸ� ��ġ�� ������ �ʾ������� ������ �������� ������.
            {
                transform.SetParent(previousSlot); 
                rect.position = previousSlot.position;
            }
            itemGroup.blocksRaycasts = true; //�ٽ� eventsystem�� ��� ray�� �ڽ��� ���� �� �ְ� ��
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