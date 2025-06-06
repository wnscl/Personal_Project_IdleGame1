using System.Collections;
using System.Collections.Generic;
using NaughtyAttributes;
using UnityEngine;

public interface IStateControl
{
    public StateInfo GetStateControlInfo();
    public void ChangeState(int stateIndex);
    public void UpdateState();
}
public enum WhoAmI
{
    Player,
    Monster,
    Boss
}

public class StateInfo
{
    public BaseState[] states;
    public Animator anim;
    public StateInfoSo info;

    public IStateControl owner;
    public WhoAmI who;

    public float maxHp;
    public float currentHp;

    public int maxMp;
    public int currentMp;

    public float atk;
    public float atkSpeed;

    public int def;

    public void InfoSetting(BaseState[] newStates, Animator newAnim, StateInfoSo newInfo, WhoAmI iam, IStateControl owner)
    {
        states = newStates;
        anim = newAnim;
        info = newInfo;
        who = iam;
        this.owner = owner;

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

        BaseState[] newStates = { new Idle(playerInfo), new Move(playerInfo) };
        StateInfoSo stateInfoSo = Resources.Load<StateInfoSo>("StateData/PlayerStateData");

        playerInfo.InfoSetting
            (newStates, 
            GetComponentInChildren<Animator>(), 
            stateInfoSo, 
            WhoAmI.Player,
            this);

    }

    public StateInfo GetStateControlInfo() //�ܺο��� �÷��̾��� �𵨿� �����ϱ� ���� �޼���
    {
        return playerInfo;
    }
    public void ChangeState(int stateIndex)
    {
        if (stateIndex > (playerInfo.states.Length - 1))
        {
            Debug.Log("�������� �ʴ� ���¿� ���� : states�迭 �ε�������");
            return;
        }

        fsm.ChangeState(playerInfo.states[stateIndex]); //������ȯ �������
    }
    public void UpdateState()
    {
        fsm.UpdateStata(); //���¿� ���� �ൿ����
    }
    [Button]
    private void Test()
    {
        playerInfo.TestPrint();
    }

}
