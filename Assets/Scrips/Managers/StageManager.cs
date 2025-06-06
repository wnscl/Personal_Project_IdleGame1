using System.Collections;
using System.Collections.Generic;
using NaughtyAttributes;
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
    }

    public GameObject player;
    public PlayerController playerController;

    [SerializeField] private Stages nowStage;
    public Stages NowStage { get { return nowStage; } }
    [SerializeField] private Stages nextStage;
    public Stages NextStage { get { return nextStage; } }

    public GameObject[] stages;


    [Button]
    public void ddddd()
    {

    }

}
