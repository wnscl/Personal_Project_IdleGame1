using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ItemType
{
    Weapon,
    Shield,
    Armour,
    Acc
}

[CreateAssetMenu(fileName = "Item Data So", menuName = "Scriptable Object/Item Data", order = 1)]
public class ItemDataSo : ScriptableObject
{
    public ItemType itemType;

    public string itemName;
    public string itemDescription;
    public Sprite icon;

}