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

    private void Start()
    {
        //�ʿ��� �޼��带 ���ϴ� ������� ���� �� ����
        StartCoroutine(InjectStart());
    }

    private IEnumerator InjectStart()
    {
        GameCondition.Instance.conditionEvent += gameOptionUi.ChangeOptionUi;
        GameCondition.Instance.conditionEvent += lobbyCam.ChangeLobbyScreen;

        StageManager.Instance.stageEvent += battleCam.ChangeBattleScreen;
        StageManager.Instance.stageEvent += player.OnStageMove;
        StageManager.Instance.stageEvent += monsterFactory.CreateMonster;

        //StageManager.Instance. ���������� ������ �� �̺�Ʈ �ʿ�

        statController.statChanged += playerStatUi.UpdateValueBar;

        yield return null;

        Destroy(this.gameObject);
        yield break;
    }

}
