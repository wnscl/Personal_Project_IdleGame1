using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

enum UiStat
{
    Hp,
    Mp
}

public class PlayerStatUi : MonoBehaviour
{
    [SerializeField] private GameObject[] valueBars;

    private void ChangeValueBar(UiStat choice)
    {
        Image barImage = valueBars[(int)choice].GetComponentInChildren<Image>();

        
    }
}
