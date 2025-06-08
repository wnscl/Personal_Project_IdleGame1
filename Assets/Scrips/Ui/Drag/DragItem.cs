using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using Unity.VisualScripting;

public class DragItem : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    public Transform Inventory;
    public Transform previousSlot;
    public RectTransform rect;
    // �� UI�� RectTransform���� �������� �ұ�? 
    // UI �ػ� ����	RectTransform�� Canvas Scaler � ���� �ڵ����� ������
    //�巡�� ��ġ ��ġ	eventData.position�� ��ũ�� ��ǥ �� �̰� ������ RectTransform�� position�� �� ��Ȯ
    //transform.position ����	�巡�� �� ��ġ Ʀ / �� ���� / Z�� ���� �߻� ����
    public Image icon;

    public ItemDataSo itemData;

    [SerializeField] private CanvasGroup itemGroup;

    private void Awake()
    {
        Inventory = FindObjectOfType<UiInventory>().gameObject.transform;
        rect = GetComponent<RectTransform>();
        itemGroup = GetComponent<CanvasGroup>();
        icon = GetComponent<Image>();

        icon.sprite = itemData.icon;
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
            previousSlot = transform.parent; //�ڽ��� ������ �ִ� ������ ���� 
            itemGroup.blocksRaycasts = false; //�ڽ��� eventsystem�� ��� ray�� ���� �ʰ���
            transform.SetParent(Inventory);
        }
        else
        {
            ItemSlot slot = transform.parent.GetComponent<ItemSlot>();
            //drop���� �Ŀ� enddrag�� ȣ�� ������ �巡�װ� ���� �� �ùٸ� ���Կ� �����ٸ� slot�� null�� �ƴ� ���̴�.
            if (slot == null) //�ùٸ� ��ġ�� ������ �ʾ������� ������ �������� ������.
            {
                transform.SetParent(previousSlot); 
                rect.position = previousSlot.position;
            }
            itemGroup.blocksRaycasts = true; //�ٽ� eventsystem�� ��� ray�� �ڽ��� ���� �� �ְ� ��
        }
    }
}