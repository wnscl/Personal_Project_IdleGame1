using System;
using System.Collections;
using System.Collections.Generic;
using NaughtyAttributes;
using Unity.VisualScripting;
using UnityEngine;


public enum Stages
{
    stage1,
    stage2,
    stage3,
    stage4
}
public class StageManager : MonoBehaviour
{
    private static StageManager instance;
    public static StageManager Instance
    {
        get { return instance; }
    }
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(this.gameObject);
        }

        nowStage = Stages.stage1;
        nextStage = Stages.stage2;
    }

    public EntityController playerController;

    [SerializeField] private Stages nowStage;
    public Stages NowStage { get { return nowStage; } }
    [SerializeField] private Stages nextStage;
    public Stages NextStage { get { return nextStage; } }

    public GameObject[] stages;

    public event Action<Stages> stageEvent;
    public event Action<Stages> stageEvent_End;

    public int stageCount;
    public bool isStageRun = true;

    public GameObject player;
    public GameObject enemy;

    public MonsterFactory mobFactory;

    public void ChangeStage(IStateControl control)
    {
        EntityInfo entityInfo = control.GetEntityInfo();
        //여기에 종료로직
        if (entityInfo.entityType == EntityType.Player)
        {
            StartCoroutine(OnStageReset(control));
            return;
        }
        StartCoroutine(OnStageChange());
    }
    public IEnumerator OnStageChange()
    {
        Destroy(enemy);
        isStageRun = false;
        stageEvent?.Invoke(nextStage);
        yield return new WaitForSeconds(3f);

        stageCount++;
        nowStage = (Stages)(stageCount % 4);
        nextStage = (Stages)((stageCount + 1) % 4);

        isStageRun = true;
    }
    public IEnumerator OnStageReset(IStateControl control)
    {
        control.ChangeState((int)EntityState.Dead);
        isStageRun = false;
        stageCount = 0;
        nowStage = Stages.stage1;
        nextStage = Stages.stage2;
        Destroy(enemy);
        stageEvent_End?.Invoke(nowStage);
        yield return new WaitForSeconds(2.5f);
        isStageRun = true;
        EntityInfo entityInfo = control.GetEntityInfo();
        entityInfo.currentHp = entityInfo.maxHp;
        yield return new WaitForSeconds(2.5f);
        mobFactory.ResetMob();
    }

    public void GiveGoldToPlayer()
    {

    }



}
