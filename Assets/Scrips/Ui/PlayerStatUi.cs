using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

enum UiStat
{
    Hp,
    Mp
}

public class PlayerStatUi : MonoBehaviour
{
    [SerializeField] private GameObject[] valueBarGroup;

    [SerializeField] private TextMeshProUGUI[] statTextGroup;

    private void ChangeValueBar(UiStat choice)
    {
        Image barImage = valueBarGroup[(int)choice].GetComponentInChildren<Image>();

        
    }
}
