using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventInjecter : MonoBehaviour
//이벤트를 순차적으로 주입만 시키는 객체
//이벤트 실행 시 메서드의 순서와 추적이 편할 것 같아 만들었습니다
{
    public PlayerStatUi playerStatUi;
    public GameOptionUi gameOptionUi;

    public LobbyCamera lobbyCam;
    public BattleCamera battleCam;

    public EntityController player;

    public StatController statController;

    public MonsterFactory monsterFactory;

    public ResourceUi resourceUi;

    private void Start()
    {
        //필요한 메서드를 원하는 순서대로 주입 후 삭제
        StartCoroutine(InjectStart());
        statController.InvokeStatUpdate(player);
    }

    private IEnumerator InjectStart()
    {
        StageManager sm = StageManager.Instance;

        GameCondition.Instance.conditionEvent += gameOptionUi.ChangeOptionUi;
        GameCondition.Instance.conditionEvent += lobbyCam.ChangeLobbyScreen;

        sm.stageEvent += battleCam.ChangeBattleScreen;
        sm.stageEvent += player.OnStageMove;
        sm.stageEvent += resourceUi.UpdateResurce;
        sm.stageEvent += monsterFactory.CreateMonster;

        sm.stageEvent_End += battleCam.ChangeBattleScreen;
        sm.stageEvent_End += resourceUi.InitUi;

        //StageManager.Instance. 스테이지를 돌리는 중 이벤트 필요

        statController.statChanged += playerStatUi.UpdateValueBar;

        yield return null;

        Destroy(this.gameObject);
        yield break;
    }

}
