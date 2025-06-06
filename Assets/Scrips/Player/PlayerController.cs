using System.Collections;
using System.Collections.Generic;
using NaughtyAttributes;
using UnityEngine;

interface IStateControl
{
    public StateInfo GetStateControlInfo();
    public void ChangeState(int stateIndex);
}

public class StateInfo
{
    public BaseState[] states;
    public Animator anim;
    public StateInfoSo info;

    public float maxHp;
    public float currentHp;

    public int maxMp;
    public int currentMp;

    public float atk;
    public float atkSpeed;

    public int def;

    public void InfoSetting(BaseState[] newStates, Animator newAnim, StateInfoSo newInfo)
    {
        states = newStates;
        anim = newAnim;
        info = newInfo;
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

    public void TestPrint()
    {
        Debug.Log($"{maxHp} {maxMp} {atk} {atkSpeed} {def}");
    }
}

public class PlayerController : MonoBehaviour, IStateControl
{
    BaseFsm fsm;
    StateInfo playerInfo;

    private void Awake()
    {
        fsm = new BaseFsm();
        playerInfo = new StateInfo();

        BaseState[] newStates = { new Idle(GetStateControlInfo()) };
        StateInfoSo stateInfoSo = Resources.Load<StateInfoSo>("StateData/PlayerStateData");

        playerInfo.InfoSetting(newStates, GetComponent<Animator>(),stateInfoSo);

    }

    public StateInfo GetStateControlInfo()
    {
        return playerInfo;
    }
    public void ChangeState(int stateIndex)
    {
        fsm.isStateRun = false; //상태종료 안내
        fsm.ChangeState(playerInfo.states[stateIndex]); //상태전환 명령전달
    }
    [Button]
    private void Test()
    {
        playerInfo.TestPrint();
    }


}
