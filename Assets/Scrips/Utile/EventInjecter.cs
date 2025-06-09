using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventInjecter : MonoBehaviour
//�̺�Ʈ�� ���������� ���Ը� ��Ű�� ��ü
//�̺�Ʈ ���� �� �޼����� ������ ������ ���� �� ���� ��������ϴ�
{
    public PlayerStatUi playerStatUi;
    public GameOptionUi gameOptionUi;

    public LobbyCamera lobbyCam;
    public BattleCamera battleCam;

    public PlayerController player;

    private void Start()
    {
        //�ʿ��� �޼��带 ���ϴ� ������� ���� �� ����
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
