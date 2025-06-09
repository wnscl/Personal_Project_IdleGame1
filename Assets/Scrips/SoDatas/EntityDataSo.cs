using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Entity Data So", menuName = "Scriptable Object/EntityData", order = 0) ]
public class EntityData : ScriptableObject
{
    [SerializeField] private float maxHp;
    public float MaxHp {  get { return maxHp; } }
    [SerializeField] private int maxMp;
    public int MaxMp { get { return maxMp; } }


    [SerializeField] private float atk;
    public float Atk { get { return atk; } }
    [SerializeField] private float attackSpeed;
    public float AttackSpeed { get { return attackSpeed; } }

    [SerializeField] private float def;
    public float Def { get { return def; } }
    //가변 데이터


    //불변 데이터
    [SerializeField] private int skillCount;
    public int SkillCount { get { return skillCount; } }
    [SerializeField] private bool canRevive;
    public bool CanRevive { get { return canRevive; } }
    [SerializeField] private EntityType entityType;
    public EntityType EntityType { get { return entityType; } }


}
