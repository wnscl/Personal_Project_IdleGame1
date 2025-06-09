using System.Collections;
using System.Collections.Generic;
using DG.Tweening.Core;
using NaughtyAttributes;
using UnityEngine;

public interface IStateControl
{
    public EntityInfo GetEntityInfo();
    public void ChangeState(int stateIndex);
    public void GetDamage(float dmg);

}
public enum EntityType
{
    Player,
    Monster
}
public enum EntityState
{
    Idle,
    Move,
    Attack,
    Dead
}

public class EntityInfo
{
    public BaseState[] states;
    public Animator anim;
    public EntityData info;

    public IStateControl control;
    public EntityType entityType;

    public float maxHp;
    public float currentHp;

    public int maxMp;
    public int currentMp;

    public float atk;
    public float atkSpeed;

    public float def;

    public Coroutine coroutine;

    public void InfoSetting(BaseState[] newStates, Animator newAnim, EntityData entityData, IStateControl control)
    {
        this.control = control;

        states = newStates;
        anim = newAnim;
        info = entityData;
        entityType = entityData.EntityType;

        StatSetting();
    }
    private void StatSetting()
    {
        maxHp = info.MaxHp;
        currentHp = maxHp;
        maxMp = info.MaxMp;
        currentMp = maxMp;
        atk = info.Atk;
        atkSpeed = info.AttackSpeed;
        def = info.Def;
    }
    public void UpdateEntityStat(bool add, MonsterPower addStats)
    {
        if (add)
        {
            maxHp += addStats.addHp;
            atk += addStats.addAtk;
            atkSpeed += addStats.addAs;
            def += addStats.addDef;
        }
        else
        {
            maxHp -= addStats.addHp;
            atk -= addStats.addAtk;
            atkSpeed -= addStats.addAs;
            def -= addStats.addDef;
        }
    }

    public void TestPrint()
    {
        Debug.Log($"{maxHp} {maxMp} {atk} {atkSpeed} {def}");
    }
}

public class EntityController : MonoBehaviour, IStateControl
{
    BaseFsm fsm;
    EntityInfo entityInfo;
    [SerializeField] private EntityData entityData;

    private void Awake()
    {
        fsm = new BaseFsm();
        entityInfo = new EntityInfo();

        BaseState[] newStates = { new Idle(entityInfo), new Move(entityInfo), new Attack(entityInfo), new Dead(entityInfo) };

        entityInfo.InfoSetting
            (newStates, 
            GetComponentInChildren<Animator>(), 
            entityData, 
            this);

    }
    private void Start()
    {
        fsm.InitFsm(entityInfo.states[0]);
    }

    public EntityInfo GetEntityInfo() //외부에서 플레이어의 모델에 접근하기 위한 메서드
    {
        return entityInfo;
    }
    public void OnStageMove(Stages nextStage)
    {
        ChangeState((int)EntityState.Move);
    }
    public void ChangeState(int stateIndex)
    {
        if (stateIndex > (entityInfo.states.Length - 1))
        {
            Debug.Log("존재하지 않는 상태에 접근 : states배열 인덱스문제");
            return;
        }

        fsm.ChangeState(entityInfo.states[stateIndex]); //상태전환 명령전달
        fsm.UpdateStata();
    }
    public void GetDamage(float dmg)
    {
        entityInfo.currentHp = Mathf.Clamp((entityInfo.currentHp - dmg), 0, entityInfo.maxHp);
        Debug.Log(entityInfo.currentHp);

        if (entityInfo.currentHp <= 0)
        {
            ChangeState((int)EntityState.Dead);
            StageManager.Instance.ChangeStage();
        }
    }
    [Button]
    private void Test()
    {
        entityInfo.TestPrint();
        //ChangeState(1);
        //UpdateState();
        ChangeState((int)EntityState.Attack);
        //UpdateState();
    }


}
