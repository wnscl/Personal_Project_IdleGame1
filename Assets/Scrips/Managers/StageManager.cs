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

    public event Action<Stages> stageEvent_Start;
    public event Action stageEvent_End;

    public event Action<Stages> stageEvent;

    public int stageCount;
    public bool isStageRun = false;
    public bool isFirstStart = true;

    public GameObject player;
    public GameObject enemy;

    [Button]
    public void ChangeStage()
    {
        StartCoroutine(OnStageChange());
    }
    public IEnumerator OnStageChange()
    {
        isStageRun = false;
        stageEvent?.Invoke(nextStage);
        yield return new WaitForSeconds(3f);

        if (!isFirstStart)
        {
            stageCount++;
            nowStage = (Stages)(stageCount % 4);
            nextStage = (Stages)((stageCount + 1) % 4);
        }
    
        isStageRun = true;

        if (isFirstStart)
        {
            isFirstStart = false;
        }
    }


}
