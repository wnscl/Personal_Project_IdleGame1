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
        //�ʿ��� �޼��带 ���ϴ� ������� ���� �� ����
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
