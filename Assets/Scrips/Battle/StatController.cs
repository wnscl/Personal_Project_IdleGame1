using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatController : MonoBehaviour
{
    public event Action<IStateControl> statChanged;

    public void InvokeStatUpdate(IStateControl control)
    {
        statChanged.Invoke(control);
    }
    public void GetDamage(IStateControl attacker, IStateControl defender)
    {
        EntityInfo info = attacker.GetEntityInfo();
        defender.GetDamage(info.atk);
    }
}
