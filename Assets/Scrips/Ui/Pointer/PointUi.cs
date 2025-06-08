using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.Rendering;

public class PointUi : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField] private Image image;
    [SerializeField] private Color originColor = new Color(0, 0, 0, 150f / 255f);
    [SerializeField] private Color changedColor = new Color(70f / 255f, 0f, 255f / 255f, 150f / 255f);
    //����Ƽ�� ������ 0���� 1���� �׷��� �ִ밪�� 255�� ��������

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (image != null)
        {
            image.color = changedColor;
        }
    }
    public void OnPointerExit(PointerEventData eventData)
    {
        if (image != null)
        {
            image.color = originColor;
        }
    }
    
}
