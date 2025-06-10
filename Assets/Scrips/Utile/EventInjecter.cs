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

    public EntityController player;

    public StatController statController;

    public MonsterFactory monsterFactory;

    public ResourceUi resourceUi;

    private void Start()
    {
        //�ʿ��� �޼��带 ���ϴ� ������� ���� �� ����
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

        //StageManager.Instance. ���������� ������ �� �̺�Ʈ �ʿ�

        statController.statChanged += playerStatUi.UpdateValueBar;

        yield return null;

        Destroy(this.gameObject);
        yield break;
    }

}
