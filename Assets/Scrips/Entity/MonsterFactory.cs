using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public struct MonsterPower
{
    public float addHp;
    public float addAtk;
    public float addAs;
    public float addDef;
}

public class MonsterFactory : MonoBehaviour
{
    [SerializeField] private GameObject[] monsterPrefabs;
    //public List<GameObject> monsterList;

    private MonsterPower monsterPower;


    private void Awake()
    {
        //monsterList = new List<GameObject>();
        monsterPower = new MonsterPower();
        monsterPower.addHp = 0;
        monsterPower.addAtk = 0;
        monsterPower.addAs = 0;
        monsterPower.addDef = 0;

        //foreach (GameObject monster in monsterPrefabs)
        //{
        //    monsterList.Add(monster);
        //}
    }
    public void CreateMonster(Stages stage)
    {
        if (StageManager.Instance.isFirstStart)
        {
            return;
        }

        int stageCount = StageManager.Instance.stageCount;
        if (stageCount / 4 == 0)
        {
            monsterPower.addHp += stageCount * 50f;
            monsterPower.addAtk += stageCount * 10f;
            monsterPower.addAs = Mathf.Clamp(monsterPower.addAs + (stageCount * 0.05f), 0, 3f);
            monsterPower.addDef += stageCount * 5f;
        }

        int monsterNum = UnityEngine.Random.Range(0, monsterPrefabs.Length);

        GameObject monsterPrefab = monsterPrefabs[monsterNum];

        GameObject nowStage = StageManager.Instance.stages[(int)StageManager.Instance.NowStage];
        GameObject nextStage = StageManager.Instance.stages[(int)StageManager.Instance.NextStage];

        GameObject newMonster = Instantiate(
            monsterPrefab,
            nextStage.transform.position,
            CalculateHelper.GetRotation(monsterPrefab, CalculateHelper.GetDirection(nextStage, nowStage, Axis.Y)),
            this.transform);

        IStateControl monsterControl = newMonster.GetComponent<IStateControl>();
        EntityInfo monsterInfo = monsterControl.GetEntityInfo();

        monsterInfo.UpdateEntityStat(true, monsterPower);

    }


}
