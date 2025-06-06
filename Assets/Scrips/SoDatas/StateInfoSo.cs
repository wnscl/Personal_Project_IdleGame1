using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "State Info So", menuName = "Scriptable Object/State Info Data", order = 0) ]
public class StateInfoSo : ScriptableObject
{
    [SerializeField] private float maxHp;
    public float MaxHp {  get { return maxHp; } }
    [SerializeField] private int maxMp;
    public int MaxMp { get { return maxMp; } }


    [SerializeField] private float atk;
    public float Atk { get { return atk; } }
    [SerializeField] private float attackSpeed;
    public float AttackSpeed { get { return attackSpeed; } }

    [SerializeField] private int def;
    public int Def { get { return def; } }
    //가변 데이터


    //불변 데이터
    [SerializeField] private int skillCount;
    public int SkillCount { get { return skillCount; } }
    [SerializeField] private bool canRevive;
    public bool CanRevive { get { return canRevive; } }


}
