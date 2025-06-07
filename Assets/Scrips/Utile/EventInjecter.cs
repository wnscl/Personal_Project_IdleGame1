using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventInjecter : MonoBehaviour
{
    public PlayerStatUi playerStatUi;
    public GameOptionUi gameOptionUi;

    public LobbyCamera lobbyCam;
    public BattleCamera battleCam;

    private void Start()
    {
        //필요한 메서드를 원하는 순서대로 주입 후 삭제
        StartCoroutine(InjectStart());
    }

    private IEnumerator InjectStart()
    {
        GameCondition.Instance.conditionEvent += gameOptionUi.ChangeOptionUi;
        GameCondition.Instance.conditionEvent += lobbyCam.ChangeLobbyScreen;

        yield return null;

        Destroy(this.gameObject);
        yield break;
    }

}
