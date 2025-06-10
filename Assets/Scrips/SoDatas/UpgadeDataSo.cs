using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum UpgadeNum
{
    Low,
    Middle,
    High
}

[CreateAssetMenu(fileName = "Upgade Data So", menuName = "Scriptable Object/Upgade Data", order = 2)]
public class UpgadeDataSo : ScriptableObject
{
    [SerializeField] private UpgadeNum upgadeType;
    [SerializeField] private int cost;


    public UpgadeNum UpgadeType => upgadeType;
    public int Cost => cost;
}
