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

    public PlayerController player;

    private void Start()
    {
        //필요한 메서드를 원하는 순서대로 주입 후 삭제
        StartCoroutine(InjectStart());
    }

    private IEnumerator InjectStart()
    {
        GameCondition.Instance.conditionEvent += gameOptionUi.ChangeOptionUi;
        GameCondition.Instance.conditionEvent += lobbyCam.ChangeLobbyScreen;

        StageManager.Instance.stageEvent_Start += battleCam.ChangeBattleScreen;
        StageManager.Instance.stageEvent_Start += player.OnStageReady;

        StageManager.Instance.stageEvent_End += player.OnStageDone;

        yield return null;

        Destroy(this.gameObject);
        yield break;
    }

}
